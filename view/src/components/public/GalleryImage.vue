<template>
  <div>
    <img class="gallery-image" :src="source" @click="openModal"/>

    <LightBox
      ref="lightbox"
      :media="media"
      :show-caption="true"
      :show-light-box="false">
    </LightBox>
  </div>
</template>

<script>
  import API from "@/api"
  import LightBox from 'vue-image-lightbox'

  export default {
    name: "GalleryImage",
    components: {
      LightBox
    },
    props: {
      token: String,
      imageId: Number,
      media: Array,
      index: Number
    },
    computed: {
      source: function() {
        return `${API.defaults.baseURL}v1/public/gallery/image/${this.token}/${this.imageId}`
      }
    },
    methods: {
      openModal: function() {
         this.$refs.lightbox.showImage(this.index)
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
