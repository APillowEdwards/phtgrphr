<template>
  <div>
    <p><a class="btn btn-primary px-4 py-2 btn-sm" @click="back">&lt; Back</a></p>
    <p v-if="errorMessage.length > 0" style="color: red">{{errorMessage}}</p>

    <div class="row">
      <div class="col-6 text-right">
        <label for="name">Name: </label>
      </div>
      <div class="col-6 text-left">
        <input id="name" v-model="name">
      </div>
    </div>

    <div class="row">
      <div class="col-6 text-right">
        <label for="password">Password: </label>
      </div>
      <div class="col-6 text-left">
        <input id="password" v-model="password">
      </div>
    </div>

    <button class="mt-2" @click="submit">Submit</button>

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
        errorMessage: "",
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
      submit: function () {
        var v = this;
        API.post("/admin/gallery", {
            id: this.id,
            name: this.name,
            password: this.password,
            token: this.token
          })
          .then(function (response) {
            if (response.data.hasError) {
              v.errorMessage = response.data.error.message;
            } else {
              v.errorMessage = "Success";
              setTimeout(function() {
                v.$emit("gallerysaved");
              }, 2000);
            }
          });
      }
    },
    created: function() {
      if (this.id != 0) {
        var v = this;
        return API.get("/admin/gallery?id=" + this.id + "&token=" + this.token)
          .then(function (response) {
            v.name = response.data.name;
            v.password = response.data.password;
          });
      }
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
