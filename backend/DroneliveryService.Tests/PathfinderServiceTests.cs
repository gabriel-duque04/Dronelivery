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

        [Fact]
        public void EncontrarCaminhoMaisCurto_DeveDesviarDeObstaculo_QuandoCaminhoDiretoEstaBloqueado()
        {
            // --- 1. Preparação (Arrange) ---
            MapaService mapa = new MapaService(10, 10);

            // Bloqueia o caminho direto de (0,0) para (0,3)
            mapa.AdicionarObstaculo(0, 1);
            mapa.AdicionarObstaculo(0, 2);

            PathFinderService pathfinder = new PathFinderService(mapa);
            (int x, int y) inicio = (0, 0);
            (int x, int y) fim = (0, 3);

            // --- 2. Ação (Act) ---
            List<(int x, int y)>? caminho = pathfinder.EncontrarCaminhoMaisCurto(inicio.x, inicio.y, fim.x, fim.y);

            // --- 3. Verificação (Assert) ---
            Assert.NotNull(caminho); // Ainda deve encontrar um caminho

            // O caminho em linha reta teria 4 pontos. Como precisa desviar, terá que ter mais.
            // Ex: (0,0)->(1,0)->(1,1)->(1,2)->(1,3)->(0,3) -> 6 pontos
            Assert.True(caminho.Count > 4);
        }
    }
}