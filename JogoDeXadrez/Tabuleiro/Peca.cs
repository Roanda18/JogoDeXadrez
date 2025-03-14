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

        public void decrementarQtdMovimentos()
        {
            QtdMovimentos--;
        }

        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();

            for (int i = 0; i < Tabuleiro.Linhas; i++)
            {
                for (int j = 0; j <Tabuleiro.Colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool moviemtoPossivel(Posicao pos)
        {
            return movimentosPossiveis()[pos.Linha,pos.Coluna];
        }

        public abstract bool[,] movimentosPossiveis();    
        
    }
}
