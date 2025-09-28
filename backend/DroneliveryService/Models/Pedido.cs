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




        public int Id
        {
            get { return _id; }
            set { _id = value; }
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
            set { _pesoPacote = value; }
        }

        public int Prioridade
        {
            get { return _prioridade; }
            set { _prioridade = value; }
        }

        public DateTime DataHoraPedido
        {
            get { return _dataHoraPedido; }
            set { _dataHoraPedido = value; }
        }
    }
}
