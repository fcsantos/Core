using Core.Business.Models.DTO.SwapDTO;
using System.Threading.Tasks;

namespace Core.Business.Interfaces
{
    public interface IDapperSwapDbRepository
    {
        Task AddTreasuryAccountHolderAsync(TreasuryAccountHolderDTO treasuryAccountHolderDTO);
        Task AddTreasuryAccountAsync(TreasuryAccountDTO treasuryAccount);
        Task AddBalanceAccountAsync(BalanceAccountDTO balanceAccount);
    }
}
