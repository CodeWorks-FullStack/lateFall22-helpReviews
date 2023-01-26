<template>
  <!-- REVIEW be sure to null check -->
  <div class="rest-page container" v-if="restaurant">
    <!-- <button @click="debugging = !debugging">debug</button> -->
    <div class="bg-dark text-light p-4" v-if="debugging">
      {{ restaurant }}
      <hr>
      {{ reports }}
    </div>

    <div class="row bg-image align-items-center">
      <div class="col-12">
        <h1 class="ms-4">
          {{ restaurant.name }} <span v-if="restaurant.shutdown" class="ms-3"> <i
              class="mdi mdi-alien mdi-spin"></i></span>
          <br>
          <marquee class="w-50">
            <i v-for="r in restaurant.exposure" class="mdi mdi-nuke"></i>
          </marquee>
        </h1>

        <button @click="shutItDown()" v-if="canShutdown && !restaurant.shutdown"
          class="btn ms-4 btn-danger text-light px-5 py-2 fs-5 mt-2">Shutdown</button>
        <button @click="reopen()" v-if="canShutdown && restaurant.shutdown" class="btn ms-4 btn-warning text-dark px-5 py-2 fs-5 mt-2">
          <b>
            re-open
          </b>
        </button>
      </div>
    </div>

    <div class="row py-3 bg-white">
      <div class="col-12 d-flex justify-content-between p-4">
        <h2>Reports:</h2>
      </div>
      <div class="col-12 p-4">
        <button class="btn btn-success fs-5 px-4 py-2" data-bs-toggle="modal" data-bs-target="#report-modal">Leave
          Report</button>
      </div>
    </div>

    <div class="row">
      <div class="col-md-8 m-auto">



      </div>
    </div>

    <div class="row">
      <div class="col-12 col-md-6 p-3" v-for="r in reports" :key="r.id">
        <ReportCard :report="r" />
      </div>
    </div>






    <Modal id="report-modal" modal-title="Report Form">
      <template #modal-header>
        <div class="py-5 cool-header d-flex align-items-center justify-content-between">
          <h1>Space is neat</h1>
          <button class="btn" data-bs-dismiss="modal"><i class="mdi mdi-close"></i></button>
        </div>
      </template>


      <div class="card">
        <div class="card-body">
          <h3>Report Form</h3>
          <ReportForm />
        </div>
      </div>
    </Modal>



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
      isClosed,
      // REVIEW CSS v-bind becomes the class .bg-image
      coverImg: computed(() => `url(${AppState.restaurant?.coverImg})`),
      canShutdown: computed(() => AppState.restaurant?.exposure > 10 && AppState.reports.length > 3 && AppState.account.id == AppState.restaurant?.ownerId),
      async reopen() {
        try {
          await restaurantsService.reopen(route.params.id)
        } catch (error) {
          Pop.error(error)
        }
      },
      async shutItDown() {
        try {

          const yes = await Pop.confirm('Are you sure you want to close this restaurant down?', 'You will have to reopen it from the db... because I don\'t have client way of doing that...')
          if (!yes) { return }

          await restaurantsService.shutItDown(route.params.id)
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.bg-image {
  min-height: 50vh;
  background-image: v-bind(coverImg);
  background-size: cover;
  background-position: center;
}

h1 {
  text-shadow: 1px 1px 2px rgb(255, 255, 255);
  color: wheat;
}

.cool-header {
  background-image: url(https://www.si.edu/sites/default/files/blog/crab_nebula.jpg);
  background-attachment: fixed;
  height: 100%;
  background-size: cover;
  background-position: center;
  width: 100%;
  padding: 1rem;

  button,
  i {
    text-shadow: 1px 1px 2px rgb(255, 255, 255);
    color: wheat
  }
}
</style>
