using DeliCode.Library.Models;
using DeliCode.ProductAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;



namespace DeliCode.ProductAPI.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void GetAllProductsShouldReturnListOfProducts()
        {
            ProductRepository repos = new ProductRepository();
            var expected = repos.products;
            List<Product> actual = repos.GetAllProducts();
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetProductShouldReturnSingleProduct()
        {
            ProductRepository repos = new ProductRepository();
            var expected = repos.products
                .Where(e => e.Id == new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"))
                .SingleOrDefault();
            Product product = repos.GetProduct(new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"));
            Assert.Equal(expected, product);
        }
        [Fact]
        public void AddProductShouldAddNewProduct()
        {
            ProductRepository repos = new ProductRepository();
            Product product = new Product { Id = Guid.NewGuid(), Name = "NyProduct", Description = "", Price = 150, ImageUrl = "#" };
            int expected = repos.products.Count() + 1;
            int actual = repos.AddProduct(product).Count();
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void DeleteProductShouldDeleteProductFromList()
        {
            ProductRepository repos = new ProductRepository();
            int expected = repos.products.Count() - 1;
            int actual = repos.DeleteProduct(new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00")).Count();
            Assert.Equal(expected, actual);
        }

        //Tests:

        // PUT: api/Products/5
        // PutProduct(Guid id, Product product)

        // ProductExists(Guid id)
    }
}