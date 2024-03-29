namespace Customer.Core.Repositories;

public interface ICustomerRepository : IRepository<Entities.Customer>
{
    Task<Entities.Customer> GetByPhone(string ddd, string phone);

    Task<IList<Entities.Customer>> GetAll();

    Task RemoveByEmail(int id, string email);

    Task<bool> EmailsExists(string email);

    Task UpdateEmail(int id, string email);

    Task UpdatePhone(int id, int phoneId, string areaCode, string number, string type);
}