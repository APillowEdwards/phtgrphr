<template>
  <div>
    <p>Login</p>
    <p v-if="errorMessage.length > 0" style="color: red">{{errorMessage}}</p>
    <input v-model="password">
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
