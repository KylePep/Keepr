<template>
  <div class="keep-card position-relative rounded">
    <div>
      <div @click="getActiveKeepById(), setActiveProfile()" type="button" data-bs-toggle="modal"
        data-bs-target="#keepModal">
        <img class="img-fluid rounded" :src="keepProp.img" :alt="keepProp.name" :title="keepProp.name">
      </div>
      <div class="keep-bar d-flex flex-grow-1 justify-content-between align-items-end px-3 py-md-1">
        <p class="fs-2 fw-bold">
          {{ keepProp.name }}
        </p>
        <router-link @click="setActiveProfile()" class="d-none d-md-block"
          :to="{ name: 'Profile', params: { profileId: keepProp.creator.id } }">
          <img class="img-fluid avatar mb-3" :src="keepProp.creator.picture" :alt="keepProp.creator.name"
            :title="keepProp.creator.name">
        </router-link>
      </div>
      <div @click="removeKeep()" v-if="AppState.account?.id == keepProp.creator.id"
        class="mdi mdi-close-circle text-danger fs-2 delete-button" title="Delete Keep">
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
      AppState: computed(() => AppState),
      async getActiveKeepById() {
        try {
          AppState.activeKeep = props.keepProp
          await keepsService.getActiveKeepById(props.keepProp.id)
          AppState.activeKeep.vaultKeepId = props.keepProp.vaultKeepId
        } catch (error) {
          Pop.error(error.message, '[ERROR - getActiveKeepById]')
        }
      },
      async removeKeep() {
        try {
          const removeConfrim = await Pop.confirm(`Delete this keep? ${props.keepProp.name}, This can not be reveresed`)
          if (!removeConfrim) {
            return
          }
          await keepsService.removeKeep(props.keepProp.id)
        } catch (error) {
          Pop.error(error.message, '[ERROR- removeKeep]')
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

.keep-card {
  min-height: 15vh;
}

.keep-card:hover .delete-button {
  display: block;
}

.delete-button {
  padding-right: .2em;
  position: absolute;
  top: 0%;
  right: 0%;
  display: block;
}

.delete-button:hover {
  cursor: pointer;
  scale: 1.1;
}

@media screen and (min-width: 768px) {
  .delete-button {
    padding-right: .2em;
    position: absolute;
    top: 0%;
    right: 0%;
    display: none;
  }

  .keep-card {
    min-height: 15vh;
  }
}
</style>