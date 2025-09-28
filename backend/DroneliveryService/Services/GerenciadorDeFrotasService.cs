using DroneliveryService.Models;
using System.Reflection.PortableExecutable;

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


        public List<Pedido> ListarPedidosPendentes()
        {
           
            return _pedidosPendentes;
        }

        public List<Drone> ListarDrones()
        {

            return _drones;
        }

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
        public void AdicionarPedido(int x, int y, double pesoPacote, Prioridade prioridade)
        {
            Pedido novo = new Pedido(_pedidoId, x, y,pesoPacote, prioridade);
            _pedidosPendentes.Add(novo);

            _pedidosPendentes = _pedidosPendentes.OrderByDescending(p => p.Prioridade).ThenBy(p => p.DataHoraPedido).ToList();

            _pedidoId++;
        }

        public void AlocarProximaEntrega()
        {
            Drone? droneDisponivel = _drones.FirstOrDefault(d => d.Status == StatusDrone.Ocioso);//acha um drone disponível

            if (droneDisponivel != null)
            {
                List<Pedido> pedidosPossiveis = _pedidosPendentes.Take(5).ToList();
                if(pedidosPossiveis.Count > 0)
                {
                    List<List<Pedido>> todosOsCombos = GerarCombosEntregas(pedidosPossiveis);


                    List<Pedido>? melhorComboEncontrado = null;
                    double menorDistanciaCalculada = double.MaxValue;

                    foreach (List<Pedido> combo in todosOsCombos)
                    {
                        double pesoTotalCombo = combo.Sum(p => p.PesoPacote);
                        if (pesoTotalCombo <= droneDisponivel.CapacidadeMaximaKg) 
                        {
                            List<List<Pedido>> permutacoes = GerarPermutacoes(combo);
                            double distanciaMinimaDoCombo = double.MaxValue;

                            foreach (List<Pedido> permutacao in permutacoes)
                            {
                                double distanciaDaPermutacao = CalcularDistanciaTotalDeRotaUnica(permutacao);

                              
                                if (distanciaDaPermutacao != -1 && distanciaDaPermutacao < distanciaMinimaDoCombo)
                                {
                                    distanciaMinimaDoCombo = distanciaDaPermutacao;
                                }


                            }
                            if (distanciaMinimaDoCombo < double.MaxValue)
                            {
                                double bateriaNecessaria = (distanciaMinimaDoCombo * (100.0 / droneDisponivel.AutonomiaMaximaKm));

                              
                                if (droneDisponivel.BateriaAtual >= bateriaNecessaria)
                                {
                                  
                                  
                                    if (distanciaMinimaDoCombo < menorDistanciaCalculada)
                                    {
                                      
                                        menorDistanciaCalculada = distanciaMinimaDoCombo;
                                        melhorComboEncontrado = combo;
                                    }
                                }
                            }

                        }

                        if (melhorComboEncontrado != null)
                        {
                            droneDisponivel.AlocarParaEntrega();
                            foreach(Pedido p in melhorComboEncontrado)
                            {
                                p.MarcarComoAlocado();
                            }

                            _pedidosPendentes.RemoveAll(pedido => melhorComboEncontrado.Contains(pedido));

                            droneDisponivel.ExecutarViagem(menorDistanciaCalculada);

                        }

                    }
                }
            }
        }

        private List<List<Pedido>> GerarCombosEntregas(List<Pedido> lista)
        {
            List<List<Pedido>> combos = new List<List<Pedido>>();
            combos.Add(new List<Pedido>());

            foreach (Pedido pedido in lista)
            {
                List<List<Pedido>> combosAtuais = combos.ToList();
                foreach (List<Pedido> comboExistente in combosAtuais)
                {
                    List<Pedido> novoCombo = new List<Pedido>(comboExistente);

                    novoCombo.Add(pedido);

                    combos.Add(novoCombo);
                }
            }

            combos.RemoveAt(0);

            return combos;
        }


        private List<List<Pedido>> GerarPermutacoes(List<Pedido> pedidos)
        {
            List<List<Pedido>> resultadoFinal = new List<List<Pedido>>();

            if (pedidos.Count == 0)
            {
                return resultadoFinal;
            }

            resultadoFinal.Add(new List<Pedido> { pedidos[0] });

            for (int i = 1; i < pedidos.Count; i++)
            {
                Pedido pedidoAtual = pedidos[i];
                List<List<Pedido>> novasPermutacoes = new List<List<Pedido>>();

   
                foreach (List<Pedido> permutacaoAnterior in resultadoFinal)
                {
       
       
                    for (int j = 0; j <= permutacaoAnterior.Count; j++)
                    {
           
                        List<Pedido> novaPermutacao = new List<Pedido>(permutacaoAnterior);

           
                        novaPermutacao.Insert(j, pedidoAtual);

           
                        novasPermutacoes.Add(novaPermutacao);
                    }
                }

   
   
                resultadoFinal = novasPermutacoes;
            }

            return resultadoFinal;
        }

        private double CalcularDistanciaTotalDeRotaUnica(List<Pedido> rota)
        {
            List<(int x, int y)> pontosDeParada = new List<(int x, int y)>();

            pontosDeParada.Add((0, 0));

            foreach (Pedido pedido in rota)
            {
                pontosDeParada.Add((pedido.LocalizacaoX, pedido.LocalizacaoY));
            }

            pontosDeParada.Add((0, 0));

            double distanciaTotal = 0.0;

            for (int i = 0; i < pontosDeParada.Count - 1; i++)
            {
   
                (int x, int y) pontoA = pontosDeParada[i];
                (int x, int y) pontoB = pontosDeParada[i + 1];

   
                List<(int x, int y)>? caminhoDoTrecho = _pathfinder.EncontrarCaminhoMaisCurto(pontoA.x, pontoA.y, pontoB.x, pontoB.y);

   
                if (caminhoDoTrecho == null)
                {
       
       
                    return -1;
                }
                else
                {
       
       
                    distanciaTotal += (caminhoDoTrecho.Count - 1);
                }
            }

            return distanciaTotal;
        }


    }
}
