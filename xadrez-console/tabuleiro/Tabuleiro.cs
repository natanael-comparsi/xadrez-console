﻿namespace tabuleiro
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

        // Método para retornar uma 'peca' referente a uma determinada posição recebida por parâmetro
        public Peca peca(Posicao pos)
        {
            return pecas[pos.linha, pos.coluna];
        }

        // Retorna se uma peça existe em uma determinada posição recebida por parâmetro
        public bool existePeca(Posicao pos)
        {
            validarPosicao(pos);
            // Verifica se existe uma peça na posição
            return peca(pos) != null;
        }

        // Coloca uma peça em uma determinada posição do tabuleiro
        public void colocarPeca(Peca p, Posicao pos) 
        {
            // Caso já exista uma peça nesta posição é lançada uma exceção
            if (existePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            // Atribui a peça a matriz de peças existente no tabuleiro
            pecas[pos.linha, pos.coluna] = p;
            // Atribui a posição da peça como sendo a recebida por parâmetro
            p.posicao = pos;
        }

        // Retira uma peça de uma determinada posição do tabuleiro
        public Peca retirarPeca(Posicao pos)
        {
            // Se a posição estiver nula significa que não existe uma peça nesta posição
            if (peca(pos) == null)
            {
                return null;
            }
            // Armazena a peça retirada da posição do tabuleiro
            Peca aux = peca(pos);
            // Define a posição da peça retirada como nula
            aux.posicao = null;
            // Define a posição do tabuleiro como nula
            pecas[pos.linha, pos.coluna] = null;
            // Retorna a peça removida do tabuleiro
            return aux;
        }

        // Retorna se uma determinada posição é valida ou não no tabuleiro
        public bool posicaoValida(Posicao pos)
        {
            // A posição não é valida se for alguma linha ou coluna que não exista no tabuleiro
            if (pos.linha < 0 || pos.linha >= linhas || pos.coluna < 0 || pos.coluna >=  colunas)
            {
                return false;
            }
            return true;
        }

        // Valida se uma determinada posição é valida caso não seja é lançada uma exceção
        public void validarPosicao(Posicao pos) 
        {
            if (!posicaoValida(pos))
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }
    }
}