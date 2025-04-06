using Model.Enums;

namespace ClassLibrary.Dto
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public int SortOrder { get; set; }
        public string? Route { get; set; }
        public string? Pre { get; set; }
        public string? ComponentPath { get; set; }
        public string? Icon { get; set; }
        public int ParentId { get; set; } = 0;
        public DateTime CreateTime { get; set; }
        public MenuStateEnum State { get; set; }
        public bool IsCache { get; set; }
        public bool IsHide { get; set; }
        public List<MenuDto>? Children { get; set; }
    }
}
