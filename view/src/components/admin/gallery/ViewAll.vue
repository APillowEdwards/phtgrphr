<template>
  <div>
    <p><a class="btn btn-primary px-4 py-2 btn-sm" @click="back">&lt; Back</a></p>
    <table>
      <tr>
        <th>Name</th>
        <th>Link</th>
        <th>Actions</th>
      </tr>
      <tr v-for="gallery in galleries" :key="gallery.id">
        <td>{{gallery.name}}</td>
        <td><a :href="baseUrl + '?gallery=' + gallery.guid" target="_blank">{{baseUrl + "?gallery=" + gallery.guid}}</a></td>
        <td>
          <a class="btn btn-primary px-4 py-2 btn-sm" @click="editGallery(gallery.id)">Edit</a>
          <a class="btn btn-primary px-4 py-2 btn-sm" @click="updateImages(gallery.id)">Update Images</a>
          <a class="btn btn-primary px-4 py-2 btn-sm" @click="deleteGallery(gallery.id, gallery.name)">Delete</a>
        </td>
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
      updateImages: function(id) {
        this.$emit("updateimagespressed", id)
      },
      deleteGallery: function(id, name) {
        if(confirm(`Are you sure you wish to delete gallery "${name}"?`)) {
          var v = this;
          API.delete(`/admin/gallery?id=${id}&token=${this.token}`)
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

  td, tr {
    padding: 8px;
    border: 1px solid white;
  }

</style>
