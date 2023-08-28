<template>
  <div class="container-fluid">

    <div class="col-12" v-if="AppState.activeVault.id">
      {{ vault.name }}
      <img :src="vault.creator.picture" :alt="vault.creator.name">
    </div>
    <div v-if="keeps" class="col-3 d-flex">
      <div class="col-12  mb-3" v-for="keep in keeps" :key="keep.id">
        <KeepCard :keepProp="keep" />
      </div>
    </div>
  </div>
</template>


<script>
import { useRoute } from "vue-router";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";
import { computed, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { keepsService } from "../services/KeepsService.js";

export default {
  setup() {
    const route = useRoute()
    async function getVaultById(vaultId) {
      try {
        await vaultsService.getVaultById(vaultId)
      } catch (error) {
        Pop.error(error.message, '[ERROR-getVaultById]')
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
    return {
      AppState: computed(() => AppState),
      keeps: computed(() => AppState.keeps),
      vault: computed(() => AppState.activeVault)
    }
  }
}
</script>


<style lang="scss" scoped></style>