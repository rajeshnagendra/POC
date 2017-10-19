using AutoMapper;
using TrailGamingSite.Models.Model;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TrailGamingSite.DAL.Repository;
using TrailGamingSite.DAL;

namespace TrailGamingSiteAPI.Controllers.API
{
    public class CustomerController : ApiController
    {
        [HttpGet]
        [Route("api/getcustomers")]
        public async Task<IHttpActionResult> GetCustomers()
        {
            ITrailSiteRepository<Customer> oCustRepo = new CustomerClient(new CustomerDAO<Customer>());
            IEnumerable<Customer> oCustomer = await Task.FromResult(oCustRepo.GetAll());
            var oCustomerItems = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerItem>>(oCustomer);            
            return Ok(oCustomerItems);

        }

        [HttpPost]      
        [Route("api/PostCustomer")]
        public HttpResponseMessage PostCustomer( Customer oCustomer)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            ITrailSiteRepository<Customer> oCustRepo = new CustomerClient(new CustomerDAO<Customer>());
            oCustRepo.Insert(oCustomer);

            return Request.CreateResponse(HttpStatusCode.OK);            
        }


        [HttpDelete]
        [Route("api/DeleteCustomer/{id}")]
        public void DeleteCustomer([FromUri] int id)
        {
            ITrailSiteRepository<Customer> oCustRepo = new CustomerClient(new CustomerDAO<Customer>());
            Customer oCustomer = new Customer();
            oCustomer.Id = id;
            oCustRepo.Delete(oCustomer);            
        }

        [HttpPost]
        [Route("api/AddFunds")]
        public HttpResponseMessage AddFunds(Transaction oTransaction)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            ITrailSiteRepository<Transaction> oCustRepo = new TransactionClient(new TransactionDAO<Transaction>());
            //ITrailSiteRepository<Transaction> oCustomerRepo = new TransactionDAO<Transaction>();
            oCustRepo.AddFunds(oTransaction);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
        

    }
}
