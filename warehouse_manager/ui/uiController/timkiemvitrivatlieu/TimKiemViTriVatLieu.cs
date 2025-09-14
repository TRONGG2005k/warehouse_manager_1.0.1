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


            comboBox1.DataSource = trangThais;
            
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
                .Where(vl => vl.IsDeleted != true)
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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã vật liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dataGridView1.DataSource = context.VatLieus
                   .Where(vl => vl.MaVatLieu == textBox1.Text && vl.IsDeleted != true)
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
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn trạng thái!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dataGridView1.DataSource = context.VatLieus
                   .Where(vl => vl.TrangThai == comboBox1.SelectedItem.ToString()
                   && vl.IsDeleted != true)
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
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            try
            {
                if (comboBox2.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn khu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dataGridView1.DataSource = context.Kes
                    .Include(k => k.VatLieus)
                    .Where(k => k.Khu == comboBox2.SelectedItem.ToString())
                    .SelectMany(k => k.VatLieus.Where(vl => vl.IsDeleted != true), (k, vl) => new
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
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
