using tabuleiro;

namespace xadrez
{
    // Herda os atributos e métodos da classe base 'Peca'
    class Peao : Peca
    {
        // Método construtor implementado a partir do construtor da classe base
        public Peao(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        // Sobrescrita do método ToString
        public override string ToString()
        {
            return "P";
        }

        // Método para retornar se uma peça pode ser movida para uma determinada posição
        private bool existeInimigo(Posicao pos)
        {
            Peca p = tab.peca(pos);
            // retorna true se a posição estiver vazia ou se a cor da peça for diferente 
            // da peça que está movendo para a posição recebida por parâmetro
            return p != null && p.cor != cor;
        }

        // Método para retornar se uma determinada posição está livre
        private bool livre(Posicao pos)
        {
            // retorna true se a posição estiver livre
            return tab.peca(pos) == null;
        }

        // Método para retornar os movimentos possiveis de uma peça do tipo 'Peao' em uma matriz de booleanos
        public override bool[,] movimentosPossiveis()
        {
            // Instância uma matriz contendo uma linha e uma coluna que armazena um booleano referente a estes 2 valores
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            // Instância uma nova posição no tabuleiro como sendo zero
            Posicao pos = new Posicao(0, 0);

            // Se a cor do peao for branca
            if (cor == Cor.Branca)
            {
                // Posição acima 1 casa
                pos.definirValores(posicao.linha - 1, posicao.coluna);
                // Se a posição do tabuleiro é valida e a posição estiver livre
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                // Posição acima 2 casas
                pos.definirValores(posicao.linha - 2, posicao.coluna);
                Posicao p2 = new Posicao(posicao.linha - 1, posicao.coluna);
                // Se na segunda casa acima as 2 casas estiverem livres e o peão não tiver se movimentado nenhuma vez permite movimentar 2 casas
                if (tab.posicaoValida(p2) && livre(p2) && tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }                

                // Posição acima 1 casa diagonal esquerda
                pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
                // Se a posição do tabuleiro é valida e existe um inimigo na posição
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                // Posição acima 1 casa diagonal direita
                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
                // Se a posição do tabuleiro é valida e existe um inimigo na posição
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
            }
            // Se a cor do peão for preta
            else
            {
                // Posição abaixo 1 casa
                pos.definirValores(posicao.linha + 1, posicao.coluna);
                // Se a posição do tabuleiro é valida e a posição estiver livre
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                // Posição abaixo 2 casas
                pos.definirValores(posicao.linha + 2, posicao.coluna);
                Posicao p2 = new Posicao(posicao.linha + 1, posicao.coluna);
                // Se na segunda casa abaixo as 2 casas estiverem livres e o peão não tiver se movimentado nenhuma vez permite movimentar 2 casas
                if (tab.posicaoValida(p2) && livre(p2) && tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                // Posição abaixo 1 casa diagonal esquerda
                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
                // Se a posição do tabuleiro é valida e existe um inimigo na posição
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                // Posição abaixo 1 casa diagonal direita
                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
                // Se a posição do tabuleiro é valida e existe um inimigo na posição
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
            }

            // retorna uma matriz contendo os movimentos possiveis
            return mat;
        }
    }
}