<template>
  <div>
    <img class="gallery-image" :src="source" @click="openModal"/>

    <div @click="closeModal">
      <modal
        :name="'image-' + imageId"
        classes="modal-box"
        height="auto"
        width="80%"
        reset="true">

        <img class="modal-image" :src="source"/>

      </modal>
    </div>
  </div>
</template>

<script>
  import API from "@/api"

  export default {
    name: "GalleryImage",
    props: {
      token: String,
      imageId: Number
    },
    computed: {
      source: function() {
        return API.defaults.baseURL + "image?token=" + this.token + "&id=" + this.imageId
      }
    },
    methods: {
      openModal: function() {
        this.$modal.show("image-" + this.imageId);
      },
      closeModal: function() {
        this.$modal.hide("image-" + this.imageId);
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

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

  .modal-image {
    max-height: 80vh;
    max-width: 100%;
  }

</style>

<style>
  .modal-box {
    background-color: transparent !important;
    box-shadow: none !important;
  }
</style>
