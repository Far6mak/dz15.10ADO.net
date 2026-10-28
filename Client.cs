using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClients;

[Table("clients")]
public sealed class Client
{
    [Key]
    [Column("client_id")]
    public int ClientId { get; set; }

    [Column("first_name")]
    public string FirstName { get; set; } = string.Empty;

    [Column("last_name")]
    public string LastName { get; set; } = string.Empty;

    [Column("status")]
    public string Status { get; set; } = string.Empty;
}
