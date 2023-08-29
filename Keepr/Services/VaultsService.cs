using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keepr.Services;

public class VaultsService
{
  private readonly VaultsRepository _vaultRepository;

  public VaultsService(VaultsRepository vaultRepository)
  {
    _vaultRepository = vaultRepository;
  }

  internal Vault createVault(Vault vaultData)
  {
    int vaultId = _vaultRepository.createVault(vaultData);
    Vault vault = getVaultById(vaultId, vaultData.CreatorId);
    return vault;
  }

  internal List<Vault> getAUsersVaults(string profileId)
  {
    List<Vault> vaults = _vaultRepository.getAUsersVaults(profileId);
    return vaults;
  }

  internal List<Vault> getMyVaults(string userId)
  {
    List<Vault> vaults = _vaultRepository.getMyVaults(userId);
    return vaults;
  }

  internal Vault getVaultById(int vaultId, string userId = null)
  {
    Vault vault = _vaultRepository.getVaultById(vaultId);
    if (vault == null)
    {
      throw new Exception($"Bad Vault Id: {vaultId}");
    }
    if (vault.isPrivate == true && vault.CreatorId != userId)
    {
      throw new Exception($"Bad vault id: {vaultId}, ⚠️");
    }
    return vault;
  }

  internal void removeVault(int vaultId, string userId)
  {
    Vault vault = getVaultById(vaultId, userId);
    if (vault.CreatorId != userId)
    {
      throw new Exception($"Bad Request");
    }
    _vaultRepository.removeVault(vaultId);
  }

  internal Vault updateVault(Vault vaultData, string userId)
  {
    Vault orignalVault = getVaultById(vaultData.Id, userId);
    if (orignalVault.CreatorId != userId)
    {
      throw new Exception($"Bad vault Id; {vaultData.Id}");
    }
    orignalVault.Description = vaultData.Description ?? orignalVault.Description;
    orignalVault.Img = vaultData.Img ?? orignalVault.Img;
    orignalVault.Name = vaultData.Name ?? orignalVault.Name;
    orignalVault.isPrivate = vaultData.isPrivate ?? orignalVault.isPrivate;

    _vaultRepository.updateVault(orignalVault);
    return orignalVault;
  }
}
