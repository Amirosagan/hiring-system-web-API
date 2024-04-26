using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HiringSystem.Models;

public class User : IdentityUser
{
   [Required, MaxLength(25)]
   public string? FirstName { get; set; }
   
   [Required, MaxLength(25)]
   public string? SecondName { get; set; }
   
   public string? FullName => $"{FirstName} {SecondName}";
}