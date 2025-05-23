﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CatShelter.Models;

namespace CatShelter.ViewModels.AdoptionViewModels
{
    public class AdoptionViewModel
    {
        public IdType Id { get; set; }
        public IdType? CatId { get; set; }
        public CatViewModels.CatViewModel? Cat { get; set; }
        public IdType? UserId { get; set; }
        public UserViewModels.UserViewModel? User { get; set; }
        public DateOnly Date { get; set; }
        public AdoptionType AdoptionType { get; set; }
    }
}
