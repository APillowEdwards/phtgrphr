import Vue from "vue"
import App from "./App.vue"
import AsyncComputed from "vue-async-computed"

import router from "./router"

Vue.use(AsyncComputed)

Vue.prototype.baseUrl = window.location.protocol + "//" + window.location.host + "/"

new Vue({
  router,
  render: h => h(App)
}).$mount("#app")
