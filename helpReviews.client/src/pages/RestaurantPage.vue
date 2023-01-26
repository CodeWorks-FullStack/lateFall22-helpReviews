<template>
  <!-- REVIEW be sure to null check -->
  <div class="rest-page" v-if="restaurant">
    <button @click="debugging = !debugging">debug</button>
    <div class="bg-dark text-light p-4" v-if="debugging">
      {{ restaurant }}
      <hr>
      {{ reports }}
    </div>




  </div>
  <div v-else>
    loading.....
  </div>
</template>


<script>
import { onMounted, computed, ref } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';
import { restaurantsService } from '../services/RestaurantsService.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    const route = useRoute()
    const debugging = ref(false)

    async function getRestaurant() {
      try {
        await restaurantsService.getRestaurantById(route.params.id)
        await restaurantsService.getReports(route.params.id)
      } catch (error) {
        Pop.error(e)
      }
    }

    onMounted(() => {
      getRestaurant()
    })

    return {
      restaurant: computed(() => AppState.restaurant),
      reports: computed(() => AppState.reports),
      debugging
    }
  }
}
</script>


<style lang="scss" scoped>

</style>
