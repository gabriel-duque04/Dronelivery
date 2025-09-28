using DroneliveryService.Models;

namespace DroneliveryService.Services
{
    public class GerenciadorDeFrotasService
    {
        private readonly List<Drone> _drones;
        private List<Pedido> _pedidosPendentes;
        private readonly MapaService _mapa;
        private readonly PathFinderService _pathfinder;
        static int _droneId = 1;
        static int _pedidoId = 1;

        //Construtor da classe
        public GerenciadorDeFrotasService(MapaService mapa, PathFinderService pathfinder)
        {
            _mapa = mapa;
            _pathfinder = pathfinder;
            _drones = new List<Drone>();
            _pedidosPendentes = new List<Pedido>();
        }

        //Métodos da classe


        /// <summary>
        /// Recebe parâmetros necessários e cria um novo drone, logo em seguida o adiciona na lista de drones
        /// </summary>
        public void AdicionarDrone(double capacidadeMaximaKG, double autonomiaMaximaKM)
        {
            Drone novo = new Drone(_droneId, capacidadeMaximaKG,autonomiaMaximaKM);
            _drones.Add(novo);
            _droneId++;
        }

        /// <summary>
        /// Recebe parâmetros necessários e cria um novo pedido, logo em seguida os ordena por prioridade 
        /// </summary>
        public void AdicionarPedido(int x, int y, double pesoPacote, int prioridade)
        {
            Pedido novo = new Pedido(_pedidoId, x, y,pesoPacote, prioridade);
            _pedidosPendentes.Add(novo);

            _pedidosPendentes = _pedidosPendentes.OrderByDescending(p => p.Prioridade).ThenBy(p => p.DataHoraPedido).ToList();

            _pedidoId++;
        }

        public void AlocarProximaEntrega()
        {
            Drone droneDisponivel = _drones.FirstOrDefault(d => d.Status == StatusDrone.Ocioso);
        }
    }
}
