using System.Runtime.Serialization;

namespace Domain.Models
{
    public enum TransactionType
    {
        [EnumMember(Value = "Sale")]
        Sale,
        [EnumMember(Value = "Purchase")]
        Purchase
    }
}
