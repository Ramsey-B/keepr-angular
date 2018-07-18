using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr_angular.Models;
using keepr_angular.Repositories;

namespace keepr_angular.Repository
{
  public class ShareRepository : DbContext
  {
    public ShareRepository(IDbConnection db) : base(db)
    {

    }

    public Keep GetById(int id)
    {
      var i = _db.Execute(@"
                UPDATE keeps SET
                  views = views + 1
                WHERE id = @id;
            ", new { id });
      if (i < 1)
      {
        return null;
      }
      return _db.QueryFirstOrDefault<Keep>(@"SELECT * FROM keeps
              WHERE (keeps.id = @id);", new { id });
    }

    public IEnumerable<Keep> GetByAuthorId(string id)
    {
      return _db.Query<Keep>(@"SELECT * FROM shares
              INNER JOIN keeps ON keeps.id = shares.keepId 
              WHERE shares.authorId = @id;
              ", new { id });
    }

    public IEnumerable<Keep> GetByVaultId(int id)
    {
      return _db.Query<Keep>(@"SELECT * FROM shares
              INNER JOIN keeps ON keeps.id = shares.keepId 
              WHERE shares.vaultId = @id;
              ", new { id });
    }

    public string ShareKeep(Share newKeep, int keepId)
    {
      var i = _db.Execute(@"
                UPDATE keeps SET
                    keeps = keeps + 1
                WHERE id = @keepId;
            ", new { keepId });
      if (i > 0)
      {
        int id = _db.ExecuteScalar<int>(@"
                INSERT INTO shares (keepId, authorId, vaultId)
                VALUES (@KeepId, @AuthorId, @VaultId);
                SELECT LAST_INSERT_ID();
            ", newKeep);
        return "Successfully Added!";
      }
      return "Failed To Add!";
    }

    public bool DeleteShare(int vaultId, int keepId, string authorId)
    {
      var i = _db.Execute(@"
      DELETE FROM shares
      WHERE keepId = @keepId
      AND authorId = @authorId
      AND vaultId = @vaultId
      LIMIT 1;
      ", new { vaultId, keepId, authorId });
      ;
      if (i > 0)
      {
        var num = _db.Execute(@"
                UPDATE keeps SET
                    keeps = keeps - 1
                WHERE id = @keepId;
            ", new { keepId });
        return num > 0;
      }
      return false;
    }
  }
}