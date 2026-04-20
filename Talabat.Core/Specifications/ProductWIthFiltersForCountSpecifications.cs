using Talabat.Core.Entities;

namespace Talabat.Core.Specifications;

public class ProductWIthFiltersForCountSpecifications :BaseSpecification<Product>
{
    public ProductWIthFiltersForCountSpecifications(ProductSpecParams productSpecParams) : base( P => 
        (!productSpecParams.BrandId.HasValue || P.ProductBrandId == productSpecParams.BrandId ) &&
        (!productSpecParams.TypeId.HasValue || P.ProductTypeId == productSpecParams.TypeId ))
    {
        
    }
    
}