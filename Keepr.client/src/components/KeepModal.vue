<template>
  <!-- Modal -->
  <div class="modal fade" id="keepModal" data-bs-backdrop="modal" data-bs-keyboard="false" tabindex="-1"
    aria-labelledby="keepModal" aria-hidden="true">
    <div class="modal-dialog modal-xl modal-dialog-centered">
      <div class="container-fluid  modal-content p-0">
        <div v-if="keep.id" class="row modal-body p-0 m-0">
          <div class="col-6 card-bg content p-0 m-0">
          </div>
          <div class="col-6 d-flex flex-column justify-content-between p-4">
            <div class="text-center">
              <i class="mdi mdi-eye"></i> {{ keep.views }} | <i class="kept">K</i> {{ keep.kept }}
            </div>
            <div class="text-center fs-1">
              {{ keep.name }}
            </div>
            <div>
              {{ keep.description }}
            </div>
            <div class="d-flex justify-content-between">
              <div v-if="route.name != 'Vault'">
                <form v-if="AppState.account.id" @submit.prevent="createVaultKeep()" class="form-group d-flex">
                  <label for="exampleFormControlSelect1"></label>
                  <select v-model="editable.vault" class="form-control" id="exampleFormControlSelect1">
                    <option v-for=" vault  in  vaults " :key="vault.id" :value="vault.id">{{ vault.name }}</option>
                  </select>
                  <button class="ms-3 btn btn-light">Save</button>
                </form>
              </div>
              <!-- <div @click="removeKeep()" v-if="route.name == 'Profile' || route.name == 'Account'"
                class="selectable fs-4">
                <i class="mdi mdi-cancel"></i> Remove Keep
              </div> -->
              <div @click="removeVaultKeep()" v-if="route.name == 'Vault'" class="selectable fs-4">
                <i class="mdi mdi-cancel"></i>Remove Keep From Vault
              </div>
              <div v-else></div>
              <div>
                <router-link :to="{ name: 'Profile', params: { profileId: keep.creator.id } }" @click="closeModal()">
                  <img class="avatar" :src="keep.creator.picture" :alt="keep.creator.name" :title="keep.creator.name">
                </router-link>
                {{ keep.creator.name }}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { Modal } from "bootstrap";
import Pop from "../utils/Pop.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js"
import { useRoute, useRouter } from "vue-router";
import { keepsService } from "../services/KeepsService.js";

export default {
  setup() {
    const route = useRoute()
    const editable = ({})
    // watchEffect(() => {
    //   route.name
    // })
    return {
      editable,
      route,
      AppState: computed(() => AppState),
      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.vaults),
      keepBg: computed(() => `url("${AppState.activeKeep.img}")`),
      closeModal() {
        Modal.getOrCreateInstance('#keepModal').hide()
      },
      async createVaultKeep() {
        try {
          const vaultKeepData = {}
          vaultKeepData.vaultId = editable.vault
          vaultKeepData.keepId = AppState.activeKeep.id
          await vaultKeepsService.createVaultKeep(vaultKeepData)
        } catch (error) {
          Pop.error(error.message, '[ERROR-createVaultKeep]')
        }
      },
      async removeVaultKeep() {
        try {
          await vaultKeepsService.removeVaultKeep()
        } catch (error) {
          Pop.error(error.message, '[ERROR-removeVaultKeep]')
        }
      },
      async removeKeep() {
        try {
          await keepsService.removeKeep(AppState.activeKeep.id)
          this.closeModal()
        } catch (error) {
          Pop.error(error.message, '[ERROR-removeKeep]')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.kept {
  border: 1px solid black;
  border-radius: 8px;
  padding: 0 5px 1px 5px;
}

.card-bg {
  background-image: v-bind(keepBg);
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  height: fit-content;
}

.content {
  height: 80vh;
}
</style>