using CyberCooperative_DAL.RepositoryBase;
using CyberCooperative_Model;
using System;

namespace CyberCooperative_DAL.ModelBLL
{
    public class LoanRepayment_BLL : Repository<LoanRepayment_Table>
    {
        public LoanRepayment_BLL(CoperativeDB context)
           : base(context)
       {
           if (context == null)
               throw new ArgumentNullException();
       }
    }
}
