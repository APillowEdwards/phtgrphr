<template>
  <div>
    <h1>This is an admin page</h1>

    <login
      v-if="!haveAdminAuthToken"
      @adminauthresponse="SetAdminAuthToken($event)">
    </login>

    <!--  -->
  </div>
</template>


<script>
  import Login from "@/components/admin/Login.vue"

  export default {
    name: "Admin",
    components: {
      Login
    },
    data: function () {
      return {
        adminAuthToken: null,
        loginPassword: ""
      }
    },
    methods: {
      SetAdminAuthToken: function (e) {
        this.adminAuthToken = e
      }
    },
    computed: {
      haveAdminAuthToken: function () {
        return this.adminAuthToken !== null;
      }
    }
  }
</script>
