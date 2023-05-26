using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace UdpChat
{
    public partial class Form1 : Form
    {
        bool alive = false; // �� ���� ��������� ���� ��� ���������
        UdpClient client;
        int LOCALPORT = 8001; // ���� ��� ��������� ����������
        int REMOTEPORT = 8001; // ���� ��� ����������� ����������
        string HOST = "239.255.0.1";
        const int TTL = 20;
        IPAddress groupAddress; // ������ ��� ��������� ����������
        string userName; // ��� ����������� � ���
        int Localport, Remoteport;
        string host;

        public Form1()
        {
            InitializeComponent();
            loginButton.Enabled = true; // ������ �����
            logoutButton.Enabled = false; // ������ ������
            sendButton.Enabled = false; // ������ ��������
            chatTextBox.ReadOnly = true; // ���� ��� ����������
            groupAddress = IPAddress.Parse(HOST);


        }


        private void ConfigureChat()
        {
            string localPortInput = Microsoft.VisualBasic.Interaction.InputBox("������ ��������� ����:", "������������ ����", LOCALPORT.ToString());
            if (!int.TryParse(localPortInput, out LOCALPORT))
            {
                MessageBox.Show("���������� �������� ���������� �����. ����������� �������� �� �������������.");
                LOCALPORT = 8001;

            }

            string remotePortInput = Microsoft.VisualBasic.Interaction.InputBox("������ ��������� ����:", "������������ ����", REMOTEPORT.ToString());
            if (!int.TryParse(remotePortInput, out REMOTEPORT))
            {
                MessageBox.Show("���������� �������� ���������� �����. ����������� �������� �� �������������.");
                REMOTEPORT = 8001;

            }

            string hostInput = Microsoft.VisualBasic.Interaction.InputBox("������ ����:", "������������ ����", HOST);
            if (!IPAddress.TryParse(hostInput, out IPAddress ipAddress))
            {
                MessageBox.Show("���������� �������� �����. ����������� �������� �� �������������.");
                HOST = "239.255.0.1";


            }
            else
            {
                HOST = ipAddress.ToString();

            }
        }




        // �������� ���������� ������ loginButton
        private void loginButton_Click(object sender, EventArgs e)
        {
            Localport = LOCALPORT;
            Remoteport = REMOTEPORT;
            host = HOST;
            ConfigureChat();
            userName = userNameTextBox.Text;
            userNameTextBox.ReadOnly = true;
            try
            {
                client = new UdpClient(LOCALPORT);
                //�������� �� ��������� ����������
                client.JoinMulticastGroup(groupAddress, TTL);

                // ������ �� ��������� ����������
                Task receiveTask = new Task(ReceiveMessages);
                receiveTask.Start();

                // ����� ����������� ��� ���� ������ �����������
                string message = userName + " ����� � ���";
                byte[] data = Encoding.Unicode.GetBytes(message);
                client.Send(data, data.Length, HOST, REMOTEPORT);

                loginButton.Enabled = false;
                logoutButton.Enabled = true;
                sendButton.Enabled = true;
                if (Localport != LOCALPORT || Remoteport != REMOTEPORT || host != HOST)
                {
                    chatTextBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ����� ��������� �����������
        // ����� ��������� �����������
        private void ReceiveMessages()
        {
            alive = true;
            try
            {
                while (alive)
                {
                    IPEndPoint remoteIp = null;
                    byte[] data = client.Receive(ref remoteIp);
                    string message = Encoding.Unicode.GetString(data);
                    // ��������� ���������� ��������� � ��������� ����
                    this.Invoke(new MethodInvoker(() =>
                    {
                        string time = DateTime.Now.ToShortTimeString();
                        chatTextBox.Text = time + " " + message + "\r\n"
                        + chatTextBox.Text;
                    }));
                }
            }
            catch (ObjectDisposedException)
            {
                if (!alive)
                    return;
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
            // ��������� ����������� ������ � �������� ����

            // �������� ���������� ������ sendButton
            private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                string message = String.Format("{0}: {1}", userName, messageTextBox.Text);
                byte[] data = Encoding.Unicode.GetBytes(message);
                client.Send(data, data.Length, HOST, REMOTEPORT);
                messageTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // �������� ���������� ������ logoutButton
        private void logoutButton_Click(object sender, EventArgs e)
        {
            ExitChat();
        }

        // ����� � ����
        private void ExitChat()
        {
            string message = userName + " �������� ���";
            byte[] data = Encoding.Unicode.GetBytes(message);
            client.Send(data, data.Length, HOST, REMOTEPORT);
            client.DropMulticastGroup(groupAddress);
            alive = false;
            client.Close();

            loginButton.Enabled = true;
            logoutButton.Enabled = false;
            sendButton.Enabled = false;
            userNameTextBox.ReadOnly = false;
        }

        // ���������� ������� �������� �����
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (alive)
                ExitChat();
        }

        // ���������� ���� ���� � ��������� ����
        private void SaveChatLog()
        {
            string logPath = "chat_log.txt";

            try
            {
                using (StreamWriter writer = new StreamWriter(logPath, true))
                {
                    writer.WriteLine("Chat log saved at: " + DateTime.Now.ToString());
                    writer.WriteLine(chatTextBox.Text);
                    writer.WriteLine("-------------------------------------------");
                }

                MessageBox.Show("Chat log saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save chat log: " + ex.Message);
            }
        }





        private void saveChatlogButton_Click(object sender, EventArgs e)
        {
            SaveChatLog();
        }

        // ���������� ������� ����� �� ���� ��� ����� ���� ChatTextBox
        private void changeBackgroundMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                chatTextBox.BackColor = colorDialog.Color;
                messageTextBox.BackColor = colorDialog.Color;
            }
        }

        // ���������� ������� ����� �� ���� ��� ����� ������ ������
        private void changeFontMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                chatTextBox.Font = fontDialog.Font;
            }
        }

        // ���������� ������� ����� �� ���� ��� ����� ������� ������
        private void changeTextSizeMenuItem_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("������� ������ ������:", "��������� ������� ������", chatTextBox.Font.Size.ToString());
            if (float.TryParse(input, out float size))
            {
                chatTextBox.Font = new Font(chatTextBox.Font.FontFamily, size, chatTextBox.Font.Style);
                messageTextBox.Font = new Font(messageTextBox.Font.FontFamily, size, messageTextBox.Font.Style);
            }
            else
            {
                MessageBox.Show("������������ ������ ������. ������������ ������� ������.");
            }
        }

        // ���������� ������� ����� �� ���� ��� ����� ����� ������
        private void changeTextColorMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                chatTextBox.ForeColor = colorDialog.Color;
                messageTextBox.ForeColor = colorDialog.Color;
            }
        }
    }
}