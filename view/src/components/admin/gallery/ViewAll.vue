<template>
  <div>
    <p><a @click="back">Back</a></p>
    <table>
      <tr>
        <th>Name</th>
        <th>Link</th>
        <th>Actions</th>
      </tr>
      <tr v-for="gallery in galleries" :key="gallery.id">
        <td>{{gallery.name}}</td>
        <td>{{baseUrl + "?gallery=" + gallery.guid}}</td>
        <td><a @click="editGallery(gallery.id)">Edit</a> | <a @click="deleteGallery(gallery.id, gallery.name)">Delete</a></td>
      </tr>
    </table>
  </div>
</template>

<script>
  import API from "@/api"

  export default {
    name: "GalleryViewAll",
    props: {
      token: String
    },
    data: function() {
      return {
        galleries: []
      }
    },
    methods: {
      back: function() {
        this.$emit("backbuttonpressed");
      },
      refresh: function() {
        var v = this;
        return API.get("/admin/gallery/list?token=" + this.token)
          .then(function (response) {
            v.galleries = response.data.galleries;
          });
      },
      editGallery: function(id) {
        this.$emit("editgallerypressed", id)
      },
      deleteGallery: function(id, name) {
        console.log(id + " " + name)
        if(confirm(`Are you sure you wish to delete gallery "${name}"?`)) {
          var v = this;
          return API.delete(`/admin/gallery?id=${id}&token=${this.token}`)
            .then(function (response) {
              if (!response.data.hasError) {
                v.refresh();
              }
            });
        }
      }
    },
    created: function() {
      this.refresh();
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
