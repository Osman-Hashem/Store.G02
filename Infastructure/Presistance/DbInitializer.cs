using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistance.Data;

namespace Presistance
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;   
      
        public DbInitializer(StoreDbContext context)
        {
            _context = context;
        }

       

        public async Task InitializeAsync() 
        {
        
            try
            {
                // Create DB If It Dosen't Exists && Apply To Any Pending Migrations 

                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }

                // Data Seeding 

                // Seeding ProductTypes From Json File 

                if (!_context.ProductType.Any())
                {
                    // 1- Read All Data From Types Json File As String 

                    var typesData = await File.ReadAllTextAsync(@"D:\course route\Store\Store.G02\Infastructure\Presistance\Data\Seeding\types.json");

                    // 2- Transform String To C# Objects [List <ProductTypes>]

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    // 3- Add List <ProductTypes> To DB 

                    if (types is not null && types.Any())
                    {
                        await _context.ProductType.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }

                }

                // Seeding ProductBrands From Json File 

                if (!_context.ProductBrands.Any())
                {
                    // 1- Read All Data From brands Json File As String 

                    var brandsData = await File.ReadAllTextAsync(@"D:\course route\Store\Store.G02\Infastructure\Presistance\Data\Seeding\brands.json");

                    // 2- Transform String To C# Objects [List <ProductBrand>]

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    // 3- Add List <ProductBrand> To DB 

                    if (brands is not null && brands.Any())
                    {
                        await _context.ProductBrands.AddRangeAsync(brands);
                        await _context.SaveChangesAsync();
                    }

                }

                // Seeding Products From Json File 

                if (!_context.Products.Any())
                {
                    // 1- Read All Data From Products Json File As String 

                    var productsData = await File.ReadAllTextAsync(@"D:\course route\Store\Store.G02\Infastructure\Presistance\Data\Seeding\products.json");

                    // 2- Transform String To C# Objects [List <Products>]

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    // 3- Add List <Products> To DB 

                    if (products is not null && products.Any())
                    {
                        await _context.Products.AddRangeAsync(products);
                        await _context.SaveChangesAsync();
                    }

                }

                // ..\Infastructure\Presistance\Data\Seeding\types.json
            }
            catch (Exception)
            {
                throw;
            }

        }
        
    }
}
