using MRP.Business.Models;
using System;
using System.Threading.Tasks;

namespace MRP.Business.Intefaces
{
    public interface IPacienteService : IDisposable
    {
        Task Adicionar(Paciente paciente);
        Task Atualizar(Paciente paciente);
        Task Remover(Guid id);

        Task AtualizarEndereco(Endereco endereco);
    }
}
