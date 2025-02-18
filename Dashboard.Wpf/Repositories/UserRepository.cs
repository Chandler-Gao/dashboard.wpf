using Dashboard.Wpf.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Data;
using System.Net;

namespace Dashboard.Wpf.Repositories;

public class UserRepository : RepositoryBase, IUserRepository
{
    public void Add(User user)
    {

    }

    public bool AuthenticateUser(NetworkCredential credential)
    {
        bool validUser;
        using (var connection = GetConnection())
        using (var command = new SqlCommand())
        {
            connection.Open();
            command.Connection = connection;
            command.CommandText = "select * from [User] where [username]=@username and [password]=@password";
            command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
            command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
            validUser = command.ExecuteScalar() == null ? false : true;
        }

        return validUser;
    }

    public IEnumerable<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public User GetById(int id)
    {
        throw new NotImplementedException();
    }

    public User GetByUsername(string username)
    {
        User? user = null;
        using (var connection = GetConnection())
        using (var command = new SqlCommand())
        {
            connection.Open();
            command.Connection = connection;
            command.CommandText = "select * from [User] where [username]=@username";
            command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    user = new User()
                    {
                        Id = reader[0].ToString(),
                        Username = reader[1].ToString(),
                        Password = string.Empty,
                        Name = reader[3].ToString(),
                        LastName = reader[4].ToString(),
                        Email = reader[5].ToString(),
                    };
                }
            }
        }

        return user;
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }
}
