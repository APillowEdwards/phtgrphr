<template>
  <div>

    <p><a class="btn btn-primary px-4 py-2 btn-sm" @click="back">&lt; Back</a></p>

    <div class="image-options">
      <a @click="changeView(IMAGES_STATES.ADD)" :class="imageState == IMAGES_STATES.ADD ? 'active' : ''">Upload</a>
      <a @click="changeView(IMAGES_STATES.SORT)" :class="imageState == IMAGES_STATES.SORT ? 'active' : ''">Order</a>
      <a @click="changeView(IMAGES_STATES.DELETE)" :class="imageState == IMAGES_STATES.DELETE ? 'active' : ''">Delete</a>
    </div>

    <add
      v-if="imageState == IMAGES_STATES.ADD"
      :galleryId="id"
      :token="token">
    </add>

    <delete
      v-if="imageState == IMAGES_STATES.DELETE"
      :galleryId="id"
      :token="token">
    </delete>

    <sort
      v-if="imageState == IMAGES_STATES.SORT"
      :galleryId="id"
      :token="token">
    </sort>

  </div>
</template>

<script>
  import Add from "./Add.vue"
  import Delete from "./Delete.vue"
  import Sort from "./Sort.vue"

  export default {
    name: "GalleryImages",
    components: {
      Add,
      Delete,
      Sort
    },
    props: {
      id: Number,
      token: String
    },
    data: function () {
      return {
        imageState: 1,
        IMAGES_STATES: {
          ADD: 1,
          DELETE: 2,
          SORT: 3
        }
      }
    },
    methods: {
      back: function() {
        this.$emit("backbuttonpressed");
      },
      changeView: function(state) {
        this.imageState = state;
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

  .image-options {
    display: flex;
    flex-wrap: wrap;

    margin-bottom: 20px;
  }

  .image-options a {
    text-align: center;
    width: 33.3%;
    padding: 10px;

    border: 1px solid lightgrey;
  }

  .image-options a.active {
    background-color: lightgrey;
    color: black;
  }

</style>
