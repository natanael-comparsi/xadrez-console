using tabuleiro;

namespace xadrez
{
    // Herda os atributos e métodos da classe base 'Peca'
    class Rei : Peca
    {
        // Armazena o objeto do tipo partida de xadrez
        private PartidaDeXadrez partida;

        // Método construtor implementado a partir do construtor da classe base
        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
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

        // Método para testar se uma torre está elegivel para efetuar uma jogada especial do tipo roque
        private bool testeTorreParaRoque(Posicao pos)
        {
            Peca p = tab.peca(pos);
            // Retorna true se a torre for elegivel para efetuar a jogada
            return p != null && p is Torre && p.cor == cor && p.qteMovimentos == 0;
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

            // Posição a direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
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
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            // Se a posição do tabuleiro é valida e a peça pode ser movida para esta posição
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            // jogada especial roque
            // Se o Rei nunca se movimentou e o mesmo não está em xeque
            if (qteMovimentos == 0 && !partida.xeque)
            {
                // jogada especial roque pequeno
                // Armazena a posição da torre
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);
                if (testeTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    // Valida se o caminho em que o Rei ira se movimentar para a jogada não existem peças
                    if (tab.peca(p1) == null && tab.peca(p2) == null)
                    {
                        mat[posicao.linha, posicao.coluna + 2] = true;
                    }
                }

                // jogada especial roque grande
                // Armazena a posição da torre
                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4);
                if (testeTorreParaRoque(posT2))
                {
                    // Armazena as 3 posições do caminho que o Rei vai se movimentar
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                    // Valida se o caminho em que o Rei ira se movimentar para a jogada não existem peças
                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null)
                    {
                        mat[posicao.linha, posicao.coluna - 2] = true;
                    }
                }
            }
            // retorna uma matriz contendo os movimentos possiveis
            return mat;
        }
    }
}