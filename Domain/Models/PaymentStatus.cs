using System.Runtime.Serialization;

namespace Domain.Models
{
    public enum PaymentStatus
    {
        [EnumMember(Value = "Completed")]
        Completed,
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Failed")]
        Failed
    }
}
