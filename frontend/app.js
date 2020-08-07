var urlParams = new URLSearchParams(window.location.search);

Vue.use(AsyncComputed);

Vue.component('gallery-exists', {
  props: ["guid", "reponse-received", "exists"],
  template: `
    <div>
      <span v-if="!reponseReceived">Checking the gallery exists...</span>
      <span v-if="reponseReceived && !exists">This gallery doesn't exist, please contact the link provider.</span>
    </div>`,
  created: function() {
    // Check Guid Exists
    var v = this;
    axios.get("http://localhost:3000/check-exists?guid=" + this.guid)
      .then(function (response) {
        v.$emit("galleryexistsresponse", response.data)
      });
  }
});

Vue.component('login', {
  props: ["guid"],
  data: function() {
    return {
      "password": "",
      "errorMessage": ""
    }
  },
  template: `
    <div>
      <p>Login</p>
      <p v-if="errorMessage.length > 0" style="color: red">{{errorMessage}}</p>
      <input v-model="password">
      <button v-on:click="authorise">Submit</button>
    </div>`,
  methods: {
    authorise: function () {
      // Get auth token for this gallery
      var v = this;
      axios.post("http://localhost:3000/gallery-auth", {
          guid: v.guid,
          password: v.password
        })
        .then(function (response) {
          if (response.data.hasError) {
            v.errorMessage = response.data.error.message;
          } else {
            v.$emit("galleryauthresponse", response.data.token)
          }
        });
    }
  }
});

Vue.component('image-gallery', {
  props: ["auth-token"],
  data: function () {
    return {
      pageSize: 3,
      page: 1
    }
  },
  template: `
    <div>
      <p>Size: <input v-model="pageSize"><br />#: <input v-model="page"></p>
      <gallery-image
        v-for="token in imageTokens"
        :token="token"
        :key="token">
      </gallery-image>
    </div>`,
  asyncComputed: {
    imageTokens: function() {
      if (this.authToken !== null) {
        // Get the list of image tokens based on the page size and number
        var v = this;
        return axios.post("http://localhost:3000/fetch-image-tokens", {
            page: v.page - 1, // pages are zero-indexed server-side
            pageSize: v.pageSize,
            token: v.authToken
          })
          .then(function (response) {
            console.log(response.data)
            return response.data.imageTokens;
          });
      }
      return [];
    }
  }
});


Vue.component('gallery-image', {
  props: ["token"],
  template: `
    <div>
      <img :src="source" />
    </div>`,
  asyncComputed: {
    source: function() {
      return axios.get("http://localhost:3000/image?token=" + this.token)
        .then(function (response) {
          return response.data.base64
        });
    }
  },
});

var vm = new Vue({
  el: '#app',
  data: {
    galleryExistsResponseData: null,
    galleryAuthToken: null,
    loginPassword: ""
  },
  methods: {
    SetGalleryExistsResponseData: function (e) {
      this.galleryExistsResponseData = e
    },
    SetGalleryAuthToken: function (e) {
      this.galleryAuthToken = e
    }
  },
  computed: {
    galleryGuid: function() {
      return urlParams.get("gallery");
    },
    galleryExistsResponseReceived: function() {
      return this.galleryExistsResponseData !== null
    },
    galleryExists: function() {
      if (this.galleryExistsResponseData === null) {
        return false;
      }
      if (this.galleryExistsResponseData.hasError) {
        return false;
      }
      return this.galleryExistsResponseData.exists;
    },
    haveGalleryAuthToken: function () {
      return this.galleryAuthToken !== null;
    }
  }
});
