using RetoProgramacionApptelink.Models;

namespace RetoProgramacionApptelink.Repository
{
    public interface ILoginRepository
    {
        bool login(LoginUser login);
    }
}
