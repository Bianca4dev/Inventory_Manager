using Inv_back.Models;
using Microsoft.EntityFrameworkCore;

namespace Inv_back.Data
{
    //Hoe lyk DbCOntext???
    public class InventoryContext :DbContext{
        //wat doen die?
        public InventoryContext(DbContextOptions<InventoryContext>options)
        :base(options){}

        public DbSet<Item> items{get;set;}

    }
}