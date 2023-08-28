<template>
  <!-- Button trigger modal -->
  <!-- <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#newDataModal">
    Launch demo modal
  </button> -->

  <!-- Modal -->
  <div class="modal fade" id="newVaultModal" tabindex="-1" aria-labelledby="newVaultModal" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="newVaultModal"> Add your vault </h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="" action="">
            <input class="form-control" type="text" name="title" id="title" placeholder="Title" required>
            <input class="form-control" type="text" name="url" id="url" placeholder="URL" required>
            <div class="d-flex flex-column align-items-end">
              <p>
                Private vaults can only be seen by you
              </p>
              <div class="d-flex ">
                <input type="checkbox" name="isPrivate" id="isPrivate"> Make Private
              </div>
            </div>
          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-primary">Create Vault</button>
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