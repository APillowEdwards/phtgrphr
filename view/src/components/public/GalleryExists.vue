<template>
  <div>
    <span v-if="exists === null">Checking the gallery exists...</span>
    <span v-if="exists !== null && !exists">This gallery doesn't exist, please contact the link provider.</span>
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
        exists: null
      }
    },
    created: function() {
      // if the guid isn't valid, just set exists to false
      if (!/^[0-9a-f]{8}-([0-9a-f]{4}-){3}[0-9a-f]{12}$/.test(this.guid)) {
          this.exists = false;
          return;
      }

      var v = this;
      API.get(`/v1/public/gallery/exists/${this.guid}`)
        .then(function (response) {
          v.exists = response.data.result.exists

          if (v.exists) {
            v.$emit("galleryexists")
          }
        });
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
