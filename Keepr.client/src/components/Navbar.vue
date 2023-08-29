<template>
  <nav class=" row navbar bg-light px-3 justify-content-around elevation-1">
    <div class="col-4 d-flex">

      <router-link class="text-dark d-flex fs-4" :to="{ name: 'Home' }">
        <div>
          Home
        </div>
      </router-link>

      <div class=" dropdown-toggle fs-4 ms-4" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
        Create
      </div>
      <ul class="dropdown-menu">
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
    <!-- <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button> -->

    <!-- <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link> -->


    <div class="col-4 d-flex justify-content-center fs-1">Keepr </div>
    <!-- LOGIN COMPONENT HERE -->
    <div class="col-4 d-flex justify-content-end">
      <Login />
    </div>
  </nav>
</template>

<script>
import { ref } from "vue";
import Login from './Login.vue';
export default {
  setup() {
    return {

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

@media screen and (min-width: 768px) {
  nav {
    height: 102px;
  }
}
</style>
