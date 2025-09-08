using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using warehouse_manager.service;

namespace warehouse_manager.ui.uiController.baocao
{
    public partial class BCNXT : UserControl
    {
        BaoCaoService baoCao = new BaoCaoService();
        public BCNXT()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = baoCao.baoCaoNXT(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            baoCao.XuatExcel(baoCao.baoCaoNXT(dateTimePicker1.Value, dateTimePicker2.Value),
                @"C:\Users\Hi ASUS TUF\Documents\BaoCaoNXT.xlsx", dateTimePicker1.Value, dateTimePicker2.Value);
        }
    }
}
