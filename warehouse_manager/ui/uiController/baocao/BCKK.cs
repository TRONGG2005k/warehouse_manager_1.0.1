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
using warehouse_manager.dto.o;
using warehouse_manager.service;

namespace warehouse_manager.ui.uiController.baocao
{
    public partial class BCKK : UserControl
    {
        private WarehouseManagerContext context = new WarehouseManagerContext();
        private BaoCaoService baoCao = new BaoCaoService();
        public BCKK()
        {
            InitializeComponent();
        }
        private string tam;
        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = context.PhieuKiemKes
             .Where(kk => kk.NgayKiemKe >= dateTimePicker1.Value && kk.NgayKiemKe <= dateTimePicker2.Value)
             .Select(kk => new PhieuKiemKeDto
             {
                 MaPhieuKiemKe = kk.MaPhieu ?? "",
                 NgayKiemKe = kk.NgayKiemKe,
                 NguoiTao = kk.NguoiKiemKe ?? "",
                 GhiChu = kk.GhiChu ?? ""
             })
             .ToList();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null &&
                dataGridView1.CurrentRow.Cells["MaPhieuKiemKe"] != null &&
               dataGridView1.CurrentRow.Cells["MaPhieuKiemKe"].Value != null)
            {
                string maPhieu = dataGridView1.CurrentRow.Cells["MaPhieuKiemKe"].Value.ToString();
                tam = maPhieu;
                dataGridView1.DataSource = baoCao.bCKienKe(maPhieu);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phiếu kiểm kê trước khi xem chi tiết.");
            }
        }

        private void BCKK_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = context.PhieuKiemKes
              .Where(kk => kk.NgayKiemKe >= dateTimePicker1.Value && kk.NgayKiemKe <= dateTimePicker2.Value)
              .ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Lưu báo cáo kiểm kê";
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = "BaoCaoKiemKe.xlsx"; // gợi ý tên file mặc định

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    baoCao.XuatExcel(
                        baoCao.bCKienKe(tam),
                        filePath,
                        dateTimePicker1.Value,
                        dateTimePicker2.Value
                    );

                    MessageBox.Show("Xuất báo cáo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
