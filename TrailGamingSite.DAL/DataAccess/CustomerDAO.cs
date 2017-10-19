using TrailGamingSite.DAL.Repository;
using TrailGamingSite.Models.Model;
using System;
using System.Data.Entity;
using System.Linq;

namespace TrailGamingSite.DAL
{
    public class CustomerDAO<T>  :  ITrailSiteRepository<T> where T : Customer
    {
        ITrailSiteRepository<T> _oCustomerRepository = null;
        DbContext _oDbContext = null;
        public CustomerDAO()
        {
            _oDbContext = new TrailGamingSiteContext();
            _oCustomerRepository = new TrailSiteRepository<T>(_oDbContext);           
        }

        public void Insert(T oEntityModel)
        {
                _oCustomerRepository.Insert(oEntityModel);                         
        }
            
        public void Delete(T oEntityModel)
        {
            using (_oDbContext)            {
                _oCustomerRepository.Delete(oEntityModel);
            }      
        }

        public IQueryable<T> GetAll() 
        {        
            return _oCustomerRepository.GetAll();
        }
                
        public void AddFunds(T oEntityModel)
        {
            //Dummy - implemented in TransactionDAO
            throw new NotImplementedException();
        }
    }
}
