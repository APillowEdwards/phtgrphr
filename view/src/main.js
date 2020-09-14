import Vue from "vue"
import App from "./App.vue"
import AsyncComputed from "vue-async-computed"
import VueLazyLoad from 'vue-lazyload'
import Clipboard from 'v-clipboard'

import router from "./router"

Vue.use(AsyncComputed)
Vue.use(VueLazyLoad)
Vue.use(Clipboard)

Vue.prototype.baseUrl = window.location.protocol + "//" + window.location.host + "/"

new Vue({
  router,
  render: h => h(App)
}).$mount("#app")
