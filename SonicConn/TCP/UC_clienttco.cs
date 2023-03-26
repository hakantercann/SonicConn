using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SonicConn.TCP
{
    public partial class UC_clienttco : UserControl
    {
        Socket client;
        private byte[] _buffer = new byte[1024];
        public UC_clienttco()
        {
            InitializeComponent();
        }
        private bool TcpPing()
        {
            Socket pingSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(textBox1.Text), Convert.ToInt32(textBox2.Text));
            try
            {
                IAsyncResult result = pingSocket.BeginConnect(endPoint, null, null);
                bool success = result.AsyncWaitHandle.WaitOne(1000);
                
                if (success)
                {
                    pingSocket.BeginDisconnect(false,new AsyncCallback(DisconnectCallback), pingSocket);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
        }

        private void DisconnectCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            try
            {
                socket.EndDisconnect(ar);
            }
            catch { }
        }

        private void pingButton_Click(object sender, EventArgs e)
        {

                if(TcpPing())
                {
                    richTextBox1.Text += "Ping successfull" + "\n";
                }
                else
                {
                    richTextBox1.Text += "Ping failure" + "\n";
                }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if(TcpPing())
            {
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(textBox1.Text), Convert.ToInt32(textBox2.Text));
                client.BeginConnect(endPoint, new AsyncCallback(ConnectCallback), null);
                richTextBox1.Text += "Connecting to " + textBox1.Text + "\n";
            }

        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                client.EndConnect(ar);
                richTextBox1.Text += "Connected to " + client.RemoteEndPoint + "\n";
                client.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), client);
            }
            catch 
            {
                richTextBox1.Text += "Failed connect to " + textBox1.Text + "\n";
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            try
            {
                int received = socket.EndReceive(ar);
                if (received < 1)
                {
                    socket.Close();
                    client.Close();
                    client = null;
                    richTextBox1.Text += "Closed connection from " + socket.RemoteEndPoint + "\n";
                }
                else
                {


                    byte[] dataBuffer = new byte[received];
                    Array.Copy(_buffer, dataBuffer, received);
                    string text = Encoding.ASCII.GetString(dataBuffer);
                    text = "Received data: " + text + "  from " + socket.RemoteEndPoint.ToString();
                    Console.WriteLine(text);
                    socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
                    richTextBox1.Text += text + "\n";
                }
            }
            catch { }
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            if(client!= null && client.Connected)
            {
                client.BeginDisconnect(false, new AsyncCallback(DisconnectCallback), client);

            }
        }

        private void sendButton1_Click(object sender, EventArgs e)
        {
            byte[] dataBuffer = Encoding.ASCII.GetBytes(textBox3.Text);
            client.BeginSend(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), client);
            richTextBox1.Text += "Send data: " + textBox2.Text + " to" + client.RemoteEndPoint + "\n";
        }

        private void SendCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            try
            {
                socket.EndSend(ar);
            }
            catch { }
        }

        private void sendButton2_Click(object sender, EventArgs e)
        {
            byte[] dataBuffer = Encoding.ASCII.GetBytes(textBox5.Text);
            client.BeginSend(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), client);
            richTextBox1.Text += "Send data: " + textBox2.Text + " to" + client.RemoteEndPoint + "\n";
        }

        private void sendButton3_Click(object sender, EventArgs e)
        {
            byte[] dataBuffer = Encoding.ASCII.GetBytes(textBox6.Text);
            client.BeginSend(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), client);
            richTextBox1.Text = richTextBox1.Text +  "Send data: " + textBox2.Text + " to" + client.RemoteEndPoint + "\n";
        }
    }
}
