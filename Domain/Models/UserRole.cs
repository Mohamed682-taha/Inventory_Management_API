using System.Runtime.Serialization;

namespace Domain.Models
{
    public enum UserRole
    {
        [EnumMember(Value ="Admin")]
        Admin,
        [EnumMember(Value = "Manager")]
        Manager,
        [EnumMember(Value = "Staff")]
        Staff
    }
}
