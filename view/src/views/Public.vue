<template>
  <div>
    <gallery-exists
      v-if="!galleryExists"
      :guid="galleryGuid"
      @galleryexists="SetGalleryExists">
    </gallery-exists>

    <login
      v-if="galleryExists && !haveGalleryAuthToken"
      :guid="galleryGuid"
      @recievedgalleryauthtoken="SetGalleryAuthToken($event)">
    </login>

    <image-gallery
      v-if="haveGalleryAuthToken"
      :token="galleryAuthToken">
    </image-gallery>
  </div>
</template>

<script>
  import GalleryExists from "@/components/public/GalleryExists.vue"
  import ImageGallery from "@/components/public/ImageGallery.vue"
  import Login from "@/components/public/Login.vue"

  let urlParams = new URLSearchParams(window.location.search);

  export default {
    name: "Public",
    components: {
      GalleryExists,
      ImageGallery,
      Login
    },
    data: function () {
      return {
        galleryExists: false,
        galleryAuthToken: null
      }
    },
    methods: {
      SetGalleryExists: function () {
        this.galleryExists = true
      },
      SetGalleryAuthToken: function (e) {
        this.galleryAuthToken = e
      }
    },
    computed: {
      galleryGuid: function() {
        return urlParams.get("gallery");
      },
      haveGalleryAuthToken: function () {
        return this.galleryAuthToken !== null;
      }
    }
  }
</script>
