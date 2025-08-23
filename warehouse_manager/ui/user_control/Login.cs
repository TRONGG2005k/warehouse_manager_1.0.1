using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using warehouse_manager.configuration;
using warehouse_manager.context;
using warehouse_manager.Models;

namespace warehouse_manager.ui
{
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            this.BackColor = Color.AliceBlue;
           
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new WarehouseManagerContext())
                {
                    if (textBox1.Text == "" || textBox2.Text == "")
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                        return;
                    }

                    var users = context.NguoiDungs;

                    var user = users.Where(
                        u => u.TenDangNhap == textBox1.Text
                        &&
                        u.MatKhau == textBox2.Text).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("action failed" + ex.Message);
            }
        }

    }
}
