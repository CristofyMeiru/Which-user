using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace WhichUser
{
    public partial class FormServer : Form
    {
       private TcpListener? server;
        private Thread? listenThread;

        public FormServer()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(18, 18, 18);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            listenThread = new Thread(InitServer);
            listenThread.IsBackground = true;
            listenThread.Start();
            label1.Text = "Servidor: Ligado";
        }
        private void InitServer()
        {
            try
            {
                server = new TcpListener(IPAddress.Any, 500);
                server.Start();

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    AppendLog("Client connected.");

                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int  bytesLidos = stream.Read(buffer, 0, buffer.Length);
                    string mensagem = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    AppendLog(mensagem);

                    string resposta = "Recebido com sucesso!";
                    byte[] respostaBytes = Encoding.UTF8.GetBytes(resposta);
                    stream.Write(respostaBytes, 0, respostaBytes.Length);

                    client.Close();
                }

            }
            catch (Exception ex)
            {
                AppendLog("Erro: " + ex.Message);
            }
        }
        private void AppendLog(string msg) 
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AppendLog), msg);
            } else
            {
                listBox1.Items.Add(msg);
            }
        }
    }
}
