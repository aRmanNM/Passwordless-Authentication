using System.Threading.Tasks;

namespace PasswordlessAuthentication.Services
{
    public interface ISMSService
    {
        bool SendSMS(string receptor, string authToken);
    }
}