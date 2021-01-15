﻿using DeliCode.Library.Models;
using DeliCode.Web.Controllers;
using DeliCode.Web.Models;
using DeliCode.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DeliCode.Web.Tests
{
    public class UnitTestsCartController
    {
        private CartController _controller;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly MockCartRepository _cartRepository;
        private readonly MockProductRepository _productRepository;
        private HttpClient _httpClient;
        public UnitTestsCartController()
        {
            var productAPIUrl = "https://localhost:44333";

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(productAPIUrl);
            _productService = new ProductService(_httpClient);
            _productRepository = new MockProductRepository();
            _cartRepository = new MockCartRepository();
            _cartService = new CartService(_cartRepository);
            _controller = new CartController(_productService, _cartService);
        }

        [Fact]
        public async Task AddProductToCart_ProductNotExists_ReturnsBadRequest()
        {
            var idNotInProductDb = Guid.NewGuid();
            var result = await _controller.AddAsync(idNotInProductDb);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task AddProductToCart_ProductExistsInProductDb_ReturnsOk()
        {
            var idInProductDb = new Guid("BD8F361D-E5E3-4F33-82CF-2594368D78C3");
            var result = await _controller.AddAsync(idInProductDb);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetCart_ReturnsCart()
        {
            var action = await _controller.GetAsync();
            var result = action as OkObjectResult;
            var actualCart = result.Value as Cart;

            Assert.IsType<Cart>(actualCart);
            Assert.NotEqual(Guid.Empty, actualCart.SessionId);
        }

        [Fact]
        public async Task GetCart_ReturnsOk()
        {
            var result = await _controller.GetAsync();
         
            Assert.IsType<OkObjectResult>(result);
        }
    }
}