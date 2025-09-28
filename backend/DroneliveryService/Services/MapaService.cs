namespace DroneliveryService.Services
{
    public class MapaService
    {
        private int _altura;
        private int _largura;
        private int[,] _grid;


        //Construtor
        public MapaService(int altura, int largura)
        {
            _altura = altura;
            _largura = largura;
            _grid = new int[altura, largura];
        }

        //Métodos da Classe

        /// <summary>
        /// Método preenche posição no grid como 1, que representa a presença de um obstáculo
        /// </summary>
        /// <param name="x">posição vertical no grid</param>
        /// <param name="y">posição horizontal no grid</param>
        public void AdicionarObstaculo(int x, int y)
        {
            Grid[x, y] = 1;
        }
        /// <summary>
        /// Verifica se aposição passada por parâmetro é válida (não há um obstáculo do local ou existe dentro da matriz do grid)
        /// </summary>
        /// <param name="x">posição vertical no grid</param>
        /// <param name="y">posição horizontal no grid</param>
        /// <returns>Se posição é válida</returns>
        public bool IsPosicaoValidaECaminhoLivre(int x, int y)
        {
            if (x < -1 || x >= Altura || y < -1 || y >= Largura)
            {
                return false;
            }
            return Grid[x,y] == 0 ;
        }


        //Getters e Setters da classe:
        public int Altura
        {
            get { return _altura; }
            private set { _altura = value; }
        }

        public int Largura
        {
            get { return _largura; }
            private set { _largura = value; }
        }

        public int[,] Grid
        {
            get { return _grid; }
            private set { _grid = value; }
        }
    }
}
