﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDeXadrez.Tabuleiro
{
    internal abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro1 Tabuleiro { get; protected set; }

        public Peca(Cor cor, Tabuleiro1 tabuleiro)
        {
            Posicao = null;
            Cor = cor;
            this.QtdMovimentos = 0;
            Tabuleiro = tabuleiro;
        }

        public void incrementarQtdMovimentos() { 
            QtdMovimentos++;
        }

        public abstract bool[,] movimentosPossiveis();    
        
    }
}
