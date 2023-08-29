import { AppState } from "../AppState.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {
  async createVaultKeep(vaultKeepData) {
    await api.post('api/vaultkeeps', vaultKeepData)
    Pop.success(`Keep has been added to your vault`)
    AppState.activeKeep.kept++
  }
  async removeVaultKeep(vaultKeepId) {
    await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    const vaultKeepIndex = AppState.keeps.findIndex(vk => vk.vaultKeepId == vaultKeepId)
    AppState.keeps.splice(vaultKeepIndex, 1)
  }
}
export const vaultKeepsService = new VaultKeepsService()