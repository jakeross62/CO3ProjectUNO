namespace UNOProjectCO3.Game_Connection_Algorithms
{
    partial class GameLobby
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Start_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chat_TextBox = new System.Windows.Forms.TextBox();
            this.text_ChatMessage = new System.Windows.Forms.TextBox();
            this.Say_Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.playerList = new System.Windows.Forms.ListBox();
            this.button_Ready = new System.Windows.Forms.CheckBox();
            this.Return_to_Lobby = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Start_button
            // 
            this.Start_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start_button.Location = new System.Drawing.Point(860, 606);
            this.Start_button.Name = "Start_button";
            this.Start_button.Size = new System.Drawing.Size(340, 53);
            this.Start_button.TabIndex = 1;
            this.Start_button.Text = "Start Game!";
            this.Start_button.UseVisualStyleBackColor = true;
            this.Start_button.Click += new System.EventHandler(this.Start_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Waiting for Players:";
            // 
            // chat_TextBox
            // 
            this.chat_TextBox.Location = new System.Drawing.Point(425, 48);
            this.chat_TextBox.Multiline = true;
            this.chat_TextBox.Name = "chat_TextBox";
            this.chat_TextBox.Size = new System.Drawing.Size(429, 552);
            this.chat_TextBox.TabIndex = 3;
            this.chat_TextBox.TextChanged += new System.EventHandler(this.chat_TextBox_TextChanged);
            // 
            // text_ChatMessage
            // 
            this.text_ChatMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_ChatMessage.Location = new System.Drawing.Point(425, 606);
            this.text_ChatMessage.Name = "text_ChatMessage";
            this.text_ChatMessage.Size = new System.Drawing.Size(310, 29);
            this.text_ChatMessage.TabIndex = 4;
            // 
            // Say_Button
            // 
            this.Say_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Say_Button.Location = new System.Drawing.Point(742, 606);
            this.Say_Button.Name = "Say_Button";
            this.Say_Button.Size = new System.Drawing.Size(112, 45);
            this.Say_Button.TabIndex = 5;
            this.Say_Button.Text = "Say";
            this.Say_Button.UseVisualStyleBackColor = true;
            this.Say_Button.Click += new System.EventHandler(this.Say_Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(420, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Game Chat:";
            // 
            // playerList
            // 
            this.playerList.FormattingEnabled = true;
            this.playerList.Location = new System.Drawing.Point(17, 48);
            this.playerList.Name = "playerList";
            this.playerList.Size = new System.Drawing.Size(386, 576);
            this.playerList.TabIndex = 8;
            // 
            // button_Ready
            // 
            this.button_Ready.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Ready.Appearance = System.Windows.Forms.Appearance.Button;
            this.button_Ready.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Ready.Location = new System.Drawing.Point(860, 542);
            this.button_Ready.Name = "button_Ready";
            this.button_Ready.Size = new System.Drawing.Size(340, 58);
            this.button_Ready.TabIndex = 9;
            this.button_Ready.Text = "Ready";
            this.button_Ready.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button_Ready.CheckedChanged += new System.EventHandler(this.button_Ready_CheckedChanged);
            // 
            // Return_to_Lobby
            // 
            this.Return_to_Lobby.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Return_to_Lobby.Location = new System.Drawing.Point(860, 473);
            this.Return_to_Lobby.Name = "Return_to_Lobby";
            this.Return_to_Lobby.Size = new System.Drawing.Size(340, 63);
            this.Return_to_Lobby.TabIndex = 10;
            this.Return_to_Lobby.Text = "Return To Lobby";
            this.Return_to_Lobby.UseVisualStyleBackColor = true;
            this.Return_to_Lobby.Click += new System.EventHandler(this.Return_to_Lobby_Click);
            // 
            // GameLobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 663);
            this.Controls.Add(this.Return_to_Lobby);
            this.Controls.Add(this.button_Ready);
            this.Controls.Add(this.playerList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Say_Button);
            this.Controls.Add(this.text_ChatMessage);
            this.Controls.Add(this.chat_TextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Start_button);
            this.Name = "GameLobby";
            this.Text = "GameLobby";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Start_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox chat_TextBox;
        private System.Windows.Forms.TextBox text_ChatMessage;
        private System.Windows.Forms.Button Say_Button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox playerList;
        private System.Windows.Forms.CheckBox button_Ready;
        private System.Windows.Forms.Button Return_to_Lobby;
    }
}