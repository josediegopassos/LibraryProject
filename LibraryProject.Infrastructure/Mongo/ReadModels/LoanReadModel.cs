using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryProject.Infrastructure.Mongo.ReadModels
{
    public class LoanReadModelDel
    {
        [BsonId]
        public Guid LoanId { get; set; }

        [BsonElement("bookId")]
        public Guid BookId { get; set; }

        [BsonElement("loanDate")]
        public DateTime LoanDate { get; set; }

        [BsonElement("returnDate")]
        public DateTime? ReturnDate { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }
    }
}
