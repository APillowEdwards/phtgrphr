<template>
  <div>
    <h2>Your Gallery Login</h2>
    <p v-if="errorMessage.length > 0" style="color: red">{{errorMessage}}</p>

    <label for="password">Please enter the password you were provided with and press Go:</label>
    <div class="row">
      <div class="col-8">
        <div class="input-group mb-3">
          <input v-model="password" type="password" class="form-control" id="password" />
        </div>
      </div>
      <div class="col-4">
        <a class="w-100 btn btn-primary px-4 py-2 btn-sm" @click="authorise">Go</a>
      </div>
    </div>


	</div>
</template>

<script>
  import API from "@/api"

  export default {
    name: "Login",
    props: {
      guid: String
    },
    data: function() {
      return {
        password: "",
        errorMessage: ""
      }
    },
    methods: {
      authorise: function () {
        // Get auth token for this gallery
        var v = this;
        API.post(`/public/gallery/authenticate/${this.guid}`, `"${this.password}"`, {headers: { "Content-Type": "application/json" }})
          .then(function (response) {
            v.$emit("recievedgalleryauthtoken", response.data.result.token);
          })
          .catch(function (error) {
            // Unauthorised
            console.log(error.response)
            if (error.response.data.code == 401) {
              v.errorMessage = error.response.data.messages.friendlyError;
            }
          });
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
