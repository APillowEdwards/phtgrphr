<template>
  <div>
    <h2>{{this.id == 0 ? "New" : "Edit"}} Gallery </h2>

    <p v-if="errorMessage.length > 0" style="color: red">{{errorMessage}}</p>

    <label for="gallery-name">Name</label>
    <div class="input-group mb-3">
      <input v-model="name" type="text" class="form-control" id="gallery-name" />
    </div>

    <label for="gallery-password">Password</label>
    <div class="input-group mb-3">
      <input v-model="password" type="text" class="form-control" id="gallery-password" />
    </div>

    <div class="row mt-3">
      <div class="col-6 text-right">
        <a class="w-100 btn btn-primary px-4 py-2 btn-sm" @click="back">&lt; Back</a>
      </div>
      <div class="col-6 text-left">
        <a class="w-100 btn btn-primary px-4 py-2 btn-sm" @click="submit">Submit</a>
      </div>
    </div>

  </div>
</template>

<script>
  import API from "@/api"

  export default {
    name: "GalleryAddEdit",
    props: {
      id: Number,
      token: String
    },
    data: function () {
      return {
        name: null,
        password: null,
        errorMessage: ""
      }
    },
    methods: {
      back: function() {
        this.$emit("backbuttonpressed");
      },
      submit: function () {
        var v = this;
        API.post(`/v1/admin/gallery/${this.token}`, {
            ID: this.id,
            Name: this.name,
            Password: this.password
          })
          .then(function (response) {
            if (response.data.hasError) {
              v.errorMessage = response.data.error.message;
            } else {
              v.errorMessage = "Success";
              setTimeout(function() {
                v.$emit("gallerysaved", response.data.result.gallery.id);
              }, 2000);
            }
          });
      }
    },
    created: function() {
      if (this.id != 0) {
        var v = this;
        return API.get(`/v1/admin/gallery/${this.token}/${this.id}`)
          .then(function (response) {
            v.name = response.data.result.gallery.name;
            v.password = response.data.result.gallery.password;
          });
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
