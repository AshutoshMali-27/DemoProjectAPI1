using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositroy
{
    public interface IloginInterface
    {
        public ClsLoginModel Login(string username, string password);
    }
}
