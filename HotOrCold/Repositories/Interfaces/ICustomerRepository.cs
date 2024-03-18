using HotOrCold.Entities;
using HotOrCold.Security.Models;

namespace HotOrCold.Repositories.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> Register(Customer customer);
    Task<Customer?> Authenticate(TokenGenerationRequest tokenGenerationRequest);
    Task<IEnumerable<Customer>> GetAll();
    Task<Customer?> Get(int id);
    Task<Customer?> Update(Customer updatedCustomer);
    Task<bool> Delete(int id);
    Task<bool> IncreaseBalance(int id, double amountToAdd);
    Task<bool> DecreaseBalance(int id, double amountToRemove);
}