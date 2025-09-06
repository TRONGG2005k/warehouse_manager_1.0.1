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

namespace warehouse_manager.ui.user_control.tonKho
{
    public partial class TimKiem : UserControl
    {
        KeService service = new KeService();
        public TimKiem()
        {
            InitializeComponent();
        }

        private void TimKiem_Load(object sender, EventArgs e)
        {

            LoadData();

        }
        private void LoadData()
        {
            dataGridView1.DataSource = service.layDanhSachKe();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == null || textBox1.Text == "")
                {
                    throw new Exception("ma kệ ko được để chống");
                }
                service.taoKes(textBox1.Text.ToString(), textBox2.Text.ToString(), textBox3.Text.ToString());
                LoadData();
            }
            catch(Exception ex) {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            service.suaKe(textBox1.Text.ToString(), textBox2.Text.ToString(), textBox3.Text.ToString(),Convert.ToInt32( dataGridView1.CurrentRow.Cells["id"].Value));
            LoadData();
        }
    }
}
