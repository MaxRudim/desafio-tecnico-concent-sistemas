using ConcentKitchen.Models;
using ConcentKitchen.Repository;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System;

namespace ConcentKitchen.Test.Repository
{
    public class OrderDishRepositoryTest
    {

        public static IEnumerable<object[]> SendOrderDishParameter()
        {
            yield return new object[]
            {
                new OrderDish
                {
                  DishId = new Guid(),
                  OrderId = new Guid()
                }
            };
        }

        public static IEnumerable<object[]> SendTwoOrderDishParameters()
        {
            yield return new object[]
            {
                new OrderDish
                {
                  DishId = new Guid(),
                  OrderId = new Guid()
                },

                new OrderDish
                {
                  DishId = new Guid(),
                  OrderId = new Guid()
                }
            };
        }

        [Theory]
        [MemberData(nameof(SendOrderDishParameter))]
        public async void ShouldCreateAnOrderDish(OrderDish orderdish)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            OrderDishRepository orderDishRepository = new(kitchenTestContext);

            //Act
            var result = await orderDishRepository.Add(orderdish);
            var orderDishSaved = await orderDishRepository.Get(result.Id);

            //Assert
            result.Should().Be(orderdish);
            orderDishSaved.Should().BeEquivalentTo(orderdish);
        }

        [Theory]
        [MemberData(nameof(SendOrderDishParameter))]
        public async void ShouldDeleteAnOrderDish(OrderDish orderdish)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            OrderDishRepository orderdishRepository = new(kitchenTestContext);

            //Act
            var result = await orderdishRepository.Add(orderdish);

            var orderdishSaved = await orderdishRepository.Get(result.Id);
            orderdishSaved.Should().BeEquivalentTo(orderdish);

            await orderdishRepository.Delete(result.Id);

            //Assert
            var orderdishOnDatabase = await orderdishRepository.Get(result.Id);
            orderdishOnDatabase.Should().Be(null);

        }

        [Theory]
        [MemberData(nameof(SendOrderDishParameter))]
        public async void ShouldGetAnOrderDish(OrderDish orderdish)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            OrderDishRepository orderdishRepository = new(kitchenTestContext);

            //Act
            var result = await orderdishRepository.Add(orderdish);
            var orderdishSaved = await orderdishRepository.Get(result.Id);

            //Assert
            orderdishSaved.Should().BeEquivalentTo(orderdish);
        }

        [Theory]
        [MemberData(nameof(SendTwoOrderDishParameters))]
        public async void ShouldGetAllOrderDishes(OrderDish orderdish, OrderDish secondOrderDish)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            OrderDishRepository orderdishRepository = new(kitchenTestContext);

            var orderdishes = new OrderDish[] { orderdish, secondOrderDish };

            //Act
            await orderdishRepository.Add(orderdish);
            await orderdishRepository.Add(secondOrderDish);
            var savedorderdishes = await orderdishRepository.GetAll();

            //Assert
            savedorderdishes.Should().BeEquivalentTo(orderdishes);
        }
    }
}