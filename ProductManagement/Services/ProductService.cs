using ProductManagement.DTOs;
using ProductManagement.Exceptions;
using ProductManagement.Models;
using ProductManagement.Repositories;

namespace ProductManagement.Services
{
    public class ProductService(IRepository<Product> productRepository) : IProductService
    {
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await productRepository.GetAllAsync();
            return products.Select(p => MapToDto(p));
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Product), id);
            return MapToDto(product);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                Stock = createProductDto.Stock,
                CreatedAt = DateTime.UtcNow
            };

            await productRepository.AddAsync(product);
            await productRepository.SaveChangesAsync();

            return MapToDto(product);
        }

        public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            var product = await productRepository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Product), id);

            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Price = updateProductDto.Price;
            product.Stock = updateProductDto.Stock;
            product.UpdatedAt = DateTime.UtcNow;

            await productRepository.UpdateAsync(product);
            await productRepository.SaveChangesAsync();

            return MapToDto(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Product), id);
            var result = await productRepository.DeleteAsync(id);
            if (result)
            {
                await productRepository.SaveChangesAsync();
            }
            return result;
        }

        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
}