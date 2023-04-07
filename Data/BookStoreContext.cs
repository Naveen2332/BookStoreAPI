using BookStore.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookStoreContext :IdentityDbContext<ApplicationUser>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            :base (options)
        {
        }
        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;

    }
}
