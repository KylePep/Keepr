
namespace Keepr.Repositories;

public class KeepsRepository
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int createKeep(Keep keepData)
  {
    string sql = @"
      INSERT INTO keeps (name, description, img, creatorId)
      VALUES (@Name, @Description, @Img, @CreatorId);
      SELECT LAST_INSERT_ID()
      ;";

    int keepId = _db.ExecuteScalar<int>(sql, keepData);
    return keepId;
  }

  internal void editKeep(Keep originalKeep)
  {
    string sql = @"
      UPDATE keeps
      SET 
      name = @Name,
      description = @Description,
      img = @Img,
      views = @Views
      WHERE id = @Id
      ;";

    _db.Execute(sql, originalKeep);
  }

  internal Keep getKeepById(int keepId)
  {
    string sql = @"
      SELECT
      kp.*,
      acc.*
      FROM keeps kp
      JOIN accounts acc ON acc.id = kp.creatorId
      WHERE kp.id = @keepId
      ;";

    Keep keep = _db.Query<Keep, Profile, Keep>(
      sql,
      (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new { keepId }
    ).LastOrDefault();

    return keep;
  }


  internal List<Keep> getKeeps()
  {
    string sql = @"
      SELECT
      kp.*,
      acc.*
      FROM keeps kp
      JOIN accounts acc On acc.id = kp.creatorId
      ORDER BY kp.createdAt DESC
      ;";

    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(
      sql,
      (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }).ToList();
    return keeps;
  }

  internal void removeKeep(int keepId)
  {
    string sql = @"DELETE FROM keeps WHERE id = @keepId;";
    _db.Execute(sql, new { keepId });
  }
}
