<template>
  <div>
    <h2>Login</h2>
    <p v-if="errorMessage.length > 0" style="color: red">{{errorMessage}}</p>

    <div class="row">
      <div class="col-6 text-right">
        <label for="username">Username: </label>
      </div>
      <div class="col-6 text-left">
        <input id="username" v-model="username">
      </div>
    </div>

    <div class="row">
      <div class="col-6 text-right">
        <label for="password">Password: </label>
      </div>
      <div class="col-6 text-left">
        <input id="password" v-model="password" type="password">
      </div>
    </div>

    <button class="btn btn-primary px-4 py-2 btn-sm mt-2" @click="authorise">Submit</button>
  </div>
</template>

<script>
  import API from "@/api"

  export default {
    name: "Login",
    data: function() {
      return {
        username: "",
        password: "",
        errorMessage: ""
      }
    },
    methods: {
      authorise: function () {
        // Attempt to get an auth token
        var v = this;
        API.post("/admin/auth", {
            username: this.username,
            password: this.password
          })
          .then(function (response) {
            if (response.data.hasError) {
              v.errorMessage = response.data.error.message;
            } else {
              v.$emit("adminauthresponse", response.data.token)
            }
          });
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
