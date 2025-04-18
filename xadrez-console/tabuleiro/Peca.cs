namespace tabuleiro
{
    abstract class Peca
    {
        // Propriedades autoimplementadas
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        // Método construtor contendo argumentos
        public Peca(Tabuleiro tab, Cor cor)
        {
            this.posicao = null; // Quem define a posição é o método 'colocarPeca'
            this.tab = tab;
            this.cor = cor;
            this.qteMovimentos = 0;
        }

        // Incrementa um movimento a peca
        public void incrementarQteMovimentos()
        {
            qteMovimentos++;
        }

        // Decrementa um movimento da peca
        public void decrementarQteMovimentos()
        {
            qteMovimentos--;
        }

        // Retorna se a peça pode se mover para alguma posição no tabuleiro a partir da posição que ela está ou não
        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();

            // Percorre todas as linhas e colunas do tabuleiro
            for (int i = 0; i < tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    if(mat[i, j])
                    {
                        return true; // Retorna true se existir algum movimento possivel
                    }
                }
            }
            return false;
        }

        // Retorna se uma peça pode ou não ser movida para uma determinada posição
        public bool podeMoverPara(Posicao pos)
        {
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }

        // Método para retornar os movimentos possiveis de uma peça do tipo 'Rei'
        public abstract bool[,] movimentosPossiveis();
    }
}