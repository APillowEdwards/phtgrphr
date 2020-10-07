<template>
  <div>
    <h2>DELETE</h2>
    <div class="gallery-wrapper">
      <div
        v-for="image in images"
        :key="image.id">

        <a class="delete-image btn btn-primary px-4 py-2 btn-sm" @click="deleteImage(image.id)">Delete</a>
        <img class="gallery-image" :src="imageUrl(image.id)" />

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
      return API.get(`/v1/admin/gallery/images/${this.token}/${this.galleryId}`)
        .then(function (response) {
          v.images = response.data.result.images;
        });
      },
      imageUrl: function(id) {
        return API.defaults.baseURL + `v1/v1/admin/gallery/image/${this.token}/${id}`;
      },
      deleteImage: function(id) {
        if(confirm(`Are you sure you wish to delete this image?`)) {
          var v = this;
          API.post(`/v1/admin/gallery/image/delete/${this.token}/${id}`)
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
    display: -webkit-flex;
    display: -moz-flex;
    display: -ms-flex;
    display: flex;

    -webkit-flex-flow: row wrap;
    -moz-flex-flow: row wrap;
    -ms-flex-flow: row wrap;
    flex-flow: row wrap;

    justify-content: center;

    margin-bottom: 30px;
  }

  .gallery-image {
    max-width: 250px;
    margin: 10px;
    position: relative;

    border: 2px solid rgba(255,255,255,0.25);

    -webkit-box-shadow: 5px 5px 2px 0px rgba(255,255,255,0.25);
    -moz-box-shadow: 5px 5px 2px 0px rgba255,255,255,0.25);
    box-shadow: 5px 5px 2px 0px rgba(255,255,255,0.25);
  }

  .delete-image {
    position: absolute;
    z-index: 2;

    background: #c00 !important;
  }

  .delete-image:hover {
    background: #a00   !important;
  }

</style>
