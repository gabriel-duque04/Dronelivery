namespace DroneliveryService.Models
{
    public class CriarPedidoRequest
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double Peso { get; set; }
        public Prioridade Prioridade { get; set; }
    }
}
