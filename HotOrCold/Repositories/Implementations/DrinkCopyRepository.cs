using HotOrCold.Datas;
using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotOrCold.Repositories.Implementations;

public class DrinkCopyRepository(ApplicationDbContext context, IDrinkRepository _drinkRepository) : IDrinkCopyRepository
{
    public async Task<DrinkCopy> Create(DrinkCopy drinkCopy)
    {
        Console.WriteLine("L id est "+drinkCopy.DrinkId);
        Console.WriteLine("Le drink est null ");
        // S'assurer que le Drink existe selon l'id
        var theDrink = await context.Drinks.FindAsync(drinkCopy.DrinkId);
        if (theDrink != null)
        {
            drinkCopy.DrinkId = theDrink.DrinkId;
            drinkCopy.Price = theDrink.PricePerLiter * drinkCopy.QuantityInLiter;
            await context.AddAsync(drinkCopy);
            await context.SaveChangesAsync();
            return drinkCopy;
        }
        throw new Exception();
    }

    public async Task<List<DrinkCopy>> GetAll()
    {
        return (await context.DrinkCopies.ToListAsync());
    }

    public async Task<DrinkCopy?> Get(int id)
    {
         return await context.DrinkCopies.FindAsync(id);
    }

    public async Task<DrinkCopy?> Update(DrinkCopy drinkCopy)
    { 
        context.DrinkCopies.Update(drinkCopy);
        await context.SaveChangesAsync();
        return drinkCopy;
    }

    public async Task<bool> Delete(int id)
    {
        await context.DrinkCopies.Where(d => d.DrinkCopyId == id).ExecuteDeleteAsync();
        return true;
    }
}