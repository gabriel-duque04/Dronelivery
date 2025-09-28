namespace DroneliveryService.Models
{
    public class Pedido
    {
        private int _id;
        private int _localizacaoX;
        private int _localizacaoY;
        private double _pesoPacote;
        private int _prioridade;
        private DateTime _dataHoraPedido;

        //Construtor
        public Pedido(int id, int x, int y, double pesoPacote, int prioridade)
        {
            //verificações dos inputs
            if(pesoPacote <= 0)
                throw new ArgumentException("O peso do pacote deve ser um valor positivo.");

            if(prioridade <= 0 || prioridade > 3)
                throw new ArgumentOutOfRangeException("A prioridade deve ser 1 (Baixa), 2 (Média) ou 3 (Alta).");

            //Atribuição dos valores
            _id = id;
            _localizacaoX = x;
            _localizacaoY = y;
            _pesoPacote = pesoPacote;
            _prioridade = prioridade;
            _dataHoraPedido = DateTime.Now;
        }

        //Getters e Setters da classe:
        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        public int LocalizacaoX
        {
            get { return _localizacaoX; }
            set { _localizacaoX = value; }
        }

        public int LocalizacaoY
        {
            get { return _localizacaoY; }
            set { _localizacaoY = value; }
        }

        public double PesoPacote
        {
            get { return _pesoPacote; }
            private set { _pesoPacote = value; }
        }

        public int Prioridade
        {
            get { return _prioridade; }
            private set { _prioridade = value; }
        }

        public DateTime DataHoraPedido
        {
            get { return _dataHoraPedido; }
            set { _dataHoraPedido = value; }
        }
    }
}
