<template>
  <!-- REVIEW be sure to null check -->
  <div class="rest-page container" v-if="restaurant">
    <button @click="debugging = !debugging">debug</button>
    <div class="bg-dark text-light p-4" v-if="debugging">
      {{ restaurant }}
      <hr>
      {{ reports }}
    </div>

    <div class="row">
      <div class="col-md-8 m-auto">

        <div class="card">
          <div class="card-body">
            <h3>Review Form</h3>
            <ReportForm />
          </div>
        </div>

      </div>
    </div>



  </div>
  <div v-else>
    loading.....
  </div>
  <div v-if="isClosed">
    <h1>This restaurant makes babies cry so it is closed</h1>
    <img
      src="https://media.istockphoto.com/id/151557041/photo/baby-crying.jpg?s=612x612&w=0&k=20&c=PR6N_B-8TRjeyBVPzud5Gw_sJZZlf3wOgtg_4-AmGbM="
      alt="">
  </div>
</template>


<script>
import { onMounted, computed, ref } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState.js';
import { router } from '../router.js';
import { restaurantsService } from '../services/RestaurantsService.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    const route = useRoute()
    const debugging = ref(false)

    const isClosed = ref(false)

    async function getRestaurant() {
      try {
        await restaurantsService.getRestaurantById(route.params.id)
      } catch (error) {
        isClosed.value = true
        // Pop.toast(error.response.data, 'info', 'center', 5000, true)
        setTimeout(() => {
          router.push({ name: 'Home' })
        }, 5000)
      }
    }

    async function getReports() {
      try {
        await restaurantsService.getReports(route.params.id)
      } catch (error) {
        Pop.error(error)
      }
    }

    onMounted(() => {
      getRestaurant()
      getReports()
    })

    return {
      restaurant: computed(() => AppState.restaurant),
      reports: computed(() => AppState.reports),
      debugging,
      isClosed
    }
  }
}
</script>


<style lang="scss" scoped>

</style>
