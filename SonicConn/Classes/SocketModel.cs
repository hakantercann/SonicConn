using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SonicConn.Classes
{
    public class SocketModel
    {
        public Socket Socket { get; set; }
        public string ipAddress { get; set; }
        public string name { get; set; }
    }
}
