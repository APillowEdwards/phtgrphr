<template>
  <div>
    <p><a v-on:click="back">Back</a></p>
    <p>GalleryAdd</p>
  </div>
</template>

<script>
  //import API from "@/api"

  export default {
    name: "GalleryAdd",
    data: function() {
      return {
        name: "",
        password: ""
        //images: []
      }
    },
    methods: {
      back: function() {
        this.$emit("backbuttonpressed");
      },
      submit: function () {

      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
