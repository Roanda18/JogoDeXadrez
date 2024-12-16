
using JogoDeXadrez.Tabuleiro;
namespace JogoDeXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro1 tabuleiro = new Tabuleiro1(8, 8);
            Tela.imprimirTabuleiro(tabuleiro);
        }
    }
}