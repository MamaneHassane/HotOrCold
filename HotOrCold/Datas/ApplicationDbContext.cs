using HotOrCold.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotOrCold.Datas;

public class ApplicationDbContext : DbContext
{
    // Le constructeur, qui est vide et dont les options sont celles des entités du context
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    // Les catégories, une catégorie contient plusieurs boissons et une boisson possède plusieurs catégories
    public DbSet<Category> Categories { get; set; }
    // Les boissons, une boisson possède plusieus catégories et plusieurs exemplaires qui seront vendus
    public DbSet<Drink> Drinks { get; set; }
    // Les exemplaires de boisson vendus, chaque exemplaire appartient à un seul panier
    public DbSet<DrinkCopy> DrinkCopies { get; set; }
    // Les clients, un client possède un panier
    public DbSet<Customer> Customers { get; set; }
    // Les paniers, un panier appartient à un client et contient plusieurs exemplaires de boissons
    public DbSet<Cart> Carts { get; set; }
    // Les commandes, un Customer donne lieu à plusieurs commandes, un commande vient d'un seul Customer
    public DbSet<Command> Commands { get; set; }
}