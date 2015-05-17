namespace ProjektBD.Forms
{
    partial class AdministratorMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdministratorMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.messageCount = new System.Windows.Forms.Label();
            this.messageImage = new System.Windows.Forms.PictureBox();
            this.notificationCount = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nowyUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notificationImage = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageImage)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.notificationImage)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.messageCount);
            this.panel1.Controls.Add(this.messageImage);
            this.panel1.Controls.Add(this.notificationCount);
            this.panel1.Controls.Add(this.notificationImage);
            this.panel1.Location = new System.Drawing.Point(855, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(248, 50);
            this.panel1.TabIndex = 18;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // messageCount
            // 
            this.messageCount.AutoSize = true;
            this.messageCount.BackColor = System.Drawing.Color.White;
            this.messageCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messageCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.messageCount.ForeColor = System.Drawing.Color.Red;
            this.messageCount.Location = new System.Drawing.Point(134, 20);
            this.messageCount.Name = "messageCount";
            this.messageCount.Size = new System.Drawing.Size(19, 20);
            this.messageCount.TabIndex = 21;
            this.messageCount.Text = "0";
            this.messageCount.Visible = false;
            // 
            // messageImage
            // 
            this.messageImage.Image = global::ProjektBD.Properties.Resources.mail2;
            this.messageImage.Location = new System.Drawing.Point(100, 0);
            this.messageImage.Name = "messageImage";
            this.messageImage.Size = new System.Drawing.Size(40, 40);
            this.messageImage.TabIndex = 20;
            this.messageImage.TabStop = false;
            // 
            // notificationCount
            // 
            this.notificationCount.AutoSize = true;
            this.notificationCount.BackColor = System.Drawing.Color.Transparent;
            this.notificationCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.notificationCount.ContextMenuStrip = this.contextMenuStrip1;
            this.notificationCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.notificationCount.ForeColor = System.Drawing.Color.Red;
            this.notificationCount.Location = new System.Drawing.Point(207, 19);
            this.notificationCount.Name = "notificationCount";
            this.notificationCount.Size = new System.Drawing.Size(19, 20);
            this.notificationCount.TabIndex = 0;
            this.notificationCount.Text = "0";
            this.notificationCount.Visible = false;
            this.notificationCount.Click += new System.EventHandler(this.notificationCount_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nowyUserToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(176, 26);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // nowyUserToolStripMenuItem
            // 
            this.nowyUserToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nowyUserToolStripMenuItem.ForeColor = System.Drawing.Color.Red;
            this.nowyUserToolStripMenuItem.Name = "nowyUserToolStripMenuItem";
            this.nowyUserToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.nowyUserToolStripMenuItem.Text = "Nowy Prowadzący";
            this.nowyUserToolStripMenuItem.MouseEnter += new System.EventHandler(this.nowyUserToolStripMenuItem_MouseEnter);
            // 
            // notificationImage
            // 
            this.notificationImage.ContextMenuStrip = this.contextMenuStrip1;
            this.notificationImage.Image = global::ProjektBD.Properties.Resources.znak2;
            this.notificationImage.Location = new System.Drawing.Point(173, 0);
            this.notificationImage.Name = "notificationImage";
            this.notificationImage.Size = new System.Drawing.Size(40, 40);
            this.notificationImage.TabIndex = 20;
            this.notificationImage.TabStop = false;
            this.notificationImage.Click += new System.EventHandler(this.notificationImage_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 9);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 22);
            this.button3.TabIndex = 17;
            this.button3.Text = "Zarządzanie kontem";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Brown;
            this.label1.Location = new System.Drawing.Point(415, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(350, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "Panel sterowania administratora";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 53);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1141, 540);
            this.tabControl1.TabIndex = 19;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1133, 514);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Przeglądanie bazy danych";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(425, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Read-only mode";
            this.label7.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(22, 55);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1086, 440);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserAddedRow);
            this.dataGridView1.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserDeletedRow);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(204, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(211, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(19, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Wybierz tabelę do wyświetlenia:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1133, 514);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Zarządzanie bazą danych";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(363, 162);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 253);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zmiana stanu bazy danych";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ProjektBD.Properties.Resources.unpressed;
            this.pictureBox1.Location = new System.Drawing.Point(112, 66);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(118, 118);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.Color.Chartreuse;
            this.label4.Location = new System.Drawing.Point(566, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "włączona";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(391, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "Aktualny stan bazy:";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.comboBox2);
            this.tabPage3.Controls.Add(this.listView1);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1133, 514);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Zarządzanie prowadzącymi";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(713, 294);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 51);
            this.button1.TabIndex = 9;
            this.button1.Text = "Przypisz do zakładu";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(675, 230);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(199, 21);
            this.comboBox2.TabIndex = 8;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(258, 74);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(368, 406);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(710, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 18);
            this.label6.TabIndex = 6;
            this.label6.Text = "Wybierz zakład:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(352, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(189, 18);
            this.label5.TabIndex = 5;
            this.label5.Text = "Wybierz prowadzącego:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ProjektBD.Properties.Resources.logout;
            this.pictureBox2.Location = new System.Drawing.Point(1109, 11);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 40);
            this.pictureBox2.TabIndex = 20;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // AdministratorMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 605);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AdministratorMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminstratorMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegisterForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdministratorMain_FormClosed);
            this.Load += new System.EventHandler(this.AdministratorMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageImage)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.notificationImage)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label notificationCount;
        private System.Windows.Forms.PictureBox notificationImage;
        private System.Windows.Forms.Label messageCount;
        private System.Windows.Forms.PictureBox messageImage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nowyUserToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label7;
    }
}