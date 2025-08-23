using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace warehouse_manager.ui
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadPage(new Login());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        public void LoadPage(UserControl page)
        {
            panel1.Controls.Clear();
            page.Dock = DockStyle.Fill; // Trang chiếm hết panel
            panel1.Controls.Add(page);
            System.Console.WriteLine("Load page" + page.Name);
        }
    }
}
