<template>
  <header class="container-fluid p-0">
    <Navbar />
  </header>
  <main class="container-fluid">
    <router-view />
  </main>
  <!-- <footer class="bg-dark text-light">
    Made with 💖 by CodeWorks
  </footer> -->
  <newKeepModal />
  <newVaultModal />
  <KeepModal />
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import KeepModal from "./components/KeepModal.vue"

export default {
  setup() {
    function onScreenResize() {
      window.addEventListener("resize", () => {
        updateScreenWidth();
      });
    }
    function updateScreenWidth() {
      AppState.screenWidth = window.innerWidth;
      // logger.log(AppState.screenWidth, 'Top')
    }
    onMounted(() => {
      updateScreenWidth()
      onScreenResize()
    })
    return {
      appState: computed(() => AppState),
      dropMenu: computed(() => {
        // logger.log(AppState.screenWidth, 'Bottom')
        if (AppState.screenWidth < 768) {
          return 'dropup'
        } else return 'dropdown'
      }),
    }
  },
  components: { Navbar, KeepModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}

.avatar {
  height: 50px;
  width: 50px;
  background-size: cover;
  background-position: center;
  border-radius: 50%;
}

.masonry-with-columns {
  columns: 2 100px;
  column-gap: 1rem;
}


.keep-Section {
  padding-bottom: 102px;
}

.profile-avatar {
  position: absolute;
  bottom: -25%;
  left: 24.5%;
  border-radius: 50%;
  object-fit: cover;
  object-position: center;
  height: 20vh;
  min-width: 20vh;
  max-width: 20vh;
}

@media screen and (min-width: 768px) {
  .masonry-with-columns {
    columns: 4 200px;
    column-gap: 1rem;
  }

  .profile-avatar {
    position: absolute;
    bottom: -25%;
    left: 43.5%;
    border-radius: 50%;
    object-fit: cover;
    object-position: center;
    height: 20vh;
    min-width: 20vh;
    max-width: 20vh;
  }
}
</style>
