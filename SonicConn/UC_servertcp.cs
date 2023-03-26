using SonicConn.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SonicConn
{
    public partial class UC_servertcp : UserControl
    {
        private byte[] _buffer = new byte[1024];
        private List<SocketModel> _clientSockets = new List<SocketModel>();
        Socket newSock;
        public UC_servertcp()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {


            newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newSock.Bind(new IPEndPoint(IPAddress.Any, 2000));
            newSock.Listen(5);
            newSock.BeginAccept(new AsyncCallback(AcceptCallBack), null);
        }

        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket socket = newSock.EndAccept(ar);
            try
            {
                byte[] welcomeMessage = Encoding.ASCII.GetBytes("Welcome");

                socket.BeginSend(welcomeMessage, 0, welcomeMessage.Length, SocketFlags.None, new AsyncCallback(WelcomeSendCallback), socket);
                SocketModel socketModel = new SocketModel()
                {
                    Socket = socket,
                    name = "1001",
                    ipAddress = socket.RemoteEndPoint.ToString(),
                };
                _clientSockets.Add(socketModel);
                comboBox1.Items.Add(_clientSockets.Last().ipAddress);

                richTextBox1.Text += "Connected from " + socket.RemoteEndPoint + "\n";
                newSock.BeginAccept(new AsyncCallback(AcceptCallBack), null);
            }
            catch { }
        }

        private void WelcomeSendCallback(IAsyncResult ar)
        {

            Socket socket = (Socket)ar.AsyncState;
            try
            {
                socket.EndSend(ar);

                socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            }
            catch { }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket socket = (Socket) ar.AsyncState;
            try
            {


                int received = socket.EndReceive(ar);
                if (received < 1)
                {
                    var socket_index = _clientSockets.Where(x => x.Socket == (socket));
                    comboBox1.SelectedIndex = -1;
                    try
                    {
                        comboBox1.Items.Remove(socket_index.First().name);
                        comboBox1.Items.Remove(socket_index.First().ipAddress);

                    }
                    catch { }
                    _clientSockets.Remove(socket_index.First());
                    socket.Close();
                    richTextBox1.Text += "Closed" + "\n";
                }
                else
                {


                    byte[] dataBuffer = new byte[received];
                    Array.Copy(_buffer, dataBuffer, received);
                    string text = Encoding.ASCII.GetString(dataBuffer);
                    var a = _clientSockets.Where(x => x.Socket == socket);

                    
                    if (text.Length > 1)
                    {
                        switch(text.Substring(0,2))
                        {
                            case "IC": //plcden gelen data  
                                var length = int.Parse(text.Substring(2,1));
                                var highByte = dataBuffer[4];
                                var lowByte = dataBuffer[3];
                                var result = (short)((highByte & 0xff) + (lowByte & 0xff) << 8);
                                Console.WriteLine(length + 1);
                                break;
                            case "X2": // başka client a mesaj gönder
                                if (text.Contains("!"))
                                {
                                    var temp = text.Split('!');
                                    var socket_index = _clientSockets.FindIndex(x => x.name.Equals(temp[1]));
                                    var remoteTarget = _clientSockets[socket_index].Socket;
                                    byte[] dataBuf = Encoding.ASCII.GetBytes(temp[0]);
                                    remoteTarget.BeginSend(dataBuf, 0, dataBuf.Length, SocketFlags.None, new AsyncCallback(SendCallback), remoteTarget);
                                }
                                break;
                            case "si": // id ayarla
                                a.First().name = text.Substring(2);
                                var i = comboBox1.Items.IndexOf(a.First().ipAddress);
                                comboBox1.Items[i] = a.First().name;
                                break;
                            ////case "RN": // yeni kayıt
                            ////    var name = text.Substring(2);
                            ////    SqlConnection conn = Database.GetDatabaseName();
                            ////    try
                            ////    {
                            ////        using (SqlCommand cmd = new SqlCommand())
                            ////        {
                            ////            cmd.Connection = conn;
                            ////            cmd.CommandText = "Insert Into clients (name) VALUES('" + name + "')";
                            ////            cmd.CommandType = CommandType.Text;
                            ////            cmd.ExecuteNonQuery();
                            ////            cmd.Dispose();
                            ////        }
                            ////    }
                            ////    catch { }
                            ////    finally { conn.Close(); }
                            ////    break;
                        }
                    }
                    if (a.First().name.Equals("1001"))
                    {
                        text = "Received data: " + text + "  from " + a.First().ipAddress;
                    }
                    else
                    {
                        text = "Received data: " + text + "  from " + a.First().name;
                    }

                    socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
                    richTextBox1.Text += text + "\n";
                }
            }
            catch { }
        }


        private void sendButton_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex < 0 )
            {
                return;
            }

            var a = _clientSockets.Where(x => x.ipAddress.Equals(comboBox1.Items[comboBox1.SelectedIndex]) || x.name.Equals(comboBox1.Items[comboBox1.SelectedIndex]));
            Socket socket = a.First().Socket;
            byte[] dataBuffer = Encoding.ASCII.GetBytes(textBox2.Text);
            socket.BeginSend(dataBuffer, 0, dataBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            richTextBox1.Text += "Send data: " + textBox2.Text + " to" + socket.RemoteEndPoint + "\n";
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
    }
}
