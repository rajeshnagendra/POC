using Ninject;
using Ninject.Modules;
using System.Data.Entity;
using TrailGamingSite.DAL.Repository;
using TrailGamingSite.Models.Model;

namespace TrailGamingSite.DAL
{
    public class StandardModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITrailSiteRepository<Customer>>().To<CustomerDAO<Customer>>();
            Bind<ITrailSiteRepository<Transaction>>().To<TransactionDAO<Transaction>>();
        }
    }
}
