
using JogoDeXadrez.Tabuleiro;
namespace JogoDeXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao p;

            p = new Posicao(3, 5);

            Console.WriteLine("Posição: " + p);

            Console.ReadLine();

            Tabuleiro1 tabuleiro = new Tabuleiro1(8,8);
        }
    }
}