using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrailGamingSite.Models.Model;
using Ninject;

namespace TrailGamingSite.DAL.Repository
{
     public class CustomerClient //: ITrailSiteRepository<Customer>
    {
        [Inject]
        public ITrailSiteRepository<Customer> _service { get; set; }
       
        public CustomerClient()
        {

        }       

        //public CustomerClient(ITrailSiteRepository<Customer> service)
        //{
        //    _service = service;
        //}

        public IQueryable<Customer> GetAll()
        {
            return _service.GetAll();
        }

        public void Delete(Customer oEntityModel)
        {
            _service.Delete(oEntityModel);
        }

        public void Insert(Customer oEntityModel)
        {
            _service.Insert(oEntityModel);
        }

        public void AddFunds(Customer oEntityModel)
        {
            throw new NotImplementedException();
        }
    }

    public class TransactionClient //: ITrailSiteRepository<Transaction>
    {
        //private ITrailSiteRepository<Transaction> _service;
        [Inject]
        public ITrailSiteRepository<Transaction> _service { get; set; }


        public TransactionClient(ITrailSiteRepository<Transaction> service)
        {
            _service = service;
        }
        public void AddFunds(Transaction oEntityModel)
        {
            _service.AddFunds(oEntityModel);
        }

        public void Delete(Transaction oEntityModel)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Transaction> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Transaction oEntityModel)
        {
            throw new NotImplementedException();
        }
    }
}
