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

    public DbSet<BoekenApplicatie.Domain.Models.Book> Books { get; set; }
    public DbSet<BoekenApplicatie.Domain.Models.Lending> Lendings { get; set; }
    public DbSet<BoekenApplicatie.Domain.Models.Rating> Ratings { get; set; }
    public DbSet<BoekenApplicatie.Domain.Models.Title> Titles { get; set; }
    public DbSet<BoekenApplicatie.Domain.Models.Publisher> Publishers { get; set; }
    public DbSet<BoekenApplicatie.Domain.Models.Person> Persons { get; set; }
    public DbSet<BoekenApplicatie.Domain.Models.Author> Authors { get; set; }
    public DbSet<BoekenApplicatie.Domain.Models.Artist> Artists { get; set; }
    public DbSet<BoekenApplicatie.Domain.Models.Translator> Translators { get; set; }
  }
}
