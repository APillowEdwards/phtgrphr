<template>
  <div>
    <p><a v-on:click="back">Back</a></p>
    <p>GalleryViewAll</p>
  </div>
</template>

<script>
  //import API from "@/api"

  export default {
    name: "GalleryAdd",
    data: function() {
      return {

      }
    },
    methods: {
      back: function() {
        this.$emit("backbuttonpressed");
      }
    },
    asyncComputed: {
      galleries: function () {

      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
