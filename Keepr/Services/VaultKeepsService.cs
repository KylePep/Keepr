using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepsRepository;
  private readonly VaultsService _vaultsService;
  private readonly KeepsService _keepsService;
  private readonly KeepsRepository _keepsRepository;

  public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService, KeepsService keepsService, KeepsRepository keepsRepository)
  {
    _vaultKeepsRepository = vaultKeepsRepository;
    _vaultsService = vaultsService;
    _keepsService = keepsService;
    _keepsRepository = keepsRepository;
  }

  internal VaultKeep createVaultKeep(VaultKeep vaultKeepData)
  {
    Vault vault = _vaultsService.getVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
    if (vault.CreatorId != vaultKeepData.CreatorId)
    {
      throw new Exception($"Bad request");
    }
    Keep keep = _keepsService.getKeepById(vaultKeepData.KeepId);
    keep.Kept++;
    _keepsRepository.editKeep(keep);
    VaultKeep vaultKeep = _vaultKeepsRepository.createVaultKeep(vaultKeepData);
    return vaultKeep;
  }



  internal VaultKeep getVaultKeepById(int vaultKeepId, string userId)
  {
    VaultKeep vaultKeep = _vaultKeepsRepository.getVaultKeepById(vaultKeepId, userId);
    if (vaultKeep == null)
    {
      throw new Exception($"Bad vaultKeep Id: {vaultKeepId}");
    }
    if (vaultKeep.CreatorId != userId)
    {
      throw new Exception($"Bad vaultKeep Id: {vaultKeepId}");
    }

    return vaultKeep;
  }

  internal void removeVaultKeep(int vaultKeepId, string userId)
  {

    VaultKeep vaultKeep = getVaultKeepById(vaultKeepId, userId);
    if (vaultKeep.CreatorId != userId)
    {
      throw new Exception($"Bad vaultKeep Id: {vaultKeepId}");
    }
    Keep keep = _keepsService.getKeepById(vaultKeep.KeepId);
    keep.Kept--;
    _keepsRepository.editKeep(keep);
    _vaultKeepsRepository.removeVaultKeep(vaultKeepId);
  }
}
