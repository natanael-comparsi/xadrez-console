using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instancia um novo objeto do tipo 'PosicaoXadrez
            PosicaoXadrez pos = new PosicaoXadrez('c', 7);

            // Imprime a posição real informada de um tabuleiro de xadrez 
            Console.WriteLine(pos);

            // Converte a posição real de um tabuleiro de xadrez 
            // para a posição da matriz do tabuleiro 
            Console.WriteLine(pos.toPosicao());

            Console.ReadLine();
        }
    }
}