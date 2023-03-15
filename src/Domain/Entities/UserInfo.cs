using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities;

public class UserInfo : BaseAuditableEntity
{
    [Key] [Required] [StringLength(36)] required public string Id { get; set; }

    [StringLength(256)] required public string UserName { get; set; }

    [StringLength(256)] required public string FirstName { get; set; }

    [StringLength(256)] required public string LastName { get; set; }

    public string FullName => FirstName + " " + LastName;
}