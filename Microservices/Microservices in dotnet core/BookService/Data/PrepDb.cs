using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModelClassLibrary;
using System;
using System.Linq;

namespace BookService.Data
{
    public static class PrepDb
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var context = new BookDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BookDbContext>>()))
            {
                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(
                    new Author() { Name = "Jane Austen" },
                    new Author() { Name = "Charles Dickens" },
                    new Author() { Name = "Miguel de Cervantes" }
                    );
                    context.SaveChanges();
                }
                if (!context.Books.Any())
                {
                    Console.WriteLine("--> Seeding Books Data...");

                    context.Books.AddRange(
                    new Book()
                    {
                        Title = "Pride and Prejudice",
                        Description = "1813",
                        AuthorId = 1,
                        Quantity = 10,
                        Category = "Comedy of manners"
                    },
                    new Book()
                    {
                        Title = "Northanger Abbey",
                        Description = "1817",
                        AuthorId = 1,
                        Quantity = 12,
                        Category = "Gothic parody"
                    },
                    new Book()
                    {
                        Title = "David Copperfield",
                        Description = "1850",
                        AuthorId = 2,
                        Quantity = 15,
                        Category = "Bildungsroman"
                    },
                    new Book()
                    {
                        Title = "Don Quixote",
                        Description = "1617",
                        AuthorId = 3,
                        Quantity = 8,
                        Category = "Picaresque"
                    }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
