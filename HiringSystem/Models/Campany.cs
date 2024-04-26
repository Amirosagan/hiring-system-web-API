using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HiringSystem.Models;

public class Campany : IdentityUser
{
    [Required, MaxLength(50)]
    public string? FirstName { get; set; }
    [Required, MaxLength(50)]
    public string? SecondName { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    [Url]
    public string? Logo { get; set; }
    [Url]
    public string? Website { get; set; }
}
