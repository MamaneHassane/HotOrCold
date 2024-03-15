using HotOrCold.Datas;
using HotOrCold.Dtos.Definitions;
using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotOrCold.Repositories.Implementations;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Cart> Create(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();
        return cart;
    }

    public async Task<Cart?>  Get(int id)
    {
        return await _context.Carts.Where(cart => cart.CartId == id).Include(cart => cart.DrinkCopies).FirstOrDefaultAsync();
    }
    
    public async Task<bool> ClearCart(int cartId)
    {
        var theCart = await _context.Carts.FindAsync(cartId);
        if (theCart is null) return false;
        theCart.DrinkCopies.Clear();
        _context.Carts.Update(theCart);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddDrinkCopyToCart(AddDrinkCopyDto addDrinkCopyDto)
    {
        var theDrink = await _context.Drinks.FindAsync((addDrinkCopyDto.DrinkId));
        var theCart = await _context.Carts.FindAsync(addDrinkCopyDto.CartId);
        if (theDrink is null || theCart is null) return false;
        var drinkCopy = new DrinkCopy
        {
            // S'assurer que le Drink existe selon l'id
            DrinkId = theDrink.DrinkId,
            Price = theDrink.PricePerLiter * addDrinkCopyDto.QuantityInLiter,
            QuantityInLiter = addDrinkCopyDto.QuantityInLiter,
            CartId = theCart.CartId
        };
        theCart.DrinkCopies.Add(drinkCopy);
        _context.Carts.Update(theCart);
        await _context.DrinkCopies.AddAsync((drinkCopy));
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> AddManyDrinkCopies(AddManyDrinkCopiesDto addManyDrinkCopiesDto)
    {
        var theCart = await _context.Carts.FindAsync(addManyDrinkCopiesDto.CartId);
        if (theCart is null) return false;
        foreach (var addDrinkCopyDto in addManyDrinkCopiesDto.AddDrinkCopyDtoCollection)
        {
            // S'assurer que le Drink existe selon l'id
            var theDrink = await _context.Drinks.FindAsync(addDrinkCopyDto.DrinkId);
            if (theDrink is not null)
            {
                var drinkCopy = new DrinkCopy
                {
                    DrinkId = theDrink.DrinkId, 
                    Price = theDrink.PricePerLiter*addDrinkCopyDto.QuantityInLiter,
                    QuantityInLiter = addDrinkCopyDto.QuantityInLiter,
                    CartId = theCart.CartId
                };
                theCart.DrinkCopies.Add(drinkCopy);
                _context.Carts.Update(theCart);
                _context.DrinkCopies.Add((drinkCopy));
            }
        }
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> RemoveDrinkCopy(RemoveDrinkCopyFromCartDto removeDrinkCopyFromCartDto)
    {
        var theDrinkCopy = await _context.DrinkCopies.FindAsync(removeDrinkCopyFromCartDto.DrinkCopyId);
        var theCart = await _context.Carts.FindAsync(removeDrinkCopyFromCartDto.CartId);
        if (theDrinkCopy is null || theCart is null) return false;
        theCart.DrinkCopies.Remove(theDrinkCopy);
        _context.Carts.Update(theCart);
        _context.DrinkCopies.Remove(theDrinkCopy);
        await _context.SaveChangesAsync();
        return true;
    }
}

