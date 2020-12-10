import Vue from "vue"
import VueRouter from "vue-router"
import Public from "../views/Public.vue"

Vue.use(VueRouter)

  const routes = [
  {
    path: "/",
    name: "Public",
    component: Public
  },
  {
    path: "/admin",
    name: "Admin",
    component: () => import("../views/Admin.vue")
  },
  {
    path: "/stats",
    name: "Stats",
    component: () => import("../views/Stats.vue")
  },
  {
    path: "*",
    name: "NotFound",
    component: () => import("../views/NotFound.vue")
  }
]

const router = new VueRouter({
  mode: "history",
  routes
})

export default router
