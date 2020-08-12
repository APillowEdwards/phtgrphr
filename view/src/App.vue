<template>
  <div>
    <gallery-exists
      v-if="!(galleryExistsResponseReceived && galleryExists)"
      :guid="galleryGuid"
      :reponse-received="galleryExistsResponseReceived"
      :exists="galleryExists"
      @galleryexistsresponse="SetGalleryExistsResponseData($event)">
    </gallery-exists>

    <login
      v-if="galleryExists && !haveGalleryAuthToken"
      :guid="galleryGuid"
      @galleryauthresponse="SetGalleryAuthToken($event)">
    </login>

    <image-gallery
      v-if="haveGalleryAuthToken"
      :token="galleryAuthToken">
    </image-gallery>
  </div>
</template>

<script>
  import GalleryExists from './components/GalleryExists.vue'
  import ImageGallery from './components/ImageGallery.vue'
  import Login from './components/Login.vue'

  let urlParams = new URLSearchParams(window.location.search);

  export default {
    name: 'App',
    components: {
      GalleryExists,
      ImageGallery,
      Login
    },
    data: function () {
      return {
        galleryExistsResponseData: null,
        galleryAuthToken: null,
        loginPassword: ""
      }
    },
    methods: {
      SetGalleryExistsResponseData: function (e) {
        this.galleryExistsResponseData = e
      },
      SetGalleryAuthToken: function (e) {
        this.galleryAuthToken = e
      }
    },
    computed: {
      galleryGuid: function() {
        return urlParams.get("gallery");
      },
      galleryExistsResponseReceived: function() {
        return this.galleryExistsResponseData !== null
      },
      galleryExists: function() {
        if (this.galleryExistsResponseData === null) {
          return false;
        }
        if (this.galleryExistsResponseData.hasError) {
          return false;
        }
        return this.galleryExistsResponseData.exists;
      },
      haveGalleryAuthToken: function () {
        return this.galleryAuthToken !== null;
      }
    }
  }
</script>

<style>
#app {

}
</style>
