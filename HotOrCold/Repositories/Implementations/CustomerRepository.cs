using HotOrCold.Datas;
using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotOrCold.Repositories;

public class CustomerRepository(ApplicationDbContext context) : ICustomerRepository
{
    private readonly ApplicationDbContext _context = context;

    public void Create(Customer customer)
    {
        _context.Add(customer);
        _context.SaveChanges();
    }
    public IEnumerable<Customer> GetAll()
    {
        return [.. _context.Customers.AsNoTracking()];
    }
    public Customer? Get(int id)
    {
        return _context.Customers.Find(id);
    }
    public void Update(Customer updatedCustomer)
    {
        _context.Update(updatedCustomer);
        _context.SaveChanges();
    }
    public bool IncreaseBalance(int id, double amountToAdd)
    {
        var theCustomer = _context.Customers.Find(id);
        if (theCustomer is null) return false;
        theCustomer.Balance += amountToAdd;
        _context.Customers.Update(theCustomer);
        _context.SaveChanges();
        return true;
    }
    public bool DecreaseBalance(int id, double amountToRemove)
    {
        var theCustomer = _context.Customers.Find(id);
        if (theCustomer is null) return false;
        theCustomer.Balance -= amountToRemove;
        _context.Customers.Update(theCustomer);
        _context.SaveChanges();
        return true;
    }
    public void Delete(int id)
    {
        _context.Customers.Where(customer => customer.CustomerId == id).ExecuteDelete();
    }
}