namespace UdpChat
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            userNameTextBox = new TextBox();
            messageTextBox = new TextBox();
            loginButton = new Button();
            logoutButton = new Button();
            sendButton = new Button();
            saveChatlogButton = new Button();
            chatTextBox = new RichTextBox();
            label1 = new Label();
            menuStrip1 = new MenuStrip();
            настройкиToolStripMenuItem = new ToolStripMenuItem();
            changeBackgroundMenuItem = new ToolStripMenuItem();
            changeFontMenuItem = new ToolStripMenuItem();
            changeTextSizeMenuItem = new ToolStripMenuItem();
            changeTextColorMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // userNameTextBox
            // 
            userNameTextBox.Location = new Point(291, 24);
            userNameTextBox.Name = "userNameTextBox";
            userNameTextBox.Size = new Size(286, 23);
            userNameTextBox.TabIndex = 0;
            // 
            // messageTextBox
            // 
            messageTextBox.Location = new Point(55, 350);
            messageTextBox.Multiline = true;
            messageTextBox.Name = "messageTextBox";
            messageTextBox.Size = new Size(549, 46);
            messageTextBox.TabIndex = 2;
            // 
            // loginButton
            // 
            loginButton.Location = new Point(630, 23);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(106, 23);
            loginButton.TabIndex = 3;
            loginButton.Text = "loginButton";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            // 
            // logoutButton
            // 
            logoutButton.Location = new Point(630, 52);
            logoutButton.Name = "logoutButton";
            logoutButton.Size = new Size(106, 33);
            logoutButton.TabIndex = 4;
            logoutButton.Text = "logoutButton";
            logoutButton.UseVisualStyleBackColor = true;
            logoutButton.Click += logoutButton_Click;
            // 
            // sendButton
            // 
            sendButton.Location = new Point(630, 363);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(106, 33);
            sendButton.TabIndex = 5;
            sendButton.Text = "sendButton";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;
            // 
            // saveChatlogButton
            // 
            saveChatlogButton.Location = new Point(630, 191);
            saveChatlogButton.Name = "saveChatlogButton";
            saveChatlogButton.Size = new Size(106, 33);
            saveChatlogButton.TabIndex = 6;
            saveChatlogButton.Text = "saveChatLog";
            saveChatlogButton.UseVisualStyleBackColor = true;
            saveChatlogButton.Click += saveChatlogButton_Click;
            // 
            // chatTextBox
            // 
            chatTextBox.Location = new Point(55, 72);
            chatTextBox.Name = "chatTextBox";
            chatTextBox.Size = new Size(549, 272);
            chatTextBox.TabIndex = 7;
            chatTextBox.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(200, 27);
            label1.Name = "label1";
            label1.Size = new Size(75, 15);
            label1.TabIndex = 8;
            label1.Text = "Введите имя";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { настройкиToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStrip1";
            // 
            // настройкиToolStripMenuItem
            // 
            настройкиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { changeBackgroundMenuItem, changeFontMenuItem, changeTextSizeMenuItem, changeTextColorMenuItem });
            настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            настройкиToolStripMenuItem.Size = new Size(79, 20);
            настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // changeBackgroundMenuItem
            // 
            changeBackgroundMenuItem.Name = "changeBackgroundMenuItem";
            changeBackgroundMenuItem.Size = new Size(219, 22);
            changeBackgroundMenuItem.Text = "Изменить цвет фона";
            changeBackgroundMenuItem.Click += changeBackgroundMenuItem_Click;
            // 
            // changeFontMenuItem
            // 
            changeFontMenuItem.Name = "changeFontMenuItem";
            changeFontMenuItem.Size = new Size(219, 22);
            changeFontMenuItem.Text = "Изменить шрифт";
            changeFontMenuItem.Click += changeFontMenuItem_Click;
            // 
            // changeTextSizeMenuItem
            // 
            changeTextSizeMenuItem.Name = "changeTextSizeMenuItem";
            changeTextSizeMenuItem.Size = new Size(219, 22);
            changeTextSizeMenuItem.Text = "Изменить размер шрифта";
            changeTextSizeMenuItem.Click += changeTextSizeMenuItem_Click;
            // 
            // changeTextColorMenuItem
            // 
            changeTextColorMenuItem.Name = "changeTextColorMenuItem";
            changeTextColorMenuItem.Size = new Size(219, 22);
            changeTextColorMenuItem.Text = "Изменить цвет шрифта";
            changeTextColorMenuItem.Click += changeTextColorMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            Controls.Add(label1);
            Controls.Add(chatTextBox);
            Controls.Add(saveChatlogButton);
            Controls.Add(sendButton);
            Controls.Add(logoutButton);
            Controls.Add(loginButton);
            Controls.Add(messageTextBox);
            Controls.Add(userNameTextBox);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox userNameTextBox;
        private TextBox messageTextBox;
        private Button loginButton;
        private Button logoutButton;
        private Button sendButton;
        private Button saveChatlogButton;
        private RichTextBox chatTextBox;
        private Label label1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem настройкиToolStripMenuItem;
        private ToolStripMenuItem changeBackgroundMenuItem;
        private ToolStripMenuItem changeFontMenuItem;
        private ToolStripMenuItem changeTextSizeMenuItem;
        private ToolStripMenuItem changeTextColorMenuItem;
    }
}