<template>
  <div>
    <h2>ADD</h2>
    <div class="gallery-wrapper">
      <div
        v-for="image in images"
        :key="image.id">

        <img class="gallery-image" :src="imageUrl(image.id)" style="max-width:250px" />

      </div>
      <div>
        <label for="upload-images" class="gallery-image upload-images" />
        <input id="upload-images" style="display:none" type="file" multiple @change="uploadImages" />
      </div>
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

  .gallery-wrapper {
    display: flex;
    flex-wrap: wrap;

    margin-bottom: 30px;
  }

  .gallery-image {
    max-width: 250px;
    margin: 10px;
    margin-bottom: 30px;
    position: relative;
    top: 50%;
    transform: translateY(-50%);

    border: 2px solid rgba(255,255,255,0.25);

    -webkit-box-shadow: 5px 5px 2px 0px rgba(255,255,255,0.25);
    -moz-box-shadow: 5px 5px 2px 0px rgba255,255,255,0.25);
    box-shadow: 5px 5px 2px 0px rgba(255,255,255,0.25);
  }

  .upload-images {
    height: 80%;
    min-height: 200px;
    width: 80%;
    min-width: 200px;

    background-color: white;
    background-image: url(https://upload.wikimedia.org/wikipedia/commons/thumb/c/ce/Plus_font_awesome.svg/1200px-Plus_font_awesome.svg.png);
    background-size: contain;
    background-repeat: no-repeat;
    background-position: center;
  }

</style>
