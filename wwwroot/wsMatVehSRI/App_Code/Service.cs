using System;
//using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
//using System.Xml.Linq;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using System.Threading;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string VerificaPagoMatricula() {
        bool error;
        string strRes = string.Empty;
        Socket sriSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            sriSocket.Connect("10.30.1.200", 2123);            
            //sriSocket.Connect("110.1.7.15", 7777);
            sriSocket.Blocking = false;
            error = false;
        }
        catch (Exception ex)
        {
            error = true;
            sriSocket.Close();
        }

        if (!error)
        {
            string str = "Hello world!!!!";

            try
            { // sends the text with timeout 10s
                Send(sriSocket, System.Text.Encoding.UTF8.GetBytes(str), 0, str.Length, 10000);
            }
            catch (Exception ex)
            {
                error = true;
                sriSocket.Close();
            }

            if (!error)
            {
                byte[] buffer = new byte[11];  // length of the text "Hello world!"
                try
                { // receive data with timeout 10s
                    Receive(sriSocket, buffer, 0, buffer.Length, 10000);
                    strRes = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    sriSocket.Close();
                }
                catch (Exception ex) {
                    sriSocket.Close();
                }
            }
        }

        

        return strRes;
    }



    public static void Send(Socket socket, byte[] buffer, int offset, int size, int timeout)
    {
        int startTickCount = Environment.TickCount;
        int sent = 0;  // how many bytes is already sent
        socket.SendTimeout = 10000;
        do
        {
            if (Environment.TickCount > startTickCount + timeout)
                throw new Exception("Timeout.");
            try
            {
                sent += socket.Send(buffer, offset + sent, size - sent, SocketFlags.None);
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.WouldBlock ||
                    ex.SocketErrorCode == SocketError.IOPending ||
                    ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                {
                    // socket buffer is probably full, wait and try again
                    Thread.Sleep(30);
                }
                else
                    throw ex;  // any serious error occurr
            }
        } while (sent < size);
    }

    public static void Receive(Socket socket, byte[] buffer, int offset, int size, int timeout)
    {
        int startTickCount = Environment.TickCount;
        int received = 0;  // how many bytes is already received
        socket.ReceiveTimeout = 10000;
        do
        {
            if (Environment.TickCount > startTickCount + timeout)
                throw new Exception("Timeout.");
            try
            {
                received += socket.Receive(buffer, offset + received, size - received, SocketFlags.None);
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.WouldBlock ||
                    ex.SocketErrorCode == SocketError.IOPending ||
                    ex.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
                {
                    // socket buffer is probably empty, wait and try again
                    Thread.Sleep(30);
                }
                else
                    throw ex;  // any serious error occurr
            }
        } while (received < size);
    }

    
}
