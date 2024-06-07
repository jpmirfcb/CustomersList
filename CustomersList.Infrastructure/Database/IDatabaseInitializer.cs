
namespace CustomersList.Infrastructure.Database
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync( IEnumerable<Type> entityTypes );
    }
}