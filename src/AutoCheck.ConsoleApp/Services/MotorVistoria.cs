using System;
using System.Collections.Generic;
using AutoCheck.ConsoleApp.Models;

namespace AutoCheck.ConsoleApp.Services
{
    public class MotorVistoria
    {
        public static int ConverterStatusParaPontos(string status)
        {
            if (status.Equals("Bom", StringComparison.OrdinalIgnoreCase))
                return 10;
            if (status.Equals("Regular", StringComparison.OrdinalIgnoreCase))
                return 5;
            return 0; // "Ruim"
        }

        public static double CalcularPercentual(int pontuacaoAtingida, int pontuacaoMaxima)
        {
            if (pontuacaoMaxima == 0) return 0.0;
            return ((double)pontuacaoAtingida / pontuacaoMaxima) * 100.0;
        }

        public static string ObterClassificacao(double percentual)
        {
            if (percentual >= 90.0)
                return "APROVADO COM EXCELÊNCIA";
            if (percentual >= 60.0)
                return "APROVADO COM APONTAMENTOS";
            return "REPROVADO NA VISTORIA";
        }

        public static void ExibirRelatorioGeral(List<Veiculo> vistorias)
        {
            Console.Clear();
            Console.WriteLine("===================================================================");
            Console.WriteLine("                 AUTOCHECK .NET - MOTOR DE VISTORIA                ");
            Console.WriteLine("===================================================================\n");

            for (int i = 0; i < vistorias.Count; i++)
            {
                Veiculo veiculo = vistorias[i];

                int pontuacaoAtingida = 0;
                int pontuacaoMaxima = veiculo.VistoriaRealizada.Count * 10;

                foreach (ItemVistoria item in veiculo.VistoriaRealizada)
                {
                    pontuacaoAtingida += ConverterStatusParaPontos(item.Status);
                }

                double percentual = CalcularPercentual(pontuacaoAtingida, pontuacaoMaxima);
                string classificacao = ObterClassificacao(percentual);

                Console.WriteLine($"[{i + 1}/{vistorias.Count}] PROCESSANDO VISTORIA");
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("> DADOS DO VEÍCULO:");

                string tipo = veiculo.GetType().Name;
                Console.WriteLine($"  - Tipo: {tipo}");
                Console.WriteLine($"  - Modelo: {veiculo.Marca} {veiculo.Modelo}");
                Console.WriteLine($"  - Ano: {veiculo.Ano} | Quilometragem: {veiculo.Quilometragem:N0} km");

                if (veiculo is Carro carro)
                {
                    Console.WriteLine($"  - Atributo Específico: {carro.QuantidadePortas} Portas");
                }
                else if (veiculo is Moto moto)
                {
                    Console.WriteLine($"  - Atributo Específico: {moto.Cilindradas} cc");
                }
                else if (veiculo is Caminhao caminhao)
                {
                    Console.WriteLine($"  - Atributo Específico: {caminhao.QuantidadeEixos} Eixos | Cap. Carga: {caminhao.CapacidadeCargaToneladas:F1} Toneladas");
                }

                Console.WriteLine($"\n> AVALIAÇÃO DOS ITENS INSPECIONADOS ({veiculo.VistoriaRealizada.Count} ITENS):");
                foreach (ItemVistoria item in veiculo.VistoriaRealizada)
                {
                    int pts = ConverterStatusParaPontos(item.Status);
                    string marcador = item.Status.ToLower() switch
                    {
                        "bom" => "[OK]",
                        "regular" => "[ ! ]",
                        _ => "[ X ]"
                    };

                    Console.WriteLine($"  {marcador} {item.Nome,-35} Status: {item.Status} ({pts} pts)");
                }

                Console.WriteLine($"\n> RESUMO DA PONTUAÇÃO:");
                Console.WriteLine($"  - Pontuação Atingida: {pontuacaoAtingida} de {pontuacaoMaxima} pontos possíveis");
                Console.WriteLine($"  - Percentual de Aprovação: {percentual:F1}%");
                Console.WriteLine($"  - Classificação Final: [ {classificacao} ]");

                Console.WriteLine($"\n> RELATÓRIO DE MANUTENÇÃO E RECOMENDAÇÕES DA OFICINA:");

                List<ItemVistoria> itensRuins = new List<ItemVistoria>();
                List<ItemVistoria> itensRegulares = new List<ItemVistoria>();

                foreach (ItemVistoria item in veiculo.VistoriaRealizada)
                {
                    if (item.Status.Equals("Ruim", StringComparison.OrdinalIgnoreCase))
                        itensRuins.Add(item);
                    else if (item.Status.Equals("Regular", StringComparison.OrdinalIgnoreCase))
                        itensRegulares.Add(item);
                }

                if (itensRuins.Count == 0 && itensRegulares.Count == 0)
                {
                    Console.WriteLine("Nenhuma pendência mecânica identificada. Veículo liberado para operação!");
                }
                else
                {
                    if (itensRuins.Count > 0)
                    {
                        Console.WriteLine("ITENS CRÍTICOS / REPROVADOS (AÇÃO IMEDIATA):");
                        foreach (ItemVistoria item in itensRuins)
                        {
                            Console.WriteLine($"     - {item.Nome}: Substituir ou reparar item imediatamente.");
                        }
                    }

                    if (itensRegulares.Count > 0)
                    {
                        Console.WriteLine("ITENS DE ATENÇÃO (REVISÃO PREVENTIVA):");
                        foreach (ItemVistoria item in itensRegulares)
                        {
                            Console.WriteLine($"     - {item.Nome}: Realizar regulagem, limpeza e inspeção preventiva.");
                        }
                    }
                }

                Console.WriteLine("-------------------------------------------------------------------\n");
            }

            Console.WriteLine("===================================================================");
            Console.WriteLine("                 FIM DO PROCESSAMENTO DE VISTORIAS                 ");
            Console.WriteLine("===================================================================\n");
        }
    }
}