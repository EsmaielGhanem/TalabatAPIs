using System.Runtime.Serialization;

namespace Talabat.Core.Entities.Order_Aggregate;

public enum OrderStatus
{
    
    [EnumMember(Value = "pending")]
    pending ,
    [EnumMember(Value = "Payment Recieved")]
    PaymentRecieved, 
    [EnumMember(Value = "Payment Failed")]
    PaymentFailed
}