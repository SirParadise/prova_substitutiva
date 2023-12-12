namespace back.Models;
public class Imc
{
    public Imc()
    {
        CriadoEm = DateTime.Now;
    }
    public int ImcId { get; set; }
    public float Peso { get; set; }
    public float Altura { get; set; }
    public float CalculoImc { get; set; }
    public string? Classificacao { get; set; }
    public Aluno? Aluno { get; set; }
    public int AlunoId { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
}
