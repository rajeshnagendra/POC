using TrailGamingSite.DAL.Repository;
using TrailGamingSite.Models.Model;
using System;
using System.Data.Entity;
using System.Linq;

namespace TrailGamingSite.DAL
{
    public class TransactionDAO<T> : ITrailSiteRepository<T> where T : Transaction
    {
        ITrailSiteRepository<T> _oTransactionRepository = null;
        DbContext _oDbContext = null;
        public TransactionDAO()
        {
            _oDbContext = new TrailGamingSiteContext();
            _oTransactionRepository = new TrailSiteRepository<T>(_oDbContext);
        }
        public void AddFunds(T oEntityModel)
        {
            _oTransactionRepository.AddFunds(oEntityModel);
        }

        public void Delete(T oEntityModel)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(T oEntityModel)
        {
            throw new NotImplementedException();
        }
    }
}
