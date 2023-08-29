import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Keep } from "../models/Keep.js"
import { Vault } from "../models/Vault.js"
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async editAccount(accountData) {
    const res = await api.put(`/account`, accountData)
    const account = new Account(res.data)
    AppState.account = account
  }
  async getProfileById(profileId) {
    const res = await api.get(`api/profiles/${profileId}`)
    AppState.activeProfile = new Account(res.data)
  }
  async getVaultsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    const vaults = res.data.map(v => new Vault(v))
    AppState.vaults = vaults
  }
  async getKeepsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    const keeps = res.data.map(v => new Keep(v))
    AppState.keeps = keeps
  }
  async getAccountVaults() {
    const res = await api.get(`/account/vaults`)
    const vaults = res.data.map(v => new Vault(v))
    AppState.vaults = vaults
  }
}

export const accountService = new AccountService()
