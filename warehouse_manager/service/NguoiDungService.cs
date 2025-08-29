using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse_manager.context;
using warehouse_manager.Models;
using warehouse_manager.ui;
using warehouse_manager.ui.user_control;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace warehouse_manager.service
{
    internal class NguoiDungService
    {
        private  WarehouseManagerContext context;
        public NguoiDungService()
        {
            context = new WarehouseManagerContext();
        }
        public Boolean login(NguoiDung nguoiDung)
        {
            try
            {
                var context = new WarehouseManagerContext();
                
                    if (nguoiDung.TenDangNhap!.Equals("") || nguoiDung.MatKhau!.Equals(""))
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                        return false;
                    }
                     
                    var user = context.NguoiDungs
                              .FirstOrDefault(
                                  u => u.TenDangNhap == nguoiDung.TenDangNhap
                                  &&
                                  u.MatKhau == nguoiDung.MatKhau
                              );

                    if (user != null)
                    {
                        MessageBox.Show("Đăng nhập thành công");
                        String filePath = "user.txt";

                     
                        File.WriteAllText(filePath, user.TenDangNhap);
                       

                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng");
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("action failed" + ex.Message);
            }
            return false;
        }

        public NguoiDung layThongTinNguoiDung()
        {
            string content = File.Exists("user.txt") ? File.ReadAllText("user.txt") : "";
            var nguoiDung = context.NguoiDungs
               .Include(nd => nd.VaiTros)  
               .FirstOrDefault(nd => nd.TenDangNhap == content)
               ?? throw new Exception("Người dùng không tồn tại");
            return nguoiDung;
        }

        public bool kiemTraVaiTroAdmin()
        {
            try
            {
                var nguoiDung = layThongTinNguoiDung();
                foreach (var vaiTro in nguoiDung.VaiTros)
                {
                    if(vaiTro.TenVaiTro == "ADMIN")
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("action failed: " + ex.Message);
                return false;
            }
        }

        public void logout()
        {
            string filePath = "user.txt";

            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath, string.Empty);
                // xóa hẳn file: File.Delete(filePath);
            }
        }
    }
}
