import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {
  async getVaultById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`)
    const vault = new Vault(res.data)
    AppState.activeVault = vault
  }
  async createVault(vaultData) {
    const res = await api.post(`api/vaults`, vaultData)
    const vault = new Vault(res.data)
    AppState.vaults.unshift(vault)
  }
  async editVault(vaultData) {
    const res = await api.put(`api/vaults/${vaultData.id}`, vaultData)
    const vault = new Vault(res.data)
    AppState.activeVault = vault
  }
  async changePrivacy(vaultData) {
    await api.put(`api/vaults/${vaultData.id}`, vaultData)
    AppState.activeVault.isPrivate = vaultData.isPrivate
  }
  async removeVault(vaultId) {
    await api.delete(`api/vaults/${vaultId}`)
    const vaultIndex = AppState.vaults.findIndex(v => v.id == vaultId)
    AppState.vaults.splice(vaultIndex, 1)
  }
}
export const vaultsService = new VaultsService()