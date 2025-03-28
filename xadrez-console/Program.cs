using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instância um tabuleiro de xadrez
            Tabuleiro tab = new Tabuleiro(8, 8);

            // Coloca as peças no tabuleiro
            tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
            tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
            tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(2, 4));

            // Imprime a situação atual do tabuleiro em tela
            Tela.imprimirTabuleiro(tab);

            Console.ReadLine();
        }
    }
}