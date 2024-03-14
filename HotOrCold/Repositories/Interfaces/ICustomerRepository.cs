using HotOrCold.Dtos.Definitions;
using HotOrCold.Entities;

namespace HotOrCold.Repositories.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> Register(Customer customer);
    Task<Customer?> Authenticate(CustomerAuthenticationDto customer);
    Task<IEnumerable<Customer>> GetAll();
    Task<Customer?> Get(int id);
    Task<Customer?> Update(Customer updatedCustomer);
    Task<bool> Delete(int id);
    Task<bool> IncreaseBalance(int id, double amountToAdd);
    Task<bool> DecreaseBalance(int id, double amountToRemove);
}