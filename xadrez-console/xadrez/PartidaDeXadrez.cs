using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        // Propriedades autoimplementadas
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        public bool xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }

        // Definição de conjuntos
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        // Construtor
        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8); // Instância um novo tabuleiro
            turno = 1; // A partida sempre começa no turno 1
            jogadorAtual = Cor.Branca; // A primeira peça que inicia a partida sempre é a branca
            terminada = false;
            xeque = false;
            vulneravelEnPassant = null;
            // Instância os conjuntos
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            // Coloca as peças iniciais no tabuleiro de xadrez
            colocarPecas();
        }

        // Executa o movimento de uma peça de uma posição de origem a uma posição de destino
        // e caso tenha capturado alguma peça na posição de destino retorna a mesma
        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            // retira a peça da posição de origem
            Peca p = tab.retirarPeca(origem);
            // Incrementa a quantidade de movimentos efetuados
            p.incrementarQteMovimentos();
            // retira a peca da posição de destino caso exista
            Peca pecaCapturada = tab.retirarPeca(destino);
            // Coloca a peça na posição de destino
            tab.colocarPeca(p, destino);

            // Caso for capturada uma peça 
            if (pecaCapturada != null)
            {
                // Adiciona a peça ao conjunto de peças capturadas
                capturadas.Add(pecaCapturada);
            }

            // jogada especial roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                // Armazena a posição de origem e de destino da torre a ser movimentada na jogada
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                // Retira a torre da posição de origem
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                // Coloca a peça na posição de destino
                tab.colocarPeca(T, destinoT);
            }

            // jogada especial roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                // Armazena a posição de origem e de destino da torre a ser movimentada na jogada
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                // Retira a torre da posição de origem
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                // Coloca a peça na posição de destino
                tab.colocarPeca(T, destinoT);
            }

            // jogada especial en passant 
            if (p is Peao)
            {
                // Se a coluna da posição de origem for diferente da coluna de destino e não tiver sido capturada nenhuma peça nesta posição
                if (origem.coluna != destino.coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    // Se o peão for branco
                    if (p.cor == Cor.Branca)
                    {
                        // Define a posição do peão a ser capturada como sendo um casa abaixo
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    // Se o peão for preto
                    else
                    {
                        // Define a posição do peão a ser capturada como sendo um casa acima
                        posP = new Posicao(destino.linha -1, destino.coluna);
                    }
                    // Captura a peça de acordo com a posição armazenada acima
                    pecaCapturada = tab.retirarPeca(posP);
                    // Adiciona a peça capturada ao conjunto de peças capturadas
                    capturadas.Add(pecaCapturada);
                }
            }
            return pecaCapturada;
        }

        // Método para desfazer o movimento executado de uma peça
        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            // Retira a peça da posição de destino e adiciona a peça retirada a variável local
            Peca p = tab.retirarPeca(destino);

            // Decrementa a quantidade de movimentos da peça retirada
            p.decrementarQteMovimentos();

            // Se existir uma peça capturada coloca a peça capturada na posição de destino
            // e remove a mesma do conjunto de peças capturadas
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            // Coloca a peça na posição de origem 
            tab.colocarPeca(p, origem);

            // Desfazer jogada especial roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                // Armazena a posição de origem e de destino da torre a ser revertida seu movimento na jogada
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                // Retira a torre da posição de destino
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQteMovimentos();
                // Coloca a torre na posição de origem
                tab.colocarPeca(T, origemT);
            }

            // Desfazer jogada especial roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                // Armazena a posição de origem e de destino da torre a ser revertida seu movimento na jogada
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                // Retira a torre da posição de destino
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQteMovimentos();
                // Coloca a torre na posição de origem
                tab.colocarPeca(T, origemT);
            }

            // Desfazer jogada especial en passant
            if (p is Peao)
            {
                // Se a coluna da posição de origem for diferente da coluna de destino e a peça capturada for vulneravel
                // a jogada especial en passant
                if (origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant)
                {
                    // Retira a peça da posição de destino 
                    Peca peao = tab.retirarPeca(destino);
                    Posicao posP;
                    // Se o peão for branco
                    if (p.cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.coluna);
                    }
                    // Se o peão for preto
                    else
                    {
                        posP = new Posicao(4, destino.coluna);
                    }
                    // Coloca a peça retirada na posição anterior do peão antes de ter sido efetuada a jogada en passant
                    tab.colocarPeca(peao, posP);
                }
            }
        } 

        // Realiza uma jogada executando o movimento da peça, passando o turno e mudando o jogador
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            // Executa o movimento da peça e adiciona a peça capturada a variável local
            Peca pecaCapturada = executaMovimento(origem, destino);

            // Se ao executar o movimento a peça movida agora está em xeque
            // é desfeito o movimento pois o proprio jogador não pode se colocar em xeque
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Voce não pode se colocar em xeque!");
            }

            // Se ao executar o movimento o Rei adversário agora está em xeque
            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
           
            // Se o adversário do jogador atual estiver em xeque-mate termina a partida
            if (testeXequemate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                // Incrementa o turno e muda o jogador
                turno++;
                mudaJogador();
            }

            Peca p = tab.peca(destino);

            // jogada especial en passant
            // Se a peça for um peao e o mesmo for movido 2 casas para cima ou 2 casas para baixo
            if (p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2))
            {
                vulneravelEnPassant = p; // Atribui a peça como sendo vulneravel ao en passant
            }
            else
            {
                vulneravelEnPassant = null; // Atribui a peça como não sendo vulneravel ao en passant
            }
        }

        // Valida a posição de origem e retorna uma mensagem de erro referente a exceção personalizada 
        public void validarPosicaoDeOrigem(Posicao pos)
        {
            // Se não existir nenhuma peça na posição informada
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            // Se a peça escolhida for de outra cor
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            // Se a peça escolhida não puder ser movida para nenhuma posição
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        // Valida a posição de destino da peça a ser movida
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            // Caso a peça não possa ser movida para um determinada posição de destino
            if (!tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        // Muda o jogador atual para branco se o mesmo for preto ou muda o jogador atual para preto caso o mesmo for branco
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

        // Retorna um conjunto contendo somente as peças capturadas de uma determinada cor
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            // Instância um conjunto auxiliar
            HashSet<Peca> aux = new HashSet<Peca>();

            // Percorre o conjunto que contem as peças capturadas
            foreach (Peca x in capturadas)
            {
                // Adiciona ao conjunto auxiliar as peças de uma determinada cor
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        // Retorna um conjunto contendo somente as peças em jogo 
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            // Instância um conjunto auxiliar
            HashSet<Peca> aux = new HashSet<Peca>();

            // Percorre o conjunto que contem todas as peças do tabuleiro
            foreach (Peca x in pecas)
            {
                // Adiciona ao conjunto auxiliar as peças de uma determinada cor
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            // Remove as peças capturadas de uma determinada cor do conjunto
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        // Método para retornar a cor da peça adversaria
        private Cor adversaria(Cor cor)
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

        // Método para retornar a peça do tipo 'Rei' de uma determinada cor caso exista
        private Peca rei(Cor cor)
        {
            // Percorre as peças existentes no jogo de uma determinada cor e caso a peça seja do tipo 'Rei' a mesma é retornada
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        // Método para retornar se uma peça do tipo 'Rei' de uma determinada cor está em xeque
        public bool estaEmXeque(Cor cor)
        {
            // Atribui a peça rei de uma determinada cor a variavel 'R'
            Peca R = rei(cor);

            // Se 'R' for nulo é lançada uma exceção
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }

            // Percorre os as peças em jogo do adversário e caso alguma peça esteja em xeque com o 'Rei' do adversário
            // retorna que o rei está em xeque senão retorna que não está 
            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                // Instância uma matriz de booleanos contendo os movimentos possiveis de cada uma das peças em jogo do adversário
                bool[,] mat = x.movimentosPossiveis();

                // Se o elemento da matriz possuir um movimento possivel igual a linha e a coluna em que o 'Rei' adversário está localizado
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        // Método para testar se o 'Rei' de uma determinada cor está em xequemate 
        public bool testeXequemate(Cor cor)
        {
            // Se não estiver em xeque 
            if (!estaEmXeque(cor))
            {
                return false;
            }
            
            // Percorre as peças em jogo existentes de uma determinada cor
            foreach (Peca x in pecasEmJogo(cor))
            {
                // Atribui a um array os movimentos possiveis de uma determinada peça em jogo    
                bool[,] mat = x.movimentosPossiveis();

                // Percorre as linhas e as colunas do tabuleiro
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        // Se a posição existir na matriz significa que é um movimento possivel
                        if (mat[i, j])
                        {
                            // Armazena a posição de origem
                            Posicao origem = x.posicao;
                            // Define a posição de destino como sendo a possição do movimento possivel
                            Posicao destino = new Posicao(i, j);
                            // Executa o movimento e armazena a peça capturada caso exista uma peça na posição de destino
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            // Retorna se o 'Rei' adversário está em Xeque
                            bool testeXeque = estaEmXeque(cor);
                            // Desfaz o movimento realizado
                            desfazMovimento(origem, destino, pecaCapturada);
                            // Caso em algum movimento possivel da peça o Rei adversário não esteja em 'Xeque'
                            // significa que o Rei adversário não está em 'Xequemate'
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            // Caso não retorne falso significa que o 'Rei' adversário está em xequemate
            return true;
        }

        // Método para colocar uma nova peça em uma determinada linha e coluna do tabuleiro
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            // Adiciona a peça ao conjunto de peças existentes no tabuleiro
            pecas.Add(peca);
        }

        // Coloca as peças iniciais de uma partida de xadrez no tabuleiro
        private void colocarPecas()
        {
            // Coloca as peças brancas no tabuleiro
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            // Coloca as peças pretas no tabuleiro
            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));
        }
    }
}