<template>
  <div>
    <h2>ORDER</h2>

    <a class="w-100 btn btn-primary px-4 py-2 btn-sm" @click="saveOrder" :disabled="saving">Save Order</a>
    <p v-if="showSuccess">Success!</p>

    <draggable class="gallery-wrapper" v-model="images">
      <div
        v-for="image in images"
        :key="image.id">
        <img class="gallery-image" :src="imageUrl(image.id)" style="max-width:250px" />
      </div>
    </draggable>

    <a class="w-100 btn btn-primary px-4 py-2 btn-sm" @click="saveOrder" :disabled="saving">Save Order</a>
  </div>
</template>

<script>
  import API from "@/api"

  import Draggable from 'vuedraggable'

  export default {
    name: "GalleryImagesSort",
    components: {
      Draggable
    },
    props: {
      token: String,
      galleryId: Number
    },
    data: function() {
      return {
        images: [],
        saving: false,
        showSuccess: false
      }
    },
    methods: {
      refresh: function() {
        var v = this;
        return API.get(`/admin/gallery/images/${this.token}/${this.galleryId}`)
          .then(function (response) {
            v.images = response.data.result.images;
          });
      },
      imageUrl: function(id) {
        return API.defaults.baseURL + `admin/gallery/image/${this.token}/${id}`;
      },
      saveOrder: function() {
        this.saving = true;

        var v = this;
        return API.post(`/admin/gallery/images/sort/${this.token}/${this.galleryId}`, this.images)
          .then(function (response) {
            v.saving = false;

            if (!response.data.hasError) {
              v.showSuccess = true;

              setTimeout(function() {
                v.showSuccess = false;
              }, 2000);
            }
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

  .gallery-wrapper div {
    min-height: 100%;
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
