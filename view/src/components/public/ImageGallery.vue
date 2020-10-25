<template>
  <div>
    <h1>{{ galleryName }}</h1>
    <hr class="grey-hr">
    <div class="row">
      <div class="col-12 col-md-6">
        <div class="row mb-4 mb-md-0">
          <div class="col-4 vertical-center">
            <label class="w-100 mb-0 text-right" for="page-size">Page Size: </label>
          </div>
          <div class="col-8">
            <select id="page-size" class="form-control" v-model="pageSize" @change="page = 1">
              <option>12</option>
              <option>24</option>
              <option>48</option>
            </select>
          </div>
        </div>

      </div>
      <div class="col-12 col-md-6">
        <b-pagination
          v-model="page"
          :total-rows="numberOfPages"
          :per-page="1"
          align="center">
        </b-pagination>
      </div>
    </div>

    <div class="gallery-wrapper">
      <gallery-image
        v-for="(image, index) in images"
        :token="token"
        :image-id="image.id"
        :media="media"
        :index="index"
        :key="image.id">
      </gallery-image>
    </div>

    <div class="row">
      <div class="col-12 col-xs-6">

      </div>
      <div class="col-12 col-xs-6">
        <b-pagination
          v-model="page"
          :total-rows="numberOfPages"
          :per-page="1"
          align="center">
        </b-pagination>
      </div>
    </div>
  </div>
</template>

<script>
  import API from "@/api"

  import GalleryImage from "./GalleryImage.vue"

  require('vue-image-lightbox/dist/vue-image-lightbox.min.css')

  export default {
    name: "ImageGallery",
    components: {
      GalleryImage
    },
    props: {
      token: String
    },
    data: function () {
      return {
        pageSize: 12,
        page: 1,
        numberOfPages: 1
      }
    },
    computed: {
      media: function() {
        if (typeof this.images == `undefined`) {
          return []
        }

        var v = this;
        return this.images.map(function(image) {
          return {
            thumb: `${API.defaults.baseURL}v1/public/gallery/image/${v.token}/${image.id}`,
            src: `${API.defaults.baseURL}v1/public/gallery/image/${v.token}/${image.id}`
          }
        });
      }
    },
    asyncComputed: {
      images: function() {
        // Get the list of image ids based on the page size and number
        var v = this;
        return API.get(`/v1/public/gallery/images/${this.token}/${this.pageSize}/${this.page}`)
          .then(function (response) {
            v.numberOfPages = Math.ceil(response.data.result.totalCount / v.pageSize);

            return response.data.result.images;
          });
      },
      galleryName: function() {
        // Get gallery metadata
        return API.get(`/v1/public/gallery/${this.token}`)
          .then(function (response) {
            return response.data.result.gallery.name;
          });
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

  .vertical-center {
    display: flex;
    flex-direction: column;
    justify-content: center;
  }

  .gallery-wrapper {
    display: -moz-box;
    display: -ms-flexbox;
    display: -webkit-flex;
    display: flex;

    -webkit-flex-flow: row wrap;
    -moz-flex-flow: row wrap;
    -ms-flex-flow: row wrap;
    flex-flow: row wrap;

    justify-content: center;

    margin-bottom: 30px;
  }

  .gallery-wrapper > * {
    flex-shrink: 0;
    -webkit-flex-shrink: 0;
    -moz-flex-shrink: 0;
    -ms-flex-shrink: 0;
  }

</style>
