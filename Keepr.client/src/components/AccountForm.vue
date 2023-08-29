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
          <h1 class="modal-title fs-5" id="accountFormModal">Add your keep</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createKeep()">
            <input v-model="editable.name" class="form-control" type="text" name="name" id="name" placeholder="Title"
              required>
            <input v-model="editable.img" class="form-control" type="text" name="img" id="img" placeholder="URL" required>
            <textarea v-model="editable.description" class="form-control" name="description" id="description" cols="60"
              rows="10" required></textarea>
            <div class="d-flex justify-content-end">
              <button type="submit" class="btn btn-primary">Create</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { ref } from "vue"
import { accountService } from "../services/AccountService.js"
import { Modal } from "bootstrap"
import Pop from "../utils/Pop.js"

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async editAccount() {
        try {
          const accountData = editable.value
          await accountService.editAccount(accountData)
          editable.value = {}
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