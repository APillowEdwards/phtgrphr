import Vue from "vue"
import App from "./App.vue"
import AsyncComputed from "vue-async-computed"

import router from "./router"

Vue.use(AsyncComputed)

new Vue({
  router,
  render: h => h(App)
}).$mount("#app")
