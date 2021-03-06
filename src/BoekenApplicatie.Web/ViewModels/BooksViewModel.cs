﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using BoekenApplicatie.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoekenApplicatie.Web.ViewModels
{
  public class BooksViewModel
  {
    public BooksViewModel()
    {
      Language = Language.Nederlands;
      OriginalLangage = Language.Engels;
      Category = Category.Roman;
      Genre = Genre.Fantasy;
    }

    public Guid TitleId { get; set; }
    [DisplayName("Titel")] public string TitleName { get; set; }
    [DisplayName("Serie")] public string SeriesName { get; set; }
    [DisplayName("Auteur")] public Author Author { get; set; }

    public string AuthorId { get; set; }

    [DisplayName("Taal")] public Language Language { get; set; }
    [DisplayName("Jaar uitgave")] public int ReleasedYear { get; set; }
    [DisplayName("Uitgever")] public Publisher Publisher { get; set; }
    public string PublisherId { get; set; }
    [DisplayName("Isbn")] public string Isbn { get; set; }
    [DisplayName("Uitgever serienummer")] public string SerialNumber { get; set; }
    [DisplayName("Druk")] public int Edition { get; set; }
    [DisplayName("Prijs")] public decimal Price { get; set; }
    [DisplayName("Deel")] public int Part { get; set; }
    [DisplayName("Categorie")] public Category Category { get; set; }
    [DisplayName("Originele titel")] public string OriginalTitle { get; set; }
    [DisplayName("Originele taal")] public Language OriginalLangage { get; set; }

    [DisplayName("Originele jaar uitgave")]
    public int OriginalReleasedYear { get; set; }

    [DisplayName("Vertaler")] public Translator Translator { get; set; }
    public string TranslatorId { get; set; }

    [DisplayName("Kunstenaar")] public Artist Artist { get; set; }
    public string ArtistId { get; set; }
    [DisplayName("Aanschaf jaar")] public int YearBought { get; set; }
    [DisplayName("Aanschaf prijs")] public decimal PriceBought { get; set; }
    [DisplayName("Prijs reden")] public string PriceReason { get; set; }
    public Genre Genre { get; set; }
    public List<SelectListItem> Authors { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> Publishers { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> Artists { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> Translators { get; set; } = new List<SelectListItem>();
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<Lending> Lendings { get; set; }
  }
}