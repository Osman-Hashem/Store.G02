using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Specifications
{
    public class ProductWithCountSpecifications : BaseSpecifications<Product , int >
    {
        public ProductWithCountSpecifications(ProductSpecificationsParemeters SpecParams)
            : base(
                    P =>
                       (string.IsNullOrEmpty(SpecParams.Search) || P.Name.ToLower().Contains(SpecParams.Search.ToLower())) &&
                       (!SpecParams.BrandId.HasValue || P.BrandId == SpecParams.BrandId) &&
                       (!SpecParams.TypeId.HasValue || P.TypeId == SpecParams.TypeId)
                  )
        {
            
        }
    }
}
