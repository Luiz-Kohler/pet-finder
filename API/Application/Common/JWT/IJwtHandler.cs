using Domain.Entities;

namespace Application.Common.JWT
{
    public interface IJwtHandler
    {
        string CreateToken(User user);
    }
}
