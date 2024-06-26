﻿using DAL;
using DAL.Models;
using DTO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
namespace BAL {
    public class ProductService {


        private readonly HttpClient? _httpClient;

        public ProductService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<ProductDto>> GetAllProducts() {
            using (var repo = new ProductRepository(_httpClient)) {
                var products = await repo.GetProductsAsync();
                return products.Select(p => new ProductDto {
                    Title = p.Title,
                    Images = p.Images,
                    ShortDescription = p.Description.Length > 100 ? p.Description.Substring(0, 100) : p.Description,
                    Price = p.Price
                }).ToList();
            }
        }


        public async Task<List<Product>> GetProduct(int id) {
            using (var repo = new ProductRepository(_httpClient)) {
                var products = await repo.GetProductByIdAsync(id);
                return products.ToList();
            }
        }


        public async Task<List<Product>> FilterProducts(string category, decimal price) {
            using (var repo = new ProductRepository(_httpClient)) {
                var products = await repo.GetProductWithFiltersAsync(category, price);
                return products.ToList();
            }
        }
        

        public async Task<List<Product>> SearchProducts(string searchTerm) {
            using (var repo = new ProductRepository(_httpClient)) {
                var products = await repo.GetProductWithSearchAsync(searchTerm);
                return products.ToList();
            }
        }
    }
}