using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.APIs.Interfaces.IRepositories;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductType> _typesRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepo 
            , IGenericRepository<ProductBrand> brandsRepo
            , IGenericRepository<ProductType> typesRepo 
            , IMapper mapper )
        {
            _productRepo = ProductRepo;
            _brandsRepo = brandsRepo;
            _typesRepo = typesRepo;
            _mapper = mapper;
        }


        
        [CachedAttribute(600)]
        [HttpGet]
        // [Authorize]
        public async Task<ActionResult<IReadOnlyList<Pagination<ProductToReturnDto>>>> GetAll([FromQuery]ProductSpecParams productSpecParams)
        {
            var spec = new ProductWithBrandAndTypeSpecification(productSpecParams);  
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            if (products == null) return NotFound(new ApiResponse(404));
            var countSpec = new ProductWIthFiltersForCountSpecifications(productSpecParams);
            var count = await _productRepo.GetCountAsync(countSpec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(new Pagination<ProductToReturnDto>(productSpecParams.PageIndex , productSpecParams.PageSize , count , data)); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetById(int id)  
        {
            var spec = new ProductWithBrandAndTypeSpecification(id );

            var product = await _productRepo.GetByIdWithSpecAsync(spec);

            if (product == null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product , ProductToReturnDto> (product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandsRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]

        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _typesRepo.GetAllAsync();
            return Ok(types);
        }
      
    }
}
