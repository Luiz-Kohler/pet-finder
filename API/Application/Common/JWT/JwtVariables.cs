namespace Application.Common.JWT
{
    public static class JwtVariables
    {
        public static string SecretKey => "1eba73e5-5e6f-45ed-bb32-daffd3301f9b";
        public static DateTime ExpiresAt => DateTime.UtcNow.AddDays(8);
    }
}
