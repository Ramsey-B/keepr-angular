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
      return _db.Query<Keep>("SELECT * FROM keeps WHERE publicPrivate = true;");
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

    public IEnumerable<Keep> GetByAuthorId(string authorId)
    {
      return _db.Query<Keep>("SELECT * FROM keeps WHERE authorId = @authorId", new { authorId });
    }

    public Keep CreateKeep(Keep newKeep)
    {
      int id = _db.ExecuteScalar<int>(@"
                INSERT INTO keeps (description, img, author, authorId, views, publicPrivate, keeps)
                VALUES (@Description, @Img, @Author, @AuthorId, @Views, @PublicPrivate, @Keeps);
                SELECT LAST_INSERT_ID();
            ", newKeep);
      newKeep.Id = id;
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
                  publicPrivate = @PublicPrivate
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