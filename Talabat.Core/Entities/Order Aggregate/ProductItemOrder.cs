namespace Talabat.Core.Entities.Order_Aggregate;

public class ProductItemOrder
{
    
    public ProductItemOrder()
    {
        // Required by EF
    }

    public ProductItemOrder(int id, string productNamee, string pictureUrl)
    {
        ProductId = id;
        ProductNamee = productNamee;
        PictureUrl = pictureUrl;
    }
    public int ProductId { get; set; }

    public string ProductNamee { get; set; }

    public string PictureUrl { get; set; }
}