using System;
using System.Threading.Tasks;
using MRP.Business.Intefaces;
using MRP.Business.Models;
using MRP.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MRP.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MeuDbContext context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await Db.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
        }

        public async Task<Endereco> ObterEnderecoPorPaciente(Guid pacienteId)
        {
            return await Db.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(p => p.PacienteId == pacienteId);
        }
    }
}