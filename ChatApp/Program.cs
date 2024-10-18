using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

class Program{
    static void Main(string[] args){
        Client client = new Client();
        Console.Write("Insert server IP address: ");
        string serverIp = Console.ReadLine();
        client.StartClient(serverIp, 3000);      
    }

    class Client{
        public void StartClient(string serverIp, int port){
            using(var stream = new TcpClient(serverIp,port).GetStream()){
                Console.WriteLine("Connected to the server!");
                string messageToSend = Console.ReadLine();
                while(!string.IsNullOrEmpty(messageToSend)){
                    byte[] buffer = Encoding.ASCII.GetBytes(messageToSend);
                    stream.Write(buffer, 0, buffer.Length);
                    messageToSend = Console.ReadLine();
                }
            }
        }
    }
}