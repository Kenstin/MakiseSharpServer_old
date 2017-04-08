using System.Threading.Tasks;

namespace MakiseSharpServer.Services
{
    public interface IMakiseSharpPipe
    {
        void Send(string data);

        Task SendAsync(string data);
    }
}
