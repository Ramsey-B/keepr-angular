using System.Collections.Generic;
using keepr_angular.Models;
using keepr_angular.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr_angular.Controllers
{
  [Route("api/[controller]")]
  public class ShareController : Controller
  {
    private readonly ShareRepository _db;
    public ShareController(ShareRepository repo)
    {
      _db = repo;
    }

    [HttpGet("vault/{vaultId}")]
    public IEnumerable<Keep> GetByVaultId(int vaultId)
    {
      return _db.GetByVaultId(vaultId);
    }

    [HttpGet("{id}")]
    public Keep GetById(int id)
    {
      return _db.GetById(id);
    }

    [HttpGet("user/{id}")]
    [Authorize]
    public IEnumerable<Keep> GetByAuthorId(string id)
    {
      return _db.GetByAuthorId(id);
    }

    [HttpPost("add/{keepId}")]
    [Authorize]
    public string ShareKeep([FromBody]Share newKeep, int keepId)
    {
      newKeep.AuthorId = HttpContext.User.Identity.Name;
      return _db.ShareKeep(newKeep, keepId);
    }

    [HttpDelete("share/{vaultId}/{keepId}")]
    [Authorize]
    public string DeleteShare(int vaultId, int keepId)
    {
      var user = HttpContext.User.Identity.Name;
      bool result = _db.DeleteShare(vaultId, keepId, user);
      if (result)
      {
        return "Successfully Removed!";
      }
      return "An Error Occurred! Try Again!";
    }
  }
}