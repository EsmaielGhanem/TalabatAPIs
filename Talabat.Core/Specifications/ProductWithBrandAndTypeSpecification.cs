using Talabat.Core.Entities;

namespace Talabat.Core.Specifications;

public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
{
    // Get All 
    public ProductWithBrandAndTypeSpecification(ProductSpecParams productSpecParams) : base(P => 
                (string.IsNullOrEmpty(productSpecParams.Search) || P.Name.ToLower().Contains(productSpecParams.Search))&&
                (!productSpecParams.BrandId.HasValue || P.ProductBrandId == productSpecParams.BrandId ) &&
                (!productSpecParams.TypeId.HasValue || P.ProductTypeId == productSpecParams.TypeId )
        )
    {
        Includes.Add(P => P.ProductBrand);
        Includes.Add(P => P.ProductType);
        
        
      

        ApplyPagination(productSpecParams.PageSize*(productSpecParams.PageIndex - 1) , productSpecParams.PageSize);
        
        
        if (!string.IsNullOrEmpty(productSpecParams.Sort))
        {
            switch (productSpecParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(P => P.Price);
                    break;
                case "priceDesc":
                    AddOrderByDesc(P => P.Price);
                    break;
                default:AddOrderBy(P => P.Name);
                    break;
                
                
            }
            
        }
    }
    
    // Get By Id
    public ProductWithBrandAndTypeSpecification(int id) : base(P => P.Id == id)
    {
        Includes.Add(P => P.ProductBrand);
        Includes.Add(P => P.ProductType);
    }

}