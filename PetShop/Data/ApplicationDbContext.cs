using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetShop.Models;

namespace PetShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PetShop.Models.Animal> Animal { get; set; } = default!;
        public DbSet<PetShop.Models.Category> Category { get; set; } = default!;
        public DbSet<PetShop.Models.Comment> Comment { get; set; } = default!;
    }
}