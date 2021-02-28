using MRP.Business.Models;
using System;
using System.Threading.Tasks;

namespace MRP.Business.Intefaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);

        Task<Endereco> ObterEnderecoPorPaciente(Guid pacienteId);
    }
}