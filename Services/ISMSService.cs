using System.Threading.Tasks;

namespace PasswordlessAuthentication.Interfaces
{
    public interface ISMSService
    {
        bool SendSMS(string receptor, string authToken);
    }
}