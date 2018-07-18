using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr_angular.Models;
using keepr_angular.Repositories;
using MySql.Data.MySqlClient;

namespace keepr_angular.Repository
{
  public class TagRepository : DbContext
  {
    public TagRepository(IDbConnection db) : base(db)
    {

    }

    public IEnumerable<Tag> AddTags(List<Tag> tags, string userId, int keepId)
    {
      Keep keep = _db.QueryFirstOrDefault<Keep>("SELECT * FROM keeps WHERE id = @keepId;", new { keepId });
      if (keep.AuthorId == userId)
      {
        tags.ForEach(tag =>
        {
          tag.KeepId = keep.Id;
          tag.AuthorId = userId;
        });
        try
        {
          int num = _db.Execute(@"
                INSERT INTO tags (tagName, authorId, keepId)
                VALUES (@TagName, @AuthorId, @KeepId);
            ", tags);
          if (num > 0)
          {
            return tags;
          }
        }
        catch (MySqlException e)
        {
          System.Console.WriteLine("ERROR: " + e.Message);
          return null;
        }
      }
      return null;
    }

    public bool RemoveTag(int id)
    {
      var i = _db.Execute(@"
      DELETE FROM tags
      WHERE id = @id
      LIMIT 1;
      ", new { id });
      return i > 0;
    }

    public IEnumerable<Tag> GetTags(int id)
    {
      return _db.Query<Tag>("SELECT * FROM tags WHERE keepId = @id", new { id });
    }

    public IEnumerable<Keep> GetByTag(string tag)
    {
      var check = _db.Query<Keep>(@"
      SELECT * FROM tags
      INNER JOIN keeps ON keeps.id = tags.keepId 
      WHERE tagName LIKE CONCAT('%',@tag,'%')
      AND keeps.public = true;
      ", new { tag });
      return check;
    }

    public IEnumerable<Keep> RelatedKeeps(string query)
    {
      return _db.Query<Keep>(@"
        SELECT * FROM tags
        INNER JOIN keeps ON keeps.id = tags.keepId
        WHERE tagName LIKE CONCAT('%',@query,'%')
        AND keeps.public = true;
      ", new { query });
    }
  }
}