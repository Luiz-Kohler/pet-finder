namespace Domain.Common.Environment
{
    public interface IEnvironmentVariables
    {
        string GetEnvironmentVariable(string variableName);
    }
}
