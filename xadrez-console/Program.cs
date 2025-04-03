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
                    // Limpa o console
                    Console.Clear();
                    // Imprime o tabuleio atualizado no console
                    Tela.imprimirTabuleiro(partida.tab);

                    Console.WriteLine();
                    // Solicita a inserção da posição da peça de origem ao usuário
                    Console.Write("Origem: ");
                    // Converte a posição lida para a posição da matriz correspondente
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao(); 
                    // Solicita a inserção da posição da peça de destino ao usuário
                    Console.Write("Destino: ");
                    // Converte a posição lida para a posição da matriz correspondente
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                    // Executa o movimento da peça existente na posição de origem informada
                    partida.executaMovimento(origem, destino);
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