<template>
  <div>
    <p class="text-left"><a class="btn btn-primary px-4 py-2 btn-sm" @click="back">&lt; Back</a></p>

    <p v-if="errorMessage.length > 0" style="color: red">{{errorMessage}}</p>

    <img v-if="galleries === null" class="loading-spinner" :src="$loadingSpinner" />

    <table v-else>
      <tr>
        <th>Name</th>
        <th>Link</th>
        <th>Actions</th>
      </tr>
      <tr v-for="gallery in galleries" :key="gallery.id">
        <td>{{gallery.name}}</td>
        <td>
          <a class="text-center mr-3" :href="makeUrl(gallery.guid)" v-html="makeUrl(gallery.guid)" target="_blank"></a>
          <a class="float-right btn btn-primary px-4 py-2 btn-sm" v-clipboard="() => makeUrl(gallery.guid)">Copy</a>
        </td>
        <td>
          <a class="btn btn-primary px-4 py-2 btn-sm" @click="editGallery(gallery.id)">Edit Details</a>
          <a class="btn btn-primary px-4 py-2 btn-sm" @click="updateImages(gallery.id)">Edit Images</a>
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
        galleries: null,
        errorMessage: ""
      }
    },
    methods: {
      back: function() {
        this.$emit("backbuttonpressed");
      },
      refresh: function() {
        var v = this;
        return API.get(`/v1/admin/gallery/${this.token}`)
          .then(function (response) {
            v.galleries = response.data.result.galleries;
          })
          .catch(function (error) {
            v.errorMessage = error.response.data.messages.friendlyError;
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
          API.post(`/v1/admin/gallery/delete/${this.token}/${id}`)
            .then(function (response) {
              if (!response.data.hasError) {
                v.refresh();
              }
            })
            .catch(function (error) {
              v.errorMessage = error.response.data.messages.friendlyError;
            });
        }
      },
      makeUrl: function(guid) {
        return this.$baseUrl + "?gallery=" + guid
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
    text-align: center;
  }

</style>
