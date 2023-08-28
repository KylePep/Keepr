import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {
  async getVaultById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`)
    logger.log(res.data)
    const vault = new Vault(res.data)
    AppState.activeVault = vault
  }
  async createVault(vaultData) {
    const res = await api.post(`api/vaults`, vaultData)
    const vault = new Vault(res.data)
    AppState.vaults.unshift(vault)
  }
}
export const vaultsService = new VaultsService()