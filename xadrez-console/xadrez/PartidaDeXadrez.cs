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

            // Incrementa o turno e muda o jogador
            turno++;
            mudaJogador();
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
            if (!tab.peca(origem).podeMoverPara(destino))
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
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            // Coloca as peças pretas no tabuleiro
            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
        }
    }
}