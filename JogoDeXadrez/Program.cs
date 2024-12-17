
using JogoDeXadrez.Tabuleiro;
using JogoDeXadrez.Xadrez;
namespace JogoDeXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.terminada)
                {
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab);
                    Console.Write("Digite posição da peça a ser movida: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                    Console.Write("Digite posição de destino da peça: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                    partida.executaMovimento(origem, destino);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }







        }
    }
}