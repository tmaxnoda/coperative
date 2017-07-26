using CyberCooperative_Model;
using System;
using System.Data.Entity;
using System.Linq;

namespace CyberCooperative_DAL.RepositoryBase
{
    public class Repository<TEntity> : RepositoryBase.IRepository<TEntity> where TEntity: class
    {
        internal CoperativeDB _context;
        internal DbSet<TEntity> _dbSet;

        public Repository(CoperativeDB context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual TEntity getById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual IQueryable<TEntity> getAll() 
        {
            return _dbSet;
        }

        public virtual IQueryable<TEntity> getAll(Object myObject)
        {
            return null;
        }

        public virtual TEntity getFullObjects(object id)
        {
            return null;

        }

        public virtual void insert(TEntity entity)
        {
            _dbSet.Add(entity);
            commit();
        }

        public virtual void update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            commit();


        }

        public virtual void delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            commit();
        }

        public virtual void delete(Object id)
        {
            TEntity entity = _dbSet.Find(id);
            delete(entity);
        }

        public virtual void commit()
        {
            _context.SaveChanges();
        }

        //public void InserChangeOfPaymentForm(ChangeOfMothlyPaymentForm changePayment)
        //{
        //    using (TransactionScope Trans = new TransactionScope())
        //    {
        //        try
        //        {
        //            Employee_Table employee = new Employee_Table();
        //            //employee = changePayment.ChangeOfFormEmployee;
        //            var employeeTable = _datacontext.Employee_Table;


        //            try
        //            {
        //                if (employeeTable.Any(s => s.Id == changePayment.ChangeOfFormEmployee.Id))
        //                {
        //                    var _employee = _datacontext.Employee_Table.SingleOrDefault(c => c.Id == changePayment.ChangeOfFormEmployee.Id);

        //                    _employee.MonthlySavings = changePayment.Amount;
        //                    _employee.Month = changePayment.Month;
        //                    _employee.Date = DateTime.Now;

        //                    // _datacontext.Employee_Table.Attach(employee);


        //                    _datacontext.Entry(_employee).State = EntityState.Modified;
        //                    _datacontext.SaveChanges();
        //                    //_datacontext.Employee_Table.Add(employee);



        //                    //Trans.Complete();
        //                };

        //            }
        //            catch (DbEntityValidationException dbe)
        //            {

        //                string message = "An Error occured Saving: ";
        //                foreach (var ex in dbe.EntityValidationErrors)
        //                {
        //                    //Aggregate Errors
        //                    string errors = ex.ValidationErrors.Select(e => e.ErrorMessage).Aggregate((ag, e) => ag + " " + e);
        //                    message += errors;
        //                }
        //            }





        //            ContributionPattern_history_Table contributionHistory = new ContributionPattern_history_Table();
        //            contributionHistory.Amount = changePayment.Amount;
        //            contributionHistory.EmployeeId = changePayment.ChangeOfFormEmployee.Id;
        //            var __employee = _datacontext.Employee_Table.FirstOrDefault(c => c.Id == changePayment.ChangeOfFormEmployee.Id);
        //            contributionHistory.Month = changePayment.Month;
        //            contributionHistory.Year = changePayment.Year;
        //            contributionHistory.RegistrationNumber = __employee.RegistrationNumber;
        //            contributionHistory.FullName = __employee.FirstName + "" + __employee.LastName;
        //            _datacontext.ContributionPattern_history_Table.Add(contributionHistory);

        //            _datacontext.SaveChanges();


        //        }
        //        catch (DbEntityValidationException dbe)
        //        {
        //            //throw ex.Message;


        //            //ex.Message;

        //            string message = "An Error occured Saving: ";
        //            foreach (var ex in dbe.EntityValidationErrors)
        //            {
        //                //Aggregate Errors
        //                string errors = ex.ValidationErrors.Select(e => e.ErrorMessage).Aggregate((ag, e) => ag + " " + e);
        //                message += errors;
        //            }
        //        }

        //        Trans.Complete();

        //    }

        //}


    }
}
