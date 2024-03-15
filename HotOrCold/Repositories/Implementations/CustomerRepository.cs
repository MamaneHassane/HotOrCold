using HotOrCold.Datas;
using HotOrCold.Dtos.Definitions;
using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotOrCold.Repositories.Implementations;

public class CustomerRepository(ApplicationDbContext context, ICartRepository cartRepository) : ICustomerRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly ICartRepository _cartRepository = cartRepository;

    public async Task<Customer> Register(Customer customer)
    {
        // Faire un AddAsync pour générer l'identifiant du customer
        await _context.Customers.AddAsync(customer);
        // Lui créér un Cart
        var newCartOfCustomer = new Cart();
        // Sauvegarder son cart
        await _cartRepository.Create(newCartOfCustomer);
        customer.CartId = newCartOfCustomer.CartId;
        await _context.SaveChangesAsync();
        return customer;
    }
    public async Task<Customer?> Authenticate(CustomerAuthenticationDto customerAuthenticationDto)
    {
        var customerFound = await _context.Customers.Where(customer =>
                customer.Username == customerAuthenticationDto.Username
                &&
                customer.Password == customerAuthenticationDto.Password
        ).FirstOrDefaultAsync();
        return customerFound;
    }
    public async Task<IEnumerable<Customer>> GetAll()
    {
        return await _context.Customers.ToListAsync();
    }
    public async Task<Customer?> Get(int id)
    {
        return await _context.Customers.FindAsync(id);
    }
    public async Task<Customer?> Update(Customer updatedCustomer)
    {
        _context.Customers.Update(updatedCustomer);
        await _context.SaveChangesAsync();
        return updatedCustomer;
    }
    public async Task<bool> IncreaseBalance(int id, double amountToAdd)
    {
        var theCustomer = await _context.Customers.FindAsync(id);
        if (theCustomer is null) return false;
        theCustomer.Balance += amountToAdd;
        _context.Customers.Update(theCustomer);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DecreaseBalance(int id, double amountToRemove)
    {
        var theCustomer = await _context.Customers.FindAsync(id);
        if (theCustomer is null) return false;
        theCustomer.Balance -= amountToRemove;
        _context.Customers.Update(theCustomer);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Delete(int id)
    {
        await _context.Customers.Where(customer => customer.CustomerId == id).ExecuteDeleteAsync();
        return true;
    }
}