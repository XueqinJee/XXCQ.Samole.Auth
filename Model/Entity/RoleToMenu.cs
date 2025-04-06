namespace Model.Entity;

public class RoleToMenu : BaseEntity
{
    public int PermissionId { get; set; }
    public int RoleId { get; set; }
}