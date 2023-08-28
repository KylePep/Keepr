<template>
  <section class="row">
    <div class="col-12">
      {{ activeProfile.name }}
      <img :src="activeProfile.picture" :alt="activeProfile.name">
    </div>
    <div class="col-12 d-flex">
      <div v-for="vault in vaults" :key="vault.id">
        {{ vault.name }}</div>
    </div>
    <div class="col-12 d-flex">
      <section class="row">
        <div class="col-3  mb-3" v-for="keep in keeps" :key="keep.id">
          <KeepCard :keepProp="keep" />
        </div>
      </section>
    </div>
  </section>
</template>


<script>
import { computed, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { useRoute } from "vue-router";
import Pop from "../utils/Pop.js";
import { accountService } from "../services/AccountService.js";
import { vaultsService } from "../services/VaultsService.js"
import KeepCard from "../components/KeepCard.vue";

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
    async function getVaultsByProfileId(profileId) {
      try {
        await accountService.getVaultsByProfileId(profileId);
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
      getProfileById(route.params.profileId);
      getVaultsByProfileId(route.params.profileId);
      getKeepsByProfileId(route.params.profileId);
    });
    return {
      activeProfile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepCard }
}
</script>


<style lang="scss" scoped></style>