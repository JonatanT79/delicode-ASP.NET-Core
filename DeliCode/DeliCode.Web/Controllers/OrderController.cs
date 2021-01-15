﻿using DeliCode.Web.Models;
using DeliCode.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliCode.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            if(OrderSummary.Cart == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(OrderSummary.Cart);
        }
        public IActionResult ConfirmOrder(string shipment, string payment)
        { 
            if (shipment == "send")
            {
                return RedirectToAction("ShipmentAddress", "Order");
            }
            return View(OrderSummary.Cart);
        }
        [HttpGet]
        public IActionResult ShipmentAddress()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ShipmentAddress(string address, string zipCode, string phoneNumber)
        {
            return RedirectToAction("ConfirmOrder", "Order");
        }
    }
}
