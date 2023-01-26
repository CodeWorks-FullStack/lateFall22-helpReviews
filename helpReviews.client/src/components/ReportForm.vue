<template>
  <form @submit.prevent="handleSubmit">
    <div class="mb-2">
      <input class="form-control" placeholder="review title" type="text" v-model="editable.title" required>
    </div>
    <div>
      <textarea placeholder="review" class="form-control" rows="8" v-model="editable.body" required></textarea>
    </div>

    <button class="mt-2 btn btn-primary" type="submit" data-bs-dismiss="modal">Submit</button>


  </form>
</template>


<script>
import { ref } from 'vue';
import { useRoute } from 'vue-router';
import { reportsService } from '../services/ReportsService.js';
import Pop from '../utils/Pop.js';

export default {
  setup() {
    const editable = ref({})
    const route = useRoute()

    return {
      editable,
      async handleSubmit() {
        try {
          editable.value.restaurantId = route.params.id
          await reportsService.createReport({
            ...editable.value, // this takes both properties from the form
            restaurantId: route.params.id // this adds the third required prop from the url
          })
          editable.value = {} // clear the form
        } catch (error) {
          Pop.error(error)
        }
      }

    }
  }
}
</script>


<style lang="scss" scoped>

</style>
