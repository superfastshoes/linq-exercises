using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class ProductsController : ApiController
    {
        private NORTHWNDEntities _db;

        public ProductsController()
        {
            _db = new NORTHWNDEntities();
        }

        //GET: api/products/discontinued/count
        [HttpGet, Route("api/products/discontinued/count"), ResponseType(typeof(int))]
        public IHttpActionResult GetDiscontinuedCount()
        {
            //Write a query to return the number of discontinued products in the Products table."
            var results = _db.Products.Count(p => p.Discontinued);
            return Ok(results);
        }

        // GET: api/categories/Condiments/products
        [HttpGet, Route("api/categories/{categoryName}/products"), ResponseType(typeof(IQueryable<Product>))]
        public IHttpActionResult GetProductsInCategory(string categoryName)
        {
            // "Write a query to return all products that fall within the given categoryName.
            var results = _db.Products.Where(p => p.Category.CategoryName == categoryName);
            return Ok(results);

           
        }

        // GET: api/products/reports/stock
        [HttpGet, Route("api/products/reports/stock"), ResponseType(typeof(IQueryable<object>))]
        public IHttpActionResult GetStockReport()
        {
            // See this blog post for more information about projecting to anonymous objects. https://blogs.msdn.microsoft.com/swiss_dpe_team/2008/01/25/using-your-own-defined-type-in-a-linq-query-expression/
            //"Write a query to return an array of anonymous objects that have two properties. 
            //A Product property and the total units in stock for each product labelled as 'TotalStockUnits' where TotalStockUnits is greater than 100.");

            var results = from p in _db.Products
                          where p.UnitsInStock > 100
                          select new
                          {
                              product = p.ProductName,
                              TotalStockUnits = p.UnitsInStock + p.UnitsOnOrder
                          };
                          
            return Ok(results);
    
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
