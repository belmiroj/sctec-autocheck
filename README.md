# sctec-autocheck
Mini-projeto AutoCheck.ConsoleApp para programa SC TEC Desenvolvedor Back-End [.Net]

# AutoCheck.ConsoleApp — Motor de Vistoria Veicular

Motor de processamento via Console Application em C# (.NET) para realização e análise de vistorias técnicas automotivas.

## 🎯 Objetivo do Sistema
Automatizar o checklist de inspeção de veículos (Carros, Motos e Caminhões) em concessionárias e seguradoras, aplicando regras de pontuação, percentuais de aprovação, classificações de estado e emissão de recomendações técnicas para a oficina mecânica.

## 🛠️ Tecnologias e Conceitos de POO Aplicados
- **C# / .NET Core Console Application**
- **Orientação a Objetos**:
  - **Classes Abstratas e Construtores**: `Veiculo` como classe base com uso explícito de `this`.
  - **Herança (`:`)**: Subclasses `Carro`, `Moto` e `Caminhao`.
  - **Polimorfismo (`virtual` / `override`)**: Método `ObterChecklistObrigatorio()` estendido por tipo de veículo.
  - **Encapsulamento**: Propriedades fortemente tipadas e controle de coleções.
- **Estruturas de Repetição**: Laços tradicionais (`foreach` e `for`) para processamento das listas de itens.

## 📊 Regras de Negócio e Cálculo
- **Pontuação por Item**:
  - `Bom`: 10 pontos
  - `Regular`: 5 pontos
  - `Ruim`: 0 pontos
- **Fórmula de Aprovação**: 
  $$\text{Percentual (\%)} = \left(\frac{\text{Pontuação Obtida}}{\text{Pontuação Máxima Possível}}\right) \times 100$$
- **Classificação**:
  - **90% a 100%**: Aprovado com Excelência
  - **60% a 89%**: Aprovado com Apontamentos
  - **0% a 59%**: Reprovado na Vistoria

## 🚀 Como Executar o Projeto
1. Certifique-se de ter o [.NET SDK](https://dotnet.microsoft.com/) instalado em sua máquina.
2. Clone este repositório:
   ```bash
   git clone [https://github.com/belmiroj/sctec-autocheck.git](https://github.com/belmiroj/sctec-autocheck.git)