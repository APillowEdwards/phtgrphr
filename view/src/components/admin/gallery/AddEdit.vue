<template>
  <div>
    <p><a @click="back">Back</a></p>
    <p v-if="errorMessage.length > 0" style="color: red">{{errorMessage}}</p>
    <p>Name: <input type="text" v-model="name"></p>
    <p>Password: <input type="text" v-model="password"></p>
    <p><a @click="submit">Submit</a></p>

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
