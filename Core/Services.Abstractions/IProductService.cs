using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Services.Abstractions
{
    public interface IProductService
    {
        // Get All Product 
        //Task<IEnumerable<ProductResultDto>> SpecParamsGetAllProductsAsync(int? brandId, int? typeId, string? sort, int pageIndex = 1, int pageSize = 5) ;
        Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParemeters SpecParams) ;   

        // Get Product By ID
        Task<ProductResultDto?>GetProductByIdAsync(int id);

        // Get All Brands
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();

        // Get All Types
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync(); 

    }
}
