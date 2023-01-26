<template>
  <div class="card my-4 elevation-5 hover-card">
    <router-link :to="{name:'Restaurant', params: {id: restaurant.id}}">
      <img :src="restaurant.coverImg" alt="" class="rounded-top w-100">
    </router-link>
    <div class="card-body">
      <div class="d-flex align-items-center justify-content-between">
        <router-link :to="{name:'Restaurant', params: {id: restaurant.id}}">
          <h3 class="text-dark">{{ restaurant.name }}</h3>
        </router-link>

        <h3 class="w-50" v-if="restaurant.exposure">
          <marquee :title="restaurant.exposure">
              <i class="mdi mdi-nuke" v-for="i in restaurant.exposure"></i>
          </marquee>
        </h3>

        <h3 v-else>
          <i class="mdi mdi-food"></i>
        </h3>

      </div>
    </div>


  </div>
</template>


<script>
import { Restaurant } from '../models/Restaurant.js';

export default {
  props: {
    restaurant: {
      type: Restaurant,
      required: true
    }
  },
  setup(props) {
    return {

    }
  }
}
</script>


<style lang="scss" scoped>
img {
  max-height: 350px;
  object-fit: cover;
  object-position: center;

}

.hover-card {
  transition: all .3s linear;

  &:hover {
    transform: translateY(-1.2rem);
  }
}
</style>
