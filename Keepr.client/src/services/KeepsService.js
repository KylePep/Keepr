import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {
  async getKeeps() {
    const res = await api.get('api/keeps')
    logger.log(res.data)
    const keeps = res.data.map(k => new Keep(k))
    AppState.keeps = keeps
  }

  async getKeepsByVaultId(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log(res.data)
    const keeps = res.data.map(k => new Keep(k))
    AppState.keeps = keeps
  }

  async getActiveKeepById(keepId) {
    const res = await api.get(`api/keeps/${keepId}`)
    const keep = new Keep(res.data)
    AppState.activeKeep = keep
  }
}
export const keepsService = new KeepsService()