using System;
using tabuleiro;

namespace xadrez_console
{
    class Tela
    {
        // Imprime um tabuleiro na tela
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            // Percorre o numero de linhas do tabuleiro 
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " "); // Imprime a posição das linhas do tabuleiro

                // Percorre o numero de colunas do tabuleiro
                for (int j = 0; j < tab.colunas; j++)
                {
                    // Se não existir nenhuma peça na posição do tabuleiro
                    if (tab.peca(i, j) == null)
                    {
                        Console.Write("- "); // Imprime um traço e um espaço em branco
                    }
                    else
                    {
                        // Imprime a letra correspondente a peça com a cor sendo cinza se
                        // for uma peça branca ou amarela se for uma preça preta
                        Tela.imprimirPeca(tab.peca(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine(); // Realiza uma quebra de linha
            }
            Console.WriteLine("  a b c d e f g h"); // Imprime a posição das colunas do tabuleiro
        }

        // Imprime uma peça na tela com a sua cor correspondente ao tipo da peça 
        public static void imprimirPeca(Peca peca)
        {
            // Se a peça for branca imprime a cor da letra sendo a cor padrão do console que é cinza
            if (peca.cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            // Se a peça for preta imprime a cor da letra sendo amarela para melhor visualização
            else
            {
                // Armazena a cor padrão do terminal (cinza)
                ConsoleColor aux = Console.ForegroundColor;
                // Define a cor do primeiro plano como sendo amarela
                Console.ForegroundColor = ConsoleColor.Yellow;
                // Imprime a letra da peça
                Console.Write(peca);
                // Atribui novamente a cor padrão da letra ao console
                Console.ForegroundColor = aux;
            }
        }
    }
}