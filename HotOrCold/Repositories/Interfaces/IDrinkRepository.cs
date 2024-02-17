using HotOrCold.Entities;

namespace HotOrCold.Repositories.Interfaces;

public interface IDrinkRepository
{
    void Create(Drink drink);
    IEnumerable<Drink> GetAll();
    Drink? Get(int id);
    void Update(Drink updatedDrink);
    void Delete(int id);
}