using ConcentKitchen.Models;
using ConcentKitchen.Repository;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System;

namespace ConcentKitchen.Test.Repository
{
    public class DishRepositoryTest
    {

        public static IEnumerable<object[]> SendDishParameter()
        {
            yield return new object[]
            {
                new Dish
                {
                  DishName = "Pudim",
                  DishPrice = 15.5F,
                  DishPreparationTimeInMinutes = 20,
                  DishCategory = "sobremesa",
                  DishIngredients = "Clara de ovo, açúcar e mais açúcar.",
                }
            };
        }

        public static IEnumerable<object[]> SendTwoDishesParameters()
        {
            yield return new object[]
            {
                new Dish
                {
                  DishName = "Pão com manteiga",
                  DishPrice = 3.5F,
                  DishPreparationTimeInMinutes = 5,
                  DishCategory = "massa",
                  DishIngredients = "Pão e manteiga.",
                },

                new Dish
                {
                  DishName = "Salada de alface",
                  DishPrice = 1.5F,
                  DishPreparationTimeInMinutes = 5,
                  DishCategory = "salada",
                  DishIngredients = "Alface",
                }
            };
        }

        [Theory]
        [MemberData(nameof(SendDishParameter))]
        public async void ShouldCreateADish(Dish dish)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            DishRepository dishRepository = new(kitchenTestContext);

            //Act
            var result = await dishRepository.Add(dish);
            var dishSaved = await dishRepository.Get(result.DishId);

            //Assert
            result.Should().Be(dish);
            dishSaved.Should().BeEquivalentTo(dish);
        }

        [Theory]
        [MemberData(nameof(SendDishParameter))]
        public async void ShouldDeleteADish(Dish dish)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            DishRepository dishRepository = new(kitchenTestContext);

            //Act
            var result = await dishRepository.Add(dish);

            var dishSaved = await dishRepository.Get(result.DishId);
            dishSaved.Should().BeEquivalentTo(dish);

            await dishRepository.Delete(result.DishId);

            //Assert
            var dishOnDatabase = await dishRepository.Get(result.DishId);
            dishOnDatabase.Should().Be(null);

        }

        [Theory]
        [MemberData(nameof(SendDishParameter))]     
        public async void ShouldUpdateADish(Dish dish)
        {
            //Arrange
            var newPrice = 100.5F;
            KitchenTestContext kitchenTestContext = new();
            DishRepository dishRepository = new(kitchenTestContext);

            //Act
            var result = await dishRepository.Add(dish);

            var dishSaved = await dishRepository.Get(result.DishId);
            dishSaved.Should().BeEquivalentTo(dish);
            dishSaved!.DishPrice = newPrice;

            await dishRepository.Update(dishSaved);

            //Assert
            var dishOnDatabase = await dishRepository.Get(result.DishId);
            dishOnDatabase!.DishPrice.Should().Be(newPrice);
        }

        [Theory]
        [MemberData(nameof(SendDishParameter))]
        public async void ShouldGetADish(Dish dish)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            DishRepository dishRepository = new(kitchenTestContext);

            //Act
            var result = await dishRepository.Add(dish);
            var dishSaved = await dishRepository.Get(result.DishId);

            //Assert
            dishSaved.Should().BeEquivalentTo(dish);
        }

        [Theory]
        [MemberData(nameof(SendTwoDishesParameters))]
        public async void ShouldGetAllDishes(Dish dish, Dish secondDish)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            DishRepository dishRepository = new(kitchenTestContext);

            var dishes = new Dish[] { dish, secondDish };

            //Act
            await dishRepository.Add(dish);
            await dishRepository.Add(secondDish);
            var savedDishes = await dishRepository.GetAll();

            //Assert
            savedDishes.Should().BeEquivalentTo(dishes);

        }
    }
}