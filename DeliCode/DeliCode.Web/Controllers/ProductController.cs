using DeliCode.Web.Services;
using DeliCode.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliCode.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public ProductController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAll();
            return View(products);
        }
        public async Task<IActionResult> DetailsAsync(Guid productId)
        {
            var product = await _productService.Get(productId);
            if (product == null)
            {
                return RedirectToAction("Index", "Product");
            }
            return View(product);

        }
        [HttpPost]
        public async Task<IActionResult> AddToCartAsync([FromForm] Guid Id)
        {
            var product = await _productService.Get(Id);
            if (product == null)
            {
                return BadRequest("produkten fanns inte");
            }

            Cart cart = await _cartService.AddProductToCart(product);
            OrderSummary.Cart = cart;

            if (cart == null)
            {
                return BadRequest("produkten kunde inte l�ggas till");
            }

            return RedirectToAction("Index", "Product");
        }
    }
}
