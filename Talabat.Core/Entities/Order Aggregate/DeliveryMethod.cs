namespace Talabat.Core.Entities.Order_Aggregate;

public class DeliveryMethod : BaseEntity
{
    
    public DeliveryMethod()
    {
        // Required by EF
    }

    public DeliveryMethod(string shortName, decimal cost, string description, string deliveryTime)
    {
        ShortName = shortName;
        Cost = cost;
        Description = description;
        DeliveryTime = deliveryTime;
    }
    public string ShortName { get; set; }

    public decimal Cost { get; set; }

    public string Description { get; set; }

    public string DeliveryTime { get; set; }
}