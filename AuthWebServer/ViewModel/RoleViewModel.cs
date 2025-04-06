using Model.Entity;

namespace AuthWebServer.ViewModel {
    public class RoleViewModel : BaseEntity{
        public string RoleName { get; set; }
        public string Description { get; set; }
        public int[] MenuIds { get; set; }
    }
}
