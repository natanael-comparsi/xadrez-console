using tabuleiro;

namespace xadrez
{
    // Herda os atributos e métodos da classe base 'Peca'
    class Bispo : Peca
    {
        // Método construtor implementado a partir do construtor da classe base
        public Bispo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        // Sobrescrita do método ToString
        public override string ToString()
        {
            return "B";
        }

        // Método para retornar se uma peça pode ser movida para uma determinada posição
        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            // retorna true se a posição estiver vazia ou se a cor da peça for diferente 
            // da peça que está movendo para a posição recebida por parâmetro
            return p == null || p.cor != cor;
        }

        // Método para retornar os movimentos possiveis de uma peça do tipo 'Bispo' em uma matriz de booleanos
        public override bool[,] movimentosPossiveis()
        {
            // Instância uma matriz contendo uma linha e uma coluna que armazena um booleano referente a estes 2 valores
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            // Instância uma nova posição no tabuleiro como sendo zero
            Posicao pos = new Posicao(0, 0);

            // Posição a nordeste
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            // Se a posição do tabuleiro é valida e a peça pode ser movida para esta posição
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                // Se a posição não estiver vazia e a peça existente na posição for uma peça adversária
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                // Aumenta uma casa de distancia enquanto a posição for valida
                pos.definirValores(pos.linha - 1, pos.coluna - 1);
            }

            // Posição a noroeste
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                // Se a posição não estiver vazia e a peça existente na posição for uma peça adversária
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                // Aumenta uma casa de distancia enquanto a posição for valida
                pos.definirValores(pos.linha - 1, pos.coluna + 1);
            }

            // Posição a sudeste
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                // Se a posição não estiver vazia e a peça existente na posição for uma peça adversária
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                // Aumenta uma casa de distancia enquanto a posição for valida
                pos.definirValores(pos.linha + 1, pos.coluna + 1);
            }

            // Posição a sudoeste
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                // Se a posição não estiver vazia e a peça existente na posição for uma peça adversária
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                // Aumenta uma casa de distancia enquanto a posição for valida
                pos.definirValores(pos.linha + 1, pos.coluna - 1);
            }

            // retorna uma matriz contendo os movimentos possiveis
            return mat;
        }
    }
}