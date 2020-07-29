using System;
using System.Collections.Generic;

namespace banco.Telas
{
    public class Tela : TelaBase
    {

        /// <summary>
        /// variavel para garantir uma unica instancia da tela
        /// </summary>
        private static Tela _instance;

        /// <summary>
        /// numero de cedulas diferentes que o programa reconhece
        /// </summary>
        private static int _numeroDeCedulas = Enum.GetNames(typeof(EnumCedula)).Length;

        /// <summary>
        /// vetor usado para guardar as cedulas
        /// </summary>
        private uint[] _cedulas = new uint[_numeroDeCedulas];


        /// <summary>
        /// garante que apenas uma instancia da classe seja instanciada
        /// </summary>
        /// <returns>retorna a instancia</returns>
        public static Tela Instance()
        {
            if (_instance == null)
            {
                _instance = new Tela();
            }
            return _instance;
        }

        /// <summary>
        /// exibe o menu principal da aplicacao
        /// lê do usuario a opção que ele deseja
        /// </summary>
        public void Executar()
        {
            var executando = true;
            do
            {
                Titulo(mensagem: $"-- Menu de opções --");
                Escrever(mensagem: $"1 - Repor Valores");
                Escrever(mensagem: $"2 - Retirar Valores");
                Escrever(mensagem: $"3 - Mostrar Cedulas Disponiveis");
                Escrever(mensagem: $"0 - Sair");

                var opcao = LerInt();

                switch (opcao)
                {
                    case 1:
                        ReporValores();
                        break;

                    case 2:
                        RetirarValores();
                        break;

                    case 3:
                        MostrarSaldo();
                        break;

                    case 0:
                        Environment.Exit(exitCode: 0);
                        break;

                    default:
                        OpcaoInvalida();
                        break;
                }

            } while (executando);
        }

        /// <summary>
        /// exibe menu de retirar 
        /// exibe notas disponiveis, ou erro se nao disponiveis
        /// lê do usuario o valor a remover
        /// </summary>
        private void RetirarValores()
        {
            Titulo(mensagem: $"-- Retirando Valores --");

            Escrever(mensagem: "Cedulas disponiveis: ", espacoEmBranco: 2);

            var cedulasDisponiveis = RetornaCedulasDisponiveis();
            Escrever(mensagem: cedulasDisponiveis, foregroundColor: ConsoleColor.Yellow, espacoEmBranco: 2);

            if (cedulasDisponiveis == "Nenhuma cedula disponivel")
            {
                AguardarTecla(mensagem: "Pressione qualquer tecla para voltar");
                return;
            }


            var valorARetirar = LerInt(mensagem: "Digite a quantia a retirar: ");

            CalcularCedulasARetirar(valorARetirar: valorARetirar);

            AguardarTecla();


        }

        /// <summary>
        /// calcula e subtrai as cedulas a remover conforme o valor desejado
        /// avisa um erro caso nao seja possivel
        /// </summary>
        /// <param name="valorARetirar">quantidade em R$ a remover</param>
        private void CalcularCedulasARetirar(uint valorARetirar)
        {

            var newCedulas = new uint[_numeroDeCedulas];

            Array.Copy(sourceArray: _cedulas, destinationArray: newCedulas, length: _numeroDeCedulas);

            uint[] cedulasRetiradas = new uint[_numeroDeCedulas];

            uint valorRetirado = valorARetirar;

            var montante = RetornaValorTotal();

            while (valorARetirar % 5 > 0)
            {
                valorARetirar -= 2;
                if (newCedulas[4] == 0)
                {
                    Escrever(mensagem: "Não foi possivel realizar esta operacao");
                    return;

                }
                newCedulas[4] -= 1;
                cedulasRetiradas[4] += 1;
            }

            while (newCedulas[0] > 0 && valorARetirar >= 50)
            {
                valorARetirar -= 50;
                newCedulas[0] -= 1;
                cedulasRetiradas[0] += 1;
            }

            while (newCedulas[1] > 0 && valorARetirar >= 20)
            {
                valorARetirar -= 20;
                newCedulas[1] -= 1;
                cedulasRetiradas[1] += 1;
            }

            while (newCedulas[2] > 0 && valorARetirar >= 10)
            {
                valorARetirar -= 10;
                newCedulas[2] -= 1;
                cedulasRetiradas[2] += 1;
            }

            while (newCedulas[3] > 0 && valorARetirar >= 5)
            {
                valorARetirar -= 5;
                newCedulas[3] -= 1;
                cedulasRetiradas[3] += 1;
            }

            if (valorARetirar == 0)
            {
                _cedulas = newCedulas;

                Escrever(mensagem: $"Total Retirado: ");

                for (var i = 0; i < _numeroDeCedulas; i++)
                    if (cedulasRetiradas[i] > 0)
                        Escrever(mensagem: $"{RetornaValorCedula(i).ToString()} - {cedulasRetiradas[i]} notas");

                Escrever(mensagem: $" = R${valorRetirado}");
            }
            else
            {
                Escrever(mensagem: "Não foi possivel realizar esta operacao");
                return;
            }
        }

        /// <summary>
        /// adiciona cedulas 
        /// </summary>
        private void ReporValores()
        {
            Titulo(mensagem: $"-- Repondo Valores --");

            for (var i = 0; i < _numeroDeCedulas; i++)
            {
                var cedula = LerInt(mensagem: $"Informe a quantidade de notas de {(EnumCedula)i} Reais");
                _cedulas[i] = (uint)cedula;
            }
            MostrarSaldo();
        }

        /// <summary>
        /// exibe na tela o saldo total e a quantidade de cada cedula
        /// </summary>
        private void MostrarSaldo()
        {
            Titulo(mensagem: $"-- Mostrando Saldo --");

            Escrever(mensagem: $"Valor total disponivel é de R$ {RetornaValorTotal()},00\n");

            for (var i = 0; i < _numeroDeCedulas; i++)
            {
                Escrever(mensagem: $"{_cedulas[i]} cedulas de {(EnumCedula)i} Reais");
            }

            AguardarTecla();
        }

        /// <summary>
        /// retorna o montante total entre todas as cedulas
        /// </summary>
        /// <returns>montante total como inteiro sem sinal</returns>
        private uint RetornaValorTotal()
        {
            var valorTotal = default(uint);

            for (var i = 0; i < _numeroDeCedulas; i++)
            {
                valorTotal += RetornaSomaDeCedulas(cedula: i, quantidade: _cedulas[i]);
            }

            return valorTotal;
        }

        /// <summary>
        /// retorna as cedulas disponiveis
        /// </summary>
        /// <returns>cedulas disponiveis como uma string</returns>
        private string RetornaCedulasDisponiveis()
        {

            var cedulasDisponiveis = new List<string>();

            for (var i = 0; i < _numeroDeCedulas; i++)
            {
                if (_cedulas[i] > 0)
                    cedulasDisponiveis.Add(item: RetornaValorCedula(cedula: i).ToString());
            }

            var cedulasDIsponiveisString = cedulasDisponiveis.Count > 0 ? String.Join(separator: " - ", values: cedulasDisponiveis) : "Nenhuma cedula disponivel";

            return cedulasDIsponiveisString;
        }

        /// <summary>
        /// retorna o montante somado da quantidade de determinada cedula
        /// </summary>
        /// <param name="cedula">indice da cedula no vetor</param>
        /// <param name="quantidade">quantidade de cedulas</param>
        /// <returns></returns>
        private uint RetornaSomaDeCedulas(int cedula, uint quantidade)
        {

            return quantidade * RetornaValorCedula(cedula: cedula);
        }

        /// <summary>
        /// Retorna o valor correspondente da cedula, passando-se o indice dela no vetor
        /// </summary>
        /// <param name="cedula">indice</param>
        /// <returns>valor da cedula como inteiro sem sinal</returns>
        private uint RetornaValorCedula(int cedula)
        {

            switch (cedula)
            {
                case 0:
                    return (50);

                case 1:
                    return (20);

                case 2:
                    return (10);

                case 3:
                    return (5);

                case 4:
                    return (2);

                default:
                    return 0;
            }
        }

    }
}