using SonicConn.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonicConn.Interfaces
{
    public interface IConnection
    {
        void Connect();
        void Disconnect();

        void CreateSocket();

        void write();

        void read();

        ConnectionStates connectionState { get; }

        int GetLastError();

    }
}
