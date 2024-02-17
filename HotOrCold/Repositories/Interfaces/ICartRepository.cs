using HotOrCold.Entities;

namespace HotOrCold.Repositories.Interfaces;

public interface ICartRepository
{
    void Create(Cart cart);
    Cart? Get(int id);
    bool ClearCart(int cartId);
    bool AddDrinkCopy(int cartId, int drinkId, int quantityInLiter);
    bool RemoveDrinkCopy(int cartId, int drinkId);
}