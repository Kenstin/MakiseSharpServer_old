using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Pipes;

namespace MakiseSharpServer.Services
{
    public interface IMakiseSharpPipe
    {
        void Send(string data);
        Task SendAsync(string data);
    }
}
