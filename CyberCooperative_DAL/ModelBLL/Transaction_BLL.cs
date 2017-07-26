using CyberCooperative_DAL.RepositoryBase;
using CyberCooperative_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberCooperative_DAL.ModelBLL
{
   public  class Transaction_BLL: Repository<Transaction_Table>
    {
       public Transaction_BLL(CoperativeDB context)
           : base(context)
       {
           if (context == null)
               throw new ArgumentNullException();
       }
    }
}
