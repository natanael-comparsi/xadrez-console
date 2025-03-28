using tabuleiro;

namespace xadrez
{
    // Herda os atributos e métodos da classe base 'Peca'
    class Torre : Peca
    {
        // Método construtor implementado a partir do construtor da classe base
        public Torre(Tabuleiro tab, Cor cor) : base(tab,cor)
        {
        }

        // Sobrescrita do método ToString
        public override string ToString()
        {
            return "T";
        }
    }
}