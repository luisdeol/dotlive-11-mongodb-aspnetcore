namespace DotLive11MongoDb.API.Core.Entities
{
    public class BookReview
    {
        public BookReview(int rating, string comment)
        {
            Rating = rating;
            Comment = comment;
        }

        public int Rating { get; private set; }
        public string Comment { get; private set; }
    }
}
