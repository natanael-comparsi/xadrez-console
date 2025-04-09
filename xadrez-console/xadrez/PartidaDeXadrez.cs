using System;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        // Propriedades autoimplementadas
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada {  get; private set; }

        // Construtor
        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8); // instância um novo tabuleiro
            turno = 1; // A partida sempre começa no turno 1
            jogadorAtual = Cor.Branca; // A primeira peça que inicia a partida sempre é a branca
            terminada = false;
            // Coloca as peças iniciais no tabuleiro de xadrez
            colocarPecas();
        }

        // Executa o movimento de uma peça de uma posição de origem a uma posição de destino
        public void executaMovimento(Posicao origem, Posicao destino)
        {
            // retira a peça da posição de origem
            Peca p = tab.retirarPeca(origem);
            // Incrementa a quantidade de movimentos efetuados
            p.incrementarQteMovimentos();
            // retira a peca da posição de destino caso exista
            Peca pecaCapturada = tab.retirarPeca(destino);
            // Coloca a peça na posição de destino
            tab.colocarPeca(p, destino);
        }

        // Realiza uma jogada executando o movimento da peça, passando o turno e mudando o jogador
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
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

        // Coloca as peças iniciais de uma partida de xadrez no tabuleiro
        private void colocarPecas()
        {
            // Coloca as peças brancas no tabuleiro
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('d', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 2).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 1).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 1).toPosicao());

            // Coloca as peças pretas no tabuleiro
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 8).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('d', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 7).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 8).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('d', 8).toPosicao());
        }
    }
}