using LibraryProject.Domain.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace LibraryProject.Infrastructure.Mongo
{
    public static class MongoMappings
    {
        public static void RegisterMaps()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("CamelCase", conventionPack, t => true);

            BsonClassMap.RegisterClassMap<BookReadModel>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.BookId).SetElementName("bookId");
                cm.MapMember(c => c.Title).SetElementName("title");
                cm.MapMember(c => c.Author).SetElementName("author");
                cm.MapMember(c => c.YearPublication).SetElementName("yearPublication");
                cm.MapMember(c => c.AvailableQuantity).SetElementName("availableQuantity");
            });

            BsonClassMap.RegisterClassMap<LoanReadModel>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(e => e.LoanId).SetElementName("loanId");
                cm.MapMember(e => e.BookId).SetElementName("bookId");
                cm.MapMember(e => e.LoanDate).SetElementName("loanDate");
                cm.MapMember(e => e.ReturnDate).SetElementName("returnDate");
                cm.MapMember(e => e.Status).SetElementName("status");
            });
        }
    }
}
