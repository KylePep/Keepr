<template>
  <div class="card-bg position-relative rounded">
    <div>
      <div type="button" data-bs-toggle="modal" data-bs-target="#keepModal">
        <img class="img-fluid img-reserve rounded" :src="keepProp.img" :alt="keepProp.name" :title="keepProp.name">
      </div>
      <div class="keep-bar d-flex flex-grow-1 justify-content-between align-items-center px-3 py-1">
        <p class="fs-2">
          {{ keepProp.name }}
        </p>
        <router-link :to="{ name: 'About' }">
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

export default {
  props: {
    keepProp: { type: Keep, required: true }
  },
  setup(props) {
    return {
      keepBg: computed(() => `url("${props.keepProp.img}")`)
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
}

.avatar {
  height: 6vh;
  width: 6vh;
  background-size: cover;
  background-position: center;
  border-radius: 50%;
}
</style>