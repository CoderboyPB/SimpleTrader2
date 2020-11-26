using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.EntityFramework.Services
{
    public class AccountDataService : IDataService<Account>
    {
        private readonly SimpleTraderDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Account> _nonQueryDataService;
        public AccountDataService(SimpleTraderDbContextFactory simpleTraderDbContextFactory)
        {
            _contextFactory = simpleTraderDbContextFactory;
            _nonQueryDataService = new NonQueryDataService<Account>(simpleTraderDbContextFactory);
        }

        public async Task<Account> Create(Account entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            using (SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Accounts.Include(a => a.AssetTransactions).ToListAsync();
            }
        }

        public async Task<Account> GetById(int id)
        {
            using (SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Accounts.Include(a => a.AssetTransactions).FirstOrDefaultAsync(e => e.Id == id);
            }
        }

        public async Task<Account> Update(int id, Account entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}

