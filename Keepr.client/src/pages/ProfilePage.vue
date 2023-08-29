<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12 mb-3">
        <section class="row d-flex justify-content-center">
          <div class="col-9 cover-img position-relative m-3 mb-5 rounded">
            <img class="profile-avatar" :src="activeProfile.picture" :alt="activeProfile.name">
          </div>
          <!-- <div class="ms-3 fs-4 col-9 d-flex justify-content-end" type="button" data-bs-toggle="modal"
            data-bs-target="#accountFormModal">
            <i class="mdi mdi-dots-horizontal p-0  selectable"></i>
          </div> -->
        </section>
        <div class="text-center fs-3">
          {{ activeProfile.name }}
          <p class="fs-5">
            Vaults {{ vaults.length }} | Keeps {{ keeps.length }}
          </p>
        </div>
      </div>
      <!-- NOTE removed d-flex -->
      <div v-if="vaults" class="col-12 fs-1">
        <!-- NOTE removed v-if keeps -->
        Vaults
        <section class="row">
          <div class="col-6 col-md-2 mb-3" v-for="vault in vaults" :key="vault.id">
            <VaultCard :vaultProp="vault" />
          </div>
        </section>
      </div>
      <p class="fs-1">
        Keeps
      </p>
      <div class="col-12 d-flex">
        <section v-if="keeps" class="keep-Section masonry-with-columns">
          <div class=" mb-3" v-for="keep in keeps" :key="keep.id">
            <KeepCard :keepProp="keep" />
          </div>
        </section>
      </div>
    </section>
  </div>
</template>


<script>
import { computed, onUnmounted, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { useRoute } from "vue-router";
import Pop from "../utils/Pop.js";
import { accountService } from "../services/AccountService.js";
import { vaultsService } from "../services/VaultsService.js"
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
    onUnmounted(() => {
      AppState.vaults = []
      AppState.keeps = []
      AppState.activeProfile = {}
    })
    return {
      AppState: computed(() => AppState),
      activeProfile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
      coverImg: computed(() => `url("${AppState.activeProfile.coverImg}")`),

    };
  },
  components: { KeepCard, VaultCard }
}
</script>


<style lang="scss" scoped>
.cover-img {
  height: 25vh;
  background-position: center;
  background-size: cover;
  background-image: v-bind(coverImg);
}
</style>