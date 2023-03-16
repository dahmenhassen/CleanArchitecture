using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities;

public class UserInfo : BaseAuditableEntity
{
    [Key]
    [Required]
    [StringLength(36)]
    public required string Id { get; set; }

    [StringLength(256)]
    public required string UserName { get; set; }

    [StringLength(256)]
    public required string FirstName { get; set; }

    [StringLength(256)]
    public required string LastName { get; set; }

    public string FullName => FirstName + " " + LastName;
}