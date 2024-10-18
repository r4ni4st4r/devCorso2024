using System.Net;
using System.Net.Sockets;
using System.Text;

class Program{
    static void Main(string[] args){
        Server server = new Server();
        server.StartServer(3000);      
    }

    class Server{
        private TcpListener _tcpListener;
        public void StartServer(int port){
            _tcpListener = new TcpListener(IPAddress.Any, port);
            _tcpListener.Start();
            Console.WriteLine("Server started on port: " + port);
        
            while(true){
                TcpClient client = _tcpListener.AcceptTcpClient();
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                clientThread.Start(client);
            }
        }
        private void HandleClient(object obj){
            TcpClient client = (TcpClient)obj;
            byte[] buffer = new byte[1024];
            NetworkStream stream = client.GetStream();
            int byteCount;

            while((byteCount = stream.Read(buffer, 0, buffer.Length))!=0){
                string message = Encoding.ASCII.GetString(buffer, 0, byteCount);
                Console.WriteLine("Recived: " + message);
                Broadcast(message);
            }
            
            client.Close();
        }
        private void Broadcast(string message){
        }
    }
}
