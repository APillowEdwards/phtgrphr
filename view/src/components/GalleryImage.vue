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
      token: String,
      imageId: Number
    },
    computed: {
      source: function() {
        return API.defaults.baseURL + "image?token=" + this.token + "&id=" + this.imageId
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
