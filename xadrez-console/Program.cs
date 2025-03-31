using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Tenta executar o bloco de código abaixo
            try
            {
                // Instância um tabuleiro de xadrez
                Tabuleiro tab = new Tabuleiro(8, 8);

                // Coloca as peças no tabuleiro
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
                tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(0, 2));

                // Imprime a situação atual do tabuleiro em tela
                Tela.imprimirTabuleiro(tab);

                Console.ReadLine();
            }
            // Caso ocorra algum erro é lançada uma mensagem de erro
            // referente a exceção personalizada
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}