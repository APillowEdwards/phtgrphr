<template>
  <div>
    <h2>SORT</h2>
    <div
      v-for="image in images"
      :key="image.id">

      <img :src="imageUrl(image.id)" style="max-width:250px" />

    </div>
  </div>
</template>

<script>
  import API from "@/api"

  export default {
    name: "GalleryImagesSort",
    props: {
      token: String,
      galleryId: Number
    },
    data: function() {
      return {
        images: []
      }
    },
    methods: {
      refresh: function() {
        var v = this;
        return API.get("/admin/gallery/images?id=" + this.galleryId  + "&token=" + this.token)
          .then(function (response) {
            v.images = response.data;
          });
      },
      imageUrl: function(id) {
        return API.defaults.baseURL + "admin/gallery/image?token=" + this.token + "&id=" + id;
      }
    },
    created: function() {
      this.refresh();
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
