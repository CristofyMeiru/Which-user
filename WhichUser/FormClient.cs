using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace WhichUser
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 500);
                NetworkStream stream = client.GetStream();

                string mensagem = messageInput.Text;
                byte[] buffer = Encoding.UTF8.GetBytes(mensagem);
                stream.Write(buffer, 0, buffer.Length);

                byte[] respostaBuffer = new byte[1024];
                int bytesLidos = stream.Read(respostaBuffer, 0, respostaBuffer.Length);
                string resposta = Encoding.UTF8.GetString(respostaBuffer, 0, bytesLidos);

                listBox1.Items.Add(resposta);

                client.Close();
            }
            catch (Exception ex)
            {
                listBox1.Items.Add((string)ex.Message);
            }
        }
    }
}
