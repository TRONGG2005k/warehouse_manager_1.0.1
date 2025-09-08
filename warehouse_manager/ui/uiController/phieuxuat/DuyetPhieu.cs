using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using warehouse_manager.dto.i;
using warehouse_manager.service;

namespace warehouse_manager.ui.uiController.phieuxuat
{
    public partial class DuyetPhieu : UserControl
    {
        private PhieuXuatService phieuXuatService;
        public DuyetPhieu()
        {
            phieuXuatService = new PhieuXuatService();
            InitializeComponent();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = radioButton1.Checked;
            button13.Visible = radioButton1.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Visible = radioButton2.Checked;
            dateTimePicker2.Visible = radioButton2.Checked;
            button14.Visible = radioButton2.Checked;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null)
                {
                    throw new Exception("Vui lòng chọn trạng thái để lọc");

                }
                var service = new service.PhieuXuatService();
                var trangThai = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "";
                var phieuXuatDtos = service.TimTheoTrangThai(trangThai);
                dataGridView1.DataSource = phieuXuatDtos;
                comboBox1.SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {

                var service = new service.PhieuXuatService();
                var startDate = dateTimePicker2.Value;
                var endDate = dateTimePicker1.Value;
                if (startDate > endDate)
                {
                    throw new Exception("Ngày bắt đầu phải nhỏ hơn ngày kết thúc");
                }
                var phieuXuatDtos = service.TimTheoNgay(new dto.i.LocTheoNgayDto
                {
                    Start = startDate,
                    End = endDate
                });
                dataGridView1.DataSource = phieuXuatDtos;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var service = new service.PhieuXuatService();
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    throw new Exception("Vui lòng chọn phiếu xuất để duyệt");
                }
                var phieuXuatId = int.Parse(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                if (service.duyetPhieuXuat(phieuXuatId))
                {
                    LoadData();
                    textBox1.Text = "";
                    MessageBox.Show("Duyệt phiếu xuất kho thành công");
                }
                else
                {
                    MessageBox.Show("Duyệt phiếu xuất kho thất bại");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Mã phiếu xuất không hợp lệ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DuyetPhieu_Load(object sender, EventArgs e)
        {
            LoadData();
            List<String> trangThais = new List<string>
            {
                "CHO_DUYET",
                "DA_DUYET",
                "DA_HUY"
            };
            foreach (var item in trangThais)
            {
                comboBox1.Items.Add(item);
            }
        }
        private void LoadData()
        {
            var service = new service.PhieuXuatService();
            var phieuXuatDtos = service.LayTatCaPhieu();
            dataGridView1.DataSource = phieuXuatDtos;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu xuất để hủy");
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập lý do hủy");
                return;
            }
            if (phieuXuatService.huyPhieuXuat(new HuyPhieuDto
            {
                PhieuXuatId = int.Parse(dataGridView1.CurrentRow.Cells["id"].Value.ToString()),
                LyDoHuy = textBox2.Text
            }))
            {
                LoadData();
                textBox1.Text = "";
                textBox2.Text = "";
                MessageBox.Show("Hủy phiếu xuất kho thành công");
            }
            else
            {
                MessageBox.Show("Hủy phiếu xuất kho thất bại");

            }
        }
    }
}
