<template>
  <!-- Button trigger modal -->
  <!-- <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#accountFormModal">
    Launch demo modal
  </button> -->

  <!-- Modal -->
  <div class="modal fade" id="accountFormModal" tabindex="-1" aria-labelledby="accountFormModal" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="accountFormModalTitle">Edit your account</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="editAccount()">
            <input v-model="editable.name" class="form-control" type="text" name="name" id="name" placeholder="Name..."
              minlength="3" maxlength="255" required>
            <input v-model="editable.picture" class="form-control" type="url" name="picture" id="picture"
              placeholder="Picture..." minlength="3" maxlength="255" required>
            <input v-model="editable.coverImg" class="form-control" type="url" name="coverImg" id="icoverImg"
              placeholder="Cover Image..." minlength="3" maxlength="700" required>
            <div class="d-flex justify-content-end">
              <button type="submit" class="btn btn-primary">Update</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { ref, watchEffect } from "vue"
import { accountService } from "../services/AccountService.js"
import { Modal } from "bootstrap"
import Pop from "../utils/Pop.js"
import { AppState } from "../AppState.js"

export default {
  setup() {
    const editable = ref({})
    watchEffect(() => {
      AppState.account
      editable.value = { ...AppState.account }
    })
    return {
      editable,
      async editAccount() {
        try {
          const accountData = editable.value
          await accountService.editAccount(accountData)
          Modal.getOrCreateInstance('#accountFormModal').hide()
        } catch (error) {
          Pop.error(error.message, '[ERROR - editAccount]')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped></style>