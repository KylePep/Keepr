<template>
  <section class=" masonry-with-columns  mx-3 pt-3">
    <div class=" mb-3" v-for="keep in keeps" :key="keep.id">
      <KeepCard :keepProp="keep" />
    </div>
  </section>
  <KeepModal />
</template>

<script>
import { computed, onMounted, watchEffect } from "vue";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js"
import { AppState } from "../AppState.js";
import KeepCard from "../components/KeepCard.vue";
import { accountService } from "../services/AccountService.js";
import KeepModal from "../components/KeepModal.vue";


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
    return {
      keeps: computed(() => AppState.keeps),
      AppState: computed(() => AppState),

    };
  },
  components: { KeepCard, KeepModal }
}
</script>

<style scoped lang="scss">
.masonry-with-columns {
  columns: 4 200px;
  column-gap: 1rem;
}
</style>
