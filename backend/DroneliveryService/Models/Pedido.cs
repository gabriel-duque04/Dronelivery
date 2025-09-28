namespace DroneliveryService.Models
{
    public class Pedido
    {
        private int _id;
        private int _localizacaoX;
        private int _localizacaoY;
        private double _pesoPacote;
        private Prioridade _prioridade;
        private DateTime _dataHoraPedido;
        private StatusPedido _status;

        //Construtor
        public Pedido(int id, int x, int y, double pesoPacote, Prioridade prioridade)
        {
            //verificações dos inputs
            if(pesoPacote <= 0)
                throw new ArgumentException("O peso do pacote deve ser um valor positivo.");

            //Atribuição dos valores
            _id = id;
            _localizacaoX = x;
            _localizacaoY = y;
            _pesoPacote = pesoPacote;
            _prioridade = prioridade;
            _dataHoraPedido = DateTime.Now;
            _status = StatusPedido.Pendente;
        }

        //Métodos da classe

        /// <summary>
        /// Muda status para Alocado
        /// </summary>
        public void MarcarComoAlocado()
        {
            if (_status == StatusPedido.Pendente)
            {
                _status = StatusPedido.Alocado;
            }
        }

        /// <summary>
        /// Muda status para EmEntrega
        /// </summary>
        public void MarcarComoEmEntrega()
        {
            if (_status == StatusPedido.Alocado)
            {
                _status = StatusPedido.EmEntrega;
            }
        }

        /// <summary>
        /// Muda status para concluido
        /// </summary>
        public void MarcarComoConcluido()
        {
            if (_status == StatusPedido.EmEntrega)
            {
                _status = StatusPedido.Concluido;
            }
        }

        /// <summary>
        /// Muda status para falha
        /// </summary>
        public void MarcarComoFalhou()
        {
            if (_status != StatusPedido.Concluido)
            {
                _status = StatusPedido.Falhou;
            }
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

        public Prioridade Prioridade
        {
            get { return _prioridade; }
            private set { _prioridade = value; }
        }

        public DateTime DataHoraPedido
        {
            get { return _dataHoraPedido; }
            set { _dataHoraPedido = value; }
        }

        public StatusPedido Status
        {
            get { return _status; }
            private set { _status = value; }
        }
    }
}
