using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using warehouse_manager.service;

namespace warehouse_manager.ui.uiController.baocao
{
    public partial class BCNXT : UserControl
    {
        BaoCaoService baoCao = new BaoCaoService();
        public BCNXT()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = baoCao.baoCaoNXT(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Lưu báo cáo NXT";
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = "BaoCaoNXT.xlsx"; // gợi ý tên mặc định

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    baoCao.XuatExcel<warehouse_manager.dto.o.BCNXT>(
                        baoCao.baoCaoNXT(dateTimePicker1.Value, dateTimePicker2.Value),
                        filePath,
                        dateTimePicker1.Value,
                        dateTimePicker2.Value,
                        "BAO CAO NHAP-XUAT-TON",
                        "BaoCaoNXT",
                        new string[] { "Mã vật liệu", "Tên vật liệu", "Đơn vị tính", "Tồn đầu kỳ", "Nhập trong kỳ", "Xuất trong kỳ", "Tồn cuối kỳ" },
                        (ws, item, row) =>
                        {
                            ws.Cell(row, 1).Value = item.MaVatLieu?.ToString();
                            ws.Cell(row, 2).Value = item.TenVatLieu?.ToString();
                            ws.Cell(row, 3).Value = item.DonViTinh?.ToString();
                            ws.Cell(row, 4).Value = item.TonDauKy;
                            ws.Cell(row, 5).Value = item.NhapTrongKy;
                            ws.Cell(row, 6).Value = item.XuatTrongKy;
                            ws.Cell(row, 7).Value = item.TonCuoiKy;
                            //ws.Cell(row, 1).Value = item.MaVatLieu?.ToString();
                            //ws.Cell(row, 1).Value = item.MaVatLieu?.ToString();
                        }
                    );

                    MessageBox.Show("Xuất báo cáo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
