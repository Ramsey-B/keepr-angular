using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using keepr_angular.Models;
using keepr_angular.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  public class AccountController : Controller
  {
    private readonly UserRepository _db;

    public AccountController(UserRepository repo)
    {
      _db = repo;
    }

    [HttpPost("register")]
    public async Task<UserReturnModel> Register([FromBody]RegisterUserModel creds)
    {
      if (ModelState.IsValid)
      {
        UserReturnModel user = _db.Register(creds);
        if (user != null)
        {
          ClaimsPrincipal principal = user.SetClaims();
          await HttpContext.SignInAsync(principal);
          return user;
        }
      }
      return null;
    }

    [HttpPost("login")]
    public async Task<UserReturnModel> Login([FromBody]LoginUserModel creds)
    {
      if (ModelState.IsValid)
      {
        UserReturnModel user = _db.Login(creds);
        if (user != null)
        {
          ClaimsPrincipal principal = user.SetClaims();
          await HttpContext.SignInAsync(principal);
          return user;
        }
      }
      return null;
    }
    [HttpGet("authenticate")]
    public UserReturnModel Authenticate()
    {
      var user = HttpContext.User;
      var id = user.Identity.Name;
      if(id == null) {
        return null;
      }
      return _db.GetUserById(id);
    }

    [Authorize]
    [HttpPut]
    public UserReturnModel UpdateAccount([FromBody]UserReturnModel user)
    {
      var email = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Email)
             .Select(c => c.Value).SingleOrDefault();
      var sessionUser = _db.GetUserByEmail(email);

      if (sessionUser.Id == user.Id)
      {
        return _db.UpdateUser(user);
      }
      return null;
    }

    [Authorize]
    [HttpPut("change-password")]
    public string ChangePassword([FromBody]ChangeUserPasswordModel user)
    {
      if (ModelState.IsValid)
      {
        var email = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Email)
               .Select(c => c.Value).SingleOrDefault();
        var sessionUser = _db.GetUserByEmail(email);

        if (sessionUser.Id == user.Id)
        {
          return _db.ChangeUserPassword(user);
        }
      }
      return "How did you even get here?";
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<Response> Logout()
    {
      await HttpContext.SignOutAsync();
      Response res = new Response("succesfully logged out");
      return res;
    }
  }
}