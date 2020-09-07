using System;
using System.Drawing;
using System.Windows.Forms;
using UNOProjectCO3.Game_Connection_Algorithms;
using UNOProjectCO3.UNO;

namespace UNOProjectCO3.Games
{
    public partial class Servers : Form
    {
        public readonly ServerListBackend ServerBackend;

        public string playerName
        {
            get { return text_PlayerName.Text; }
            set { text_PlayerName.Text = value; }
        }

        public bool CanCreateAGame
        {
            get
            {
                return !string.IsNullOrEmpty(playerName.Trim());
            }
        }

        public bool CanJoinGame
        {
            get
            {
                return CanCreateAGame && list_Servers.SelectedIndex >= 0;
            }
        }

        public Servers()
        {
            InitializeComponent();
            text_PlayerName_TextChanged(null, null);
            text_PlayerName.Focus();
            ServerBackend = new ServerListBackend();
        }

        private void Servers_Load(object sender, EventArgs e)
        {
            ServerBackend.EntryReceived += (obj) =>
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    var items = list_Servers.Items;
                    lock (items)
                    {
                        var i = items.IndexOf(obj);
                        if (i >= 0)
                            items.RemoveAt(i);

                        if (obj.State != GameState.ShuttingDown)
                        {
                            if (i == -1)
                            {
                                items.Add(obj);
                            }
                            else
                            {
                                items.Insert(i, obj);
                            }
                        }
                    }
                    UpdateButtonStates();
                }));
            };
            RefreshServers();
        }        

        private void text_PlayerName_TextChanged(object sender, EventArgs e)
        {
            text_PlayerName.BackColor = CanCreateAGame ? Color.White : Color.White;
            UpdateButtonStates();
        }

        public void UpdateButtonStates()
        {
            Create_Button.Enabled = CanCreateAGame;
            Join_Button.Enabled = CanJoinGame;
        }

        public void RefreshServers()
        {
            lock (list_Servers.Items)
                list_Servers.Items.Clear();
            ServerBackend.SendExistenceRequest();
        }

        void lobby_Post(ref GameLobby lobby)
        {
            if (lobby != null)
            {
                if (!lobby.ConnectedEvent.WaitOne(5000))
                {
                    GameHost.ShutDown();
                    MessageBox.Show("Unable to connect to server!", "Session timeout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lobby.Close();
                    lobby.Dispose();
                    return;
                }
                lobby.FormClosed += lobby_FormClosed;
                Hide();
            }
        }
        void list_Servers_Select(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void Join_Button_Click(object sender, EventArgs e)
        {
            Join_Button.Enabled = true;
            var gho = list_Servers.SelectedItem as GameHostEntry;

            if (gho == null)
            {
                if (list_Servers.Items.Count < 1)
                    MessageBox.Show("No games available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Select the game you would like to join", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var lobby = GameLobby.TryToJoin(gho.Address.Address, gho.HostId, playerName, UNOHostCreator.Instance);
            lobby_Post(ref lobby);
        }

        void lobby_FormClosed(object sender, FormClosedEventArgs e)
        {
            Show();
            RefreshServers();
            UpdateButtonStates();
        }        

        private void Create_Button_Click(object sender, EventArgs e)
        {
            if (GameHost.IsHosting)
            {
                MessageBox.Show("Cannot host several games!");
                return;
            }
            var lobby = GameLobby.CreateGame(playerName, UNOHostCreator.Instance);
            lobby_Post(ref lobby);
        }

        private void Refresh_Button_Click(object sender, EventArgs e)
        {
            RefreshServers();
        }

        private void text_PlayerName_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
