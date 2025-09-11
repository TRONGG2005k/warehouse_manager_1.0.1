using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using warehouse_manager.context;
using warehouse_manager.service;

namespace warehouse_manager.ui.uiController.timkiemvitrivatlieu
{
    public partial class TimKiemViTriVatLieu : UserControl
    {
        private WarehouseManagerContext context = new WarehouseManagerContext();
        public TimKiemViTriVatLieu()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = radioButton1.Checked;
            button1.Visible = radioButton1.Checked;
        }

        private void TimKiemViTriVatLieu_Load(object sender, EventArgs e)
        {
            List<String> trangThais = new List<string>
            {
                "CON_HANG",
                "HET_HANG"
            };

            foreach (var item in trangThais)
            {
                comboBox1.Items.Add(item);
            }
            var danhSachKhu = context.Kes.Select(k => k.Khu!).ToList();
            foreach (var item in danhSachKhu) {
                comboBox2.Items.Add(item);
            }
            LoadData();
        }
        private void LoadData()
        {
            dataGridView1.DataSource = context.VatLieus
                .Include(vl => vl.Kes)
                .SelectMany(vl => vl.Kes, (vl, ke) => new
                {
                    Id = vl.Id,
                    MaLieu = vl.MaVatLieu,
                    Ten = vl.Ten,
                    SoluonTon = vl.SoLuongTon,
                    MaKe = ke.MaKe,
                    Khu = ke.Khu,
                    TrangThai = vl.TrangThai
                }).ToList();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = context.VatLieus
               .Where(vl => vl.MaVatLieu == textBox1.Text)
               .Include(vl => vl.Kes)
               .SelectMany(vl => vl.Kes, (vl, ke) => new
               {
                   Id = vl.Id,
                   MaLieu = vl.MaVatLieu,
                   Ten = vl.Ten,
                   SoluonTon = vl.SoLuongTon,
                   MaKe = ke.MaKe,
                   Khu = ke.Khu,
                   TrangThai = vl.TrangThai == "CON_HANG" ? "Còn hàng" : "Hết hàng"

               }).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = context.VatLieus
               .Where(vl => vl.TrangThai == comboBox1.SelectedItem.ToString())
               .Include(vl => vl.Kes)
               .SelectMany(vl => vl.Kes, (vl, ke) => new
               {
                   Id = vl.Id,
                   MaLieu = vl.MaVatLieu,
                   Ten = vl.Ten,
                   SoluonTon = vl.SoLuongTon,
                   MaKe = ke.MaKe,
                   Khu = ke.Khu,
                   TrangThai = vl.TrangThai == "CON_HANG" ? "Còn hàng" : "Hết hàng"
               }).ToList();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = radioButton2.Checked;
            button2.Visible = radioButton2.Checked;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Visible = radioButton3.Checked;
            button3.Visible = radioButton3.Checked;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = context.Kes
                .Where(k => k.Khu == comboBox2.SelectedItem.ToString())
                .Include(k => k.VatLieus)
                .SelectMany(k => k.VatLieus, (k, vl) => new
                {
                    Id = vl.Id,
                    MaLieu = vl.MaVatLieu,
                    Ten = vl.Ten,
                    SoluonTon = vl.SoLuongTon,
                    MaKe = k.MaKe,
                    Khu = k.Khu,
                    TrangThai = vl.TrangThai == "CON_HANG" ? "Còn hàng" : "Hết hàng"
                }).ToList();
        }
    }
}
