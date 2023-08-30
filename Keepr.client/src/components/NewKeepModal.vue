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
          <h1 class="modal-title fs-5" id="newKeepModalTitle">Add your keep</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createKeep()">
            <input v-model="editable.name" class="form-control" type="text" name="name" id="name" placeholder="Title"
              minlength="3" maxlength="50" required>
            <input v-model="editable.img" class="form-control" type="url" name="img" id="img" placeholder="URL"
              minlength="3" maxlength="700" required>
            <textarea v-model="editable.description" class="form-control" name="description" id="description" cols="60"
              rows="10" minlength="3" maxlength="1000" required></textarea>
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
import { ref } from "vue";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { Modal } from "bootstrap";

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async createKeep() {
        try {
          const keepData = editable.value
          await keepsService.createKeep(keepData)
          editable.value = {}
          Modal.getOrCreateInstance('#newKeepModal').hide()
        } catch (error) {
          Pop.error(error.message, '[ERROR - createKeep]')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
textarea {
  min-height: 15vh;
  max-height: 50vh;
}
</style>