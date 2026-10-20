namespace leandroluiz.Models;

public class Folha
{
    // CHAVE PRIMARIA
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty.ToString();
    public int Mes { get; set; }
    public int Ano { get; set; }
    public int HorasTrabalhadas { get; set; }
    //public bool Disponivel { get; set; } = true;
    public double ValorHora { get; set; }
    public double SalarioBruto { get; set; }
    public double ImpostoRenda { get; set; }
    public double Inss { get; set; }
    public double Fgts { get; set; }
    public double SalarioLiquido { get; set; }   
    public DateTime CriadoEm { get; set; } = DateTime.Now;
}