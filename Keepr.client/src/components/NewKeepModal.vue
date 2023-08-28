<template>
  <!-- Button trigger modal -->
  <!-- <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#newDataModal">
    Launch demo modal
  </button> -->

  <!-- Modal -->
  <div class="modal fade" id="newKeepModal" tabindex="-1" aria-labelledby="newKeepModal" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="newKeepModal">Add your keep</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="" action="">
            <input class="form-control" type="text" name="title" id="title" placeholder="Title" required>
            <input class="form-control" type="text" name="url" id="url" placeholder="URL" required>
            <textarea class="form-control" name="description" id="description" cols="60" rows="10" required></textarea>

          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-primary">Create</button>
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


<style lang="scss" scoped>
textarea {
  min-height: 15vh;
  max-height: 50vh;
}
</style>