using banco.Telas;

namespace banco
{
    class Program
    {
        static void Main(string[] args)
        {
            var tela = Tela.Instance();
            tela.Executar();
        }
    }
}
