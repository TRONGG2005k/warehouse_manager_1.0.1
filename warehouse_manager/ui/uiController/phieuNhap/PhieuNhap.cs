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

namespace warehouse_manager.ui.uiController.phieuNhap
{
    public partial class PhieuNhap : UserControl
    {
        private PhieuNhapService phieuNhapService;
        private NguoiDungService nguoiDungService;
        public PhieuNhap()
        {
            nguoiDungService = new NguoiDungService();
            phieuNhapService = new PhieuNhapService();
            InitializeComponent();
            LoadDataChodataGridView();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        
        private void LoadDataChodataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = phieuNhapService.phieuNhapDtos();
        }
        private void LoadData()
        {
            List<String> loaiVatlieus = new LoaiVatLieuService().danhSachLoaiVatLieu();
            comboBox1.Items.Add("");
            foreach (var item in loaiVatlieus)
            {
                comboBox1.Items.Add(item);
            }

            List<String> donViTinhs = new List<string>
            {
                "",
                "Cái",
                "Chiếc",
                "Bộ",
                "Hộp",
                "Thùng",
                "Kg",
                "Gram",
                "Mét",
                "Mét Vuông",
                "Mét Khối",
                "Lít",
                "Chiều"
            };
            foreach (var item in donViTinhs)
            {
                comboBox2.Items.Add(item);
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            List<String> nhaCungCaps = new NhaCungCapService().danhSachNhaCungCap();
            foreach (var item in nhaCungCaps)
            {
                comboBox3.Items.Add(item);
            }
            List<String> kes = new KeService().danhSachKe();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //PhieuNhapService phieuService = new PhieuNhapService();
            try
            {

                if (comboBox1.Items.Contains(comboBox1.Text) == false)
                {
                    throw new Exception("Loại vật liệu không tồn tại");
                }
                else if (comboBox2.Items.Contains(comboBox2.Text) == false)
                {
                    throw new Exception("Loại vật liệu không tồn tại");
                }
                else if (comboBox3.Items.Contains(comboBox3.Text) == false)
                {
                    throw new Exception("Nhà cung cấp không tồn tại");
                }
               
                if (string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()) ||
                  string.IsNullOrEmpty(textBox1.Text) ||
                  string.IsNullOrEmpty(comboBox2.SelectedItem.ToString()) ||
                  string.IsNullOrEmpty(comboBox3.SelectedItem.ToString()) ||
                  string.IsNullOrEmpty(textBox2.Text) ||
                  (int)numericUpDown2.Value == 0
                )
                {
                    //MessageBox.Show("vui lòng nhập đủ thông tìn");
                    throw new Exception("vui lòng nhập đủ thông tìn");
                }
                phieuNhapService.ThemPhieuNhap(new TaoPhieuNhapKhoDto
                {
                    LoaiVatLieu = comboBox1.SelectedItem.ToString(),
                    TenVatLieu = textBox1.Text,
                    DonViTinh = comboBox2.SelectedItem.ToString(),
                    DonGia = numericUpDown1.Value,
                    NhaCungCap = comboBox3.SelectedItem.ToString(),
                    MaVatLieu = textBox2.Text,
                    SoLuong = (int)numericUpDown2.Value,
                   
                });
                LoadDataChodataGridView();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //throw new Exception("lỗi " + ex.Message);
                throw new Exception("Thêm phiếu nhập thất bại: " + ex.Message + " |" + ex.InnerException);

            }
        }

        private void PhieuNhap_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (comboBox1.Items.Contains(comboBox1.Text) == false)
                {
                    throw new Exception("Loại vật liệu không tồn tại");
                }
                else if (comboBox2.Items.Contains(comboBox2.Text) == false)
                {
                    throw new Exception("Loại vật liệu không tồn tại");
                }
                else if (comboBox3.Items.Contains(comboBox3.Text) == false)
                {
                    throw new Exception("Nhà cung cấp không tồn tại");
                }
               

                if (string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()) ||
                string.IsNullOrEmpty(textBox1.Text) ||
                string.IsNullOrEmpty(comboBox2.SelectedItem.ToString()) ||
                string.IsNullOrEmpty(comboBox3.SelectedItem.ToString()) ||
                string.IsNullOrEmpty(textBox2.Text) ||
               
                (int)numericUpDown2.Value == 0
                )
                {
                    //MessageBox.Show("vui lòng nhập đủ thông tìn");
                    throw new Exception("vui lòng nhập đủ thông tìn");
                }


                var isSuccess = phieuNhapService.suaPhieuNhap(new SuaPhieuNhapDto
                {
                    Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value),
                    LoaiVatLieu = comboBox1.SelectedItem.ToString(),
                    TenVatLieu = textBox1.Text,
                    DonViTinh = comboBox2.SelectedItem.ToString(),
                    DonGia = Convert.ToDecimal(numericUpDown1.Value),
                    NhaCungCap = comboBox3.SelectedItem.ToString(),
                    MaVatLieu = textBox2.Text,
                    SoLuong = Convert.ToInt32(numericUpDown2.Value),
                  
                });
                if (isSuccess)
                {
                    MessageBox.Show("Sửa phiếu nhập thành công");
                    LoadDataChodataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("sửa thất bại " + ex.Message);
                //throw new Exception(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var row = dataGridView1.CurrentRow;
                comboBox1.SelectedItem = row.Cells["LoaiVatLieu"].Value.ToString();
                textBox1.Text = row.Cells["TenHang"].Value.ToString();
                comboBox2.SelectedItem = row.Cells["DonViTinh"].Value.ToString();
                numericUpDown1.Value = Convert.ToDecimal(row.Cells["DonGia"].Value);
                comboBox3.SelectedItem = row.Cells["NhaCungCap"].Value.ToString();
                textBox2.Text = row.Cells["MaVatLieu"].Value.ToString();
                numericUpDown2.Value = Convert.ToDecimal(row.Cells["SoLuong"].Value);
                //comboBox4.SelectedItem = row.Cells["Ke"].Value.ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!nguoiDungService.kiemTraVaiTroAdmin())
                {
                    throw new Exception("Bạn không có quyền xoá phiếu");
                }
                if (dataGridView1.CurrentCell == null)
                {
                    MessageBox.Show("Vui lòng chọn phiếu nhập cần xóa");
                    return;
                }

                phieuNhapService.xoaPhieuNhap(Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value));
                LoadDataChodataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateTimePicker1.Value > dateTimePicker2.Value)
                {
                    throw new Exception("Ngày bắt đầu phải nhỏ hơn ngày kết thúc");
                }
                //MessageBox.Show("Tính năng tìm ngày đang được phát triển");
                
                List<dto.o.PhieuNhapDto> list = phieuNhapService.TimPhieuTheoKhoangThoiGian(
                        new LocTheoNgayDto
                        {
                            Start = dateTimePicker1.Value,
                            End = dateTimePicker2.Value
                        }
                    );
                dataGridView1.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message);
                return;
            }
        }
    }
}
