using ConcentKitchen.Models;
using ConcentKitchen.Repository;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System;

namespace ConcentKitchen.Test.Repository
{
    public class OrderRepositoryTest
    {

        public static IEnumerable<object[]> SendOrderParameter()
        {
            yield return new object[]
            {
                new Order
                {
                  TotalPrice = 23.5F,
                  Status = "em preparo",
                  ClientId = new Guid()
                }
            };
        }

        public static IEnumerable<object[]> SendTwoOrderParameters()
        {
            yield return new object[]
            {
                new Order
                {
                  TotalPrice = 21.5F,
                  Status = "concluído",
                  ClientId = new Guid()
                },

                new Order
                {
                  TotalPrice = 17.3F,
                  Status = "cancelado",
                  ClientId = new Guid()
                }
            };
        }

        [Theory]
        [MemberData(nameof(SendOrderParameter))]
        public async void ShouldCreateAnOrder(Order order)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            OrderRepository orderRepository = new(kitchenTestContext);

            //Act
            var result = await orderRepository.Add(order);
            var orderSaved = await orderRepository.Get(result.OrderId);

            //Assert
            result.Should().Be(order);
            orderSaved.Should().BeEquivalentTo(order);
        }

        [Theory]
        [MemberData(nameof(SendOrderParameter))]
        public async void ShouldDeleteAnOrder(Order order)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            OrderRepository orderRepository = new(kitchenTestContext);

            //Act
            var result = await orderRepository.Add(order);

            var orderSaved = await orderRepository.Get(result.OrderId);
            orderSaved.Should().BeEquivalentTo(order);

            await orderRepository.Delete(result.OrderId);

            //Assert
            var orderOnDatabase = await orderRepository.Get(result.OrderId);
            orderOnDatabase.Should().Be(null);

        }

        [Theory]
        [MemberData(nameof(SendOrderParameter))]     
        public async void ShouldUpdateAnOrder(Order order)
        {
            //Arrange
            var newTotalPrice = 100.5F;
            KitchenTestContext kitchenTestContext = new();
            OrderRepository orderRepository = new(kitchenTestContext);

            //Act
            var result = await orderRepository.Add(order);

            var orderSaved = await orderRepository.Get(result.OrderId);
            orderSaved.Should().BeEquivalentTo(order);
            orderSaved!.TotalPrice = newTotalPrice;

            await orderRepository.Update(orderSaved);

            //Assert
            var orderOnDatabase = await orderRepository.Get(result.OrderId);
            orderOnDatabase!.TotalPrice.Should().Be(newTotalPrice);
        }

        [Theory]
        [MemberData(nameof(SendOrderParameter))]
        public async void ShouldGetAnOrder(Order order)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            OrderRepository orderRepository = new(kitchenTestContext);

            //Act
            var result = await orderRepository.Add(order);
            var orderSaved = await orderRepository.Get(result.OrderId);

            //Assert
            orderSaved.Should().BeEquivalentTo(order);
        }

        [Theory]
        [MemberData(nameof(SendTwoOrderParameters))]
        public async void ShouldGetAllOrders(Order order, Order secondOrder)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            OrderRepository orderRepository = new(kitchenTestContext);

            var orders = new Order[] { order, secondOrder };

            //Act
            await orderRepository.Add(order);
            await orderRepository.Add(secondOrder);
            var savedOrders = await orderRepository.GetAll();

            //Assert
            savedOrders.Should().BeEquivalentTo(orders);
        }
    }
}