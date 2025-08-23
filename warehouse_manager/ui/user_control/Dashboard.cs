using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace warehouse_manager.ui.user_control
{
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
            this.BackColor = Color.AliceBlue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent!.Parent!;
            mainForm.LoadPage(new Login());// new Login là tên class của UserControl 
        }
    }
}
