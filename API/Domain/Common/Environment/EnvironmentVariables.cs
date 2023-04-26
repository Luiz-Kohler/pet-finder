namespace Domain.Common.Environment
{
    public class EnvironmentVariables : IEnvironmentVariables
    {
        public string GetEnvironmentVariable(string variableName)
        {
            return System.Environment.GetEnvironmentVariable(variableName);
        }
    }
}
