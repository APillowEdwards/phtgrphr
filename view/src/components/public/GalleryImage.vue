<template>
  <div>
    <img v-if="visible" class="gallery-image" :src="source"/>
  </div>
</template>

<script>
  import API from "@/api"

  export default {
    name: "GalleryImage",
    props: {
      token: String,
      imageId: Number,
      visible: Boolean
    },
    computed: {
      source: function() {
        return `${API.defaults.baseURL}v1/public/gallery/image/${this.token}/${this.imageId}`
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

    border: 2px solid rgba(255,255,255,0.25);

    -webkit-box-shadow: 5px 5px 2px 0px rgba(255,255,255,0.25);
    -moz-box-shadow: 5px 5px 2px 0px rgba255,255,255,0.25);
    box-shadow: 5px 5px 2px 0px rgba(255,255,255,0.25);
  }

  div {
    display: -moz-box;
    display: -ms-flexbox;
    display: -webkit-flex;
    display: flex;

    align-items:center;
    justify-content:center;
  }
</style>
