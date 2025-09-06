using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using warehouse_manager.context;
using warehouse_manager.dto.i;
using warehouse_manager.dto.o;
using warehouse_manager.Models;
using warehouse_manager.service;

namespace warehouse_manager.ui.user_control.tonKho
{
    public partial class KiemKe : UserControl
    {
        private VatLieuService vatLieuService = new VatLieuService();
        private PhieuKiemKeService phieuKiemKeService = new PhieuKiemKeService();
        private WarehouseManagerContext db = new WarehouseManagerContext();

        private List<ChiTietPhieuKiemKeCreateDto> danhSachChiTietPhieuKK = new List<ChiTietPhieuKiemKeCreateDto>();
        private List<KetQuaKiemKeDto> ketQuaKiemKeDtos = new List<KetQuaKiemKeDto>();

        PrintDocument printDocument = new PrintDocument();
        public KiemKe()
        {
            InitializeComponent();
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void KiemKe_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(vatLieuService.danhSachVatLieu().ToArray());

        }

        private void LoadSoLuongTon()
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                numericUpDown1.Value = 0;
                return;
            }

            var vatlieu = vatLieuService.layVatLieuTheoMa(comboBox1.Text);
            numericUpDown1.Value = vatlieu?.SoLuongTon ?? 0;
            numericUpDown1.Maximum = int.MaxValue;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) => LoadSoLuongTon();
        private void comboBox1_TextChanged(object sender, EventArgs e) => LoadSoLuongTon();

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn mã vật liệu");
                    return;
                }

                var vatLieu = vatLieuService.layVatLieuTheoMa(comboBox1.SelectedItem.ToString());


                if (danhSachChiTietPhieuKK.Any(c => c.VatLieuId == vatLieu.Id))
                {
                    MessageBox.Show("Vật liệu này đã có trong danh sách!");
                    return;
                }

                danhSachChiTietPhieuKK.Add(new ChiTietPhieuKiemKeCreateDto
                {
                    VatLieuId = vatLieu.Id,
                    TonHeThong = (int)numericUpDown1.Value,
                    TonThucTe = (int)numericUpDown2.Value
                });

                LamMoiDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!danhSachChiTietPhieuKK.Any())
            {
                MessageBox.Show("Danh sách kiểm kê đang trống!");
                return;
            }

            var dialog = MessageBox.Show("Bạn có chắc chắn muốn tạo phiếu kiểm kê không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.No) return;

            var phieuKiemKe = new PhieuKiemKeCreateDto
            {
                NgayKiemKe = DateTime.Now,
                GhiChu = textBox1.Text,
                ChiTietPhieuKiemKes = danhSachChiTietPhieuKK
            };
            phieuKiemKeService.taoPhieuKiemKe(phieuKiemKe);
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }

            MessageBox.Show("Kiểm kê thành công!");
        }

        // Xóa hàng đang chọn
        private void button11_Click(object sender, EventArgs e) => XoaHang();

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                XoaHang();
        }

        private void XoaHang()
        {
            if (dataGridView1.CurrentRow == null) return;

            int index = dataGridView1.CurrentRow.Index;
            if (index >= 0 && index < danhSachChiTietPhieuKK.Count)
            {
                danhSachChiTietPhieuKK.RemoveAt(index);
                LamMoiDataGrid();
            }
        }

        private void LamMoiDataGrid()
        {
            ketQuaKiemKeDtos.Clear();
            foreach (var item in danhSachChiTietPhieuKK)
            {
                var vatLieu = db.VatLieus.FirstOrDefault(v => v.Id == item.VatLieuId);
                if (vatLieu != null)
                {
                    ketQuaKiemKeDtos.Add(new KetQuaKiemKeDto
                    {
                        MaVatTu = vatLieu.MaVatLieu,
                        DonViTinh = vatLieu.DonViTinh,
                        TonHeThong = item.TonHeThong,
                        TonThucTe = item.TonThucTe
                    });
                }
            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ketQuaKiemKeDtos;
            dataGridView1.Columns["MaVatTu"].HeaderText = "Mã Vật Tư";
            dataGridView1.Columns["DonViTinh"].HeaderText = "Đơn Vị Tính";
            dataGridView1.Columns["TonHeThong"].HeaderText = "Tồn Hệ Thống";
            dataGridView1.Columns["TonThucTe"].HeaderText = "Tồn Thực Tế";
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int startX = 50;
            int startY = 50;
            int offsetY = 0;
            var phieu = phieuKiemKeService.LayPhieuKiemKeMoiNhat();
            Font fontNormal = new Font("Arial", 10);
            Font fontBold = new Font("Arial", 10, FontStyle.Bold);

            // Tiêu đề phiếu
            e.Graphics.DrawString("PHIẾU KIỂM KÊ", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, startX, startY);
            offsetY += 40;

            // Thông tin phiếu
            e.Graphics.DrawString($"Ngày kiểm kê: {phieu.NgayKiemKe.ToShortDateString()}", fontNormal, Brushes.Black, startX, startY + offsetY);
            e.Graphics.DrawString($"Người kiểm kê: {phieu.NguoiKiemKe}", fontNormal, Brushes.Black, startX + 300, startY + offsetY);
            offsetY += 30;

            // Header bảng
            e.Graphics.DrawString("Mã VL", fontBold, Brushes.Black, startX, startY + offsetY);
            e.Graphics.DrawString("Tên VL", fontBold, Brushes.Black, startX + 80, startY + offsetY);
            e.Graphics.DrawString("ĐVT", fontBold, Brushes.Black, startX + 250, startY + offsetY);
            e.Graphics.DrawString("Tồn HT", fontBold, Brushes.Black, startX + 300, startY + offsetY);
            e.Graphics.DrawString("Tồn TT", fontBold, Brushes.Black, startX + 370, startY + offsetY);
            e.Graphics.DrawString("Chênh lệch", fontBold, Brushes.Black, startX + 440, startY + offsetY);
            offsetY += 20;

            // Nội dung chi tiết phiếu
            foreach (var ct in phieu.ChiTietPhieuKiemKes)
            {
                e.Graphics.DrawString(ct.VatLieu.MaVatLieu, fontNormal, Brushes.Black, startX, startY + offsetY);
                e.Graphics.DrawString(ct.VatLieu.Ten, fontNormal, Brushes.Black, startX + 80, startY + offsetY);
                e.Graphics.DrawString(ct.VatLieu.DonViTinh, fontNormal, Brushes.Black, startX + 250, startY + offsetY);
                e.Graphics.DrawString(ct.TonHeThong.ToString(), fontNormal, Brushes.Black, startX + 300, startY + offsetY);
                e.Graphics.DrawString(ct.TonThucTe.ToString(), fontNormal, Brushes.Black, startX + 370, startY + offsetY);
                e.Graphics.DrawString(ct.ChenhLech.ToString(), fontNormal, Brushes.Black, startX + 440, startY + offsetY);
                offsetY += 20;
            }

            offsetY += 30;
            e.Graphics.DrawString("Chữ ký quản lý", fontNormal, Brushes.Black, startX, startY + offsetY);
            e.Graphics.DrawString("Chữ ký người nhập", fontNormal, Brushes.Black, startX + 200, startY + offsetY);
            e.Graphics.DrawString("Chữ ký người kiểm", fontNormal, Brushes.Black, startX + 400, startY + offsetY);


        }

        private void button5_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent.Parent;
            mainForm.LoadPage(new DanhSachDonNhapKho());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent.Parent;
            mainForm.LoadPage(new DanhSachNhapKho());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent.Parent;
            mainForm.LoadPage(new DanhSachXuatKho());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent.Parent;
            mainForm.LoadPage(new BaoCaoNXT());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MainForm mainForm = (MainForm)this.Parent.Parent;
            mainForm.LoadPage(new TimKiem()); 
        }
    }
}
