using tabuleiro;

namespace xadrez
{
    class PosicaoXadrez
    {
        // Propriedades autoimplementadas
        public char coluna { get; set; }
        public int linha { get; set; }

        // Método construtor contendo argumentos
        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }

        // Converte a posição real de um tabuleiro de xadrez 
        // para a posição da matriz do tabuleiro 
        public Posicao toPosicao()
        {
            // Ao ser subtraida a coluna pelo caractere 'a' é subtraido o seu valor internamente como sendo inteiro
            return new Posicao(8 - linha, coluna - 'a'); 
        }

        // Sobrescrita de método 'ToString'
        public override string ToString()
        {
            return "" + coluna + linha;
        }
    }
}