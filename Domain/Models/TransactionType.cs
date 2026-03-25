using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

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
