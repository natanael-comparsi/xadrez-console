using tabuleiro;

namespace xadrez
{
    // Herda os atributos e métodos da classe base 'Peca'
    class Torre : Peca
    {
        // Método construtor implementado a partir do construtor da classe base
        public Torre(Tabuleiro tab, Cor cor) : base(tab,cor)
        {
        }

        // Sobrescrita do método ToString
        public override string ToString()
        {
            return "T";
        }

        // Método para retornar se uma peça pode ser movida para uma determinada posição
        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            // retorna true se a posição estiver vazia ou se a cor da peça for diferente 
            // da peça que está movendo para a posição recebida por parâmetro
            return p == null || p.cor != cor;
        }

        // Método para retornar os movimentos possiveis de uma peça do tipo 'Torre' em uma matriz de booleanos
        public override bool[,] movimentosPossiveis()
        {
            // Instância uma matriz contendo uma linha e uma coluna que armazena um booleano referente a estes 2 valores
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            // Instância uma nova posição no tabuleiro como sendo zero
            Posicao pos = new Posicao(0, 0);

            // Posições acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            // Enquanto a posição do tabuleiro é valida e a peça pode ser movida para esta posição
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                // Se a posição não estiver vazia e a peça existente na posição for uma peça adversária
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                // Enquanto não existir uma peça adversária ou não for o fim do tabuleiro
                pos.linha = pos.linha - 1;
            }

            // Posições abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            // Enquanto a posição do tabuleiro é valida e a peça pode ser movida para esta posição
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                // Se a posição não estiver vazia e a peça existente na posição for uma peça adversária
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                // Enquanto não existir uma peça adversária ou não for o fim do tabuleiro
                pos.linha = pos.linha + 1;
            }

            // Posições a direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            // Enquanto a posição do tabuleiro é valida e a peça pode ser movida para esta posição
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                // Se a posição não estiver vazia e a peça existente na posição for uma peça adversária
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                // Enquanto não existir uma peça adversária ou não for o fim do tabuleiro
                pos.coluna = pos.coluna + 1;
            }

            // Posições a esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            // Enquanto a posição do tabuleiro é valida e a peça pode ser movida para esta posição
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                // Se a posição não estiver vazia e a peça existente na posição for uma peça adversária
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                // Enquanto não existir uma peça adversária ou não for o fim do tabuleiro
                pos.coluna = pos.coluna - 1;
            }

            // retorna uma matriz contendo os movimentos possiveis
            return mat;
        }
    }
}