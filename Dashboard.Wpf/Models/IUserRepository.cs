using System.Net;

namespace Dashboard.Wpf.Models;

public interface IUserRepository
{
    bool AuthenticateUser(NetworkCredential credential);
    void Add(User user);
    void Update(User user);
    void Remove(int id);
    User GetById(int id);
    User GetByUsername(string username);
    IEnumerable<User> GetAll();
}
