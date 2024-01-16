using Customer.Core.Entities;
using Customer.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infra.Repositories;

public class CustomerRepository : Repository<Core.Entities.Customer>, ICustomerRepository
{
    public CustomerRepository(CustomerContext context) : base(context)
    {
    }

    public async Task<Core.Entities.Customer> GetByPhone(string areaCode, string number)
    {
        var customer = await _context.Customers
            .Include(c => c.Phones)
            .FirstOrDefaultAsync(c => c.Phones.Any(p => p.AreaCode == areaCode && p.Number == number));
        return customer!;
    }

    public async Task<IList<Core.Entities.Customer>> GetAll()
    {
        var customers = await _context.Customers
            .Include(c => c.Phones).ToListAsync();
            
        return customers!;
    }

    public async Task RemoveByEmail(int id, string email)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(e => e.Id == id && e.Email == email);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
        }
    }

    public async  Task<bool> EmailsExists(string email)
    {
        return await _context.Customers.AnyAsync(e => e.Email == email);
    }

    public async Task UpdateEmail(int id, string email)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(e => e.Id == id && e.Email == email);
        if (customer != null)
        {
            customer.Email = email;
        }
    }
    
    public async Task UpdatePhone(int id, int phoneId, string areaCode, string number, string type)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(e => e.Id == id);
        if (customer != null)
        {
            customer.UpdatePhoneNumber(phoneId, areaCode, number, Enum.Parse<PhoneType>(type));
        }
    }
}