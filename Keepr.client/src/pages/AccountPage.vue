<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12 mb-3">
        <section class="row d-flex justify-content-center">
          <div class="col-9 cover-img position-relative m-3 rounded">
            <img class="profile-avatar" :src="activeProfile.picture" :alt="activeProfile.name">
          </div>
          <div class="ms-3 fs-4 col-9 d-flex justify-content-end" type="button" data-bs-toggle="modal"
            data-bs-target="#accountFormModal">
            <i class="mdi mdi-dots-horizontal p-0  selectable"></i>
          </div>
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
  <AccountForm />
</template>

<script>
import { computed, onMounted, onUnmounted, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { useRoute } from "vue-router";
import { accountService } from "../services/AccountService.js";
import Pop from "../utils/Pop.js";
import KeepCard from "../components/KeepCard.vue";
import VaultCard from "../components/VaultCard.vue";
import AccountForm from "../components/AccountForm.vue";
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
      coverImg: computed(() => `url("${AppState.account.coverImg}")`),
    };
  },
  components: { KeepCard, VaultCard, AccountForm }
}
</script>

<style scoped>
img {
  max-width: 100px;
}

.cover-img {
  height: 25vh;
  background-position: center;
  background-size: cover;
  background-image: v-bind(coverImg);
}
</style>
