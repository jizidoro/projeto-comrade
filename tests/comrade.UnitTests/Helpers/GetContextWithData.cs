#region

using comrade.Infrastructure.DataAccess;

#endregion

namespace comrade.UnitTests.Helpers
{
    public class GetContextWithData
    {
        public ComradeContext Excute(ComradeContext context)
        {
            context.SaveChanges();

            return context;
        }
    }
}