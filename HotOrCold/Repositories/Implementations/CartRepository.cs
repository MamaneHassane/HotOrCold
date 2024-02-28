using HotOrCold.Datas;
using HotOrCold.Entities;
using HotOrCold.Dtos;
using HotOrCold.Repositories.Interfaces;

namespace HotOrCold.Repositories;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Create(Cart cart)
    {
        _context.Carts.Add(cart);
        _context.SaveChanges();
    }

    public Cart? Get(int id)
    {
        return _context.Carts.Find((id));
    }
    
    public bool ClearCart(int cartId)
    {
        var theCart = _context.Carts.Find(cartId);
        if (theCart is null) return false;
        theCart.DrinkCopies.Clear();
        _context.Carts.Update(theCart);
        _context.SaveChanges();
        return true;
    }

    public bool AddDrinkCopy(AddDrinkCopyDto addDrinkCopyDto)
    {
        var theDrink = _context.Drinks.Find(addDrinkCopyDto.DrinkId);
        var theCart = _context.Carts.Find(addDrinkCopyDto.CartId);
        if (theDrink is null || theCart is null) return false;
        var drinkCopy = new DrinkCopy
        {
            Drink = theDrink,
            QuantityInLiter = addDrinkCopyDto.QuantityInLiter,
            Cart = theCart
        };
        theCart.DrinkCopies.Add(drinkCopy);
        _context.Carts.Update(theCart);
        _context.DrinkCopies.Add((drinkCopy));
        _context.SaveChanges();
        return true;
    }
    public bool AddManyDrinkCopies(AddManyDrinkCopiesDto addManyDrinkCopiesDto)
    {
        var theCart = _context.Carts.Find(addManyDrinkCopiesDto.CartId);
        if (theCart is null) return false;
        foreach (var addDrinkCopyDto in addManyDrinkCopiesDto.AddDrinkCopyDtoCollection)
        {
            var theDrink = _context.Drinks.Find(addDrinkCopyDto.DrinkId);
            if (theDrink is not null)
            {
                var drinkCopy = new DrinkCopy
                {
                    Drink = theDrink,
                    QuantityInLiter = addDrinkCopyDto.QuantityInLiter,
                    Cart = theCart
                };
                theCart.DrinkCopies.Add(drinkCopy);
                _context.Carts.Update(theCart);
                _context.DrinkCopies.Add((drinkCopy));
            }
        }
        _context.SaveChanges();
        return true;
    }
    public bool RemoveDrinkCopy(int cartId, int drinkCopyId)
    {
        var theDrinkCopy = _context.DrinkCopies.Find(drinkCopyId);
        var theCart = _context.Carts.Find(cartId);
        if (theDrinkCopy is null || theCart is null) return false;
        theCart.DrinkCopies.Remove(theDrinkCopy);
        _context.Carts.Update(theCart);
        _context.DrinkCopies.Remove((theDrinkCopy));
        _context.SaveChanges();
        return true;
    }
}

