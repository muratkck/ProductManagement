using Microsoft.Extensions.Logging;
using ProductManagement.DTOs;
using ProductManagement.Exceptions;
using ProductManagement.Models;
using ProductManagement.Repositories;

namespace ProductManagement.Services
{
    public class ProductService(
        IRepository<Product> productRepository,
        ILogger<ProductService> logger) : IProductService
    {
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            logger.LogInformation("Fetching all products");
            var products = await productRepository.GetAllAsync();
            logger.LogInformation("Retrieved {ProductCount} products", products.Count());
            
            return products.Select(p => MapToDto(p));
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            logger.LogInformation("Fetching product with id: {ProductId}", id);
            var product = await productRepository.GetByIdAsync(id);
            if (product == null)
            {
                logger.LogWarning("Product with id {ProductId} not found", id);
                throw new NotFoundException(nameof(Product), id);
            }
            logger.LogInformation("Retrieved product with id: {ProductId}", id);

            return MapToDto(product);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            logger.LogInformation(
                "Creating product: Name={ProductName}, Price={Price}, Stock={Stock}",
                createProductDto.Name,
                createProductDto.Price,
                createProductDto.Stock);
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
            logger.LogInformation("Product created successfully with id {ProductId}", product.Id);
            
            return MapToDto(product);
        }

        public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            logger.LogInformation("Updating product with id: {ProductId}", id);
            var product = await productRepository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Product), id);
            if (product == null)
            {
                logger.LogWarning("Cannot update: Product with id {ProductId} not found", id);
                throw new NotFoundException(nameof(Product), id);
            }

            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Price = updateProductDto.Price;
            product.Stock = updateProductDto.Stock;
            product.UpdatedAt = DateTime.UtcNow;

            await productRepository.UpdateAsync(product);
            await productRepository.SaveChangesAsync();
            logger.LogInformation("Product {ProductId} updated successfully", id);

            return MapToDto(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            logger.LogInformation("Deleting product with id: {ProductId}", id);
            var product = await productRepository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Product), id);
            if (product == null)
            {
                logger.LogWarning("Cannot delete: Product with id {ProductId} not found", id);
                throw new NotFoundException(nameof(Product), id);
            }

            var result = await productRepository.DeleteAsync(id);
            if (result)
            {
                await productRepository.SaveChangesAsync();
                logger.LogInformation("Product with id {ProductId} deleted successfully", id);
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