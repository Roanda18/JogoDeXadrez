
using JogoDeXadrez.Tabuleiro;
using JogoDeXadrez.Xadrez;
namespace JogoDeXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro1 tab = new Tabuleiro1(8,8);

            PosicaoXadrez pos = new PosicaoXadrez('a', 1);

            Console.WriteLine(pos);

            Console.WriteLine(pos.toPosicao());

            tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(3, 3));
            tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(6, 2));
            tab.colocarPeca(new Rei(tab, Cor.Branca), new Posicao(1, 1));

            Tela.imprimirTabuleiro(tab);


        }
    }
}