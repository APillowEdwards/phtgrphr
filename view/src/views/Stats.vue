<template>
  <div>
    <h1>Application Statistics</h1>

    <img v-if="!stats" class="loading-spinner" :src="$loadingSpinner" />

    <div v-else class="row">
      <div class="col-12 col-md-6">
        <h3 class="stat-title">Total Galleries</h3>
        <p class="stat-value">{{ stats.totalGalleries }}</p>
      </div>

      <div class="col-12 col-md-6">
        <h3 class="stat-title">Total Images</h3>
        <p class="stat-value">{{ stats.totalImages }}</p>
      </div>
    </div>

    <div class="row">
      <div class="col-12 col-md-3"></div>

      <div class="col-12 col-md-6">
        <h3 class="stat-title">Total Gallery Views</h3>
        <p class="stat-value">{{ stats.totalAccesses }}</p>
      </div>

      <div class="col-12 col-md-3"></div>
    </div>

  </div>
</template>

<script>
  import API from "@/api"

  export default {
    name: "Stats",
    data: function () {
      return {
        stats: null
      }
    },
    methods: {
      refresh: function () {
        var v = this;
        return API.get(`/v1/public/stats/`)
          .then(function (response) {
            v.stats = response.data.result;
          })
          .catch(function () {
            v.stats = null;
          });
      }
    },
    created: function() {
      this.refresh();
    }
  }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .stat-title {
    text-align: center;
  }
  .stat-value {
    text-align: center;
    font-size: 4em;
    line-height: 0.8em;
  }
</style>
