using ConcentKitchen.Models;
using ConcentKitchen.Repository;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System;

namespace ConcentKitchen.Test.Repository
{
    public class ClientRepositoryTest
    {

        public static IEnumerable<object[]> SendClientParameters()
        {
            yield return new object[]
            {
                new Client
                {
                  TableNumber = 1,
                  Name = "Max",
                  Cpf = "776.059.730-47",
                }
            };
        }

        public static IEnumerable<object[]> SendLoginParameters()
        {
            yield return new object[]
            {
                new Client
                {
                  TableNumber = 1,
                  Name = "Max",
                  Cpf = "776.059.730-47",
                },

                new LoginData
                {
                  Name = "Max",
                  Cpf = "776.059.730-47",
                }
            };
        }

        public static IEnumerable<object[]> SendTwoClientsParameters()
        {
            yield return new object[]
            {
                new Client
                {
                  TableNumber = 1,
                  Name = "Max",
                  Cpf = "776.059.730-47",
                },

                new Client
                {
                  TableNumber = 2,
                  Name = "Otávio",
                  Cpf = "387.058.620-60",
                }
            };
        }
        
        [Theory]
        [MemberData(nameof(SendClientParameters))]
        public async void ShouldCreateAClient(Client client)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            ClientRepository clientRepository = new(kitchenTestContext);

            //Act
            var result = await clientRepository.Add(client);
            var clientSaved = await clientRepository.Get(result.ClientId);

            //Assert
            result.Should().Be(client);
            clientSaved.Should().BeEquivalentTo(client);
        }

        [Theory]
        [MemberData(nameof(SendClientParameters))]
        public async void ShouldDeleteAClient(Client client)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            ClientRepository clientRepository = new(kitchenTestContext);

            //Act
            var result = await clientRepository.Add(client);

            var clientSaved = await clientRepository.Get(result.ClientId);
            clientSaved.Should().BeEquivalentTo(client);

            await clientRepository.Delete(result.ClientId);

            //Assert
            var clientOnDatabase = await clientRepository.Get(result.ClientId);
            clientOnDatabase.Should().Be(null);

        }

        [Theory]
        [MemberData(nameof(SendClientParameters))]     
        public async void ShouldUpdateAClient(Client client)
        {
            //Arrange
            var newTable = 4;
            KitchenTestContext kitchenTestContext = new();
            ClientRepository clientRepository = new(kitchenTestContext);

            //Act
            var result = await clientRepository.Add(client);

            var clientSaved = await clientRepository.Get(result.ClientId);
            clientSaved.Should().BeEquivalentTo(client);
            clientSaved!.TableNumber = newTable;

            await clientRepository.Update(clientSaved);

            //Assert
            var clientOnDatabase = await clientRepository.Get(result.ClientId);
            clientOnDatabase!.TableNumber.Should().Be(newTable);
        }

        [Theory]
        [MemberData(nameof(SendClientParameters))]
        public async void ShouldGetAClient(Client client)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            ClientRepository clientRepository = new(kitchenTestContext);

            //Act
            var result = await clientRepository.Add(client);
            var clientSaved = await clientRepository.Get(result.ClientId);

            //Assert
            clientSaved.Should().BeEquivalentTo(client);
            clientSaved.Should().BeOfType<Client>();
        }

        [Theory]
        [MemberData(nameof(SendLoginParameters))]
        public async void ShouldLoginAClient(Client client, LoginData loginData)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            ClientRepository clientRepository = new(kitchenTestContext);

            //Act
            await clientRepository.Add(client);
            var clientSaved = await clientRepository.Login(loginData);

            //Assert
            clientSaved.Should().BeEquivalentTo(client);
            clientSaved.Should().BeOfType<Client>();
        }

        [Theory]
        [MemberData(nameof(SendTwoClientsParameters))]
        public async void ShouldGetAllClients(Client client, Client secondClient)
        {
            //Arrange
            KitchenTestContext kitchenTestContext = new();
            ClientRepository clientRepository = new(kitchenTestContext);

            var clients = new Client[] { client, secondClient };

            //Act
            await clientRepository.Add(client);
            await clientRepository.Add(secondClient);
            var savedClients = await clientRepository.GetAll();

            //Assert
            savedClients.Should().BeEquivalentTo(clients);

        }
    }
}