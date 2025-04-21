using Domain.Models;
using Shared;

namespace Services.Specifications
{
    public class ProductWithBrandsAndTypesSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandsAndTypesSpecifications(int id) : base(P => P.Id == id)
        {
            ApplyIncluds();
        }
        public ProductWithBrandsAndTypesSpecifications(ProductSpecificationsParemeters SpecParams)
            : base(
                  P => 
                       (string.IsNullOrEmpty(SpecParams.Search) || P.Name.ToLower().Contains(SpecParams.Search.ToLower())) &&
                       (!SpecParams.BrandId.HasValue || P.BrandId == SpecParams.BrandId) &&
                       (!SpecParams.TypeId.HasValue || P.TypeId == SpecParams.TypeId)
                  )
        {
            ApplyIncluds();
            ApplySorting(SpecParams.Sort);
            ApplyPagination(SpecParams.PageIndex, SpecParams.PageSize);
        }

        private void ApplyIncluds()
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

        private void ApplySorting( string? sort )
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "namedesc":
                        AddOrderByDescending(P => P.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "Pricedesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }
        }

    }
}
