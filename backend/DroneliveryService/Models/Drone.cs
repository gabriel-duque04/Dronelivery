using System.Net.NetworkInformation;

namespace DroneliveryService.Models
{
    public class Drone
    {
        private int _id;
        private double _capacidadeMaximaKg;
        private double _autonomiaMaximaKm;
        private double _bateriaAtual;
        private StatusDrone _status;

        //Construtor da classe
        public Drone(int id, double capacidadeMaximaKG, double autonomiaMaximaKM)
        {
            _id = id;
            _capacidadeMaximaKg = capacidadeMaximaKG;
            _autonomiaMaximaKm = autonomiaMaximaKM;
            _bateriaAtual = 100;
            _status = StatusDrone.Ocioso;
        }

        //métodos da classe
        /// <summary>
        /// Método para alterar o status para EmVooParaEntrega
        /// </summary>
        public void AlocarParaEntrega()
        {
            if (_status == StatusDrone.Ocioso)
            {
                _status = StatusDrone.EmVooParaEntrega;
            }
        }

        /// <summary>
        /// Método para alterar o status para RetornandoParaBase
        /// </summary>
        public void IniciarRetornoParaBase()
        {
            if (_status == StatusDrone.EmVooParaEntrega)
            {
                _status = StatusDrone.RetornandoParaBase;
            }
        }

        /// <summary>
        /// Método para alterar o status para Ocioso e recarrega a bateria
        /// </summary>
        public void FinalizarCiclo()
        {
            if (_status == StatusDrone.RetornandoParaBase)
            {
                _status = StatusDrone.Ocioso;
            }

            BateriaAtual = 100;
        }

        //Getters e Setters da classe:
        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        public double CapacidadeMaximaKg
        {
            get { return _capacidadeMaximaKg; }
            private set { _capacidadeMaximaKg = value; }
        }

        public double AutonomiaMaximaKm
        {
            get { return _autonomiaMaximaKm; }
            private set { _autonomiaMaximaKm = value; }
        }

        public double BateriaAtual
        {
            get { return _bateriaAtual; }
            set { _bateriaAtual = value; } // A bateria pode ser alterada externamente por enquanto
        }

        public StatusDrone Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}
