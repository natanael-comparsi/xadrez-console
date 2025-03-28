namespace tabuleiro
{
    class Peca
    {
        // Propriedades autoimplementadas
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        // Método construtor contendo argumentos
        public Peca(Tabuleiro tab, Cor cor)
        {
            this.posicao = null; // Quem define a posição é o método colocarPeca
            this.tab = tab;
            this.cor = cor;
            this.qteMovimentos = 0;
        }
    }
}