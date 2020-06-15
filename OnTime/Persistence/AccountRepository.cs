using OnTime.Model;
using OnTime.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTime.Persistence
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext context;
        public AccountRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Get(int id)
        {
            
        }

        public async void Update(ClientPersonal clientPersonal)
        {
            // conteawaitxt.ClientPersonal.Update(clientPersonal);
            //await context.SaveChangesAsync();
        }
    }
}
