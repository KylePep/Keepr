<template>
  <div class=" position-relative rounded">
    <div>
      <div @click="getActiveKeepById(), setActiveProfile()" type="button" data-bs-toggle="modal"
        data-bs-target="#keepModal">
        <img class="img-fluid rounded" :src="keepProp.img" :alt="keepProp.name" :title="keepProp.name">
      </div>
      <div class="keep-bar d-flex flex-grow-1 justify-content-between align-items-center px-3 py-1">
        <p class="fs-2">
          {{ keepProp.name }}
        </p>
        <router-link @click="setActiveProfile()" :to="{ name: 'Profile', params: { profileId: keepProp.creator.id } }">
          <img class="img-fluid avatar " :src="keepProp.creator.picture" :alt="keepProp.creator.name"
            :title="keepProp.creator.name">
        </router-link>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "vue";
import { Keep } from "../models/Keep.js";
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";

export default {
  props: {
    keepProp: { type: Keep, required: true }
  },
  setup(props) {
    return {
      keepBg: computed(() => `url("${props.keepProp.img}")`),
      setActiveProfile() {
        AppState.activeProfile = props.keepProp.creator
      },
      async getActiveKeepById() {
        try {
          await keepsService.getActiveKeepById(props.keepProp.id)
        } catch (error) {
          Pop.error(error.message, '[ERROR - getActiveKeepById]')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
img {
  object-fit: cover;
  object-position: center;
  max-height: 60vh;
  width: 100%;
}

.keep-bar {
  color: white;
  position: absolute;
  background-image: linear-gradient(rgba(255, 255, 255, 0), rgba(0, 0, 0, 0.846));
  border-radius: 7px;
  bottom: 0%;
  right: 0%;
  left: 0%;
}

// .card-bg {
//   background-image: v-bind(keepBg);
//   background-position: center;
//   background-repeat: no-repeat;
//   background-size: cover;
//   height: fit-content;
// }
</style>