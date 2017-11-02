using Ninject;
using System.Linq;
using TrailGamingSite.Models.Model;

namespace TrailGamingSite.DAL.Repository
{
    public class CustomerClient
    {
        [Inject]
        public ITrailSiteRepository<Customer> _service { get; set; }

        public CustomerClient() { }

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

    }

    public class TransactionClient
    {
        [Inject]
        public ITrailSiteRepository<Transaction> _service { get; set; }

        public void AddFunds(Transaction oEntityModel)
        {
            _service.AddFunds(oEntityModel);
        }
    }
}
