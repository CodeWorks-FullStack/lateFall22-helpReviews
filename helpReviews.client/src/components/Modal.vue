<template>
  <div class="modal fade" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <slot name="modal-header">
            <h1 class="modal-title fs-5" id="exampleModalLabel">{{ modalTitle }}</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </slot>
        </div>

        <slot></slot>

      </div>
    </div>
  </div>
</template>


<script>
export default {
  props: {
    modalTitle: {
      type: String,
      required: false,
      default: 'Close'
    },
  },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>

</style>
