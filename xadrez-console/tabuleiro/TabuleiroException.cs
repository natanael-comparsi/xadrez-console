using System;

namespace tabuleiro
{
    class TabuleiroException : Exception
    {
        // Inicializa uma exceção personalizada do tipo 'TabuleiroException' contendo a mensagem de erro especificada
        public TabuleiroException(string msg) : base(msg) 
        {
        }
    }
}