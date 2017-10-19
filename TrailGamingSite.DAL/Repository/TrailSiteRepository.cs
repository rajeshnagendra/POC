using System.Data.Entity;
using System.Linq;

namespace TrailGamingSite.DAL.Repository
{

    class TrailSiteRepository<T> : ITrailSiteRepository<T> where T : class
    {
        protected DbSet<T> _oDbSet = null;
        DbContext _oDbContext = null;
                   
        public TrailSiteRepository(DbContext oDbContext)
        {
            _oDbContext = oDbContext;
            _oDbSet = oDbContext.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return _oDbSet;
        }

        public void Insert(T oEntityModel)
        {
            _oDbSet.Add(oEntityModel);
            _oDbContext.SaveChanges();
        }    
        
        public void Delete(T oEntityModel)
        {            
            _oDbSet.Attach(oEntityModel);
            _oDbSet.Remove(oEntityModel);
            _oDbContext.SaveChanges();
            //soft delete 
            //_oDbContext.Entry(oEntityModel).State = EntityState.Modified;
            //_oDbContext.Entry(oEntityModel).Property("Active").IsModified = true;
            //_oDbContext.SaveChanges();
        }

        public void AddFunds(T oEntityModel)
        {
            _oDbSet.Add(oEntityModel);
            _oDbContext.SaveChanges();
        }

       
    }
}
