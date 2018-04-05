using System;
using System.IO.Pipes;
using System.Threading.Tasks;
using MakiseSharpServer.Utility;

namespace MakiseSharpServer.Services
{
    public class MakiseSharpPipe : IMakiseSharpPipe, IDisposable
    {
        private readonly NamedPipeServerStream pipe;

        public MakiseSharpPipe()
        {
            pipe = new NamedPipeServerStream("makisesharp", PipeDirection.InOut, 2, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
        }

        public void Dispose()
        {
            pipe.Dispose();
        }

        public void Send(string data)
        {
            try
            {
                pipe.WaitForConnection();
            }
            catch (Exception)
            {
                pipe.Disconnect();
                pipe.WaitForConnection();
            }

            try
            {
                StringStream.WriteMessage(data, pipe).GetAwaiter().GetResult(); //TODO: prob change to ascii
            }
            catch (Exception)
            {
                return;
            }
        }

        public async Task SendAsync(string data)
        {
            try
            {
                await pipe.WaitForConnectionAsync();
            }
            catch (Exception)
            {
                pipe.Disconnect();
                await pipe.WaitForConnectionAsync();
            }

            try
            {
                await StringStream.WriteMessage(data, pipe);
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
