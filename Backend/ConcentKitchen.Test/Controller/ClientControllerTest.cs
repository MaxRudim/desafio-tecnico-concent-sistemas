using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using RichardSzalay.MockHttp;
using Xunit;
using FluentAssertions;

namespace ConcentKitchen.Test.Controller;

/*

--> Obs: Por questão de produtividade, apenas desenvolvi o teste do controller de client.

         Para as demais entidades, a lógica seria semelhante a esta.

*/

public class ClientControllerTest
{
    public string apiUri = "https://localhost:7279";
    
    [Trait("Client", "1 - Client Test")]
    [Fact(DisplayName = "Teste para Create Client Controller")]
    public async Task TestCreateAClientController()
    {
    
        var mockHttp = new MockHttpMessageHandler();

        var newClient = JsonConvert.SerializeObject(new
          {
            TableNumber = 1,
            Name = "Max",
            Cpf = "776.059.730-47",
          });

        mockHttp.When(HttpMethod.Post, $"{apiUri}/client")
                .Respond("application/json", newClient);

        var client = mockHttp.ToHttpClient();

      
        var response = await client.PostAsync($"{apiUri}/client", new StringContent(newClient, Encoding.UTF8, "application/json"));

        var json = await response.Content.ReadAsStringAsync();

        response.Should().BeSuccessful();
        json.Should().Contain(newClient);

    }

    [Trait("Client", "1 - Client Test")]
    [Fact(DisplayName = "Teste para Delete Client")]
    public async Task TestDeleteAClientController()
    {
    
        var mockHttp = new MockHttpMessageHandler();

        var newClient = JsonConvert.SerializeObject(new
          {
            TableNumber = 1,
            Name = "Max",
            Cpf = "776.059.730-47",
          });

        mockHttp.When(HttpMethod.Post, $"{apiUri}/client")
                .Respond("application/json", newClient);

        mockHttp.When(HttpMethod.Get, $"{apiUri}/client/*")
                .Respond("application/json", newClient);

        mockHttp.When(HttpMethod.Delete, $"{apiUri}/client/*")
                .Respond(System.Net.HttpStatusCode.NoContent);
                
        var client = mockHttp.ToHttpClient();

        var response = await client.PostAsync($"{apiUri}/client", new StringContent(newClient, Encoding.UTF8, "application/json"));
        response.Should().BeSuccessful();

        var getClient = await client.GetAsync($"{apiUri}/client/id");
        getClient.Should().BeSuccessful();

        var deleteClient = await client.DeleteAsync($"{apiUri}/client/id");
        deleteClient.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

    }

    [Trait("Client", "1 - Client Test")]
    [Fact(DisplayName = "Teste para Get Client")]
    public async Task TestGetAClientController()
    {
    
        var mockHttp = new MockHttpMessageHandler();

        var newClient = JsonConvert.SerializeObject(new
          {
            TableNumber = 1,
            Name = "Max",
            Cpf = "776.059.730-47",
          });

        mockHttp.When(HttpMethod.Post, $"{apiUri}/client")
                .Respond("application/json", newClient);

        mockHttp.When(HttpMethod.Get, $"{apiUri}/client/*")
                .Respond("application/json", newClient);
                
        var client = mockHttp.ToHttpClient();

        var response = await client.PostAsync($"{apiUri}/client", new StringContent(newClient, Encoding.UTF8, "application/json"));
        response.Should().BeSuccessful();

        var getClient = await client.GetAsync($"{apiUri}/client/id");
        getClient.Should().BeSuccessful();

        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain(newClient);

    }

    [Trait("Client", "1 - Client Test")]
    [Fact(DisplayName = "Teste para Login Client")]
    public async Task TestLoginAClientController()
    {
    
        var mockHttp = new MockHttpMessageHandler();

        var newClient = JsonConvert.SerializeObject(new
          {
            TableNumber = 1,
            Name = "Max",
            Cpf = "776.059.730-47",
          });

        mockHttp.When(HttpMethod.Post, $"{apiUri}/client")
                .Respond("application/json", newClient);

        mockHttp.When(HttpMethod.Get, $"{apiUri}/login")
                .Respond("application/json", newClient);
                
        var client = mockHttp.ToHttpClient();

        var response = await client.PostAsync($"{apiUri}/client", new StringContent(newClient, Encoding.UTF8, "application/json"));
        response.Should().BeSuccessful();

        var loginClient = await client.GetAsync($"{apiUri}/login");
        loginClient.Should().BeSuccessful();

        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain(newClient);

    }

    [Trait("Client", "1 - Client Test")]
    [Fact(DisplayName = "Teste para Get All Clients")]
    public async Task TestGetAllClientsController()
    {
    
        var mockHttp = new MockHttpMessageHandler();

        var newClient = JsonConvert.SerializeObject(new
          {
            TableNumber = 1,
            Name = "Max",
            Cpf = "776.059.730-47",
          });

        var newClient2 = JsonConvert.SerializeObject(new
          {
            TableNumber = 1,
            Name = "Max",
            Cpf = "776.059.730-47",
          });

        mockHttp.When(HttpMethod.Post, $"{apiUri}/client")
                .Respond("application/json", newClient + newClient2);

        mockHttp.When(HttpMethod.Get, $"{apiUri}/client")
                .Respond("application/json", newClient + newClient2);
                
        var client = mockHttp.ToHttpClient();

        var response = await client.PostAsync($"{apiUri}/client", new StringContent(newClient + newClient2, Encoding.UTF8, "application/json"));
        response.Should().BeSuccessful();

        var getClients = await client.GetAsync($"{apiUri}/client");
        getClients.Should().BeSuccessful();

        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain(newClient);
        json.Should().Contain(newClient2);

    }

    [Trait("Client", "1 - Client")]
    [Fact(DisplayName = "Teste para Update Client")]
    public async Task TestUpdateAClientController()
    {
    
        var mockHttp = new MockHttpMessageHandler();

        var newClient = JsonConvert.SerializeObject(new
          {
            TableNumber = 1,
            Name = "Max",
            Cpf = "776.059.730-47",
          });

        var updatedClient = JsonConvert.SerializeObject(new
          {
            TableNumber = 5,
            Name = "Cliente Atualizado",
            Cpf = "776.059.730-47",
          });

        mockHttp.When(HttpMethod.Post, $"{apiUri}/client")
                .Respond("application/json", newClient);

        mockHttp.When(HttpMethod.Put, $"{apiUri}/client")
                .Respond("application/json", updatedClient);
                
        var client = mockHttp.ToHttpClient();

        var response = await client.PostAsync($"{apiUri}/client", new StringContent(newClient, Encoding.UTF8, "application/json"));
        response.Should().BeSuccessful();

        var getClient = await client.PutAsync($"{apiUri}/client", new StringContent(updatedClient, Encoding.UTF8, "application/json"));
        getClient.Should().BeSuccessful();

        var json = await getClient.Content.ReadAsStringAsync();
        json.Should().Contain(updatedClient);

    }
}