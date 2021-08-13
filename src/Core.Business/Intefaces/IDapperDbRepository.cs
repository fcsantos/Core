using Core.Business.Models;
using Core.Business.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Business.Intefaces
{
    public interface IDapperDbRepository
    {
        #region Claims
        Task<IEnumerable<UserClaimsDto>> GetAllUserClaimsByUserId(string userId);
        Task<UserClaimsDto> GetUserClaimsById(int Id);
        Task<IEnumerable<AllUsersDto>> GetAllUsers();
        #endregion

        #region Combos

        #endregion

        #region Paged

        #endregion
    }
}