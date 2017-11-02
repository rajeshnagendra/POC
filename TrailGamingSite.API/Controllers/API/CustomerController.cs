using AutoMapper;
using TrailGamingSite.Models.Model;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TrailGamingSite.DAL.Repository;
using TrailGamingSite.DAL;
using Ninject;

namespace TrailGamingSiteAPI.Controllers.API
{
    public class CustomerController : ApiController
    {
        IKernel kernel = new StandardKernel(new StandardModule());
        CustomerClient oCustRepo = null;
        [HttpGet]
        [Route("api/getcustomers")]
        public async Task<IHttpActionResult> GetCustomers()
        {   
            oCustRepo = kernel.Get<CustomerClient>();

            IEnumerable<Customer> oCustomer = await Task.FromResult(oCustRepo.GetAll());
            var oCustomerItems = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerItem>>(oCustomer);            
            return Ok(oCustomerItems);

        }

        [HttpPost]      
        [Route("api/postcustomer")]
        public HttpResponseMessage PostCustomer( Customer oCustomer)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            oCustRepo = kernel.Get<CustomerClient>();
            oCustRepo.Insert(oCustomer);

            return Request.CreateResponse(HttpStatusCode.OK);            
        }


        [HttpDelete]
        [Route("api/deletecustomer/{id}")]
        public void DeleteCustomer([FromUri] int id)
        {
           oCustRepo = kernel.Get<CustomerClient>();
            Customer oCustomer = new Customer();
            oCustomer.Id = id;
            oCustRepo.Delete(oCustomer);            
        }

        [HttpPost]
        [Route("api/addfund")]
        public HttpResponseMessage AddFunds(Transaction oTransaction)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            TransactionClient oCustRepo = kernel.Get<TransactionClient>();
            
            oCustRepo.AddFunds(oTransaction);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
        

    }
}
