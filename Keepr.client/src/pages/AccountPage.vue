<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12 mb-3">
        {{ activeProfile.name }}
        <img :src="activeProfile.picture" :alt="activeProfile.name">
        Vaults {{ vaults.length }} | Keeps {{ keeps.length }}
      </div>
      <!-- NOTE removed d-flex -->
      <div v-if="vaults" class="col-12">
        <!-- NOTE removed v-if keeps -->
        <section class="row">
          <div class="col-2 mb-3" v-for="vault in vaults" :key="vault.id">
            <VaultCard :vaultProp="vault" />
          </div>
        </section>
      </div>
      <div class="col-12 d-flex">
        <section v-if="keeps" class="masonry-with-columns">
          <div class=" mb-3" v-for="keep in keeps" :key="keep.id">
            <KeepCard :keepProp="keep" />
          </div>
        </section>
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { useRoute } from "vue-router";
import { accountService } from "../services/AccountService.js";
import Pop from "../utils/Pop.js";
import KeepCard from "../components/KeepCard.vue";
import VaultCard from "../components/VaultCard.vue";
export default {
  setup() {
    const route = useRoute();
    async function getProfileById(profileId) {
      try {
        await accountService.getProfileById(profileId);
      }
      catch (error) {
        Pop.error(error.message, '[ERROR OCCURED- getProfileById]');
      }
    }
    async function getAccountVaults(profileId) {
      try {
        await accountService.getAccountVaults();
      }
      catch (error) {
        Pop.error(error.message, '[ERROR OCCURED- getVaultsByProfileId]');
      }
    }
    async function getKeepsByProfileId(profileId) {
      try {
        await accountService.getKeepsByProfileId(profileId);
      }
      catch (error) {
        Pop.error(error.message, '[ERROR OCCURED- getKeepsByProfileId]');
      }
    }

    watchEffect(() => {
      getProfileById(AppState.account.id);
      getAccountVaults(AppState.account.id);
      getKeepsByProfileId(AppState.account.id);
    });
    return {
      AppState: computed(() => AppState),
      activeProfile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepCard, VaultCard }
}
</script>

<style scoped>
img {
  max-width: 100px;
}

.masonry-with-columns {
  columns: 4 200px;
  column-gap: 1rem;
}
</style>
