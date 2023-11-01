using DotLive11MongoDb.API.Core.Entities;
using DotLive11MongoDb.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotLive11MongoDb.API.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BooksController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(Book book, [FromServices] IOptions<MongoConfig> option)
        {
            var config = option.Value;
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.Database);
            var collection = database.GetCollection<Book>("books");

            collection.InsertOne(book);

            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAll([FromServices] IOptions<MongoConfig> option)
        {
            var config = option.Value;
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.Database);
            var collection = database.GetCollection<Book>("books");

            var books = collection.Find(new BsonDocument()).ToList();

            return Ok(books);
        }

        [HttpPost("{id}/reviews")]
        public IActionResult Post(string id, BookReview bookReview, [FromServices] IOptions<MongoConfig> option)
        {
            var config = option.Value;
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.Database);
            var collection = database.GetCollection<Book>("books");

            var filter = Builders<Book>.Filter.And(
                Builders<Book>.Filter.Eq(o => o.Id, id)
            );

            var updateDefinition = Builders<Book>.Update.Push(o => o.Reviews, bookReview);

            collection.UpdateOne(filter, updateDefinition);

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id, [FromServices] IOptions<MongoConfig> option)
        {
            var config = option.Value;
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.Database);
            var collection = database.GetCollection<Book>("books");

            var result = collection.Find(o => o.Id.Equals(id)).FirstOrDefault();

            if (result is null)
                return NotFound();

            return Ok(result);
        }
    }
}
