using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keepr.Repositories;

public class VaultsRepository
{
  private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }
  internal int createVault(Vault vaultData)
  {
    string sql = @"
      INSERT INTO vaults (name, description, img, creatorId, isPrivate)
      VALUES (@Name, @Description, @Img, @CreatorId, @isPrivate);
      SELECT LAST_INSERT_ID()
      ;";

    int vaultId = _db.ExecuteScalar<int>(sql, vaultData);
    return vaultId;
  }

  internal List<Vault> getAUsersVaults(string profileId)
  {
    string sql = @"
      SELECT 
      vt.*,
      acc.*
      FROM vaults vt
      JOIN accounts acc ON acc.id = vt.creatorId
      WHERE vt.creatorId = @profileId AND
      vt.isPrivate = false;
      ;";

    List<Vault> vault = _db.Query<Vault, Profile, Vault>(
      sql,
      (vault, profile) =>
      {
        vault.Creator = profile;
        return vault;
      }, new { profileId }).ToList();
    return vault;
  }

  internal List<Vault> getMyVaults(string userId)
  {
    string sql = @"
      SELECT 
      vt.*,
      acc.*
      FROM vaults vt
      JOIN accounts acc ON acc.id = vt.creatorId
      WHERE vt.creatorId = @userId
      ;";

    List<Vault> vault = _db.Query<Vault, Profile, Vault>(
      sql,
      (vault, profile) =>
      {
        vault.Creator = profile;
        return vault;
      }, new { userId }).ToList();
    return vault;
  }

  internal Vault getVaultById(int vaultId)
  {
    string sql = @"
      SELECT 
      vt.*,
      acc.*
      FROM vaults vt
      JOIN accounts acc ON acc.id = vt.creatorId
      WHERE vt.id = @vaultId 
      ;";

    Vault vault = _db.Query<Vault, Profile, Vault>(
      sql,
      (vault, profile) =>
      {
        vault.Creator = profile;
        return vault;
      }, new { vaultId }).LastOrDefault();
    return vault;
  }

  internal void removeVault(int vaultId)
  {
    string sql = @"DELETE FROM vaults Where id = @vaultId;";
    _db.Execute(sql, new { vaultId });
  }

  internal void updateVault(Vault orignalVault)
  {
    string sql = @"
      UPDATE vaults
      SET
      description = @Description,
      img = @Img,
      name = @Name,
      isPrivate = @IsPrivate
      WHERE id = @Id;
      SELECT LAST_INSERT_ID()
      ;";

    _db.ExecuteScalar<int>(sql, orignalVault);
  }
}
