<template>
  <section class="row  mx-3">
    <div class="col-3  mb-3" v-for="keep in keeps" :key="keep.id">
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
    onMounted(() => {
      getKeeps();
    });
    return {
      keeps: computed(() => AppState.keeps)
    };
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss">
// .home {
//   display: grid;
//   height: 80vh;
//   place-content: center;
//   text-align: center;
//   user-select: none;

//   .home-card {
//     width: 50vw;

//     >img {
//       height: 200px;
//       max-width: 200px;
//       width: 100%;
//       object-fit: contain;
//       object-position: center;
//     }
//   }
// }
</style>
