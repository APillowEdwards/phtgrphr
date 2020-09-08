<template>
  <div>
    <h2>{{ galleryName }}</h2>
    <hr class="grey-hr">
    <div class="row">
      <div class="col-6">
        <label for="page-size">Page Size: </label>
          <select v-model="pageSize">
          <option>16</option>
          <option>32</option>
          <option>64</option>
        </select>
      </div>
      <div class="col-6">
        <label for="page-size">Page Number: </label>
        <select v-model="page">
          <option v-for="n in Math.ceil(numberOfPages / pageSize)" :key="n" :value="n">{{n}}</option>
        </select>
      </div>
    </div>

    <div class="gallery-wrapper">
      <gallery-image
        v-for="image in images"
        :token="token"
        :image-id="image.id"
        :key="image.id">
      </gallery-image>
    </div>
  </div>
</template>

<script>
  import API from "@/api"

  import GalleryImage from "./GalleryImage.vue"

  export default {
    name: "ImageGallery",
    components: {
      GalleryImage
    },
    props: {
      token: String
    },
    data: function () {
      return {
        pageSize: 16,
        page: 1,
        numberOfPages: 1
      }
    },
    asyncComputed: {
      images: function() {
        // Get the list of image ids based on the page size and number
        var v = this;
        return API.get("/gallery/images?page=" + this.page + "&pageSize=" + this.pageSize + "&token=" + this.token)
          .then(function (response) {
            v.numberOfPages = response.data.count;
            if (v.numberOfPages < v.page) {
              v.page = v.numberOfPages;
            }
            return response.data.images;
          });
      },
      galleryName: function() {
        // Get gallery metadata
        return API.get("/gallery?token=" + this.token)
          .then(function (response) {
            return response.data.name;
          });
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

  .gallery-wrapper {
    display: flex;
    flex-wrap: wrap;

    margin-bottom: 30px;
  }

</style>
