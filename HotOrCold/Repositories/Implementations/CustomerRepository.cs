using HotOrCold.Datas;
using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotOrCold.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;
    
    public CustomerRepository(ApplicationDbContext context)
    {
        this._context = context;
    }

    public void Create(Customer customer)
    {
        this._context.Add(customer);
        this._context.SaveChanges();
    }

    public IEnumerable<Customer> GetAll()
    {
        return this._context.Customers.AsNoTracking().ToList();
    }

    public Customer? Get(int id)
    {
        return this._context.Customers.Find(id);
    }

    public void Update(Customer updatedCustomer)
    {
        this._context.Update(updatedCustomer);
        this._context.SaveChanges();
    }

    public bool IncreaseBalance(int id, int amountToAdd)
    {
        var theCustomer = this._context.Customers.Find(id);
        if (theCustomer is null) return false;
        theCustomer.Balance += amountToAdd;
        this._context.Customers.Update(theCustomer);
        this._context.SaveChanges();
        return true;
    }
    
    public bool DecreaseBalance(int id, int amountToRemove)
    {
        var theCustomer = this._context.Customers.Find(id);
        if (theCustomer is null) return false;
        theCustomer.Balance -= amountToRemove;
        this._context.Customers.Update(theCustomer);
        this._context.SaveChanges();
        return true;
    }

    public void Delete(int id)
    {
        this._context.Customers.Where(customer => customer.CustomerId == id)
                               .ExecuteDelete();
    }
    
}