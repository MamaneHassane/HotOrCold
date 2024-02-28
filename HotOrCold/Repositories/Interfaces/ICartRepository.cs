using HotOrCold.Entities;
using HotOrCold.Dtos;

namespace HotOrCold.Repositories.Interfaces;

public interface ICartRepository
{
    void Create(Cart cart);
    Cart? Get(int id);
    bool ClearCart(int cartId);
    bool AddDrinkCopy(AddDrinkCopyDto addDrinkCopyDto);
    bool RemoveDrinkCopy(int cartId, int drinkId);
}