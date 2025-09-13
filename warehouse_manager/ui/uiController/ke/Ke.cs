using DocumentFormat.OpenXml.Office2010.Excel;
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

namespace warehouse_manager.ui.uiController.ke
{
    public partial class Ke : UserControl
    {
        private WarehouseManagerContext context;
        public Ke()
        {
            context = new WarehouseManagerContext();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    throw new Exception("Mã kệ không được để trống");
                }
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    throw new Exception("Vị trí không được để trống");
                }
                if(context.Kes.FirstOrDefault(k => k.MaKe == textBox1.Text) != null)
                {
                    throw new Exception("Mã kệ đã tồn tại");
                }
                context.Kes.Add(new Models.Ke
                {
                    Khu = textBox3.Text,
                    MaKe = textBox1.Text,
                    MoTa = textBox2.Text
                });
                context.SaveChanges();
                LoadData();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                //throw new Exception("Lỗi: " + ex.Message);
            }
        }
       
        private void Ke_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dataGridView1.DataSource = context.Kes.Select(k => new
            {
                id = k.Id,
                MaKe = k.MaKe,
                Khu = k.Khu,
                Ghichu = k.MoTa
            }).ToList(); ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //long id = (long)dataGridView1.CurrentRow.Cells["Id"].Value;
                
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    throw new Exception("Mã kệ không được để trống");
                }
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    throw new Exception("Vị trí không được để trống");
                }
                var ke = context.Kes.FirstOrDefault(k => k.MaKe == textBox1.Text.ToString());
                if (ke == null)
                {
                    throw new Exception("không tìm thấy kệ với mã kệ" + textBox1.Text.ToString());
                }
                ke.Khu = textBox3.Text;
                ke.MaKe = textBox1.Text;
                ke.MoTa = textBox2.Text;
                context.SaveChanges();
                LoadData();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                //throw new Exception("Lỗi: " + ex.Message);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                long id = (long)dataGridView1.CurrentRow.Cells["Id"].Value;
                
              
                var ke = context.Kes.Find(id);
                if (ke == null)
                {
                    throw new Exception("không tìm thấy kệ với id" + id);
                }
                context.Kes.Remove(ke);
                context.SaveChanges();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.CurrentRow;
            if (dataGridView1.CurrentRow != null)
            {
                MessageBox.Show("bạn chọn kệ có mã:" + row.Cells["MaKe"].Value.ToString());
                textBox1.Text = row.Cells["MaKe"].Value.ToString();
                textBox2.Text = row.Cells["Ghichu"].Value.ToString();
                textBox3.Text = row.Cells["Khu"].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox4.Text))
                {
                    throw new Exception("vui long nhập giá trị vào ô tìm kiếm" +
                        "");
                }

                var ke = context.Kes.Where(
                    k => k.MaKe == textBox4.Text)
                    .Select(k => new
                    {
                        id = k.Id,
                        MaKe = k.MaKe,
                        Khu = k.Khu,
                        Ghichu = k.MoTa
                    }).ToList();
                if (ke == null)
                {
                    throw new Exception("không tìm thấy kệ với ");
                }
                dataGridView1.DataSource = ke;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
