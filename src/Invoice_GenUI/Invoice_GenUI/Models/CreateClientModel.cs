
using System.ComponentModel.DataAnnotations;

namespace Invoice_GenUI.Models;

public class CreateClientModel
{
    [Required] // Checks if value is null
    public string? ClientName { get; set; }
    [Required]
    public string? ClientAddress { get; set; }
    [Required]
    public string? ContactName { get; set; }
    [Required]
    [EmailAddress]
    public string? ContactEmail { get; set; }

}
