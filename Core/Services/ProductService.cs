using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Services.Specifications;
using Shared;

namespace Services
{
    #region Error
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {

        public async Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParemeters SpecParams)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(SpecParams);

            
            // Get ALl Products Through ProductRepository 

            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);


            var specCount = new ProductWithCountSpecifications(SpecParams);

            var count = await unitOfWork.GetRepository<Product , int>().CountAsync(specCount);

            // Mapping IEnumrable<Product> To <IEnumerable<ProductResultDto>> : Automapper

            var result = mapper.Map<IEnumerable<ProductResultDto>>(products);

            return new PaginationResponse<ProductResultDto>(SpecParams.PageIndex ,SpecParams.PageSize , count , result);
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {

            var spec = new ProductWithBrandsAndTypesSpecifications(id);

            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(spec);
            if (product is null) return null;

            var result = mapper.Map<ProductResultDto>(product);
            return result;
        }

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return result;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<TypeResultDto>>(types);
            return result;
        }

        
    }
    #endregion

    #region Test
    //public class ProductService : IProductService
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly IMapper _mapper;

    //    // Constructor to inject dependencies
    //    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    //    {
    //        _unitOfWork = unitOfWork;
    //        _mapper = mapper;
    //    }

    //    public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
    //    {
    //        // Get All Products Through ProductRepository
    //        var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();

    //        // Mapping IEnumerable<Product> to IEnumerable<ProductResultDto> using AutoMapper
    //        var result = _mapper.Map<IEnumerable<ProductResultDto>>(products);
    //        return result;
    //    }

    //    public async Task<ProductResultDto?> GetProductByIdAsync(int id)
    //    {
    //        var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
    //        if (product is null) return null;

    //        var result = _mapper.Map<ProductResultDto>(product);
    //        return result;
    //    }

    //    public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
    //    {
    //        var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
    //        var result = _mapper.Map<IEnumerable<BrandResultDto>>(brands);
    //        return result;
    //    }

    //    public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
    //    {
    //        var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
    //        var result = _mapper.Map<IEnumerable<TypeResultDto>>(types);
    //        return result;
    //    }
    //}
    #endregion

}
