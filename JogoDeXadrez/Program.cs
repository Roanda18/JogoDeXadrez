
using JogoDeXadrez.Tabuleiro;
using JogoDeXadrez.Xadrez;
namespace JogoDeXadrez
{
    class Program
    {
        static void Main(string[] args)
        {

            PosicaoXadrez pos = new PosicaoXadrez('a', 1);

            Console.WriteLine(pos);

            Console.WriteLine(pos.toPosicao());


        }
    }
}