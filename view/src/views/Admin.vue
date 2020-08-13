<template>
  <div>
    <h1>This is an admin page</h1>

    <login
      v-if="!haveAdminAuthToken"
      @adminauthresponse="SetAdminAuthToken($event)">
    </login>

    <admin-menu
      v-if="IsPageState(PAGE_STATES.MENU)"
      :states="PAGE_STATES"
      @menuselectionmade="SetPageState($event)">
    </admin-menu>

    <gallery-add
      v-if="IsPageState(PAGE_STATES.ADD_GALLERY)"
      @backbuttonpressed="SetPageState(PAGE_STATES.MENU)">
    </gallery-add>

    <gallery-view-all
      v-if="IsPageState(PAGE_STATES.VIEW_ALL_GALLERIES)"
      @backbuttonpressed="SetPageState(PAGE_STATES.MENU)">
    </gallery-view-all>

    <!--  -->
  </div>
</template>


<script>
  import Login from "@/components/admin/Login.vue"
  import AdminMenu from "@/components/admin/AdminMenu.vue"

  import GalleryAdd from "@/components/admin/gallery/Add.vue"
  import GalleryViewAll from "@/components/admin/gallery/ViewAll.vue"


  export default {
    name: "Admin",
    components: {
      Login,
      AdminMenu,
      GalleryAdd,
      GalleryViewAll
    },
    data: function () {
      return {
        adminAuthToken: null,
        PAGE_STATES: {
          MENU: 1,
          ADD_GALLERY: 2,
          VIEW_ALL_GALLERIES: 3
        },
        pageState: ""
      }
    },
    methods: {
      SetAdminAuthToken: function (e) {
        this.pageState = this.PAGE_STATES.MENU;
        this.adminAuthToken = e;
      },
      SetPageState: function (e) {
        this.pageState = e;
      },
      IsPageState: function (state) {
        console.log("Comparing " + this.pageState + " to " + state);
        return this.pageState == state;
      }
    },
    computed: {
      haveAdminAuthToken: function () {
        return this.adminAuthToken !== null;
      },
      pageStateName: function () {
        return Object.keys(this.PAGE_STATES).find(key => this.PAGE_STATES[key] === this.pageState)
      }
    }
  }
</script>
