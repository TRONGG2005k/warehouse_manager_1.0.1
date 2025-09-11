namespace warehouse_manager.ui.uiController.phieuxuat
{
    partial class DuyetPhieu
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
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            panel10 = new Panel();
            button2 = new Button();
            label2 = new Label();
            textBox2 = new TextBox();
            button1 = new Button();
            textBox1 = new TextBox();
            comboBox1 = new ComboBox();
            button14 = new Button();
            dateTimePicker2 = new DateTimePicker();
            dateTimePicker1 = new DateTimePicker();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            button13 = new Button();
            button11 = new Button();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1097, 699);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(panel10, 0, 0);
            tableLayoutPanel2.Controls.Add(dataGridView1, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 73);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 228F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 356F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(1091, 623);
            tableLayoutPanel2.TabIndex = 5;
            // 
            // panel10
            // 
            panel10.Anchor = AnchorStyles.None;
            panel10.Controls.Add(button2);
            panel10.Controls.Add(label2);
            panel10.Controls.Add(textBox2);
            panel10.Controls.Add(button1);
            panel10.Controls.Add(textBox1);
            panel10.Controls.Add(comboBox1);
            panel10.Controls.Add(button14);
            panel10.Controls.Add(dateTimePicker2);
            panel10.Controls.Add(dateTimePicker1);
            panel10.Controls.Add(radioButton2);
            panel10.Controls.Add(radioButton1);
            panel10.Controls.Add(button13);
            panel10.Controls.Add(button11);
            panel10.Location = new Point(54, 14);
            panel10.Name = "panel10";
            panel10.Size = new Size(982, 200);
            panel10.TabIndex = 0;
            // 
            // button2
            // 
            button2.Location = new Point(837, 62);
            button2.Name = "button2";
            button2.Size = new Size(113, 36);
            button2.TabIndex = 19;
            button2.Text = "Huỷ đơn ";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 81);
            label2.Name = "label2";
            label2.Size = new Size(104, 20);
            label2.TabIndex = 18;
            label2.Text = "Lý do Huỷ đơn";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(3, 104);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.Size = new Size(955, 93);
            textBox2.TabIndex = 17;
            // 
            // button1
            // 
            button1.Location = new Point(837, 16);
            button1.Name = "button1";
            button1.Size = new Size(113, 36);
            button1.TabIndex = 14;
            button1.Text = "Duyệt";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(647, 25);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(28, 27);
            textBox1.TabIndex = 13;
            textBox1.Visible = false;
            // 
            // comboBox1
            // 
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(3, 39);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(180, 28);
            comboBox1.TabIndex = 12;
            comboBox1.Visible = false;
            // 
            // button14
            // 
            button14.Location = new Point(515, 41);
            button14.Name = "button14";
            button14.Size = new Size(94, 29);
            button14.TabIndex = 11;
            button14.Text = "Tìm";
            button14.UseVisualStyleBackColor = true;
            button14.Visible = false;
            button14.Click += button14_Click;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Location = new Point(3, 40);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(250, 27);
            dateTimePicker2.TabIndex = 10;
            dateTimePicker2.Visible = false;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(259, 40);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(250, 27);
            dateTimePicker1.TabIndex = 9;
            dateTimePicker1.Visible = false;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(164, 3);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(129, 24);
            radioButton2.TabIndex = 8;
            radioButton2.TabStop = true;
            radioButton2.Text = "Tìm theo ngày ";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(3, 3);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(155, 24);
            radioButton1.TabIndex = 7;
            radioButton1.TabStop = true;
            radioButton1.Text = "Lọc theo trạng thái";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // button13
            // 
            button13.Location = new Point(189, 36);
            button13.Name = "button13";
            button13.Size = new Size(94, 29);
            button13.TabIndex = 5;
            button13.Text = "tìm";
            button13.UseVisualStyleBackColor = true;
            button13.Visible = false;
            button13.Click += button13_Click;
            // 
            // button11
            // 
            button11.Anchor = AnchorStyles.None;
            button11.BackColor = Color.FromArgb(128, 255, 128);
            button11.ForeColor = Color.Black;
            button11.Location = new Point(1116, 80);
            button11.Name = "button11";
            button11.Size = new Size(124, 62);
            button11.TabIndex = 2;
            button11.Text = "Duyệt đơn";
            button11.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 231);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1085, 389);
            dataGridView1.TabIndex = 3;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 22.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(1091, 70);
            label1.TabIndex = 0;
            label1.Text = "Duyệt phiếu";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DuyetPhieu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "DuyetPhieu";
            Size = new Size(1097, 699);
            Load += DuyetPhieu_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel10;
        private TextBox textBox1;
        private ComboBox comboBox1;
        private Button button14;
        private DateTimePicker dateTimePicker2;
        private DateTimePicker dateTimePicker1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Button button13;
        private Button button11;
        private DataGridView dataGridView1;
        private Button button1;
        private Button button2;
        private Label label2;
        private TextBox textBox2;
    }
}
