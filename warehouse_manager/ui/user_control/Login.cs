using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using warehouse_manager.configuration;
using warehouse_manager.context;
using warehouse_manager.Models;
using warehouse_manager.service;
using warehouse_manager.ui.user_control;

namespace warehouse_manager.ui
{
    public partial class Login : UserControl
    {
        private NguoiDungService nguoiDungService;
        public Login()
        {
            InitializeComponent();
            this.BackColor = Color.AliceBlue;
            nguoiDungService = new NguoiDungService();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean resultLogin = nguoiDungService.login(new NguoiDung
            {
                TenDangNhap = textBox1.Text,
                MatKhau = textBox2.Text
            });

            if (resultLogin)
            {
                MainForm mainForm = (MainForm)this.Parent!.Parent!;
                mainForm.LoadPage(new DanhSachDonNhapKho());
            }
        }

    }
}
