using System;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.Text;


namespace MakiseSharpServer.Services
{
    public class MakiseSharpPipe : IMakiseSharpPipe, IDisposable
    {
        private readonly NamedPipeServerStream pipe;
        public MakiseSharpPipe()
        {
            pipe = new NamedPipeServerStream("MakiseSharp",PipeDirection.InOut,2, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
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
            catch (Exception e)
            {
                pipe.Disconnect();
                pipe.WaitForConnection();
            }
            try
            {
                var heh = Encoding.ASCII.GetBytes(data);
                pipe.Write(heh,0,heh.Length);

            }
            catch (Exception e)
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
            catch (Exception e)
            {
                pipe.Disconnect();
                await pipe.WaitForConnectionAsync();
            }
            try
            {
                var heh = Encoding.ASCII.GetBytes(data);
                await pipe.WriteAsync(heh, 0, heh.Length);

            }
            catch (Exception e)
            {
                return;
            }
        }
    }
}
