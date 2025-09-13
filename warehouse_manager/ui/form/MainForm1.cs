using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using warehouse_manager.dto.o;
using warehouse_manager.Models;
using warehouse_manager.service;
using warehouse_manager.ui.uiController.kiemke;
using warehouse_manager.ui.uiController.phieuNhap;
using warehouse_manager.ui.uiController.phieuxuat;
namespace warehouse_manager.ui.form
{
    public partial class MainForm1 : Form
    {
        private NguoiDungService nguoiDungService;
        public MainForm1()
        {
            nguoiDungService = new NguoiDungService();
            InitializeComponent();
            LoadPage(new Login());
        }
        public void LoadPage(UserControl page)
        {
            panel1.Controls.Clear();
            page.Dock = DockStyle.Fill; // Trang chiếm hết panel
            panel1.Controls.Add(page);
            System.Console.WriteLine("Load page" + page.Name);
        }

        private void MainForm1_Load(object sender, EventArgs e)
        {

        }

        private void kiểmTraVaiTròTrướcKiVào(UserControl control)
        {
            try
            {
                if (nguoiDungService.KiemTraDangNhap())
                {
                    if (nguoiDungService.kiemTraVaiTroAdmin())
                    {
                        LoadPage(control);
                    }
                    else
                    {
                        throw new Exception("bạn không có quyền dùng chức năng này");
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa đăng nhập" +
                        "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MainForm1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string filePath = "user.txt";

            if (File.Exists(filePath))
            {

                File.WriteAllText(filePath, string.Empty);
                // xóa hẳn file: File.Delete(filePath);
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nguoiDungService.logout();
            LoadPage(new Login());
            Console.WriteLine("Logout clicked!");
        }

        private void phiếuNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nguoiDungService.KiemTraDangNhap())
            {
                LoadPage(new uiController.phieuNhap.PhieuNhap());
            }
            else
            {
                MessageBox.Show("Bạn chưa đăng nhập" +
                    "");
            }
        }

        private void phiếuXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tạoPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nguoiDungService.KiemTraDangNhap())
            {
                LoadPage(new uiController.phieuxuat.PhieuXuat());
            }
            else
            {
                MessageBox.Show("Bạn chưa đăng nhập" +
                    "");
            }
        }

        private void duyệtPhiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (nguoiDungService.KiemTraDangNhap())
                {
                    if (nguoiDungService.kiemTraVaiTroAdmin())
                    {
                        LoadPage(new uiController.phieuxuat.DuyetPhieu());
                    }
                    else
                    {
                        throw new Exception("bạn không có quyền dùng chức năng này");
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa đăng nhập" +
                        "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void kiểmKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nguoiDungService.KiemTraDangNhap())
            {
                LoadPage(new KiemKe());
            }
            else
            {
                MessageBox.Show("Bạn chưa đăng nhập" +
                    "");
            }
        }

        private void báoCáoNhậpXuấtTồnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nguoiDungService.KiemTraDangNhap())
            {
                LoadPage(new uiController.baocao.BCNXT());
            }
            else
            {
                MessageBox.Show("Bạn chưa đăng nhập" +
                    "");
            }
        }

        private void báoCáoKiểmKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nguoiDungService.KiemTraDangNhap())
            {
                LoadPage(new uiController.baocao.BCKK());
            }
            else
            {
                MessageBox.Show("Bạn chưa đăng nhập" +
                    "");
            }
        }

        private void xuấtFilePdfToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (nguoiDungService.KiemTraDangNhap())
            {
                LoadPage(new XuatPhieuXuatPDF());
            }
            else
            {
                MessageBox.Show("Bạn chưa đăng nhập" +
                    "");
            }
        }

        private void xuấtFilePdfToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (nguoiDungService.KiemTraDangNhap())
            {
                LoadPage(new XuatPhieuNhapPDF());
            }
            else
            {
                MessageBox.Show("Bạn chưa đăng nhập" +
                    "");
            }
        }

        private void quảnLýKệToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kiểmTraVaiTròTrướcKiVào(new uiController.ke.Ke());
        }

        private void quảnLýVậtLiệToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kiểmTraVaiTròTrướcKiVào(new uiController.vatlieu.VatLieu());
        }

        private void quảnLýNhàCungCấpToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            kiểmTraVaiTròTrướcKiVào(new uiController.nhacungcap.NhaCungCap());
        }

        private void quảnLýLoạiVậtLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kiểmTraVaiTròTrướcKiVào(new uiController.ke.LoaiVatLieu());
        }

        private void quảnLýCơSởSảnXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kiểmTraVaiTròTrướcKiVào(new uiController.cososanxuat.CoSoSanXuat());


        }

       

        private void tìmKiếnVậtLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nguoiDungService.KiemTraDangNhap())
            {
                LoadPage(new uiController.timkiemvitrivatlieu.TimKiemViTriVatLieu());
            }
            else
            {
                MessageBox.Show("Bạn chưa đăng nhập" +
                    "");
            }

        }
    }
}
