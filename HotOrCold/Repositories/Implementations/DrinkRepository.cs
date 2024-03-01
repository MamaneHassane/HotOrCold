using HotOrCold.Datas;
using HotOrCold.Entities;
using HotOrCold.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotOrCold.Repositories;

public class DrinkRepository(ApplicationDbContext context) : IDrinkRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Drink> Create(Drink drink)
    {
        await _context.AddAsync(drink);
        await _context.SaveChangesAsync();
        return drink;
    }

    public async Task<List<Drink>> GetAll()
    {
        var theDrinks = await _context.Drinks.ToListAsync();
        return theDrinks;
    }

    public async Task<Drink?> Get(int id)
    {
        Drink? theDrink = await _context.Drinks.FindAsync(id);
        return theDrink;
    }

    public async Task<Drink?> Update(Drink updatedDrink)
    {
        _context.Drinks.Update(updatedDrink);
        await _context.SaveChangesAsync();
        return updatedDrink;
    }

    public async Task<bool> Delete(int id)
    {
        await _context.Drinks.Where(drink => drink.DrinkId == id)
                             .ExecuteDeleteAsync();
        return true;
    }
}