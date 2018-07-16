using System;
using System.Collections.Generic;
using System.Text;
using BoekenApplicatie.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoekenApplicatie.Data.Context
{
  public class LibraryContext : IdentityDbContext<ApplicationUser,IdentityRole<Guid>,Guid>
  {
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Lending> Lendings { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
  }
}
