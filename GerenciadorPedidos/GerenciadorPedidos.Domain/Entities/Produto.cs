using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorPedidos.Domain.Validations;

namespace GerenciadorPedidos.Domain.Entities
{
    public class Produto
    {
        public int Id { get;  set; }
        public string Descricao { get;  set; }
        public int Quantidade { get;  set; }
        public decimal PrecoUnitario { get;  set; }
        public DateTime DataCadastro { get;  set; }
        public ICollection<ItemPedido> ItensPedido { get;  set; }
        public Produto() { }

        public Produto(int id, string descricao, int quantidade, decimal precoUnitario, DateTime dataCadastro, ICollection<ItemPedido> itensPedido)
        {
            Id = id;
            Descricao = descricao;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            DataCadastro = dataCadastro;
            ItensPedido = itensPedido;
        }

        public Produto(int id, string descricao, int quantidade, decimal precoUnitario, DateTime dataCadastro)
        {
            DomainExceptionValidation.When(id < 0, "O ID do produto deve ser positivo!");
            Id = id;
            ItensPedido = new List<ItemPedido>();
            ValidateDomain(descricao, quantidade, precoUnitario, dataCadastro);
        }

        public Produto(string descricao, int quantidade, decimal precoUnitario, DateTime dataCadastro)
        {
            ValidateDomain(descricao, quantidade, precoUnitario, dataCadastro);
        }

        public void Update(string descricao, int quantidade, decimal precoUnitario, DateTime dataCadastro)
        {
            ValidateDomain(descricao, quantidade, precoUnitario, dataCadastro);
        }
        public void ValidateDomain(string descricao, int quantidade, decimal precoUnitario, DateTime dataCadastro)
        {
            DomainExceptionValidation.When(descricao.Length > 255, "A Descrição do produto deve ter no máximo 255 caracteres!");

            Descricao = descricao;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            DataCadastro = dataCadastro;
        }
    }
}
