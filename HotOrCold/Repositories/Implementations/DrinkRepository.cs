using HotOrCold.Datas;
using HotOrCold.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotOrCold.Repositories;

public class DrinkRepository
{
    private readonly ApplicationDbContext _context;
    public DrinkRepository(ApplicationDbContext context)
    {
        this._context = context;
    }

    public void Create(Drink drink)
    {
        this._context.Add((drink));
        this._context.SaveChanges();
    }

    public IEnumerable<Drink> GetAll()
    {
        return this._context.Drinks.AsNoTracking().ToList();
    }

    public Drink? Get(int id)
    {
        return this._context.Drinks.Find(id);
    }

    public void Update(Drink updatedDrink)
    {
        this._context.Update(updatedDrink);
        this._context.SaveChanges();
    }

    public void Delete(int id)
    {
        this._context.Drinks.Where(drink => drink.DrinkId == id)
                            .ExecuteDelete();
    }
}