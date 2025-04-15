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
                // Instancia uma nova partida de xadrez 
                PartidaDeXadrez partida = new PartidaDeXadrez();

                // Enquanto a partida não estiver terminada
                while (!partida.terminada)
                {
                    // Tenta executar o bloco de código abaixo
                    try
                    {
                        // limpa a tela do console
                        Console.Clear();

                        // Imprime a partida atual em tela
                        Tela.imprimirPartida(partida);

                        Console.WriteLine();
                        // Solicita a inserção da posição da peça de origem ao usuário
                        Console.Write("Origem: ");
                        // Converte a posição lida para a posição da matriz correspondente
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();

                        // Valida a posição de origem inserida pelo usuário
                        partida.validarPosicaoDeOrigem(origem);

                        // Instância uma matriz de booleanos contendo os movimentos possiveis de uma determinada peça
                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();

                        // limpa a tela do console
                        Console.Clear();
                        // Imprime o tabuleiro em tela contendo as posições possiveis da movimentação de uma determina peça
                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);
                        Console.WriteLine();

                        // Solicita a inserção da posição da peça de destino ao usuário
                        Console.Write("Destino: ");
                        // Converte a posição lida para a posição da matriz correspondente
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                        // Valida a posição de destino a ser movida a peça
                        partida.validarPosicaoDeDestino(origem, destino);

                        // Executa o movimento da peça existente na posição de origem informada
                        partida.realizaJogada(origem, destino);
                    }
                    // Caso ocorra algum erro é lançada uma mensagem de erro
                    // referente a exceção personalizada
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
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