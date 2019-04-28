using System;
using System.IO;
using System.Windows.Forms;
using MatterLib;
using Newtonsoft.Json;

namespace MattermostApp {
    public partial class FormMain : Form {
        public FormMain() {
            InitializeComponent();

            try {
                var config_data = File.ReadAllText("config.json");
                var settings = JsonConvert.DeserializeObject<UserSettings>(config_data);
                this.textBox1.Text = settings.login;
                this.textBox2.Text = settings.password;
                this.textBox3.Text = settings.server;
            }
            catch (FileNotFoundException) {
                // file not found - no settings, OK
            }
            catch (Exception e) {
                MessageBox.Show(e.ToString(), "Config F A I L");
            }
        }

        private void button1_Click(object sender, EventArgs ev) {
            DefaultAPIQuerySettings.https_server = textBox3.Text;
            SessionTokenQuery st = new SessionTokenQuery {
                login_id = this.textBox1.Text
              , password = this.textBox2.Text
            };
            try {
                var ret = (SessionTokenQuery.ResponseImpl) st.Execute();
                DefaultAPIQuerySettings.token = ret.Token;
                GetUserTeamsQuery gut = new GetUserTeamsQuery { user_id = "me" };
                var teams = (GetUserTeamsQuery.ResponseImpl) gut.Execute();
                listBox1.Items.Clear();
                foreach (var team in teams.teams) {
                    listBox1.Items.Add(team.display_name);
                }
            }
            catch (APIException e) {
                MessageBox.Show(e.ToString(), "F A I L");
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e) {
            var settings = new UserSettings {
                login = this.textBox1.Text
              , password = this.textBox2.Text
              , server = this.textBox3.Text
            };
            File.WriteAllText("config.json", JsonConvert.SerializeObject(settings));
        }
    }
}
