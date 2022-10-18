using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactsHub.Data;

using Model;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Code>()
            .HasIndex(x => x.Short).IsUnique();
        
        base.OnModelCreating(builder);
    }

    public DbSet<Contact> Contacts { get; set; }
    
    public DbSet<Friend> Friends { get; set; }
    
    public DbSet<FriendRequest> FriendRequests { get; set; }
    
    public DbSet<Code> Codes { get; set; }
}