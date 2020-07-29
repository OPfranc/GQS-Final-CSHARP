using System;

namespace banco.Telas
{
    public class TelaBase
    {
        
        /// <summary>
        /// Função base para escrita no console
        /// </summary>
        /// <param name="mensagem">Mensagem a ser escrita</param>
        /// <param name="foregroundColor">Cor dos caracteres</param>
        /// <param name="backgroundColor">Cor do fundo</param>
        /// <param name="espacoEmBranco">Quantidade de linhas quebrar após escrever</param>
        protected void Escrever(string mensagem = "", ConsoleColor foregroundColor = ConsoleColor.Gray, ConsoleColor backgroundColor = ConsoleColor.Black, int espacoEmBranco = 0)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            Console.WriteLine(mensagem.PadRight(Console.WindowWidth - 1));
            Console.ResetColor();

            for (int i = 0; i < espacoEmBranco; i++)
            {
                Escrever();
            }
        }

        /// <summary>
        /// função que lê e trata texto do console
        /// </summary>
        /// <returns>retorna o texto digitado</returns>
        protected string LerString()
        {
            var retorno = "";
            var executando = true;
            do
            {
                if (string.IsNullOrWhiteSpace(value: retorno = Console.ReadLine()))
                {
                    var mensagem = "Entrada invalida. Digite novamente.";
                    Escrever(mensagem: mensagem, foregroundColor: ConsoleColor.DarkRed);
                    continue;
                }
                executando = false;
            } while (executando);

            return retorno;
        }

        /// <summary>
        /// função que lê e trata texto do console
        /// Alem de escrever uma mensagem em tela
        /// </summary>
        /// <param name="mensagem">Mensagem a ser escrita</param>
        /// <returns>retorna o texto digitado</returns>
        protected string LerString(string mensagem)
        {
            Escrever(mensagem: mensagem);

            return LerString();
        }

        /// <summary>
        /// função que lê e trata um valor numerico positivo
        /// </summary>
        /// <returns>retorna o valor lido</returns>
        protected uint LerInt()
        {
            var retorno = 0;
            var mensagem = "";
            var executando = true;
            do
            {
                if (!string.IsNullOrWhiteSpace(value: mensagem))
                {
                    Escrever(mensagem: mensagem, foregroundColor: ConsoleColor.DarkRed);
                    mensagem = "";
                }

                try
                {
                    retorno = int.Parse(s: Console.ReadLine());
                    if (retorno >= 0)
                        executando = false;
                    else
                        mensagem = "Entrada invalida. Digite novamente.";
                }
                catch
                {
                    mensagem = "Entrada invalida. Digite novamente.";
                }
            } while (executando);

            return (uint)retorno;
        }

        /// <summary>
        /// função que lê e trata um valor numerico positivo
        /// Alem de escrever uma mensagem em tela
        /// </summary>
        /// <param name="mensagem">Mensagem a ser escrita</param>
        /// <param name="foregroundColor">Cor dos caracteres</param>
        /// <param name="backgroundColor">Cor do fundo</param>
        /// <returns>retorna o valor lido</returns>
        protected uint LerInt(string mensagem, ConsoleColor foregroundColor = ConsoleColor.Gray, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Escrever(mensagem: mensagem, foregroundColor: foregroundColor, backgroundColor: backgroundColor);

            return LerInt();
        }

        /// <summary>
        /// função para limpar o console
        /// </summary>
        protected void LimparTela()
        {
            Console.Clear();
        }

        /// <summary>
        /// função que para a execução do programa até o usuario pressionar uma tecla
        /// </summary>
        protected void AguardarTecla()
        {
            Console.ReadKey();
        }

        /// <summary>
        /// função que para a execução do programa até o usuario pressionar uma tecla
        /// alem de escrever uma mensagem em tela
        /// </summary>
        /// <param name="mensagem">Mensagem a ser escrita</param>
        /// <param name="foregroundColor">Cor dos caracteres</param>
        /// <param name="backgroundColor">Cor do fundo</param>
        protected void AguardarTecla(string mensagem, ConsoleColor foregroundColor = ConsoleColor.DarkYellow, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Escrever(mensagem: mensagem, foregroundColor: foregroundColor, backgroundColor: backgroundColor);
            AguardarTecla();
        }

        /// <summary>
        /// função que limpa a tela e escreve um titulo
        /// </summary>
        /// <param name="mensagem">Titulo</param>
        /// <param name="foregroundColor">Cor dos caracteres</param>
        /// <param name="backgroundColor">Cor do fundo</param>
        protected void Titulo(string mensagem, ConsoleColor backgroundColor = ConsoleColor.DarkGray)
        {
            LimparTela();
            Escrever(mensagem: mensagem, foregroundColor: ConsoleColor.White, backgroundColor: backgroundColor, espacoEmBranco: 1);
        }

        /// <summary>
        /// função que escreve um erro na tela
        /// </summary>
        public void OpcaoInvalida()
        {
            Escrever(mensagem: "Opção inválida", foregroundColor: ConsoleColor.DarkRed);
        }
    }
}