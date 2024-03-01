using HotOrCold.Entities;

namespace HotOrCold.Repositories.Interfaces;

public interface IDrinkRepository
{
    Task<Drink> Create(Drink drink);
    Task<List<Drink>> GetAll();
    Task<Drink?> Get(int id);
    Task<Drink?> Update(Drink updatedDrink);
    Task<bool> Delete(int id);
}