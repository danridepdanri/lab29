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
        bool alive = false; // чи буде працювати потік для приймання
        UdpClient client;
        int LOCALPORT = 8001; // порт для приймання повідомлень
        int REMOTEPORT = 8001; // порт для передавання повідомлень
        string HOST = "239.255.0.1";
        const int TTL = 20;
        IPAddress groupAddress; // адреса для групового розсилання
        string userName; // ім’я користувача в чаті
        int Localport, Remoteport;
        string host;

        public Form1()
        {
            InitializeComponent();
            loginButton.Enabled = true; // кнопка входу
            logoutButton.Enabled = false; // кнопка виходу
            sendButton.Enabled = false; // кнопка отправки
            chatTextBox.ReadOnly = true; // поле для повідомлень
            groupAddress = IPAddress.Parse(HOST);


        }


        private void ConfigureChat()
        {
            string localPortInput = Microsoft.VisualBasic.Interaction.InputBox("Введіть локальний порт:", "Налаштування чату", LOCALPORT.ToString());
            if (!int.TryParse(localPortInput, out LOCALPORT))
            {
                MessageBox.Show("Некоректне значення локального порту. Використано значення за замовчуванням.");
                LOCALPORT = 8001;

            }

            string remotePortInput = Microsoft.VisualBasic.Interaction.InputBox("Введіть віддалений порт:", "Налаштування чату", REMOTEPORT.ToString());
            if (!int.TryParse(remotePortInput, out REMOTEPORT))
            {
                MessageBox.Show("Некоректне значення віддаленого порту. Використано значення за замовчуванням.");
                REMOTEPORT = 8001;

            }

            string hostInput = Microsoft.VisualBasic.Interaction.InputBox("Введіть хост:", "Налаштування чату", HOST);
            if (!IPAddress.TryParse(hostInput, out IPAddress ipAddress))
            {
                MessageBox.Show("Некоректне значення хоста. Використано значення за замовчуванням.");
                HOST = "239.255.0.1";


            }
            else
            {
                HOST = ipAddress.ToString();

            }
        }




        // обробник натискання кнопок loginButton
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
                //підєднання до групового розсилання
                client.JoinMulticastGroup(groupAddress, TTL);

                // задача на приймання повідомлень
                Task receiveTask = new Task(ReceiveMessages);
                receiveTask.Start();

                // перше повідомлення про вхід нового користувача
                string message = userName + " вошел в чат";
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

        // метод приймання повідомлення
        // метод приймання повідомлення
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
                    // добавляем полученное сообщение в текстовое поле
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
            // додавання кольорового тексту в текстове поле

            // обробник натискання кнопки sendButton
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

        // обробник натискання кнопки logoutButton
        private void logoutButton_Click(object sender, EventArgs e)
        {
            ExitChat();
        }

        // вихід з чату
        private void ExitChat()
        {
            string message = userName + " покидает чат";
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

        // обработчик события закрытия формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (alive)
                ExitChat();
        }

        // збереження логу чату у текстовий файл
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

        // Обработчик события клика по меню для смены фона ChatTextBox
        private void changeBackgroundMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                chatTextBox.BackColor = colorDialog.Color;
                messageTextBox.BackColor = colorDialog.Color;
            }
        }

        // Обработчик события клика по меню для смены шрифта текста
        private void changeFontMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                chatTextBox.Font = fontDialog.Font;
            }
        }

        // Обработчик события клика по меню для смены размера текста
        private void changeTextSizeMenuItem_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Введите размер текста:", "Изменение размера текста", chatTextBox.Font.Size.ToString());
            if (float.TryParse(input, out float size))
            {
                chatTextBox.Font = new Font(chatTextBox.Font.FontFamily, size, chatTextBox.Font.Style);
                messageTextBox.Font = new Font(messageTextBox.Font.FontFamily, size, messageTextBox.Font.Style);
            }
            else
            {
                MessageBox.Show("Некорректный размер текста. Используется текущий размер.");
            }
        }

        // Обработчик события клика по меню для смены цвета текста
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