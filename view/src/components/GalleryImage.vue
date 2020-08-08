<template>
  <div>
    <img :src="source" />
  </div>
</template>

<script>
  import API from '../api'

  export default {
    name: 'GalleryImage',
    props: {
      token: String
    },
    asyncComputed: {
      source: function() {
        return API.get("/image?token=" + this.token)
          .then(function (response) {
            return response.data.base64
          });
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
