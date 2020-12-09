import Vue from "vue"
import App from "./App.vue"
import AsyncComputed from "vue-async-computed"
import VueLazyLoad from 'vue-lazyload'
import Clipboard from 'v-clipboard'
import BootstrapVue from 'bootstrap-vue'

import VueBlurHash from 'vue-blurhash'
import 'vue-blurhash/dist/vue-blurhash.css'

import router from "./router"

Vue.use(AsyncComputed)
Vue.use(VueLazyLoad)
Vue.use(Clipboard)
Vue.use(BootstrapVue)
Vue.use(VueBlurHash)

Vue.prototype.$baseUrl = window.location.protocol + "//" + window.location.host + "/"
Vue.prototype.$loadingSpinner = require('@/assets/loading.svg')

new Vue({
  router,
  render: h => h(App)
}).$mount("#app")
