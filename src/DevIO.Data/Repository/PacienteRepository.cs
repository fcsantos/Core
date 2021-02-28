using Microsoft.EntityFrameworkCore;
using MRP.Business.Intefaces;
using MRP.Business.Models;
using MRP.Data.Context;
using System;
using System.Threading.Tasks;

namespace MRP.Data.Repository
{
    public class PacienteRepository : Repository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(MeuDbContext context) : base(context)
        {
            
        }
        public async Task<Paciente> ObterPacienteEndereco(Guid id)
        {
            return await Db.Pacientes.AsNoTracking()
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
