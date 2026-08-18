namespace AutoCheck.ConsoleApp.Models
{
    public class ItemVistoria
    {
        public string Nome { get; set; }
        public string Status { get; set; } // "Bom", "Regular" ou "Ruim"

        public ItemVistoria(string nome, string status)
        {
            Nome = nome;
            Status = status;
        }
    }
}