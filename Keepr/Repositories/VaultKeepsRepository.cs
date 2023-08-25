using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keepr.Repositories;

public class VaultKeepsRepository
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal VaultKeep createVaultKeep(VaultKeep vaultKeepData)
  {
    string sql = @"
      INSERT INTO vaultKeeps 
      (creatorId, vaultId, keepId)
      VALUES
      (@creatorId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID()
      ;";

    int vaultId = _db.ExecuteScalar<int>(sql, vaultKeepData);
    vaultKeepData.Id = vaultId;
    return vaultKeepData;
  }

  internal List<KeepVaultKeep> getKeepsByVaultId(int vaultId)
  {
    string sql = @"
      SELECT
      vk.*,
      kp.*,
      acc.*
      FROM vaultKeeps vk
      JOIN keeps kp ON kp.id = vk.keepId
      JOIN accounts acc ON acc.id = kp.creatorId
      WHERE vk.vaultId = @vaultId
      ;";

    List<KeepVaultKeep> keeps = _db.Query<VaultKeep, KeepVaultKeep, Profile, KeepVaultKeep>(
      sql,
      (vaultKeep, keep, profile) =>
      {
        keep.vaultKeepId = vaultKeep.Id;
        keep.Creator = profile;
        return keep;
      },
      new { vaultId }
    ).ToList();

    return keeps;
  }

  internal VaultKeep getVaultKeepById(int vaultKeepId, string userId)
  {
    string sql = @"
      SELECT
      *
      FROM vaultKeeps
      WHERE id = @vaultKeepId
      ;";

    VaultKeep vaultKeep = _db.QueryFirstOrDefault<VaultKeep>(sql, new { vaultKeepId });
    return vaultKeep;
  }

  internal void removeVaultKeep(int vaultKeepId)
  {
    string sql = @"
      DELETE FROM vaultKeeps WHERE id = @vaultKeepId
      ;";
    _db.ExecuteScalar(sql, new { vaultKeepId });
  }
}
