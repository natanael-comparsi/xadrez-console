using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        // Método para imprimir uma partida de xadrez em tela
        public static void imprimirPartida(PartidaDeXadrez partida)
        {
            // Imprime o tabuleiro atualizado no console
            Tela.imprimirTabuleiro(partida.tab);
            Console.WriteLine();

            // Imprime as peças capturadas de ambas as cores em tela
            imprimirPecasCapturadas(partida);
            Console.WriteLine();

            // Imprime em qual turno está a partida 
            Console.WriteLine("Turno: " + partida.turno);

            // Se a partida não estiver terminada
            if (!partida.terminada)
            {
                // Imprime o jogador atual
                Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);
                // Se a partida estiver em 'Xeque' retorna em tela
                if (partida.xeque)
                {
                    Console.WriteLine("Xeque!");
                }
            }
            else
            {
                // Imprime o vencedor da partida
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + partida.jogadorAtual);
            }
        }

        // Método para imprimir as peças capturadas em uma partida de xadrez
        public static void imprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            // Imprime as peças brancas e pretas capturadas em tela
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            // As peças pretas capturadas são imprimidas na cor amarela
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        // Imprime os elementos existentes em um determinado conjunto entre colchetes 
        public static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            // Percorre os elementos do conjunto e imprime os mesmos entre colchetes
            Console.Write("[");
            foreach (Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

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
                    // Imprime a letra correspondente a peça com a cor sendo cinza se
                    // for uma peça branca ou amarela se for uma preça preta
                    imprimirPeca(tab.peca(i, j));
                }
                Console.WriteLine(); // Realiza uma quebra de linha
            }
            // Imprime a posição das colunas do tabuleiro
            Console.WriteLine("  a b c d e f g h");
        }

        // Imprime um tabuleiro na tela contendo as posições possiveis da peça selecionada 
        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            // Atribui as cores do terminal as variáveis abaixo
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            // Percorre o numero de linhas do tabuleiro 
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " "); // Imprime a posição das linhas do tabuleiro

                // Percorre o numero de colunas do tabuleiro
                for (int j = 0; j < tab.colunas; j++)
                {
                    // Se a posição do tabuleiro for uma posição possivel de movimentação da peça 
                    // é alterada a cor de fundo da posição 
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    // Senão for uma posição possivel de movimentação da peça é atribuido a cor de fundo original
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    // Imprime a letra da peça na posição do tabuleiro
                    imprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine(); // Realiza uma quebra de linha
            }
            // Imprime a posição das colunas do tabuleiro
            Console.WriteLine("  a b c d e f g h");
            // Define a cor de fundo do terminal como sendo a cor original novamente
            Console.BackgroundColor = fundoOriginal;
        }

        // Le e retorna uma posição do tabuleiro de xadrez inserida em tela pelo usário 
        public static PosicaoXadrez lerPosicaoXadrez()
        {
            // Armazena a posição inserida pelo usuário
            string s = Console.ReadLine();
            // define a coluna como sendo o primeiro caractere
            char coluna = s[0];
            // define a linha como sendo o segundo caractere
            int linha = int.Parse(s[1] + "");
            // Retorna um objeto contendo uma posição do tabuleiro do xadrez
            return new PosicaoXadrez(coluna, linha);
        }

        // Imprime uma peça na tela com a sua cor correspondente ao tipo da peça 
        public static void imprimirPeca(Peca peca)
        {
            // Se não existir uma peça em uma determinada posição do tabuleiro é impresso um traço
            if (peca == null)
            {
                Console.Write("- ");
            }
            else 
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
                Console.Write(" ");
            }
        }
    }
}