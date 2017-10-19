using System.Linq;

namespace TrailGamingSite.DAL.Repository
{
    public interface ITrailSiteRepository<T>
    {
      
        /// <summary>
        /// Insert details of type <T>
        /// </summary>
        /// <param name="oEntityModel">Model class</param>
        void Insert(T oEntityModel);

        /// <summary>
        /// Delete a record i.e. of type <T>
        /// </summary>
        /// <param name="oEntityModel">Model class</param>
        void Delete(T oEntityModel);

        /// <summary>
        /// To get data(collections) for a specific type <T>
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        //  decimal GetAvailableBalance(int iCustomerId);
        void AddFunds(T oEntityModel);

    }
}
