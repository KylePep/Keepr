<template>
  <section class=" masonry-with-columns  mx-3 pt-3">
    <div class=" mb-3" v-for="keep in keeps" :key="keep.id">
      <KeepCard :keepProp="keep" />
    </div>
  </section>
</template>

<script>
import { computed, onMounted } from "vue";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js"
import { AppState } from "../AppState.js";
import KeepCard from "../components/KeepCard.vue";
import { accountService } from "../services/AccountService.js";

export default {
  setup() {
    async function getKeeps() {
      try {
        const keeps = await keepsService.getKeeps();
      }
      catch (error) {
        Pop.error(error.message, '[ERROR OCCURED - getKeeps]');
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
    onMounted(() => {
      getKeeps();
      getAccountVaults();
    });
    return {
      keeps: computed(() => AppState.keeps),
      AppState: computed(() => AppState),

    };
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss">
.masonry-with-columns {
  columns: 4 200px;
  column-gap: 1rem;
}
</style>
