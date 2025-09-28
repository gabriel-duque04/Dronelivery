using DroneliveryService.Models;
using DroneliveryService.Services;
using Microsoft.AspNetCore.Mvc;
namespace DroneliveryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly GerenciadorDeFrotasService _gerenciador;

        public PedidosController(GerenciadorDeFrotasService gerenciador)
        {
            _gerenciador = gerenciador;
        }

        [HttpPost]
        public IActionResult CriarNovoPedido([FromBody] CriarPedidoRequest novoPedidoRequest)
        {
            try
            {
                // Usamos o nosso Gerenciador para criar e adicionar o pedido,
                // passando os dados que recebemos na requisição.
                _gerenciador.AdicionarPedido(
                    novoPedidoRequest.X,
                    novoPedidoRequest.Y,
                    novoPedidoRequest.Peso,
                    novoPedidoRequest.Prioridade
                );

                // Se tudo deu certo, retornamos uma resposta HTTP 200 OK com uma mensagem.
                return Ok("Pedido recebido e adicionado à fila com sucesso.");
            }
            catch (Exception ex)
            {
                // Lembra que nosso construtor de Pedido lança uma exceção se os dados
                // forem inválidos (ex: peso negativo)? Este 'catch' captura esse erro.
                // Se isso acontecer, retornamos um erro HTTP 400 Bad Request com a
                // mensagem da exceção (ex: "O peso do pacote deve ser um valor positivo.").
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ObterPedidosPendentes()
        {
            List<Pedido> pedidos = _gerenciador.ListarPedidosPendentes();
            return Ok(pedidos);
        }
    }
}
