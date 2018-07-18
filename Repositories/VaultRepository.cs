using System.Collections.Generic;
using System.Data;
using keepr_angular.Repositories;
using Dapper;
using keepr_angular.Models;

namespace keepr_angular.Repository
{
  public class VaultRepository : DbContext
  {
    public VaultRepository(IDbConnection db) : base(db)
    {
    }

    public Vault CreateVault(Vault vault)
    {
      int id = _db.ExecuteScalar<int>(@"
                INSERT INTO vaults (title, authorId)
                VALUES (@Title, @AuthorId);
                SELECT LAST_INSERT_ID();
            ", vault);
      vault.Id = id;
      return vault;
    }

    public IEnumerable<Vault> GetAll()
    {
      return _db.Query<Vault>("SELECT * FROM vaults WHERE public = true;");
    }

    public Vault GetById(int id)
    {
      return _db.QueryFirstOrDefault<Vault>("SELECT * FROM vaults WHERE id = @id;", new { id });
    }

    public IEnumerable<Vault> GetByAuthorId(string id)
    {
      return _db.Query<Vault>("SELECT * FROM vaults WHERE authorId = @id", new { id });
    }

    public Vault EditVault(int id, Vault edit)
    {
      edit.Id = id;
      var i = _db.Execute(@"
                UPDATE vaults SET
                    title = @Title
                WHERE id = @Id
                AND authorId = @AuthorId;
            ", edit);
      if (i > 0)
      {
        return edit;
      }
      return null;
    }

    public bool DeleteVault(int id, string authorId)
    {
      var i = _db.Execute(@"
      DELETE FROM vaults
      WHERE id = @id
      AND authorId = @authorId
      LIMIT 1;
      ", new { id, authorId });
      if (i > 0)
      {
        return true;
      }
      return false;
    }
  }
}