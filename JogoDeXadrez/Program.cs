
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
                Tabuleiro1 tabuleiro = new Tabuleiro1(8, 8);
                tabuleiro.colocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0, 0));
                tabuleiro.colocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(1, 3));
                tabuleiro.colocarPeca(new Rei(tabuleiro, Cor.Preta), new Posicao(0, 9));
                Tela.imprimirTabuleiro(tabuleiro);
            }
            catch(TabuleiroException ex) 
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}