<template>
  <div>
    <img v-if="!responseRecieved && !errored" class="loading-spinner" :src="$loadingSpinner" />

    <span v-if="responseRecieved && !exists">This gallery doesn't exist, please contact the link provider.</span>

    <span v-if="errored">Sorry, there is a problem. Please try again later.</span>

  </div>
</template>

<script>
  import API from "@/api"

  export default {
    name: "GalleryExists",
    props: {
      guid: String
    },
    data: function() {
      return {
        exists: false,
        responseRecieved: false,
        errored: false
      }
    },
    created: function() {
      // if the guid isn't valid, just set exists to false
      if (!/^[0-9a-f]{8}-([0-9a-f]{4}-){3}[0-9a-f]{12}$/.test(this.guid)) {
          this.exists = false;
          v.responseRecieved = true;
          return;
      }

      var v = this;
      API.get(`/v1/public/gallery/exists/${this.guid}`)
        .then(function (response) {
          v.exists = response.data.result.exists;
          v.responseRecieved = true;

          if (v.exists) {
            v.$emit("galleryexists");
          }
        })
        .catch(function() {
          v.errored = true;
        });
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
