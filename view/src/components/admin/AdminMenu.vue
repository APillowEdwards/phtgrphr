<template>
  <div class="text-center">
    <p><a class="btn btn-primary px-4 py-2 btn-sm" @click="selectAddGallery">Add Gallery</a></p>
    <p><a class="btn btn-primary px-4 py-2 btn-sm" @click="selectViewAllGalleries">View All Galleries</a></p>
  </div>
</template>

<script>
  export default {
    name: "AdminMenu",
    props: {
      states: Object
    },
    methods: {
      selectAddGallery: function () {
        this.$emit("menuselectionmade", this.states.ADD_GALLERY)
      },
      selectViewAllGalleries: function () {
        this.$emit("menuselectionmade", this.states.VIEW_ALL_GALLERIES)
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
