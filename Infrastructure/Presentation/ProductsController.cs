﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Presentation
{
    // API Controller
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        // EndPoint : Public non-static Method


        [HttpGet] // EndPoint : GET : /api/products
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecificationsParemeters SpecParams)
        {
            var result = await serviceManager.ProductService.GetAllProductsAsync(SpecParams);
            if (result is null) return BadRequest(); // 400
            return Ok(result); //200 
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await serviceManager.ProductService.GetProductByIdAsync(id);
            if (result is null) return NotFound(); //404
            return Ok(result);
        }

        [HttpGet ("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await serviceManager.ProductService.GetAllBrandsAsync();
            if (result is null) return BadRequest(); //400
            return Ok(result);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await serviceManager.ProductService.GetAllTypesAsync();
            if (result is null) return BadRequest(); //400
            return Ok(result);
        }




    }
}
