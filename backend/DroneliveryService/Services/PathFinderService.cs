namespace DroneliveryService.Services
{
    public class PathFinderService
    {
        private MapaService _mapa;

        //Construtor da classe
        public PathFinderService(MapaService mapa)
        {
            this._mapa = mapa;
        }

        //Métodos da classe


        /// <summary>
        /// Método que faz o mapeamento e definição do caminho mais curto por meio do algorítimo de Busca em LArgura (BFS)
        /// </summary>
        /// <param name="startX">ponto de partida x</param>
        /// <param name="startY">ponto de partida y</param>
        /// <param name="endX">destino x</param>
        /// <param name="endY">destino y</param>
        /// <returns>A lista de posições que indica o caminho a ser percorrido</returns>
        public List<(int x, int y)>? EncontrarCaminhoMaisCurto(int startX, int startY,int endX, int endY)
        {
            //Fila que gurada os próximos pontos a serem visitados na busca
            Queue<(int x, int y)> exploracao = new Queue<(int x, int y)>();

            //Matriz com os pontos já mapeados
            bool[,] explorados = new bool[_mapa.Altura, _mapa.Largura];

            //Dicionário com os antecessores de todos os vértices
            Dictionary<(int x, int y), (int x, int y)> veioDe = new Dictionary<(int x, int y), (int x, int y)>();

            //Verificação da validade de todos os pontos passados;
            if (!_mapa.IsPosicaoValidaECaminhoLivre(startX, startY) || !_mapa.IsPosicaoValidaECaminhoLivre(endX, endY))
                return null;

            //Inicio da busca
            exploracao.Enqueue((startX, startY));//adiciona o ponto de partida da busca a fila de exploração
            explorados[startX, startY] = true;//marca o ponto de partida como explorado

            while(exploracao.Count > 0)
            {
                (int x, int y) pontoAtual = exploracao.Dequeue();//tira da fila dos a ser visitados
                if(pontoAtual.x == endX &&  pontoAtual.y == endY)//se verdadeiro caminho encontrado ponto de parada do while
                {
                    List<(int x, int y)> caminho = new List<(int x, int y)>();
                    (int x, int y) passo = pontoAtual;

                    while (veioDe.ContainsKey(passo))
                    {
                        caminho.Add(passo);
                        passo = veioDe[passo];
                    }
                    caminho.Add(passo);
                    caminho.Reverse();

                    return caminho;
                }

                //Demarcação dos vizinhos
                (int x, int y) cima = (pontoAtual.x, pontoAtual.y + 1);
                (int x, int y) baixo = (pontoAtual.x, pontoAtual.y - 1);
                (int x, int y) esquerda = (pontoAtual.x - 1, pontoAtual.y);
                (int x, int y) direita = (pontoAtual.x + 1, pontoAtual.y);

                //Exploração e verificação de cada um dos vizinhos
                //Vizinho de cima
                if(_mapa.IsPosicaoValidaECaminhoLivre(cima.x, cima.y) && !explorados[cima.x, cima.y])
                {
                    explorados[cima.x, cima.y] = true;//marca como explorado
                    veioDe.Add(cima, pontoAtual);//adiciona ao dicionário que monta o caminho
                    exploracao.Enqueue(cima);//adiciona a lista de exploracao
                }

                //Vizinho de baixo
                if (_mapa.IsPosicaoValidaECaminhoLivre(baixo.x, baixo.y) && !explorados[baixo.x, baixo.y])
                {
                    explorados[baixo.x, baixo.y] = true;//marca como explorado
                    veioDe.Add(baixo, pontoAtual);//adiciona ao dicionário que monta o caminho
                    exploracao.Enqueue(baixo);//adiciona a lista de exploracao
                }

                //Vizinho da esquerda
                if (_mapa.IsPosicaoValidaECaminhoLivre(esquerda.x, esquerda.y) && !explorados[esquerda.x, esquerda.y])
                {
                    explorados[esquerda.x, esquerda.y] = true;//marca como explorado
                    veioDe.Add(esquerda, pontoAtual);//adiciona ao dicionário que monta o caminho
                    exploracao.Enqueue(esquerda);
                }

                //Vizinho da direita
                if (_mapa.IsPosicaoValidaECaminhoLivre(direita.x, direita.y) && !explorados[direita.x, direita.y])
                {
                    explorados[direita.x, direita.y] = true;//marca como explorado
                    veioDe.Add(direita, pontoAtual);//adiciona ao dicionário que monta o caminho
                    exploracao.Enqueue(direita);//adiciona a lista de exploracao
                }

            }

            return null;
        }
    }
}
