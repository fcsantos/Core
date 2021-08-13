using Core.Business.Intefaces;
using Core.Business.Models;
using Core.Business.Models.DTO;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Data.Repository
{
    public class DapperDbRepository : IDapperDbRepository
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<DapperDbRepository> _logger;
        private readonly IUser _user;

        public DapperDbRepository(IConfiguration configuration, ILogger<DapperDbRepository> logger, IUser user)
        {
            _configuration = configuration;
            _logger = logger;
            _user = user;
        }

        #region Claims
        public async Task<IEnumerable<UserClaimsDto>> GetAllUserClaimsByUserId(string userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    //var sql = $"SELECT * FROM(SELECT u.Id, u.UserId, ClaimType, ClaimValue, d.Name FROM AspNetUserClaims u " +
                    //          $"INNER JOIN Doctors d on u.UserId = d.UserId " +
                    //          $"UNION " +
                    //          $"SELECT u.Id, u.UserId, ClaimType, ClaimValue, CONCAT(p.FirstName, ' ', p.LastName) AS Name FROM AspNetUserClaims u " +
                    //          $"INNER JOIN Patients p on u.UserId = p.UserId " +
                    //          $"UNION " +
                    //          $"SELECT u.Id, u.UserId, ClaimType, ClaimValue,'Utilizador' AS Name FROM AspNetUserClaims u " +
                    //          $"WHERE u.UserId NOT IN(SELECT D.UserId FROM Doctors D) AND " +
                    //          $"u.UserId NOT IN(SELECT P.UserId FROM Patients P)) T " +
                    //          $"WHERE T.UserId = '{userId}'";

                    var sql = $"SELECT u.Id, u.UserId, ClaimType, ClaimValue,'Utilizador' AS Name FROM AspNetUserClaims u " +
                              $"WHERE UserId = '{userId}'";

                    return await conn.QueryAsync<UserClaimsDto>(sql);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllUserClaimsByUserId: " + ex.Message, ex);
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserClaimsDto> GetUserClaimsById(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    return await conn.QueryFirstAsync<UserClaimsDto>($"SELECT Id, UserId, ClaimType, ClaimValue FROM AspNetUserClaims WHERE Id = {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetUserClaimsById: " + ex.Message, ex);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<AllUsersDto>> GetAllUsers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    //var sql = $"SELECT U.Id, U.Email, CONCAT(P.FirstName, ' ',P.LastName) AS Name, 'Paciente' as UserType FROM Patients P " +
                    //          $"INNER JOIN AspNetUsers U ON P.UserId = U.Id " +
                    //          $"UNION " +
                    //          $"SELECT U.Id, U.Email, D.Name, 'Médico' as UserType FROM Doctors D " +
                    //          $"INNER JOIN AspNetUsers U ON D.UserId = U.Id " +
                    //          $"UNION " +
                    //          $"SELECT U.Id, U.Email, U.Email AS Name, 'Utilizador' as UserType FROM AspNetUsers U " +
                    //          $"WHERE Id NOT IN(SELECT D.UserId FROM Doctors D) AND " +
                    //          $"Id NOT IN(SELECT P.UserId FROM Patients P) " +
                    //          $"ORDER BY UserType ASC";

                    var sql = $"SELECT U.Id, U.Email, U.Email AS Name, 'Utilizador' as UserType FROM AspNetUsers U " +
                              $"ORDER BY UserType ASC";
                    return await conn.QueryAsync<AllUsersDto>(sql);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetAllUsers: " + ex.Message, ex);
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Combos

        #endregion

        #region Paged

        #endregion
    }
}
