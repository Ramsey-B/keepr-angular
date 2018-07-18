using System;
using System.Collections.Generic;
using System.Data;
using keepr_angular.Models;
using Dapper;
using MySql.Data.MySqlClient;
using keepr_angular.Repositories;

namespace keepr_angular.Repositories
{
  public class UserRepository : DbContext
  {
    public UserRepository(IDbConnection db) : base(db)
    {
    }

    public UserReturnModel Register(RegisterUserModel creds)
    {
      //sql
      try
      {
        var sql = @"
      INSERT INTO users (id, username, email, password)
      VALUES (@Id, @Username, @Email, @Password);
      ";
        creds.Password = BCrypt.Net.BCrypt.HashPassword(creds.Password);
        var id = Guid.NewGuid().ToString();
        _db.ExecuteScalar<string>(sql, new
        {
          Id = id,
          Username = creds.Username,
          Email = creds.Email,
          Password = creds.Password
        });

        return new UserReturnModel()
        {
          Id = id,
          Username = creds.Username,
          Email = creds.Email
        };
      }
      catch (MySqlException e)
      {
        System.Console.WriteLine("ERROR: " + e.Message);
        return null;
      }
    }

    public UserReturnModel Login(LoginUserModel creds)
    {
      // more sql
      User user = _db.QueryFirstOrDefault<User>(@"
                SELECT * FROM users WHERE email = @Email
            ", creds);
      if (user != null)
      {
        var valid = BCrypt.Net.BCrypt.Verify(creds.Password, user.Password);
        if (valid)
        {
          return user.GetReturnModel();
        }
      }
      return null;
    }

    internal UserReturnModel GetUserByEmail(string email)
    {
      User user = _db.QueryFirstOrDefault<User>(@"
                SELECT * FROM users WHERE email = @Email
            ", new { email });
      return user.GetReturnModel();
    }

    internal UserReturnModel GetUserById(string id)
    {
      if (id != null)
      {
        User savedUser = _db.QueryFirstOrDefault<User>(@"
            SELECT * FROM users WHERE id = @id
            ", new { id });
        return savedUser.GetReturnModel();

      }
      return null;
    }

    internal UserReturnModel UpdateUser(UserReturnModel user)
    {
      var i = _db.Execute(@"
                UPDATE users SET
                  email = @Email,
                  username = @Username
                WHERE id = @id
            ", user);
      if (i > 0)
      {
        return user;
      }
      return null;

    }

    internal string ChangeUserPassword(ChangeUserPasswordModel user)
    {
      User savedUser = _db.QueryFirstOrDefault<User>(@"
            SELECT * FROM users WHERE id = @id
            ", user);

      var valid = BCrypt.Net.BCrypt.Verify(user.OldPassword, savedUser.Password);
      if (valid)
      {
        user.NewPassword = BCrypt.Net.BCrypt.HashPassword(user.NewPassword);
        var i = _db.Execute(@"
                    UPDATE users SET
                        password = @NewPassword
                    WHERE id = @id
                ", user);
        return "Good Job";
      }
      return "Umm nope!";
    }
  }
}