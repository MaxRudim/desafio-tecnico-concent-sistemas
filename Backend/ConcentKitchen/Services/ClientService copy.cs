// using Microsoft.AspNetCore.Mvc;
// using ConcentKitchen.Models;
// using ConcentKitchen.Repository;
// using ConcentKitchen.Middlewares;
// using System.Globalization;

// namespace ConcentKitchen.Controllers;

// [ApiController]
// [Route("client")]
// public class ClientController : Controller
// {
//     private readonly IClientRepository _repository;
//     public ClientController(IClientRepository repository)
//     {
//         _repository = repository;
//     }

//     [HttpPost()]
//     public async Task<IActionResult> CreateClient([FromBody] Client client)
//     {
//         try
//         {
//             var clientExist = await _repository.GetByCpf(client.Cpf);
//             if (clientExist is not null) throw new InvalidOperationException("Este cliente já existe");

//             // var validBirthDate = DateTime.Parse(client.Birthdate, new CultureInfo("pt-BR", false));

//             var validCpf = ValidaCPF.IsCpf(client.Cpf);
//             if (validCpf == false) throw new InvalidOperationException("Cpf inválido");

//             var output = await _repository.Add(client);
//             return CreatedAtAction("GetClient", new { id = output.ClientId }, output);
//         }
//         catch (InvalidOperationException ex)
//         {
//           return BadRequest(ex.Message);
//         }
//     }

//     [HttpDelete("{id}")]
//     public async Task<IActionResult> DeleteClient(Guid id)
//     {
//         try
//         {
//             var clientExist = await _repository.Get(id);
//             if (clientExist is null) throw new InvalidOperationException("Este cliente não existe");

//             await _repository.Delete(id);
//             return NoContent();
//         }
//         catch (InvalidOperationException ex)
//         {
//             return BadRequest(ex.Message);
//         }
//     }
    
//     [HttpGet()]
//     public async Task<IActionResult> GetAllClients()
//     {
//         try
//         {
//             var clients = await _repository.GetAll();
//             if (!clients.Any()) throw new InvalidOperationException("Não existem clientes cadastrados");

//             return Ok(clients);
//         }
//         catch (InvalidOperationException ex)
//         {
//             return NotFound(ex.Message);
//         }
//     }
    
//     [HttpGet("{id}")]
//     public async Task<IActionResult> GetClient(string id)
//     {
//         try
//         {
//             var client = await _repository.Get(new Guid(id));
//             if (client == null) throw new InvalidOperationException("O cliente não existe");

//             return Ok(client);
//         }
//         catch (InvalidOperationException ex)
//         {
//             return NotFound(ex.Message);
//         }
//     }

//     [HttpPut()]
//     public async Task<IActionResult> UpdateClient([FromBody] Client client)
//     {
//         try
//         {
//             var clientExist = await _repository.Get(client.ClientId);
//             if (clientExist == null) throw new InvalidOperationException("O cliente não existe");

//             clientExist.Name = client.Name;
//             clientExist.TableNumber = client.TableNumber;

//             await _repository.Update(clientExist);

//             var updatedClient = await _repository.Get(client.ClientId);
//             return Ok(updatedClient);
//         }
//         catch (InvalidOperationException ex)
//         {
//             return NotFound(ex.Message);
//         }

//     }
// }