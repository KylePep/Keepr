<template>
  <div class="row nav-div navbar bg-light px-3 justify-content-around elevation-1">
    <div class="col-4 d-flex">
      <router-link class="order-4 order-md-1 d-none d-md-block text-dark d-flex fs-4 " :to="{ name: 'Home' }">
        <div>
          Home
        </div>
      </router-link>

      <div :class="dropMenu" class=" order-2 order-md-2 ">
        <div v-if="Appstate.account.id" class=" dropdown-toggle  fs-4 ms-4" href="#" role="button"
          data-bs-toggle="dropdown" data-display="static" aria-expanded="false">
          Create
        </div>
        <ul class="dropdown-menu ">
          <li>
            <div class="ms-3 fs-4" type="button" data-bs-toggle="modal" data-bs-target="#newKeepModal">
              New Keep
            </div>
          </li>
          <li>
            <div class="ms-3 fs-4" type="button" data-bs-toggle="modal" data-bs-target="#newVaultModal">
              New Vault
            </div>
          </li>
        </ul>
      </div>
    </div>
    <!-- <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button> -->

    <!-- <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link> -->

    <router-link class="order-1 order-md-3 col-4 d-flex justify-content-center fs-1 text-dark d-flex fs-4"
      :to="{ name: 'Home' }">
      <div class="">Keepr </div>
    </router-link>

    <!-- LOGIN COMPONENT HERE -->
    <div class="order-3 order-md-4 col-4 d-flex justify-content-end">
      <Login />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, ref } from "vue";
import Login from './Login.vue';
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
export default {
  setup() {

    // function onScreenResize() {
    //   window.addEventListener("resize", () => {
    //     updateScreenWidth();
    //   });
    // }
    // function updateScreenWidth() {
    //   AppState.screenWidth = window.innerWidth;
    //   // logger.log(AppState.screenWidth, 'Top')
    // }
    // onMounted(() => {
    //   updateScreenWidth()
    //   onScreenResize()
    // })
    return {
      dropMenu: computed(() => {
        // logger.log(AppState.screenWidth, 'Bottom')
        if (AppState.screenWidth < 768) {
          return 'dropup'
        } else return 'dropdown'
      }),
      Appstate: computed(() => AppState),

    }
  },

  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.nav-div {
  position: fixed;
  bottom: 0%;
  z-index: 100;
  width: 100vw;
}


@media screen and (min-width: 768px) {
  .nav-div {
    height: 102px;
    position: relative;
  }
}
</style>
