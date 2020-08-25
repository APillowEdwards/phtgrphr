<template>
  <div>
    <p><a @click="back">Back</a></p>
    <hr>
    <p><a @click="changeView(IMAGES_STATES.ADD)">Add</a></p>
    <p><a @click="changeView(IMAGES_STATES.SORT)">Sort</a></p>
    <p><a @click="changeView(IMAGES_STATES.DELETE)">Delete</a></p>

    <hr>

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

</style>
