<template>
  <div class="container">
    <div class="row">
      <div class="col-md-8 m-auto" v-for="r in restaurants">
        <RestaurantCard :restaurant="r" />
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, computed } from 'vue';
import { AppState } from '../AppState.js';
import { restaurantsService } from '../services/RestaurantsService.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {

    async function getRestaurants() {
      try {
        await restaurantsService.getRestaurants()
      } catch (error) {
        Pop.error(e)
      }
    }

    onMounted(() => {
      getRestaurants()
    })

    return {
      restaurants: computed(() => AppState.restaurants)
    }
  }
}
</script>

<style scoped lang="scss">

</style>
