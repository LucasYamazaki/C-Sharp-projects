using System;
using System.Net;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        Console.WriteLine("Bem-vindo ao Conversor de Moedas!");

        Console.Write("Digite a moeda de origem: ");
        string moedaOrigem = Console.ReadLine().ToUpper();

        Console.Write("Digite a moeda de destino: ");
        string moedaDestino = Console.ReadLine().ToUpper();

        Console.Write("Digite o valor a ser convertido: ");
        decimal valorOrigem = decimal.Parse(Console.ReadLine());

        decimal valorConvertido = ConverterMoeda(moedaOrigem, moedaDestino, valorOrigem);
        Console.WriteLine($"O valor convertido é: {valorConvertido} {moedaDestino}");

        Console.WriteLine("Pressione qualquer tecla para sair...");
        Console.ReadKey();
    }

    static decimal ConverterMoeda(string moedaOrigem, string moedaDestino, decimal valorOrigem)
    {
        string url = $"https://api.exchangeratesapi.io/latest?base={moedaOrigem}&symbols={moedaDestino}";

        try
        {
            using (var client = new WebClient())
            {
                string json = client.DownloadString(url);
                dynamic rates = JsonConvert.DeserializeObject(json);

                decimal taxaCambio = rates["rates"][moedaDestino];
                decimal valorConvertido = valorOrigem * taxaCambio;

                return valorConvertido;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro na conversão: {ex.Message}");
            return 0;
        }
    }
}