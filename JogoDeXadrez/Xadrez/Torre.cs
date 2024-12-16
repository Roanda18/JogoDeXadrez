using JogoDeXadrez.Tabuleiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDeXadrez.Xadrez
{
    internal class Torre : Peca
    {
        public Torre(Tabuleiro1 tabuleiro, Cor cor) : base(cor, tabuleiro)
        {

        }
        public override string ToString()
        {
            return "T";
        }
    }
}
