<template>
  <div class="container-fluid">

    <div class="col-12 " v-if="vault?.id">
      <section class="row d-flex justify-content-center">
        <div class="col-7 vault-bg d-flex justify-content-center align-items-end p-0 mx-5 mt-5 rounded fs-3 text-light">

          <div class="d-flex flex-column keep-bar p-3 flex-grow-1">
            <p class="text-center fs-1 fw-bold">
              {{ vault.name }}
              <span v-if="vault.isPrivate == true" class="mdi mdi-lock"></span>
            </p>

            <p class="text-center">
              By {{ vault.creator.name }}
            </p>
          </div>
        </div>
        <div class="col-7 d-flex justify-content-end ">

          <div v-if="vault.creator.id == accountId" @click="setEdit()" class="ms-3 fs-4" type="button"
            data-bs-toggle="modal" data-bs-target="#newVaultModal">
            <i class="mdi mdi-dots-horizontal p-0  selectable"></i>
          </div>
        </div>
        <div class="col-7 d-flex justify-content-center fs-1">
          {{ keeps.length }} Keeps
        </div>
      </section>
    </div>
    <div v-if="keeps" class="keep-Section masonry-with-columns mt-3">
      <div class="  mb-3" v-for="keep in keeps" :key="keep.id">
        <KeepCard :keepProp="keep" />
      </div>
    </div>
  </div>
</template>


<script>
import { useRoute } from "vue-router";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";
import { computed, onUnmounted, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { keepsService } from "../services/KeepsService.js";
import { router } from "../router.js";

export default {
  setup() {
    const route = useRoute()
    async function getVaultById(vaultId) {
      try {
        let vaultId = route.params.vaultId
        await vaultsService.getVaultById(vaultId)
      } catch (error) {
        Pop.error(error.message, '[ERROR-getVaultById]')
        if (error.response.data.includes('⚠️')) {
          router.push({ name: "Home" })
        }
      }
    }
    async function getKeepsByVaultId(vaultId) {
      try {
        await keepsService.getKeepsByVaultId(vaultId)
      } catch (error) {
        Pop.error(error.message, '[ERROR-getKeepsByVaultId]')
      }
    }
    watchEffect(() => {
      getVaultById(route.params.vaultId)
      getKeepsByVaultId(route.params.vaultId)
    })
    onUnmounted(() => {
      AppState.activeVault = {}
      AppState.keeps = []
    })
    return {
      AppState: computed(() => AppState),
      keeps: computed(() => AppState.keeps),
      vault: computed(() => AppState.activeVault),
      vaultBg: computed(() => `url("${AppState.activeVault.img}")`),
      accountId: computed(() => AppState.account?.id),
      setEdit() {
        AppState.edit = true
      }

    }
  }
}
</script>


<style lang="scss" scoped>
.vault-bg {
  background-image: v-bind(vaultBg);
  background-position: center;
  background-size: cover;
  background-repeat: no-repeat;
  height: 40vh;
}

.keep-bar {
  color: white;
  // position: absolute;
  background-image: linear-gradient(rgba(255, 255, 255, 0), rgba(0, 0, 0, 0.767));
  border-radius: 7px;
  // bottom: 0%;
  // left: 0%;
}
</style>