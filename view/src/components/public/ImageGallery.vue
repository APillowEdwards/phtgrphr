<template>
  <div>
    <h1>{{ galleryName }}</h1>
    <hr class="grey-hr">
    <div class="row">
      <div class="col-12 col-md-4">
        <div class="row mb-4 mb-md-0">
          <div class="col-4 vertical-center">
            <label class="w-100 mb-0 text-right" for="page-size">Page Size: </label>
          </div>
          <div class="col-8">
            <select id="page-size" class="form-control" v-model="pageSize" @change="page = 1">
              <option v-for="option in pageSizeOptions" :key="option">{{option}}</option>
            </select>
          </div>
        </div>

      </div>
      <div class="col-12 col-md-4">
        <a :href="zipSource" class="w-100 btn btn-primary px-4 py-2 btn-sm" target="_blank">Download All Photos</a>
      </div>
      <div class="col-12 col-md-4">
        <b-pagination
          v-if="numberOfPages > 1"
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
        :visible="image.visible"
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
          v-if="numberOfPages > 1"
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
        pageSize: 10,
        page: 1,
        totalCount: 10
      }
    },
    computed: {
      pageSizeOptions: function () {
        var count = 10;
        var options = [];

        while (count < this.totalCount) {
          options.push(count);
          count = count * 2;
        }
        options.push("All");

        return options;
      },
      numberOfPages: function () {
        return Math.ceil(this.totalCount / this.pageSize)
      },
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
      },
      zipSource: function() {
        return `${API.defaults.baseURL}v1/public/gallery/images/download/${this.token}`
      }
    },
    asyncComputed: {
      images: function() {
        // Get the list of image ids based on the page size and number
        var v = this;

        // If all is selected, just make it a huge number
        var pageSizeParameter = this.pageSize;
        if (pageSizeParameter == "All") {
          pageSizeParameter = 1000;
        }

        return API.get(`/v1/public/gallery/images/${this.token}/${pageSizeParameter}/${this.page}`)
          .then(function (response) {
            v.totalCount = response.data.result.totalCount;

            if (v.totalCount <= 10) {
              v.pageSize = "All";
            }

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
