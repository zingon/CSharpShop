using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CShop.Models;
namespace CShop.Managers
{
    interface IManager
    {
        void create(IModel model);
        void update(IModel model);
        void remove(IModel model);
        IModel findBy(int id);
        
    }
}
