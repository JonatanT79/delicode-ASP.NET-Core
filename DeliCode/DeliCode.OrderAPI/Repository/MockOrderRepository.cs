﻿using DeliCode.Library.Models;
using DeliCode.OrderAPI.Models;
using DeliCode.OrderAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("DeliCode.OrderAPI.Tests")]
namespace DeliCode.OrderAPI.Repository
{
    public class MockOrderRepository : IOrderRepository
    {
        internal List<Order> orders = new List<Order>()
        {
            new Order()
           {
                Id = new Guid("fb6f6dd2-f6c5-4893-ab35-03167f6ebe28"),
                OrderDate = new DateTime(2020, 11, 20),
                Status = OrderStatus.Delivered,
                UserId = "11223344-5566-7788-99AA-BBCCDDEEFF00",
                Email = "marie.dahlmalm@iths.se",
                FirstName = "Marie",
                LastName = "Dahlmalm",
                Address = "Årstaängsvägen 9",
                ZipCode = "12345",
                City = "Stockholm",
                Country = "Sweden",
                Phone = "555123456",
                ShippingNotes = "",
                OrderProducts = new List<OrderProduct>()
                {
                     new OrderProduct()
                    {
                        Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"),
                        Name = "Kladdkaka",
                        Quantity = 11,
                        Price = 11.99M,
                        OrderId = new Guid("fb6f6dd2-f6c5-4893-ab35-03167f6ebe28")
                    },
                     new OrderProduct()
                    {
                        Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF11"),
                        Name = "Cheesecake",
                        Quantity = 2,
                        Price = 29M,
                        OrderId = new Guid("fb6f6dd2-f6c5-4893-ab35-03167f6ebe28")
                     }
                }
            },
            new Order{
                Id=new Guid("ed9ef515-8735-4116-b444-8a42b187bbfa") ,
                UserId="d514be83-bebb-4fe7-b905-e8db158a9ffd"
            }
        };

        public Task<Order> GetOrderById(Guid id)
        {
            Order order = orders.Where(x => x.Id == id).SingleOrDefault();
            return Task.FromResult(order);
        }

        public Task<Order> AddOrder(Order order)
        {
            order.Id = Guid.NewGuid();

            return Task.FromResult(order);
        }

        public Task<List<Order>> GetAllOrdersByUserId(string userId)
        {
            List<Order> ordersList = orders.Where(x => x.UserId == userId).OrderBy(d => d.OrderDate).ToList();
            return Task.FromResult(ordersList);
        }
        public List<Order> DeleteOrderByOrderId(Guid id)
        {
            var orderToDelete = orders.Where(x => x.Id == id).SingleOrDefault();
            orders.Remove(orderToDelete);
            return orders;
        }

        public Task<List<Order>> GetAllOrders()
        {
            return Task.FromResult(orders);
        }
        public Task<Order> UpdateOrder(Order order)
        {
            var orderToUpdate = orders.Where(x => x.Id == order.Id).SingleOrDefault();
            if (orderToUpdate != null)
            {
                orderToUpdate = order;
            }

            return Task.FromResult(orderToUpdate);
        }
    }
}