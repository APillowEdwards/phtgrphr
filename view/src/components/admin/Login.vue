<template>
  <div>
    <p>Admin Login</p>
    <p v-if="errorMessage.length > 0" style="color: red">{{errorMessage}}</p>
    <p>Username: <input v-model="username"></p>
    <p>Password: <input v-model="password"></p>
    <button v-on:click="authorise">Submit</button>
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
