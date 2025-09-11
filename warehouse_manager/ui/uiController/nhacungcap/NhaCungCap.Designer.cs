namespace warehouse_manager.ui.uiController.nhacungcap
{
    partial class NhaCungCap
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            dataGridView1 = new DataGridView();
            tableLayoutPanel3 = new TableLayoutPanel();
            panel2 = new Panel();
            panel3 = new Panel();
            label7 = new Label();
            label6 = new Label();
            label4 = new Label();
            txtMoTa = new TextBox();
            txtEmail = new TextBox();
            txtSoDienThoai = new TextBox();
            txtDiaChi = new TextBox();
            txtTenNCC = new TextBox();
            label5 = new Label();
            txtSearch = new TextBox();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            label3 = new Label();
            label2 = new Label();
            button1 = new Button();
            label1 = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(990, 25);
            panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 296);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(996, 222);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(panel1, 0, 0);
            tableLayoutPanel3.Controls.Add(panel2, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 10.97561F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 89.02439F));
            tableLayoutPanel3.Size = new Size(996, 287);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 34);
            panel2.Name = "panel2";
            panel2.Size = new Size(990, 250);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.None;
            panel3.BackColor = Color.Transparent;
            panel3.Controls.Add(label7);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(label4);
            panel3.Controls.Add(txtMoTa);
            panel3.Controls.Add(txtEmail);
            panel3.Controls.Add(txtSoDienThoai);
            panel3.Controls.Add(txtDiaChi);
            panel3.Controls.Add(txtTenNCC);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(txtSearch);
            panel3.Controls.Add(button4);
            panel3.Controls.Add(button3);
            panel3.Controls.Add(button2);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(button1);
            panel3.Location = new Point(91, -7);
            panel3.Name = "panel3";
            panel3.Size = new Size(809, 265);
            panel3.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            label7.Location = new Point(49, 139);
            label7.Name = "label7";
            label7.Size = new Size(62, 23);
            label7.TabIndex = 19;
            label7.Text = "Địa chỉ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            label6.Location = new Point(49, 73);
            label6.Name = "label6";
            label6.Size = new Size(111, 23);
            label6.TabIndex = 18;
            label6.Text = "Số điện thoại";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            label4.Location = new Point(49, 106);
            label4.Name = "label4";
            label4.Size = new Size(51, 23);
            label4.TabIndex = 17;
            label4.Text = "Email";
            // 
            // txtMoTa
            // 
            txtMoTa.Location = new Point(200, 139);
            txtMoTa.Multiline = true;
            txtMoTa.Name = "txtMoTa";
            txtMoTa.Size = new Size(248, 115);
            txtMoTa.TabIndex = 16;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(200, 106);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(248, 27);
            txtEmail.TabIndex = 15;
            // 
            // txtSoDienThoai
            // 
            txtSoDienThoai.Location = new Point(200, 73);
            txtSoDienThoai.Name = "txtSoDienThoai";
            txtSoDienThoai.Size = new Size(248, 27);
            txtSoDienThoai.TabIndex = 14;
            // 
            // txtDiaChi
            // 
            txtDiaChi.Location = new Point(200, 40);
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(248, 27);
            txtDiaChi.TabIndex = 13;
            // 
            // txtTenNCC
            // 
            txtTenNCC.Location = new Point(200, 7);
            txtTenNCC.Name = "txtTenNCC";
            txtTenNCC.Size = new Size(248, 27);
            txtTenNCC.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            label5.Location = new Point(461, 170);
            label5.Name = "label5";
            label5.Size = new Size(258, 46);
            label5.TabIndex = 11;
            label5.Text = "Tìm kiếm theo tên nhà cung cấp\r\n\r\n";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(461, 219);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(245, 27);
            txtSearch.TabIndex = 10;
            // 
            // button4
            // 
            button4.Location = new Point(712, 219);
            button4.Name = "button4";
            button4.Size = new Size(94, 29);
            button4.TabIndex = 9;
            button4.Text = "Tìm";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new Point(461, 80);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 8;
            button3.Text = "Xoá";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(461, 45);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 7;
            button2.Text = "Sửa";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            label3.Location = new Point(49, 40);
            label3.Name = "label3";
            label3.Size = new Size(62, 23);
            label3.TabIndex = 5;
            label3.Text = "Địa chỉ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            label2.Location = new Point(49, 8);
            label2.Name = "label2";
            label2.Size = new Size(145, 23);
            label2.TabIndex = 4;
            label2.Text = "Tên nhà cung cấp";
            // 
            // button1
            // 
            button1.Location = new Point(461, 10);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "Thêm";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label1, 2);
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 163);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(1002, 65);
            label1.TabIndex = 1;
            label1.Text = "Quản lý nhà cung cấp\r\n";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.Transparent;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(dataGridView1, 0, 1);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 68);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 56.41892F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 43.58108F));
            tableLayoutPanel2.Size = new Size(1002, 521);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1441307F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 88.8558655F));
            tableLayoutPanel1.Size = new Size(1008, 592);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // NhaCungCap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "NhaCungCap";
            Size = new Size(1008, 592);
            Load += NhaCungCap_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DataGridView dataGridView1;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel panel2;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel3;
        private Label label5;
        private TextBox txtSearch;
        private Button button4;
        private Button button3;
        private Button button2;
        private Label label3;
        private Label label2;
        private Button button1;
        private Label label7;
        private Label label6;
        private Label label4;
        private TextBox txtMoTa;
        private TextBox txtEmail;
        private TextBox txtSoDienThoai;
        private TextBox txtDiaChi;
        private TextBox txtTenNCC;
    }
}
