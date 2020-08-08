<template>
  <div>
    <p>Size: <input v-model="pageSize"><br />#: <input v-model="page"></p>
    <gallery-image
      v-for="token in imageTokens"
      :token="token"
      :key="token">
    </gallery-image>
  </div>
</template>

<script>
  import API from '../api'

  import GalleryImage from './GalleryImage.vue'

  export default {
    name: 'ImageGallery',
    components: {
      GalleryImage
    },
    props: {
      authToken: String
    },
    data: function () {
      return {
        pageSize: 3,
        page: 1
      }
    },
    asyncComputed: {
      imageTokens: function() {
        if (this.authToken !== null) {
          // Get the list of image tokens based on the page size and number
          var v = this;
          return API.post("/fetch-image-tokens", {
              page: v.page - 1, // pages are zero-indexed server-side
              pageSize: v.pageSize,
              token: v.authToken
            })
            .then(function (response) {
              console.log(response.data)
              return response.data.imageTokens;
            });
        }
        return [];
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
