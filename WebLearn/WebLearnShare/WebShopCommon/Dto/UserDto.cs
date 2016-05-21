using WebShopCommon.Enums;

namespace WebShopCommon.Dto
{
    public class UserDto
    {
        public System.Guid Id { get; set; }
        public string UserName { get; set; }
        public int Role { get; set; }
        public System.Guid ImageId { get; set; }
        public int SendNumber { get; set; }
        public int BadSend { get; set; }
        public int GoodSend { get; set; }
        public string VehicleNumber { get; set; }
        public int NeedNotification { get; set; }
        public int TotalPoint { get; set; }
    }
}
