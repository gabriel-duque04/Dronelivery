using DroneliveryService.Services;
using Xunit;

// O namespace deve ser o do projeto de testes
namespace DroneliveryService.Tests
{
    public class PathfinderServiceTests
    {
        [Fact]
        public void EncontrarCaminhoMaisCurto_DeveEncontrarCaminhoEmLinhaReta_QuandoNaoHaObstaculos()
        {
            // --- 1. Preparação (Arrange) ---
            MapaService mapa = new MapaService(10, 10);
            PathFinderService pathfinder = new PathFinderService(mapa);
            (int x, int y) inicio = (0, 0);
            (int x, int y) fim = (0, 3);

            // --- 2. Ação (Act) ---
            List<(int x, int y)>? caminho = pathfinder.EncontrarCaminhoMaisCurto(inicio.x, inicio.y, fim.x, fim.y);

            // --- 3. Verificação (Assert) ---
            Assert.NotNull(caminho);
            Assert.Equal(4, caminho.Count);
            Assert.Equal(fim, caminho.Last());
        }
    }
}