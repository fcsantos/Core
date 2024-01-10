using Core.Business.Interfaces;
using Core.Business.Models.DTO.SwapDTO;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Core.Data.Repository
{
    public class DapperSwapDbRepository : IDapperSwapDbRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DapperDbRepository> _logger;
        private readonly IUser _user;

        public DapperSwapDbRepository(IConfiguration configuration, 
                                      ILogger<DapperDbRepository> logger, 
                                      IUser user)
        {
            _configuration = configuration;
            _logger = logger;
            _user = user;
        }

        public async Task AddTreasuryAccountHolderAsync(TreasuryAccountHolderDTO treasuryAccountHolder)
        {
            try
            {
                using (var conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var sql = @"INSERT INTO CoreDB.dbo.treasury_account_holders
                                (id, added_time, balance_account_id, currency, document, legal_name, email, phone_number, status, street, number, 
                                complement, neighborhood, postal_code, city, state, country, address_type)
                            VALUES
                                (@id, @added_time, @balance_account_id, @currency, @document, @legal_name, @email, @phone_number, @status, @street, @number, 
                                @complement, @neighborhood, @postal_code, @city, @state, @country, @address_type);";
                    await conexao.ExecuteAsync(sql, treasuryAccountHolder);
                }            
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AddTreasuryAccountHolderAsync: " + ex.Message, ex);
                throw new Exception(ex.Message);
            }
        }

        public async Task AddTreasuryAccountAsync(TreasuryAccountDTO treasuryAccount)
        {
            try
            {
                using (var conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var sql = @"INSERT INTO CoreDB.dbo.treasury_accounts
                                    (id, treasury_account_holder_id, currency, account_type, added_time, balance_account_id, status)
                                VALUES
                                    (@id, @treasury_account_holder_id, @currency, @account_type, @added_time, @balance_account_id, @status);";
                    await conexao.ExecuteAsync(sql, treasuryAccount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AddTreasuryAccountHolderAsync: " + ex.Message, ex);
                throw new Exception(ex.Message);
            }
        }

        public async Task AddBalanceAccountAsync(BalanceAccountDTO balanceAccount)
        {
            try
            {
                using (var conexao = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var sql = @"INSERT INTO CoreDB.dbo.balance_accounts
                                    (id, treasury_account_id, currency, balance)
                                VALUES
                                    (@id, @treasury_account_id, @currency, @balance);";
                    await conexao.ExecuteAsync(sql, balanceAccount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in AddTreasuryAccountHolderAsync: " + ex.Message, ex);
                throw new Exception(ex.Message);
            }
        }
    }
}
