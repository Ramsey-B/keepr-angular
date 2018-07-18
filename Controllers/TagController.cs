using System.Collections.Generic;
using keepr_angular.Models;
using keepr_angular.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr_angular.Controllers
{
  [Route("api/[controller]")]
  public class TagController : Controller
  {
    private readonly TagRepository _db;

    public TagController(TagRepository repo)
    {
      _db = repo;
    }

    [HttpPost("{id}")]
    [Authorize]
    public IEnumerable<Tag> AddTags([FromBody]List<Tag> tags, int id)
    {
      var user = HttpContext.User.Identity.Name;
      if (ModelState.IsValid)
      {
        return _db.AddTags(tags, user, id);
      }
      return null;
    }

    [HttpDelete("{id}")]
    [Authorize]
    public string RemoveTag(int id)
    {
      bool result = _db.RemoveTag(id);
      if (result)
      {
        return "Tag Successfully Removed!";
      }
      return "An Error Occurred! Try Again!";
    }

    [HttpGet("tags/{keepId}")]
    public IEnumerable<Tag> GetTags(int keepId)
    {
      return _db.GetTags(keepId);
    }

    [HttpGet("query/{name}")]
    public IEnumerable<Keep> GetByTag(string name)
    {
      return _db.GetByTag(name);
    }

    [HttpGet("query/{query}")]
    public IEnumerable<Keep> RelatedKeeps(string query)
    {
      return _db.RelatedKeeps(query);
    }
  }
}