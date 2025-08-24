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
        public Boolean login(NguoiDung nguoiDung)
        {
            try
            {
                using (var context = new WarehouseManagerContext())
                {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("action failed" + ex.Message);
            }
            return false;
        }
    }
}
