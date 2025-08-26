using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace warehouse_manager.ui
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadPage(new Login());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        public void LoadPage(UserControl page)
        {
            panel1.Controls.Clear();
            page.Dock = DockStyle.Fill; // Trang chiếm hết panel
            panel1.Controls.Add(page);
            System.Console.WriteLine("Load page" + page.Name);
        }













        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string filePath = "user.txt";

            if (File.Exists(filePath))
            {
                // Xóa nội dung file
                File.WriteAllText(filePath, string.Empty);
                // Hoặc xóa hẳn file: File.Delete(filePath);
            }
        }
    }
}
