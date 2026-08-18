using System;
using System.Collections.Generic;
using AutoCheck.ConsoleApp.Models;
using AutoCheck.ConsoleApp.Services;

namespace AutoCheck.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Veiculo> listaVistorias = new List<Veiculo>();
            bool executando = true;

            while (executando)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("          AUTOCHECK .NET - MENU PRINCIPAL         ");
                Console.WriteLine("==================================================");
                Console.WriteLine("1 - Realizar Nova Vistoria");
                Console.WriteLine("2 - Exibir Relatório das Vistorias");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("==================================================");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine()!;

                switch (opcao)
                {
                    case "1":
                        CadastrarNovaVistoria(listaVistorias);
                        break;
                    case "2":
                        ExibirRelatorios(listaVistorias);
                        break;
                    case "0":
                        executando = false;
                        Console.WriteLine("\nEncerrando o sistema. Até logo!");
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida! Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void CadastrarNovaVistoria(List<Veiculo> listaVistorias)
        {
            Console.Clear();
            Console.WriteLine("--- NOVA VISTORIA ---");
            Console.WriteLine("Selecione o tipo de veículo:");
            Console.WriteLine("1 - Carro");
            Console.WriteLine("2 - Moto");
            Console.WriteLine("3 - Caminhão");
            Console.Write("Opção: ");
            string tipoOpcao = Console.ReadLine()!;

            if (tipoOpcao != "1" && tipoOpcao != "2" && tipoOpcao != "3")
            {
                Console.WriteLine("Tipo de veículo inválido! Pressione qualquer tecla para voltar.");
                Console.ReadKey();
                return;
            }

            Console.Write("Marca: ");
            string marca = Console.ReadLine()!;

            Console.Write("Modelo: ");
            string modelo = Console.ReadLine()!;

            Console.Write("Ano: ");
            int.TryParse(Console.ReadLine(), out int ano);

            Console.Write("Quilometragem: ");
            int.TryParse(Console.ReadLine(), out int km);

            Veiculo? veiculo = null;

            if (tipoOpcao == "1")
            {
                Console.Write("Quantidade de Portas: ");
                int.TryParse(Console.ReadLine(), out int portas);
                veiculo = new Carro(marca, modelo, ano, km, portas);
            }
            else if (tipoOpcao == "2")
            {
                Console.Write("Cilindradas (cc): ");
                int.TryParse(Console.ReadLine(), out int cilindradas);
                veiculo = new Moto(marca, modelo, ano, km, cilindradas);
            }
            else if (tipoOpcao == "3")
            {
                Console.Write("Quantidade de Eixos: ");
                int.TryParse(Console.ReadLine(), out int eixos);

                Console.Write("Capacidade de Carga (Toneladas): ");
                double.TryParse(Console.ReadLine(), out double carga);
                veiculo = new Caminhao(marca, modelo, ano, km, eixos, carga);
            }

            if (veiculo == null) return;

            List<string> checklist = veiculo.ObterChecklistObrigatorio();

            Console.WriteLine("\n--- AVALIAÇÃO DO CHECKLIST ---");
            Console.WriteLine("Informe o status digitando o número correspondente:");
            Console.WriteLine("[1] Bom  |  [2] Regular  |  [3] Ruim\n");

            foreach (string itemNome in checklist)
            {
                string status = "";
                while (status == "")
                {
                    Console.Write($"Item: {itemNome} [1-Bom, 2-Regular, 3-Ruim]: ");
                    string entrada = (Console.ReadLine() ?? "").Trim();

                    switch (entrada)
                    {
                        case "1":
                            status = "Bom";
                            break;
                        case "2":
                            status = "Regular";
                            break;
                        case "3":
                            status = "Ruim";
                            break;
                        default:
                            Console.WriteLine("Opção inválida! Digite 1 para Bom, 2 para Regular ou 3 para Ruim.");
                            break;
                    }
                }

                veiculo.AdicionarItemVistoriado(itemNome, status);
            }

            listaVistorias.Add(veiculo);

            Console.WriteLine("\nVistoria concluída com sucesso! Pressione qualquer tecla para retornar ao menu...");
            Console.ReadKey();
        }

        static void ExibirRelatorios(List<Veiculo> listaVistorias)
        {
            if (listaVistorias.Count == 0)
            {
                Console.WriteLine("\nNenhuma vistoria realizada até o momento.");
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();
                return;
            }

            MotorVistoria.ExibirRelatorioGeral(listaVistorias);
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }
    }
}