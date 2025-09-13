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
    public partial class LoaiVatLieu : UserControl
    {
        private WarehouseManagerContext context = new WarehouseManagerContext();
        public LoaiVatLieu()
        {
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

                context.LoaiVatLieus.Add(new Models.LoaiVatLieu
                {
                    TenLoai = textBox1.Text,
                    MoTa = textBox2.Text,
                });

                context.SaveChanges();
                LoadData();

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi: " + ex.Message);
                throw new Exception("Lỗi: " + ex.Message);

            }
        }

        private void LoaiVatLieu_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            dataGridView1.DataSource = context.LoaiVatLieus
                .Where(l => l.IsDeleted != true)
                .Select(
                l => new
                {
                    id = l.Id,
                    Ten = l.TenLoai,
                    GhiChu = l.MoTa
                }).ToList();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.CurrentRow;

            if (row != null)
            {
                textBox1.Text = row.Cells["Ten"].Value.ToString();
                textBox2.Text = row.Cells["GhiChu"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            long id = (long)dataGridView1.CurrentRow.Cells["Id"].Value;

            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    throw new Exception("Mã kệ không được để trống");
                }

                var loai = context.LoaiVatLieus.Find(id);
                if (loai == null)
                {
                    throw new Exception("loại vật liệu khôg tồn tại");

                }
                loai.MoTa = textBox2.Text;
                loai.TenLoai = textBox1.Text;
                context.SaveChanges();
                LoadData();

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi: " + ex.Message);
                throw new Exception("Lỗi: " + ex.Message);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = textBox4.Text.Trim().ToLower();
             

                var loai = context.LoaiVatLieus
                    .Where(l => l.TenLoai.ToLower().Contains(keyword) && l.IsDeleted != true)
                    .Select(l => new
                    {
                        id = l.Id,

                        Ten = l.TenLoai,
                        GhiChu = l.MoTa
                    })
                    .ToList();

                dataGridView1.DataSource = loai;
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi: " + ex.Message);
                throw new Exception("Lỗi: " + ex.Message);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            long id = (long)dataGridView1.CurrentRow.Cells["Id"].Value;

            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    throw new Exception("Mã kệ không được để trống");
                }

                var loai = context.LoaiVatLieus.Find(id);
                if (loai == null)
                {
                    throw new Exception("loại vật liệu khôg tồn tại");

                }
                loai.IsDeleted = true;
                context.SaveChanges();
                LoadData();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                //throw new Exception("Lỗi: " + ex.Message);

            }
        }
    }
}
