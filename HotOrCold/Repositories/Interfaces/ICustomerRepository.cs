using HotOrCold.Entities;

namespace HotOrCold.Repositories.Interfaces;

public interface ICustomerRepository
{
    void Create(Customer customer);
    IEnumerable<Customer> GetAll();
    Customer? Get(int id);
    void Update(Customer updatedCustomer);
    bool IncreaseBalance(int id, double amountToAdd);
    bool DecreaseBalance(int id, double amountToRemove);
}