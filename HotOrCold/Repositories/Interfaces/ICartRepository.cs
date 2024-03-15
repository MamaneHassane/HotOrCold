using HotOrCold.Entities;
using HotOrCold.Dtos.Definitions;

namespace HotOrCold.Repositories.Interfaces;

public interface ICartRepository
{
    Task<Cart> Create(Cart cart);
    Task<Cart?> Get(int id);
    Task<bool> ClearCart(int cartId);
    Task<bool> AddDrinkCopyToCart(AddDrinkCopyDto addDrinkCopyDto);
    Task<bool> RemoveDrinkCopy(RemoveDrinkCopyFromCartDto removeDrinkCopyFromCartDto);
    Task<bool> AddManyDrinkCopies(AddManyDrinkCopiesDto addManyDrinkCopiesDto);
}