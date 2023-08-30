<template>
  <!-- Modal -->
  <div class="modal fade" id="keepModal" data-bs-backdrop="modal" data-bs-keyboard="false" tabindex="-1"
    aria-labelledby="keepModal" aria-hidden="true">
    <div class="modal-dialog modal-xl modal-dialog-centered">
      <div class="container-fluid  modal-content p-0">
        <div v-if="keep.id" class="row modal-body p-0 m-0">
          <div class="col-12 col-md-6 card-bg content p-0 m-0">
          </div>
          <div class="col-12 col-md-6 d-flex flex-column justify-content-between p-4">
            <div class="text-center">
              <i class="mdi mdi-eye"></i> {{ keep.views }} | <i class="kept">K</i> {{ keep.kept }}
            </div>
            <div>
              <div class="text-center fs-1 mb-3">
                {{ keep.name }}
              </div>
              <div>
                {{ keep.description }}
              </div>
            </div>
            <div class="d-md-flex justify-content-between">
              <div v-if="route.name != 'Vault'">
                <form v-if="AppState.account.id" @submit.prevent="createVaultKeep()" class="form-group d-flex mb-3">
                  <label for="exampleFormControlSelect1"></label>
                  <select v-model="editable.vault" class="form-control" id="exampleFormControlSelect1">
                    <option v-for=" vault  in  vaults " :key="vault.id" :value="vault.id">{{ vault.name }}</option>
                  </select>
                  <button class="ms-3 btn btn-light" title="Save Keep to Vault">Save</button>
                </form>
              </div>
              <div @click="removeVaultKeep()" v-if="route.name == 'Vault' && AppState.account.id" class="selectable fs-4">
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
import { logger } from "../utils/Logger.js";

export default {
  setup() {
    const route = useRoute()
    const editable = ({})
    // function editableDefault() {
    //   logger.log('tried to set default')
    //   let vault = 
    //   editable.vault = vault
    // }
    // onMounted(() => {
    //   editableDefault()
    // })
    // watchEffect(() => {
    //   AppState.activeKeep
    //   editableDefault()
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
          const removeConfrim = await Pop.confirm(`Remove this Keep from your vault? ${AppState.activeKeep.name}`)
          if (!removeConfrim) {
            return
          }
          await vaultKeepsService.removeVaultKeep(AppState.activeKeep.vaultKeepId)
          this.closeModal()
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
  height: 70vh;
}

@media screen and (min-width: 768px) {
  .content {
    height: 80vh;
  }
}
</style>