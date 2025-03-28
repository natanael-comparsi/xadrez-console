using System;
using tabuleiro;

namespace xadrez_console
{
    class Tela
    {
        // Método para imprimir um tabuleiro na tela
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            // Percorre o numero de linhas do tabuleiro 
            for (int i = 0; i < tab.linhas; i++)
            {
                // Percorre o numero de colunas do tabuleiro
                for (int j = 0; j < tab.colunas; j++)
                {
                    // Se não existir nenhuma peça na posição do tabuleiro
                    if (tab.peca(i, j) == null)
                    {
                        Console.Write("- "); // Imprime um traço e um espaço em branco
                    }
                    // Senão imprime a letra correspondente a peça
                    else
                    {
                        Console.Write(tab.peca(i,j) + " ");
                    }
                }
                Console.WriteLine(); // Realiza uma quebra de linha
            }
        }
    }
}