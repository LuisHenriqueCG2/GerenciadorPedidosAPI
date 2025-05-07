namespace GerenciadorPedidos.Application.Dtos;

public class ProdutoDto
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public DateTime DataCadastro { get; set; }
    public decimal PrecoUnitario { get; set; }
    public int Quantidade { get; set; }
}