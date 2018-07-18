using System.Collections.Generic;
using keepr_angular.Models;
using keepr_angular.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  public class VaultController : Controller
  {
    private readonly VaultRepository _db;

    public VaultController(VaultRepository repo)
    {
      _db = repo;
    }

    [HttpPost]
    [Authorize]
    public Vault CreateVault([FromBody]Vault newVault)
    {
      newVault.AuthorId = HttpContext.User.Identity.Name;
      if (ModelState.IsValid)
      {
        return _db.CreateVault(newVault);
      }
      return null;
    }

    [HttpGet("{id}")]
    public Vault GetById(int id)
    {
      return _db.GetById(id);
    }

    [HttpGet("user/{id}")]
    [Authorize]
    public IEnumerable<Vault> GetByAuthorId(string id)
    {
      if(id == HttpContext.User.Identity.Name) {
        return _db.GetByAuthorId(id);
      }
      return null;
    }

    [HttpPut("{id}")]
    [Authorize]
    public Vault EditVault(int id, [FromBody]Vault editVault)
    {
      if (ModelState.IsValid)
      {
       return _db.EditVault(id, editVault);
      }
      return null;
    }

    [HttpDelete("{id}")]
    [Authorize]
    public string DeleteVault(int id)
    {
      var user = HttpContext.User.Identity.Name;
      bool delete = _db.DeleteVault(id, user);
      if(delete) {
        return "Successfully Deleted!";
      }
      return "An Error Occurred!";
    }
  }
}