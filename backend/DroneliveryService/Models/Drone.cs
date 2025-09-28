namespace DroneliveryService.Models
{
    public class Drone
    {
        private int _id;
        private double _capacidadeMaximaKg;
        private double _autonomiaMaximaKm;
        private double _bateriaAtual;

        //Construtor da classe
        public Drone(int id, double capacidadeMaximaKG, double autonomiaMaximaKM)
        {
            _id = id;
            _capacidadeMaximaKg = capacidadeMaximaKG;
            _autonomiaMaximaKm = autonomiaMaximaKM;
            _bateriaAtual = 100;
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

    }
}
