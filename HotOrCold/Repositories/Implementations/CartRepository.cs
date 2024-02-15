using HotOrCold.Datas;
using HotOrCold.Entities;

namespace HotOrCold.Repositories;

public class CartRepository 
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context)
    {
        this._context = context;
    }

    public void Create(Cart cart)
    {
        this._context.Carts.Add(cart);
        this._context.SaveChanges();
    }

    public Cart? Get(int id)
    {
        return this._context.Carts.Find((id));
    }
    
    public bool ClearCart(int cartId)
    {
        var theCart = this._context.Carts.Find(cartId);
        if (theCart is null) return false;
        theCart.DrinkCopies.Clear();
        this._context.Carts.Update(theCart);
        this._context.SaveChanges();
        return true;
    }

    public bool AddDrinkCopy(int cartId, int drinkId, int quantityInLiter)
    {
        var theDrink = this._context.Drinks.Find(drinkId);
        var theCart = this._context.Carts.Find(cartId);
        if (theDrink is null || theCart is null) return false;
        var drinkCopy = new DrinkCopy
        {
            Drink = theDrink,
            QuantityInLiter = quantityInLiter,
            Cart = theCart
        };
        theCart.DrinkCopies.Add(drinkCopy);
        this._context.Carts.Update(theCart);
        this._context.DrinkCopies.Add((drinkCopy));
        this._context.SaveChanges();
        return true;
    }
    
    public bool RemoveDrinkCopy(int cartId, int drinkCopyId, int quantityInLiter)
    {
        var theDrinkCopy = this._context.DrinkCopies.Find(drinkCopyId);
        var theCart = this._context.Carts.Find(cartId);
        if (theDrinkCopy is null || theCart is null) return false;
        theCart.DrinkCopies.Remove(theDrinkCopy);
        this._context.Carts.Update(theCart);
        this._context.DrinkCopies.Remove((theDrinkCopy));
        this._context.SaveChanges();
        return true;
    }
}

