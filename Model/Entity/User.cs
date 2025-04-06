using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model.Entity;

[Index(nameof(LoginName), IsUnique = true)]
public class User : BaseEntity
{
    public string? Name { get; set; }
    
    public string? LoginName { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    
    [ForeignKey(nameof(Entity.Role))]
    public int? RoleId { get; set; }
    
    public Role? Role { get; set; }
}