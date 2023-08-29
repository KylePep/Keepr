<template>
  <div class="col-12">
    <section class=" keep-Section masonry-with-columns  px-1 pt-3">
      <div class=" mb-3" v-for="keep in keeps" :key="keep.id">
        <KeepCard :keepProp="keep" />
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted, onUnmounted, watchEffect } from "vue";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js"
import { AppState } from "../AppState.js";
import KeepCard from "../components/KeepCard.vue";
import { accountService } from "../services/AccountService.js";


export default {
  setup() {
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        Pop.error(error.message, '[ERROR OCCURED - getKeeps]');
      }
    }
    async function getAccountVaults() {
      try {
        if (AppState.account.id) {
          await accountService.getAccountVaults();
        }
      }
      catch (error) {
        Pop.error(error.message, '[ERROR OCCURED- getVaultsByProfileId]');
      }
    }
    onMounted(() => {
      getKeeps();
      getAccountVaults();
    });
    watchEffect(() => {
      AppState.account.id
      getAccountVaults();
    })
    onUnmounted(() => {
      AppState.vaults = []
      AppState.keeps = []
    })
    return {
      keeps: computed(() => AppState.keeps),
      AppState: computed(() => AppState),

    };
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss"></style>
