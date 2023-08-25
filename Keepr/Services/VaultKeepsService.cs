using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keepr.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepsRepository;

  public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository)
  {
    _vaultKeepsRepository = vaultKeepsRepository;
  }

  internal VaultKeep createVaultKeep(VaultKeep vaultKeepData)
  {
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

    return vaultKeep;
  }

  internal void removeVaultKeep(int vaultKeepId, string userId)
  {
    VaultKeep vaultKeep = getVaultKeepById(vaultKeepId, userId);
    if (vaultKeep.CreatorId != userId)
    {
      throw new Exception($"Bad vaultKeep Id: {vaultKeepId}");
    }
    _vaultKeepsRepository.removeVaultKeep(vaultKeepId);
  }
}
