using CatShelter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CatShelter.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<IdType>, IdType>
    {
        public required override DbSet<User> Users { get; set; }
        public required DbSet<Cat> Cats { get; set; }
        public required DbSet<Adoption> Adoptions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
