using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoDBdeneme.Models
{
    public class DataAccess
    {
        MongoClient _client;
        MongoServer _server;
        MongoDatabase _database;

        public DataAccess()
        {
            _client = new MongoClient("mongodb://104.131.189.219:27017");
            _server = _client.GetServer();
            _database = _server.GetDatabase("EmployeeDB");
        }

        public IEnumerable<Product> GetProducts()
        {
            return _database.GetCollection<Product>("Products").FindAll();
        }

        public Product GetProduct(ObjectId id)
        {
            var res = Query<Product>.EQ(p => p.Id, id); 
            return _database.GetCollection<Product>("Products").FindOne(res);
        }

        public Product Create(Product p)
        {
            _database.GetCollection<Product>("Products").Save(p);
            return p;
        }

        public void Update(ObjectId id, Product p)
        {
            p.Id = id;
            var res = Query<Product>.EQ(pd => pd.Id, id);
            var operation = Update<Product>.Replace(p);
            _database.GetCollection<Product>("Products").Update(res, operation);
        }

        public void Remove(ObjectId id)
        {
            var res = Query<Product>.EQ(e => e.Id,id);
            var operation = _database.GetCollection <Product> ("Products").Remove(res);

        }


    }
}
