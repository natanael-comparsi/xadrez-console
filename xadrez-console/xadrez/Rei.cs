using tabuleiro;

namespace xadrez
{
    // Herda os atributos e métodos da classe base 'Peca'
    class Rei : Peca
    {
        // Método construtor implementado a partir do construtor da classe base
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        // Sobrescrita do método ToString
        public override string ToString()
        {
            return "R";
        }
        
        // Método para retornar se uma peça pode ser movida para uma determinada posição
        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            // retorna true se a posição estiver vazia ou se a cor da peça for diferente 
            // da peça que está movendo para a posição recebida por parâmetro
            return p == null || p.cor != cor;
        }

        // Método para retornar os movimentos possiveis de uma peça do tipo 'Rei' em uma matriz de booleanos
        public override bool[,] movimentosPossiveis()
        {
            // Instância uma matriz contendo uma linha e uma coluna que armazena um booleano referente a estes 2 valores
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            // Instância uma nova posição no tabuleiro como sendo zero
            Posicao pos = new Posicao(0, 0);

            // Posição acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            // Se a posição do tabuleiro é valida e a peça pode ser movida para esta posição
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // Posição a nordeste
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            // Se a posição do tabuleiro é valida e a peça pode ser movida para esta posição
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // Posição a sudeste
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            // Se a posição do tabuleiro é valida e a peça pode ser movida para esta posição
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // Posição abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            // Se a posição do tabuleiro é valida e a peça pode ser movida para esta posição
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // Posição a sudoeste
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            // Se a posição do tabuleiro é valida e a peça pode ser movida para esta posição
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // Posição a esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            // Se a posição do tabuleiro é valida e a peça pode ser movida para esta posição
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // Posição a noroeste
            pos.definirValores(posicao.linha -1, posicao.coluna - 1);
            // Se a posição do tabuleiro é valida e a peça pode ser movida para esta posição
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // retorna uma matriz contendo os movimentos possiveis
            return mat;
        }
    }
}