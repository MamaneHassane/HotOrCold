using HotOrCold.Dtos.Definitions;
using HotOrCold.Entities;

namespace HotOrCold.Repositories.Interfaces;

public interface IDrinkCopyRepository
{
    Task<DrinkCopy> Create(DrinkCopy drinkCopy);
    Task<List<DrinkCopy>> GetAll();
    Task<DrinkCopy?> Get(int id);
    Task<DrinkCopy?> Update(DrinkCopy drinkCopy);
    Task<bool> Delete(int id);
}