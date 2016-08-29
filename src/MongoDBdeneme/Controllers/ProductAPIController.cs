using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDBdeneme.Models;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDBdeneme.Controllers
{
    [Route("api/Product")]
    public class ProductAPIController : Controller
    {
        DataAccess objDataAccess;

        public ProductAPIController()
        {
            objDataAccess = new DataAccess(); 
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return objDataAccess.GetProducts();
        }

        [HttpGet("{id:length(24)}")]
        public IActionResult Get(string id)
        {
            var product = objDataAccess.GetProduct(new ObjectId(id));
            if (product == null)
            {
                return NotFound();
            }
            return new ObjectResult(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product p)
        {
            objDataAccess.Create(p);
            return new OkObjectResult(p);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, [FromBody] Product p)
        {
            var RecID = new ObjectId(id);
            var product = objDataAccess.GetProduct(RecID);
            if (product == null)
            {
                return NotFound();
            }
            objDataAccess.Update(RecID,product);
            return new OkResult();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var product = objDataAccess.GetProduct(new ObjectId(id));
            if (product == null)
            {
                return NotFound();
            }
            objDataAccess.Remove(new ObjectId(id));
            return new OkResult();
        }
    }
}
