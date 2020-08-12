<template>
  <div>
    <span v-if="!reponseReceived">Checking the gallery exists...</span>
    <span v-if="reponseReceived && !exists">This gallery doesn't exist, please contact the link provider.</span>
  </div>
</template>

<script>
  import API from '../api'

  export default {
    name: 'GalleryExists',
    props: {
      guid: String,
      reponseReceived: Boolean,
      exists: Boolean
    },
    created: function() {
      // Check Guid Exists
      var v = this;
      API.get("/gallery/exists?guid=" + this.guid)
        .then(function (response) {
          v.$emit("galleryexistsresponse", response.data)
        });
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
