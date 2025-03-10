﻿using JogoDeXadrez.Tabuleiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDeXadrez.Xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro1 tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> pecasCapturadas;
        public bool xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro1(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            vulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            pecasCapturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQtdMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                pecasCapturadas.Add(pecaCapturada);
            }

            //#jogadaEspecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.retirarPeca(origemTorre);
                T.incrementarQtdMovimentos();
                tab.colocarPeca(T, destinoTorre);

            }

            //#jogadaEspecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(origemTorre);
                T.incrementarQtdMovimentos();
                tab.colocarPeca(T, destinoTorre);

            }

            //#jogadaEspecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posPiao;
                    if (p.Cor == Cor.Branca)
                    {
                        posPiao = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posPiao = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = tab.retirarPeca(posPiao);
                    pecasCapturadas.Add(pecaCapturada);
                }
            }
            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQtdMovimentos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                pecasCapturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);

            //#jogadaEspecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.retirarPeca(destinoTorre);
                T.decrementarQtdMovimentos();
                tab.colocarPeca(T, origemTorre);

            }

            //#jogadaEspecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(destinoTorre);
                T.decrementarQtdMovimentos();
                tab.colocarPeca(T, origemTorre);

            }

            //#jogadaEspecial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == vulneravelEnPassant)
                {
                    Peca peao = tab.retirarPeca(destino);
                    Posicao posPiao;
                    if (p.Cor == Cor.Branca)
                    {
                        posPiao = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posPiao = new Posicao(4, destino.Coluna);
                    }
                    tab.colocarPeca(peao, posPiao);
                }
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode fazer essa jogada, seu Rei ficara em xeque");
            }

            Peca p = tab.peca(destino);

            //#jogadaEspecial promoção

            if (p is Peao)
            {
                if ((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preta && destino.Linha == 7))
                {
                    p = tab.retirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(tab, p.Cor);
                    tab.colocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }

                if (estaEmXeque(adnversaria(jogadorAtual)))
                {
                    xeque = true;
                }
                else
                {
                    xeque = false;
                }
                if (testeXequeMate(adnversaria(jogadorAtual)))
                {
                    terminada = true;
                }
                else
                {
                    turno++;
                    mudaJogador();
                }



                //#jogadaEspecial en passant
                if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
                {
                    vulneravelEnPassant = p;
                }
                else
                {
                    vulneravelEnPassant = null;
                }
            }

            public void validarPosicaoDeOrigem(Posicao pos)
            {
                if (tab.peca(pos) == null)
                {
                    throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
                }
                if (jogadorAtual != tab.peca(pos).Cor)
                {
                    throw new TabuleiroException("A peça de orim escolhida não e sua !");
                }
                if (!tab.peca(pos).existeMovimentosPossiveis())
                {
                    throw new TabuleiroException("A peça de origem não possui movimentos possiveis");
                }
            }

            public void validarPosicaoDestino(Posicao origem, Posicao destino)
            {
                if (!tab.peca(origem).moviemtoPossivel(destino))
                {
                    throw new TabuleiroException("Essa peça não pode fazer esse movimento");
                }
            }

            private void mudaJogador()
            {
                if (jogadorAtual == Cor.Branca)
                {
                    jogadorAtual = Cor.Preta;
                }
                else
                {
                    jogadorAtual = Cor.Branca;
                }
            }

            public HashSet<Peca> pecasCapturad(Cor cor)
            {
                HashSet<Peca> aux = new HashSet<Peca>();
                foreach (Peca x in pecasCapturadas)
                {
                    if (x.Cor == cor)
                    {
                        aux.Add(x);
                    }
                }
                return aux;
            }

            public HashSet<Peca> pecasEmJogo(Cor cor)
            {
                HashSet<Peca> aux = new HashSet<Peca>();
                foreach (Peca x in pecas)
                {
                    if (x.Cor == cor)
                    {
                        aux.Add(x);
                    }
                }
                aux.ExceptWith(pecasCapturad(cor));
                return aux;
            }

            private Cor adnversaria(Cor cor)
            {
                if (cor == Cor.Branca)
                {
                    return Cor.Preta;
                }
                else
                {
                    return Cor.Branca;
                }
            }

            private Peca rei(Cor cor)
            {
                foreach (Peca x in pecasEmJogo(cor))
                {
                    if (x is Rei)
                    {
                        return x;
                    }
                }
                return null;
            }

            public bool estaEmXeque(Cor cor)
            {
                Peca re = rei(cor);
                if (re == null)
                {
                    throw new TabuleiroException("Não existe Rei!!");
                }

                foreach (Peca x in pecasEmJogo(adnversaria(cor)))
                {
                    bool[,] mat = x.movimentosPossiveis();
                    if (mat[re.Posicao.Linha, re.Posicao.Coluna])
                    {
                        return true;
                    }
                }
                return false;
            }

            public bool testeXequeMate(Cor cor)
            {
                if (!estaEmXeque(cor))
                {
                    return false;
                }
                foreach (Peca p in pecasEmJogo(cor))
                {
                    bool[,] mat = p.movimentosPossiveis();
                    for (int i = 0; i < tab.Linhas; i++)
                    {
                        for (int j = 0; j < tab.Colunas; j++)
                        {
                            if (mat[i, j])
                            {
                                Posicao origem = p.Posicao;
                                Posicao destino = new Posicao(i, j);
                                Peca pecaCapturada = executaMovimento(origem, destino);
                                bool testeXeque = estaEmXeque(cor);
                                desfazMovimento(origem, destino, pecaCapturada);
                                if (!testeXeque)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                return true;
            }

            public void colocarNovaPeca(char coluna, int linha, Peca peca)
            {
                tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
                pecas.Add(peca);
            }
            public void colocarPecas()
            {
                colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
                colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
                colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
                colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
                colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
                colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
                colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
                colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));
                colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
                colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
                colocarNovaPeca('c', 1, new Bisbo(tab, Cor.Branca));
                colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
                colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
                colocarNovaPeca('f', 1, new Bisbo(tab, Cor.Branca));
                colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
                colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));

                colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
                colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
                colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
                colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
                colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
                colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
                colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
                colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));
                colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
                colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
                colocarNovaPeca('c', 8, new Bisbo(tab, Cor.Preta));
                colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
                colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
                colocarNovaPeca('f', 8, new Bisbo(tab, Cor.Preta));
                colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
                colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));

            }
        }
    }
