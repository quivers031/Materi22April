using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Materi22April
{
    public partial class FormLatihan : Form
    {
        public FormLatihan()
        {
            InitializeComponent();
        }

        static string connectionString = "server=localhost;uid=root;pwd=;database=premier_league;";
        public MySqlConnection sqlConnect = new MySqlConnection(connectionString);
        public MySqlCommand sqlCommand;
        public MySqlDataAdapter sqlAdapter;
        public string sqlQuery;

        DataTable dtTeamAway = new DataTable();
        DataTable dtTeamHome = new DataTable();

        private void FormLatihan_Load(object sender, EventArgs e)
        {
            sqlQuery = "SELECT t.team_id as `ID Tim`, t.team_name as `Nama Tim`, m.manager_name as `Nama Manager`, IF(m2.manager_name IS NULL,'----',m2.manager_name) as `Nama Asisten Manager`,p.player_name as `Nama Kapten`, home_stadium as `Stadium`, capacity as `Kapasitas` FROM team t LEFT JOIN manager m2 ON  t.assmanager_id = m2.manager_id ,manager m, player p WHERE t.manager_id = m.manager_id AND t.captain_id = p.player_id ;";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTeamHome);
            sqlAdapter.Fill(dtTeamAway);
            comboHome.DataSource = dtTeamHome;
            comboHome.DisplayMember = "Nama Tim";
            comboHome.ValueMember = "ID Tim";
            comboAway.DataSource = dtTeamAway;
            comboAway.DisplayMember = "Nama Tim";
        }

        private void comboHome_SelectedIndexChanged(object sender, EventArgs e)
        {
            int posisiIndex = comboHome.SelectedIndex;
            labHomeManager.Text = dtTeamHome.Rows[posisiIndex]["Nama Manager"].ToString();
            labHomeAssManager.Text = dtTeamHome.Rows[posisiIndex]["Nama Asisten Manager"].ToString();
            labHomeKapten.Text = dtTeamHome.Rows[posisiIndex]["Nama Kapten"].ToString();
            labStadium.Text = dtTeamHome.Rows[posisiIndex]["Stadium"].ToString();
            labCapacity.Text = dtTeamHome.Rows[posisiIndex]["Kapasitas"].ToString();
        }

        private void comboAway_SelectedIndexChanged(object sender, EventArgs e)
        {
            int posisiIndex = comboAway.SelectedIndex;
            labAwayManager.Text = dtTeamAway.Rows[posisiIndex]["Nama Manager"].ToString();
            labAwayAssManager.Text = dtTeamAway.Rows[posisiIndex]["Nama Asisten Manager"].ToString();
            labAwayKapten.Text = dtTeamAway.Rows[posisiIndex]["Nama Kapten"].ToString();
        }
    }
}
