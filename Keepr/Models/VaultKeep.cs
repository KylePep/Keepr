using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keepr.Models;

public class VaultKeep : RepoItem<int>
{
  public string CreatorId { get; set; }
  public int VaultId { get; set; }
  public int KeepId { get; set; }
}

public class KeepVaultKeep : Keep
{
  public int vaultKeepId { get; set; }
}