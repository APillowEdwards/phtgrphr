<template>
  <div class="gallery-image-container-container">
    <div class="gallery-image-container" @click="openModal">
      <img class="gallery-image" :src="imageUrl" />

      <blur-hash-canvas
        v-if="!imageIsLoaded"
        class="gallery-image"
        :hash="blurhash || 'LEHV6nWB2yk8pyo0adR*.7kCMdnj'"
        :width="100"
        :height="100"
        :style="{ 'width': canvasWidth, 'height': canvasHeight }" />
    </div>
  </div>
</template>

<script>
  import API from "@/api"

  import { BlurHashCanvas } from 'vue-blurhash'

  export default {
    name: "GalleryImage",
    components: {
      BlurHashCanvas
    },
    props: {
      token: String,
      imageId: Number,
      index: Number,
      lightbox: Object,
      blurhash: String,
      width: Number,
      height: Number,
    },
    data: function () {
      return {
        imageIsLoaded: false,
        imageUrl: ""
      };
    },
    computed: {
      source: function() {
        return `${API.defaults.baseURL}v1/public/gallery/image/${this.token}/${this.imageId}`
      },
      canvasWidth: function() {
        if (!this.width) {
          return "250px";
        }
        if (this.width > this.height) {
          return "250px";
        }
        return ((this.width / this.height) * 250) + "px";
      },
      canvasHeight: function() {
        if (!this.height) {
          return "180px";
        }
        if (this.height > this.width) {
          return "250px";
        }
        return ((this.height / this.width) * 250) + "px";
      }
    },
    methods: {
      openModal: function() {
         this.lightbox.showImage(this.index)
      },
      loadImage: function() {

      }
    },
    mounted: function() {
      var actualImage = new Image();
      var v = this;
      actualImage.onload = function() {
        v.imageUrl = v.source;
        v.imageIsLoaded = true;
      }
      actualImage.src = this.source;
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

  .gallery-image-container-container {
    display: -moz-box;
    display: -ms-flexbox;
    display: -webkit-flex;
    display: flex;

    align-items: center;
    justify-content: center;

    height: 100%;
  }

  .gallery-image-container {
    margin: 10px;
    position: relative;

    border: 2px solid rgba(255,255,255,0.25);

    -webkit-box-shadow: 5px 5px 2px 0px rgba(255,255,255,0.25);
    -moz-box-shadow: 5px 5px 2px 0px rgba255,255,255,0.25);
    box-shadow: 5px 5px 2px 0px rgba(255,255,255,0.25);
  }

  .gallery-image {
    max-width: 250px;
    max-height: 250px;
  }

</style>
