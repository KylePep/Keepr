<template>
  <div class="card-bg vault-card position-relative rounded d-flex align-items-end">
    <div class="d-flex flex-grow-1 keep-bar">
      <router-link @click="setActiveVault(), emptyKeeps()" :to="{ name: 'Vault', params: { vaultId: vaultProp.id } }">
        <div class="d-flex align-items-center text-light px-3 py-3">
          <div class="fs-2 fw-bold">
            {{ vaultProp.name }}
          </div>
          <div v-if="vaultProp.isPrivate == true" class="mdi mdi-lock fs-3"></div>
        </div>
      </router-link>
    </div>
    <div @click="removeVault()" v-if="AppState.account?.id == vaultProp.creator.id"
      class="mdi mdi-close-circle text-danger fs-2 delete-button" title="Delete Keep">
    </div>
  </div>
</template>


<script>
import { computed } from "vue";
import { Vault } from "../models/Vault.js";
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";

export default {
  props: {
    vaultProp: { type: Vault, required: true }
  },
  setup(props) {
    return {
      keepBg: computed(() => `url("${props.vaultProp.img}")`),
      AppState: computed(() => AppState),
      setActiveVault() {
        AppState.activeVault = props.vaultProp
      },
      emptyKeeps() {
        AppState.keeps = []
      },
      async removeVault() {
        try {
          const removeConfrim = await Pop.confirm(`Delete this vault? ${props.vaultProp.name}, This can not be reveresed`)
          if (!removeConfrim) {
            return
          }
          await vaultsService.removeVault(props.vaultProp.id)
        } catch (error) {
          Pop.error(error.message, '[ERROR - removeVault]')
        }
      }

    }
  }
}
</script>


<style lang="scss" scoped>
.img-reserve {
  visibility: hidden;
}

.keep-bar {
  color: white;
  // position: absolute;
  background-image: linear-gradient(rgba(255, 255, 255, 0), rgba(0, 0, 0, 0.846));
  border-radius: 7px;
  // bottom: 0%;
  // right: 0%;
  // left: 0%;
}

.card-bg {
  background-image: v-bind(keepBg);
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  height: fit-content;
  height: 25vh;
}

.vault-card:hover .delete-button {
  display: block;
}

.delete-button {
  padding-right: .2em;
  position: absolute;
  top: 0%;
  right: 0%;
  display: none;
}

.delete-button:hover {
  cursor: pointer;
  scale: 1.1;
}
</style>