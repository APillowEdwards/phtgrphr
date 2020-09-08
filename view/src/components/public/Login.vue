<template>
  <div>
    <h2>Your Gallery Login</h2>
    <p v-if="errorMessage.length > 0" style="color: red">{{errorMessage}}</p>
    <label for="password">Please enter the password you were provided with and press Go:</label><br/>
    <input id="password" v-model="password" type="password">
    <button class="btn btn-primary px-4 py-2 btn-sm" @click="authorise">Go</button>
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
        API.post("/gallery/auth", {
            guid: v.guid,
            password: v.password
          })
          .then(function (response) {
            if (response.data.hasError) {
              v.errorMessage = response.data.error.message;
            } else {
              v.$emit("galleryauthresponse", response.data.token)
            }
          });
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
