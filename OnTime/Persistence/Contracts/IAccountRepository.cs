using OnTime.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTime.Persistence
{
    interface IAccountRepository
    {
        public void Update(ClientPersonal clientPersonal);

        public void Get(int id);
    }
}
