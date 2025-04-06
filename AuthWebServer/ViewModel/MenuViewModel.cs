using Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuthWebServer.ViewModel {
    public class MenuViewModel {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name不可为空")]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }
        public int SortOrder { get; set; }

        [Required]
        public string Route { get; set; }

        [Required]
        public string Pre { get; set; }
        public string ComponentPath { get; set; }

        [Required]
        public string Icon { get; set; }
        public int ParentId { get; set; } = 0;
        public MenuStateEnum State { get; set; }
        public bool IsCache { get; set; }
        public bool IsHide { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
