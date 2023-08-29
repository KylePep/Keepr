import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {
  async createVaultKeep(vaultKeepData) {
    await api.post('api/vaultkeeps', vaultKeepData)
    Pop.success(`Keep has been added to your vault`)
  }
  async removeVaultKeep() { }
}
export const vaultKeepsService = new VaultKeepsService()