using DroneliveryService.Models;
using DroneliveryService.Services;
using Microsoft.AspNetCore.Mvc;

namespace DroneliveryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DronesController : ControllerBase
    {
        private readonly GerenciadorDeFrotasService _gerenciador;

        public DronesController(GerenciadorDeFrotasService gerenciador)
        {
            _gerenciador = gerenciador;
        }

        [HttpGet]
        public IActionResult ObterStatusDrones()
        {
            // Este método chama o Gerenciador para obter a lista de drones
            List<Drone> drones = _gerenciador.ListarDrones();
            return Ok(drones);
        }

        [HttpPost]
        public IActionResult AdicionarNovoDrone([FromBody] CriarDroneRequest request)
        {
            try
            {
                _gerenciador.AdicionarDrone(request.CapacidadeMaximaKg, request.AutonomiaMaximaKm);

                // HTTP 201 Created é a resposta padrão para criação de um novo recurso.
                return StatusCode(201, "Drone adicionado à frota com sucesso.");
            }
            catch (Exception ex)
            {
                // Embora nosso Drone não tenha validações que lancem exceções,
                // é uma boa prática ter o try-catch.
                return BadRequest(ex.Message);
            }
        }
    }
}