<template>
  <!-- Button trigger modal -->
  <!-- <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#keepModal">
    Launch static backdrop modal
  </button> -->

  <!-- Modal -->
  <div class="modal fade" id="keepModal" data-bs-backdrop="modal" data-bs-keyboard="false" tabindex="-1"
    aria-labelledby="keepModal" aria-hidden="true">
    <div class="modal-dialog modal-xl modal-dialog-centered">
      <div class="container-fluid modal-content">
        <div class="row modal-body">
          <div class="col-6">
            Image Goes here
          </div>
          <div class="col-6">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary">Understood</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped></style>