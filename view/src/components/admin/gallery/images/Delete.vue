<template>
  <div>
    <h2>DELETE</h2>
    <div class="gallery-wrapper">
      <div
        v-for="image in images"
        :key="image.id">

        <img class="gallery-image" :src="imageUrl(image.id)" />
        <a @click="deleteImage(image.id)">&lt;- Delete</a>

      </div>
    </div>
  </div>
</template>

<script>
  import API from "@/api"

  export default {
    name: "GalleryImagesDelete",
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
      deleteImage: function(id) {
        if(confirm(`Are you sure you wish to delete this image?`)) {
          var v = this;
          API.delete(`/admin/gallery/image?id=${id}&token=${this.token}`)
            .then(function (response) {
              if (!response.data.hasError) {
                v.refresh();
              }
            });
        }
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
    position: relative;
    top: 50%;
    transform: translateY(-50%);

    border: 2px solid rgba(255,255,255,0.25);

    -webkit-box-shadow: 5px 5px 2px 0px rgba(255,255,255,0.25);
    -moz-box-shadow: 5px 5px 2px 0px rgba255,255,255,0.25);
    box-shadow: 5px 5px 2px 0px rgba(255,255,255,0.25);
  }

</style>
