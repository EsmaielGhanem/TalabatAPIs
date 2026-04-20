using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.APIs.Interfaces.IRepositories;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Service;

namespace Talabat.APIs.Extensions;

public static class ApplicationServisesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        
        services.AddSingleton<IResponseCacheService, ResponseCacheService>(); 
        
        
        services.AddScoped(typeof(ITokenService), typeof(TokenServices)); 
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork)); 
        services.AddScoped(typeof(IOrderService) , typeof(OrderService)); 
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));


        services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));   
        
        
        
        services.AddScoped(typeof(IPaymentService), typeof(IPaymentService));   
        
        
        
        services.AddControllers().ConfigureApiBehaviorOptions(option =>
        {
            option.InvalidModelStateResponseFactory = (actionContext) =>
            {
                // Dictionary of model states that have errors 
                var errors = actionContext.ModelState.Where(M
                        => M.Value.Errors.Count() > 0)
                    .SelectMany(M => M.Value.Errors)
                    .Select(E => E.ErrorMessage).ToArray();
                var validationErrorResponse = new ApivalidationResponse()
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(validationErrorResponse);
            };
        });
        return services;
    }
}