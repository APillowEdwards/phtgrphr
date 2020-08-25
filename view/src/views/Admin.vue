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

    <gallery-add-edit
      v-if="IsPageState(PAGE_STATES.ADD_GALLERY)"
      :id="0"
      :token="adminAuthToken"
      @backbuttonpressed="SetPageState(PAGE_STATES.MENU)"
      @gallerysaved="SetPageState(PAGE_STATES.VIEW_ALL_GALLERIES)">
    </gallery-add-edit>

    <gallery-view-all
      v-if="IsPageState(PAGE_STATES.VIEW_ALL_GALLERIES)"
      :token="adminAuthToken"
      @backbuttonpressed="SetPageState(PAGE_STATES.MENU)"
      @editgallerypressed="SetGalleryEditId"
      @updateimagespressed="SetGalleryImagesId">
    </gallery-view-all>

    <gallery-add-edit
      v-if="IsPageState(PAGE_STATES.EDIT_GALLERY)"
      :id="galleryEditId"
      :token="adminAuthToken"
      @backbuttonpressed="SetPageState(PAGE_STATES.VIEW_ALL_GALLERIES)"
      @gallerysaved="SetPageState(PAGE_STATES.VIEW_ALL_GALLERIES)">
    </gallery-add-edit>

    <gallery-images
      v-if="IsPageState(PAGE_STATES.GALLERY_IMAGES)"
      :id="galleryImagesId"
      :token="adminAuthToken"
      @backbuttonpressed="SetPageState(PAGE_STATES.VIEW_ALL_GALLERIES)">
    </gallery-images>

  </div>
</template>


<script>
  import Login from "@/components/admin/Login.vue"
  import AdminMenu from "@/components/admin/AdminMenu.vue"

  import GalleryAddEdit from "@/components/admin/gallery/AddEdit.vue"
  import GalleryViewAll from "@/components/admin/gallery/ViewAll.vue"

  import GalleryImages from "@/components/admin/gallery/images/Index.vue"

  export default {
    name: "Admin",
    components: {
      Login,
      AdminMenu,
      GalleryAddEdit,
      GalleryViewAll,
      GalleryImages
    },
    data: function () {
      return {
        adminAuthToken: null,
        PAGE_STATES: {
          MENU: 1,
          ADD_GALLERY: 2,
          EDIT_GALLERY: 3,
          VIEW_ALL_GALLERIES: 4,
          GALLERY_IMAGES: 5
        },
        pageState: -1,
        galleryEditId: null,
        galleryImagesId: null
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
        return this.pageState == state;
      },
      SetGalleryEditId: function (id) {
        this.galleryEditId = id;
        this.pageState = this.PAGE_STATES.EDIT_GALLERY
      },
      SetGalleryImagesId: function (id) {
        this.galleryImagesId = id;
        this.pageState = this.PAGE_STATES.GALLERY_IMAGES
      },
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
