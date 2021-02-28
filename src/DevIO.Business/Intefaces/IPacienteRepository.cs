using MRP.Business.Intefaces;
using MRP.Business.Models;
using System;
using System.Threading.Tasks;

namespace MRP.Business.Intefaces
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        Task<Paciente> ObterPacienteEndereco(Guid id);
    }
}
