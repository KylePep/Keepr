<template>
  <span class="navbar-text">
    <button class="btn selectable text-dark lighten-30 text-uppercase my-2 my-lg-0" @click="login"
      v-if="!user.isAuthenticated">
      Login
    </button>
    <div v-else>
      <div :class="dropMenu" class=" my-2 my-lg-0">
        <div type="button" role="button" title="Account" class="bg-light border-0 selectable no-select"
          data-bs-toggle="dropdown" aria-expanded="false">
          <div v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" alt="account photo" height="40" class="avatar " />
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-lg-end dropdown-menu-end p-0" aria-labelledby="authDropdown">
          <div class="list-group">
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Manage Account
              </div>
            </router-link>
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </div>
    </div>
  </span>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      },
      dropMenu: computed(() => {
        // logger.log(AppState.screenWidth, 'Bottom')
        if (AppState.screenWidth < 768) {
          return 'dropup'
        } else return 'dropdown'
      }),
    }
  }
}
</script>

<style lang="scss" scoped>
.avatar {
  height: 50px;
  width: 50px;
  background-size: cover;
  background-position: center;
  border-radius: 50%;
}
</style>
