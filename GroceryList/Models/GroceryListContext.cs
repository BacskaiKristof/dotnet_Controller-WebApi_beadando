using Microsoft.EntityFrameworkCore;

namespace GroceryList.Models;

public class GroceryListContext : DbContext
{
    public GroceryListContext(DbContextOptions<GroceryListContext> options) : base(options)
    {

    }


    public DbSet<GroceryListItem> GroceryList { get; set; } = null!;
}

