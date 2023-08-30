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
          <h1 class="modal-title fs-5" id="newVaultModalTitle">
            <p v-if="edit == false">
              Add your Vault
            </p>
            <p v-else>
              Edit your Vault
            </p>
          </h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form v-if="edit == false" @submit.prevent="createVault()">
            <input v-model="editable.name" class="form-control" type="text" name="title" id="title" placeholder="Title"
              minlength="3" maxlength="50" required>
            <input v-model="editable.description" class="form-control" type="text" name="description" id="description"
              placeholder="Description" minlength="3" maxlength="1000" required>
            <input v-model="editable.img" class="form-control" type="url" name="url" id="url" placeholder="URL"
              minlength="3" maxlength="700" required>
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
          <form v-else @submit.prevent="editVault()" action="">
            <input v-model="updateable.name" class="form-control" type="text" name="title" id="title" placeholder="Title">
            <input v-model="updateable.description" class="form-control" type="text" name="description" id="description"
              placeholder="Description">
            <input v-model="updateable.img" class="form-control" type="text" name="url" id="url" placeholder="URL">
            <div class="d-flex justify-content-between">
              <i @click="changePrivacy()" class="mdi mdi-rotate-3d-variant btn btn-primary d-flex">
                <p v-if="vault.isPrivate">Private</p>
                <p v-else>Public</p>
              </i>
              <button type="submit" class="btn btn-primary" title="Vault Submit">
                <p v-if="edit == false">Create Vault</p>
                <p v-else>Edit Vault</p>
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted, ref, watchEffect } from "vue";
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";
import { Modal } from "bootstrap";
import { AppState } from "../AppState.js";

export default {
  setup() {
    const editable = ref({})
    const updateable = ref({})
    onMounted(() => editable.value.isPrivate = false)
    watchEffect(() => {
      AppState.activeVault
      updateable.value = { ...AppState.activeVault }
    })
    return {
      editable,
      updateable,
      edit: computed(() => AppState.edit),
      vault: computed(() => AppState.activeVault),
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
      },
      async editVault() {
        try {
          const vaultData = updateable.value
          await vaultsService.editVault(vaultData)
          editable.value = {}
          AppState.edit = false
          Modal.getOrCreateInstance('#newVaultModal').hide()
        } catch (error) {
          Pop.error(error.message, '[ERROR- editVault]')
        }
      },
      async changePrivacy() {
        try {
          const vaultData = AppState.activeVault
          vaultData.isPrivate = !AppState.activeVault.isPrivate
          await vaultsService.changePrivacy(vaultData)
        } catch (error) {
          Pop.error(error.message, '[ERROR- changePrivacy]')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
p {
  padding: 0;
  margin: 0;
}
</style>