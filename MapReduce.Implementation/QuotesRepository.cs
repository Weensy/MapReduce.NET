namespace MapReduce.Analysis.Finance
{
    using Storage.Configurations;


    public class QuotesRepository : MapReduce.Infrastructure.TextBlockRepository
    {
        public QuotesRepository(JobConfiguration configuration) : base(configuration) { }
    }
}