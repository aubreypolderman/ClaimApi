using ClaimApi.Model;

namespace ClaimApi.Repository;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUser(int id);
    Task<User> CreateUser(User user);
    Task<bool> UpdateUser(User user);
    Task<bool> DeleteUser(int id);
    Task<User> GetUserByEmail(string email);

}
