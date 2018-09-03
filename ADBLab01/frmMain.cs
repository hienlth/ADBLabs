using Db4objects.Db4o;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADBLab01
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            Manager.DbFileName = "pilot.yap";
        }

        public void StorePilot(IObjectContainer db)
        {
            Pilot pilot = new Pilot(txtName.Text, int.Parse(txtPoint.Text));
            db.Store(pilot);
            db.Close();
        }

        public void GetPilots(IObjectContainer db)
        {
            //khai báo đối tượng Pilot
            IObjectSet results = db.QueryByExample(typeof(Pilot));
            List<Pilot> pilots = new List<Pilot>();
            foreach (var item in results)
            {
                pilots.Add(item as Pilot);
            }
            db.Close();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = pilots;
        }

        public void UpdatePilot(IObjectContainer db)
        {
            IObjectSet result = db.QueryByExample(new Pilot(txtName.Text, 0));
            Pilot p = (Pilot)result.Next();
            //Pilot p = (Pilot)result[0];
            p.AddPoints(int.Parse(txtPoint.Text));
            db.Store(p);
            db.Close();
        }

        public void DeletePilotByName(IObjectContainer db)
        {
            IObjectSet result = db.QueryByExample(new Pilot(txtName.Text, 0));
            Pilot p = (Pilot)result[0];
            db.Delete(p);
            db.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            StorePilot(Manager.Database);
            GetPilots(Manager.Database);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtPoint.Clear();
            txtName.Focus();
        }

        private void btnLoadMore_Click(object sender, EventArgs e)
        {
            GetPilots(Manager.Database);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdatePilot(Manager.Database);
            GetPilots(Manager.Database);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeletePilotByName(Manager.Database);
            GetPilots(Manager.Database);
            btnClear_Click(null, null);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            txtName.Text = row.Cells[0].Value.ToString();
            txtPoint.Text = row.Cells[1].Value.ToString();
        }
    }
}
