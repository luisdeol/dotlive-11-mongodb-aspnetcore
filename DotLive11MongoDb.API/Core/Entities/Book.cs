namespace DotLive11MongoDb.API.Core.Entities
{
    public class Book
    {
        public Book(string id, string title, string author)
        {
            Id = id;
            Title = title;
            Author = author;

            Reviews = new List<BookReview>();
        }

        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public List<BookReview> Reviews { get; private set; }

        public void AddReiew(BookReview review)
        {
            Reviews.Add(review);
        }
    }
}
