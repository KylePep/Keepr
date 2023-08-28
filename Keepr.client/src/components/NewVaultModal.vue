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
          <form @submit.prevent="createVault()">
            <input v-model="editable.name" class="form-control" type="text" name="title" id="title" placeholder="Title"
              required>
            <input v-model="editable.description" class="form-control" type="text" name="description" id="description"
              placeholder="Description" required>
            <input v-model="editable.img" class="form-control" type="text" name="url" id="url" placeholder="URL" required>
            <div class="d-flex flex-column align-items-end">
              <p>
                Private vaults can only be seen by you
              </p>
              <div class="d-flex ">
                <input v-model="editable.isPrivate" type="checkbox" name="isPrivate" id="isPrivate"> Make Private
              </div>
            </div>
            <div class="d-flex justify-content-end">
              <button type="submit" class="btn btn-primary">Create Vault</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { onMounted, ref } from "vue";
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";
import { Modal } from "bootstrap";

export default {
  setup() {
    const editable = ref({})
    onMounted(() => editable.value.isPrivate = false)
    return {
      editable,
      async createVault() {
        try {
          const vaultData = editable.value
          if (vaultData.isPrivate != true) {
            vaultData.isPrivate = false
          }
          await vaultsService.createVault(vaultData)
          editable.value = {}
          Modal.getOrCreateInstance('#newVaultModal').hide()
        } catch (error) {
          Pop.error(error.message, '[ERROR- createVault]')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped></style>