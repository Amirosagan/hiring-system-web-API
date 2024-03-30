using System.ComponentModel.DataAnnotations;

namespace HiringSystem.Models.DTOs;

public class UserRegistrationRequestDto
{
   [Required]
   public string? FirstName { get; set; }
   [Required]
   public string? SecondName { get; set; }
   [Required]
   public string? Email { get; set; } 
   [Required]
   public string? Password { get; set; }
}