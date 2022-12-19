using ConcentKitchen.Models;
using ConcentKitchen.Test.Repository;
using ConcentKitchen.Services;
using ConcentKitchen.Repository;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;

/*

--> Obs: Por questão de produtividade, apenas produzi o teste do service de client.

         Para as demais entidades, a lógica seria semelhante a esta.

*/

namespace ConcentKitchen.Test
{
    public class ClientServiceTest
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

        public static IEnumerable<object[]> SendClientWrongParameters()
        {
            yield return new object[]
            {
                new Client
                {
                  TableNumber = 1,
                  Name = "Max",
                  Cpf = "invalido",
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
            var context = new KitchenTestContext();
            var mockRepository = new Mock<IClientRepository>();

            // mockRepository.Setup(library => library.GetUserByLoginName(login)).ReturnsAsync(value: null);
            mockRepository.Setup(library => library.Add(client)).ReturnsAsync(client);

            var service = new ClientService(mockRepository.Object);

            //Act
            var result = await service.CreateClient(client);

            //Assert
            result.TableNumber.Should().Be(client.TableNumber);
            result.Name.Should().Be(client.Name);
            result.Cpf.Should().Be(client.Cpf);
        }

        [Theory]
        [MemberData(nameof(SendClientWrongParameters))]
        public async void ShouldThrownAnErrorWhenClientHasInvalidCpf(Client client)
        {
            //Arrange
            var context = new KitchenTestContext();
            var mockRepository = new Mock<IClientRepository>();

            mockRepository.Setup(library => library.Add(client)).ReturnsAsync(client);

            var service = new ClientService(mockRepository.Object);

            //Act
            Func<Task> act = async () => await service.CreateClient(client);

            //Assert
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Cpf inválido");
        }

        [Theory]
        [MemberData(nameof(SendClientParameters))]
        public async void ShouldDeleteAClient(Client client)
        {
            //Arrange
            var context = new KitchenTestContext();
            var mockRepository = new Mock<IClientRepository>();

            mockRepository.Setup(library => library.Get(client.ClientId)).ReturnsAsync(client);
            mockRepository.Setup(library => library.Add(client)).ReturnsAsync(client);


            var service = new ClientService(mockRepository.Object);

            //Act
            Func<Task> act = async () => await service.DeleteClient(client.ClientId);

            //Assert
            await act.Should().NotThrowAsync<InvalidOperationException>();
        }

        [Theory]
        [MemberData(nameof(SendClientParameters))]
        public async void ShouldThrownAnErrorWhenClientDoesntExist(Client client)
        {
            //Arrange
            var context = new KitchenTestContext();
            var mockRepository = new Mock<IClientRepository>();

            mockRepository.Setup(library => library.Get(client.ClientId)).ReturnsAsync(value: null);

            var service = new ClientService(mockRepository.Object);

            //Act
            Func<Task> act = async () => await service.DeleteClient(client.ClientId);

            //Assert
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Este cliente não existe");
        }

        [Theory]
        [MemberData(nameof(SendClientParameters))]
        public async void ShouldGetAClient(Client client)
        {
            //Arrange
            var context = new KitchenTestContext();
            var mockRepository = new Mock<IClientRepository>();

            mockRepository.Setup(library => library.Get(client.ClientId)).ReturnsAsync(client);

            var service = new ClientService(mockRepository.Object);

            //Act
            var output = await service.GetClient(client.ClientId.ToString());

            //Assert
            output.Should().BeOfType<Client>();
            output.Should().Be(client);
        }

        [Theory]
        [MemberData(nameof(SendClientParameters))]
        public async void ShouldThrownAnErrorWhenClientIsNotFound(Client client)
        {
            //Arrange
            var context = new KitchenTestContext();
            var mockRepository = new Mock<IClientRepository>();

            mockRepository.Setup(library => library.Get(client.ClientId)).ReturnsAsync(value: null);

            var service = new ClientService(mockRepository.Object);

            //Act
            Func<Task> act = async () => await service.GetClient(client.ClientId.ToString());

            //Assert
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("O cliente não existe");
        }

        [Theory]
        [MemberData(nameof(SendTwoClientsParameters))]
        public async void ShouldGetAllClients(Client client, Client secondClient)
        {
            //Arrange
            var context = new KitchenTestContext();
            var mockRepository = new Mock<IClientRepository>();
            var clients = new List<Client>(){client, secondClient};

            mockRepository.Setup(library => library.Add(client)).ReturnsAsync(client);
            mockRepository.Setup(library => library.Add(secondClient)).ReturnsAsync(secondClient);
            mockRepository.Setup(library => library.GetAll()).ReturnsAsync(clients);

            var service = new ClientService(mockRepository.Object);

            //Act
            await service.CreateClient(client);
            await service.CreateClient(secondClient);
            var output = await service.GetAllClients();

            //Assert
            output.Should().BeOfType<List<Client>>();
            output.Should().Contain(client);
            output.Should().Contain(secondClient);
        }

        [Theory]
        [InlineData("Não existem clientes cadastrados")]
        public async void ShouldThrowAnErrrorWhenNoClientsIsStored(string expectedResult)
        {
            //Arrange
            var mockRepository = new Mock<IClientRepository>();

            var service = new ClientService(mockRepository.Object);

            //Act
            Func<Task> act = async () => await service.GetAllClients();

            //Assert
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage(expectedResult);
        }

        [Theory]
        [MemberData(nameof(SendClientParameters))]
        public async void ShouldUpdateAClient(Client client)
        {
            //Arrange
            var newName = "Cliente Atualizado";
            var newTable = 100;
            var context = new KitchenTestContext();
            var mockRepository = new Mock<IClientRepository>();

            mockRepository.Setup(library => library.Get(client.ClientId)).ReturnsAsync(client);
            mockRepository.Setup(library => library.Add(client)).ReturnsAsync(client);

            var service = new ClientService(mockRepository.Object);
            client.Name = newName;
            client.TableNumber = newTable;

            //Act
            var output = await service.UpdateClient(client);

            //Assert
            output.Should().BeOfType<Client>();
            output.Name.Should().Be(newName);
            output.TableNumber.Should().Be(newTable);
        }

        [Theory]
        [MemberData(nameof(SendClientParameters))]
        public async void ShouldThrownAnErrorWhenClientDoesntExists(Client client)
        {
            //Arrange
            var context = new KitchenTestContext();
            var mockRepository = new Mock<IClientRepository>();

            mockRepository.Setup(library => library.Get(client.ClientId)).ReturnsAsync(value: null);

            var service = new ClientService(mockRepository.Object);

            //Act
            Func<Task> act = async () => await service.UpdateClient(client);     

            //Assert
            await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("O cliente não existe");
        }
    }
}