<template>
  <router-link @click="setActiveVault(), emptyKeeps()" :to="{ name: 'Vault', params: { vaultId: vaultProp.id } }">
    <div class="card-bg position-relative rounded d-flex align-items-end">
      <div class="keep-bar d-flex flex-grow-1  px-3 py-1">
        <p class="fs-2">
          {{ vaultProp.name }}
        </p>
      </div>
    </div>
  </router-link>
</template>


<script>
import { computed } from "vue";
import { Vault } from "../models/Vault.js";
import { AppState } from "../AppState.js";

export default {
  props: {
    vaultProp: { type: Vault, required: true }
  },
  setup(props) {
    return {
      keepBg: computed(() => `url("${props.vaultProp.img}")`),
      setActiveVault() {
        AppState.activeVault = props.vaultProp
      },
      emptyKeeps() {
        AppState.keeps = []
      }

    }
  }
}
</script>


<style lang="scss" scoped>
.img-reserve {
  visibility: hidden;
}

.keep-bar {
  color: white;
  // position: absolute;
  background-image: linear-gradient(rgba(255, 255, 255, 0), rgba(0, 0, 0, 0.767));
  border-radius: 7px;
  // bottom: 0%;
  // left: 0%;

}

.card-bg {
  background-image: v-bind(keepBg);
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  height: fit-content;
  height: 25vh;
}
</style>