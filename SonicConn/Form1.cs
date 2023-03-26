using SonicConn.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SonicConn
{
    public partial class Form1 : Form
    {
        Thread connThread;
        private bool flashor1 = false;
        
        private static readonly Form1 _instance = new Form1();

        public static Form1 Instance
        {
            get
            {
                return _instance;
            }
        }
        
        private Form1()
        {
            InitializeComponent();
            connThread = new Thread(connLoop);
            connThread.Start();
        }

        private void connLoop()
        {
            while(true)
            {
                flashor1 = !flashor1;
                if(true)
                {
                    if (flashor1)
                    {
                        connState.BackColor = Color.Lime;
                    }
                    else
                    {
                        connState.BackColor = Color.Green;
                    }
                }
                else
                {
                    if(flashor1)
                    {
                        connState.BackColor = Color.Orange;
                    }
                    else
                    {
                        connState.BackColor = Color.Red;
                    }
                }

                Thread.Sleep(200);
            }
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
