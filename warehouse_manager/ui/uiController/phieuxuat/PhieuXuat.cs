using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using warehouse_manager.dto.i;

namespace warehouse_manager.ui.uiController.phieuxuat
{
    public partial class PhieuXuat : UserControl
    {
        public PhieuXuat()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Items.Contains(comboBox1.Text) == false)
                {
                    throw new Exception("Tên chuyền sản xuất không tồn tại");
                }
                else if (comboBox2.Items.Contains(comboBox2.Text) == false)
                {
                    throw new Exception("Mã vật liệu không tồn tại vui lòng báo cáo quản lý");
                }
                var service = new service.PhieuXuatService();
                var phieuXuat = new ThemPhieuXuatDto
                {
                    GhiChu = textBox1.Text,
                    MaVatLieu = comboBox2.SelectedItem != null ? comboBox2.SelectedItem.ToString() : "",
                    SoLuongYeuCau = (long)numericUpDown1.Value,
                    TenChuyenSanXuat = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : ""
                };

                var context = new ValidationContext(phieuXuat);


                Validator.ValidateObject(phieuXuat, context, validateAllProperties: true);
                if (service.YeuCauthemPhieuXuat(phieuXuat))
                {
                    textBox1.Text = "";
                    comboBox1.SelectedItem = null;
                    comboBox2.SelectedItem = null;
                    numericUpDown1.Value = 0;
                    MessageBox.Show("Tạo phiếu xuất kho thành công");
                }
                //MessageBox.Show("Tạo phiếu xuất kho thành công");
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PhieuXuat_Load(object sender, EventArgs e)
        {
            LoadData();
            var service = new service.PhieuXuatService();
            var phieuXuatDtos = service.SelectPhieuThieu();
            dataGridView2.DataSource = phieuXuatDtos;
        }
        private void LoadData()
        {
            var vatLieuService = new service.VatLieuService();
            List<String> vatLieus = vatLieuService.danhSachVatLieu();
            foreach (var item in vatLieus)
            {
                comboBox2.Items.Add(item);
            }
            var chuyenSanXuatService = new service.ChuyenSanXuatService();
            List<String> chuyenSanXuats = chuyenSanXuatService.danhSachCoSoSanXuat();
            foreach (var item in chuyenSanXuats)
            {
                comboBox1.Items.Add(item);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.CurrentRow != null)
                {
                    long maPhieu = (long)dataGridView2.CurrentRow.Cells["Id"].Value;
                    textBox1.Text = $"|Bù cho phiếu {maPhieu}";

                    comboBox1.SelectedItem = dataGridView2.CurrentRow.Cells["MaTruyenSanXuat"].Value.ToString();

                    comboBox2.SelectedItem = dataGridView2.CurrentRow.Cells["MaVatLieu"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
