namespace tabuleiro
{
    class Tabuleiro
    {
        // Propriedades autoimplementadas
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca[,] pecas; // Matriz

        // Método construtor contendo argumentos
        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas];
        }

        // Método para retornar uma 'peca' referente a uma determinada linha e coluna  
        public Peca peca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }
    }
}