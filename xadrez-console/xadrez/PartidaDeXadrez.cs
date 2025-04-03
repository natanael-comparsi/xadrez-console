using System;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        // Propriedades autoimplementadas
        public Tabuleiro tab { get; private set; }
        private int turno;
        private Cor jogadorAtual;
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