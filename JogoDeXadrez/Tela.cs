using JogoDeXadrez.Tabuleiro;
using JogoDeXadrez.Xadrez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDeXadrez
{
    internal class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro1 tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (tabuleiro.peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        imprimirPeca(tabuleiro.peca(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h ");
        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string jogada = Console.ReadLine();
            char letra = jogada[0];
            int numero = int.Parse(jogada[1] + "");
            return new PosicaoXadrez(letra, numero);
        }

        public static void imprimirPeca(Peca peca)
        {
            if (peca.Cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}
