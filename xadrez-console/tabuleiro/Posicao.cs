namespace tabuleiro
{
    class Posicao
    {
        // Propriedades autoimplementadas
        public int linha { get; set; }
        public int coluna { get; set; }

        // Método construtor contendo argumentos
        public Posicao(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        // Sobrescrita do método ToString
        public override string ToString()
        {
            return linha + ", " + coluna;
        }
    }
}