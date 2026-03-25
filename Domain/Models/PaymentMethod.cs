using System.Runtime.Serialization;

namespace Domain.Models
{
    public enum PaymentMethod
    {
        [EnumMember(Value = "CreditCard")]
        CreditCard,
        [EnumMember(Value = "Cash")]
        Cash
    }
}
