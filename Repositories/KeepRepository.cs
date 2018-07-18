using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr_angular.Models;
using keepr_angular.Repositories;
using MySql.Data.MySqlClient;

namespace keepr_angular.Repository
{
  public class KeepRepository : DbContext
  {
    public KeepRepository(IDbConnection db) : base(db)
    {
    }

    public IEnumerable<Keep> GetAll()
    {
      return _db.Query<Keep>("SELECT * FROM keeps WHERE public = true;");
    }

    public Keep GetById(int id) 
    {
      return _db.ExecuteScalar<Keep>("SELECT * WHERE id = @id", new { id });
    }

    public IEnumerable<Keep> GetByAuthorId(string authorId) 
    {
      return _db.Query<Keep>("SELECT * WHERE authorId = @authorId", new { authorId });
    }

    public Keep CreateKeep(Keep newKeep)
    {
      int id = _db.ExecuteScalar<int>(@"
                INSERT INTO keeps (description, img, author, vaultId, views, public, keeps)
                VALUES (@Description, @Img, @Author, @VaultId, @Views, @Public, @Keeps);
                SELECT LAST_INSERT_ID();
            ", newKeep);
      return newKeep;
    }

    public Keep EditKeep(int id, Keep editKeep, string user)
    {
      editKeep.Id = id;
      editKeep.AuthorId = user;
      var i = _db.Execute(@"
                UPDATE keeps SET
                  description = @Description,
                  img = @Img,
                  public = @Public
                WHERE id = @Id
                AND authorId = @AuthorId;
            ", editKeep);
      if (i > 0)
      {
        return editKeep;
      }
      return null;
    }

    public bool DeleteKeep(int id, string authorId)
    {
      var i = _db.Execute(@"
      DELETE FROM keeps
      WHERE id = @id
      AND keeps = 1
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