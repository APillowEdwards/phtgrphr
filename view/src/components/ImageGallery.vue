<template>
  <div>
    <h2>{{ galleryName }}</h2>
    <p>Size: <input v-model="pageSize"><br />
    #: <input v-model="page"></p>
    <gallery-image
      v-for="image in images"
      :token="token"
      :image-id="image.id"
      :key="image.id">
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
      token: String
    },
    data: function () {
      return {
        pageSize: 3,
        page: 1
      }
    },
    asyncComputed: {
      images: function() {
        if (this.token !== null) {
          // Get the list of image ids based on the page size and number
          return API.get("/gallery/images?page=" + this.page + "&pageSize=" + this.pageSize + "&token=" + this.token)
            .then(function (response) {
              return response.data.images;
            });
        }
        return [];
      },
      galleryName: function() {
        if (this.token !== null) {
          // Get gallery metadata
          return API.get("/gallery?token=" + this.token)
            .then(function (response) {
              return response.data.name;
            });
        }
        return "...";
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
