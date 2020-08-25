<template>
  <div>
    <h2>ADD</h2>
    <div
      v-for="image in images"
      :key="image.id">

      <img :src="imageUrl(image.id)" style="max-width:250px" />

    </div>
    <div>
      <input type="file" multiple
        @change="uploadImages"/>
    </div>
  </div>
</template>

<script>
  import API from "@/api"

  export default {
    name: "GalleryImagesAdd",
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
      },
      uploadImages: function(e) {
        var images = e.target.files || e.dataTransfer.files;
        if (!images.length) {
          return;
        }

        var data = new FormData();
        for(var i = 0; i < images.length; i++){
          data.append("images", images[i]);
        }

        var v = this;
        API.post("/admin/gallery/images?id=" + this.galleryId + "&token=" + this.token, data, {headers: {"Content-Type": "multipart/form-data"}})
          .then(function () {
            v.refresh();
          });
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
