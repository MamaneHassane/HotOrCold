using HotOrCold.Entities;
using HotOrCold.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace HotOrCold.Datas;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    // Les catégories, une catégorie contient plusieurs boissons et une boisson possède plusieurs catégories
    public DbSet<Category> Categories => Set<Category>();
    // Les boissons, une boisson possède plusieus catégories et plusieurs exemplaires qui seront vendus
    public DbSet<Drink> Drinks => Set<Drink>();
    // Les exemplaires de boisson vendus, chaque exemplaire appartient à un seul panier
    public DbSet<DrinkCopy> DrinkCopies => Set<DrinkCopy>();
    // Les clients, un client possède un panier
    public DbSet<Customer> Customers => Set<Customer>();
    // Les paniers, un panier appartient à un client et contient plusieurs exemplaires de boissons
    public DbSet<Cart> Carts => Set<Cart>();
    // Les commandes, un Customer donne lieu à plusieurs commandes, un commande vient d'un seul Customer
    public DbSet<Command> Commands => Set<Command>();

    // Création de model
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}