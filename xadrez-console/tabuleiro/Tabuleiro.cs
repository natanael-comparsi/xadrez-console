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

        // Método para colocar uma peça em uma determinada posição do tabuleiro
        public void colocarPeca(Peca p, Posicao pos) 
        {
            // Atribui a peça proveniente por parâmetro para uma determinada posição na matriz 
            pecas[pos.linha, pos.coluna] = p;
            // Define a posição da peça vindo por parâmetro como sendo a posição vindo por parâmetro
            p.posicao = pos;
        }
    }
}