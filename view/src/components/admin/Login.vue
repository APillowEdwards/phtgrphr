<template>
  <div>
    <h2>Login</h2>
    <p v-if="errorMessage.length > 0" style="color: red">{{errorMessage}}</p>

    <label for="username">Username</label>
    <div class="input-group mb-3">
      <input v-model="username" type="text" class="form-control" id="username" />
    </div>

    <label for="password">Password</label>
    <div class="input-group mb-3">
      <input v-model="password" type="password" class="form-control" id="password">
    </div>

    <button class="w-100 btn btn-primary px-4 py-2 btn-sm mt-2" @click="authorise">Submit</button>
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
        API.post(`/v1/admin/user/authenticate/${this.username}`, `"${this.password}"`, {headers: { "Content-Type": "application/json" }})
          .then(function (response) {
            v.$emit("recievedadminauthtoken", response.data.result.token)
          })
          .catch(function (error) {
            v.errorMessage = error.response.data.messages.friendlyError;
          });
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
