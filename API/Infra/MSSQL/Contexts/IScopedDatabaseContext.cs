namespace Infra.MSSQL.Contexts
{
    public interface IScopedDatabaseContext
    {
        DatabaseContext Context { get; }
    }
}
