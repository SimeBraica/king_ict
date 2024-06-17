﻿using BAL;
using DAL.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {



        private readonly ProductService _productService;

        public ProductController(HttpClient httpClient) {
            _productService = new ProductService(httpClient);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Product() {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<IEnumerable<Product>>> ProductByTitle(string title) {
            var product = await _productService.GetProduct(title);
            return Ok(product);
        }
    }
}