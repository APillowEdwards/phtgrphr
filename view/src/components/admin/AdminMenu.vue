<template>
  <div>
    <h2>Admin Menu</h2>
    <ul>
      <li><a @click="selectAddGallery">Add Gallery</a></li>
      <li><a @click="selectViewAllGalleries">View All Galleries</a></li>
    </ul>
  </div>
</template>

<script>
  export default {
    name: "AdminMenu",
    props: {
      states: Array
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
