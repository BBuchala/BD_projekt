namespace ProjektBD.Forms
{
    partial class StudentMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudentMain));
            this.label1 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.customListView6 = new ProjektBD.Custom_Controls.customListView();
            this.customListView3 = new ProjektBD.Custom_Controls.customListView();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.customListView7 = new ProjektBD.Custom_Controls.customListView();
            this.customListView4 = new ProjektBD.Custom_Controls.customListView();
            this.customListView1 = new ProjektBD.Custom_Controls.customListView();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.customListView9 = new ProjektBD.Custom_Controls.customListView();
            this.button13 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.customListView8 = new ProjektBD.Custom_Controls.customListView();
            this.customListView5 = new ProjektBD.Custom_Controls.customListView();
            this.customListView2 = new ProjektBD.Custom_Controls.customListView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Brown;
            this.label1.Location = new System.Drawing.Point(445, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Panel sterowania studenta";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(12, 9);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(116, 22);
            this.button8.TabIndex = 5;
            this.button8.Text = "Zarządzanie kontem";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.customListView6);
            this.tabPage1.Controls.Add(this.customListView3);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1133, 514);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "Zgłoszenie";
            // 
            // customListView6
            // 
            this.customListView6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.customListView6.FullRowSelect = true;
            this.customListView6.GridLines = true;
            this.customListView6.Location = new System.Drawing.Point(629, 72);
            this.customListView6.MultiSelect = false;
            this.customListView6.Name = "customListView6";
            this.customListView6.Size = new System.Drawing.Size(386, 353);
            this.customListView6.TabIndex = 11;
            this.customListView6.UseCompatibleStateImageBehavior = false;
            this.customListView6.View = System.Windows.Forms.View.Details;
            this.customListView6.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.customListView6_ItemSelectionChanged);
            this.customListView6.Enter += new System.EventHandler(this.customListView6_Enter);
            this.customListView6.Leave += new System.EventHandler(this.customListView6_Leave);
            // 
            // customListView3
            // 
            this.customListView3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.customListView3.FullRowSelect = true;
            this.customListView3.GridLines = true;
            this.customListView3.Location = new System.Drawing.Point(107, 72);
            this.customListView3.MultiSelect = false;
            this.customListView3.Name = "customListView3";
            this.customListView3.Size = new System.Drawing.Size(386, 353);
            this.customListView3.TabIndex = 10;
            this.customListView3.UseCompatibleStateImageBehavior = false;
            this.customListView3.View = System.Windows.Forms.View.Details;
            this.customListView3.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.customListView3_ItemSelectionChanged);
            this.customListView3.Enter += new System.EventHandler(this.customListView3_Enter);
            this.customListView3.Leave += new System.EventHandler(this.customListView3_Leave);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(747, 448);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 40);
            this.button2.TabIndex = 5;
            this.button2.Text = "Zapisz";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(103, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(390, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Wybierz przedmiot, na który chcesz się zapisać:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(225, 448);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 40);
            this.button1.TabIndex = 2;
            this.button1.Text = "Zapisz";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(639, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(366, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Wybierz projekt, na który chcesz się zapisać:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.customListView7);
            this.tabPage3.Controls.Add(this.customListView4);
            this.tabPage3.Controls.Add(this.customListView1);
            this.tabPage3.Controls.Add(this.label17);
            this.tabPage3.Controls.Add(this.label18);
            this.tabPage3.Controls.Add(this.label19);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1133, 514);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Podgląd";
            // 
            // customListView7
            // 
            this.customListView7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.customListView7.FullRowSelect = true;
            this.customListView7.GridLines = true;
            this.customListView7.Location = new System.Drawing.Point(609, 51);
            this.customListView7.MultiSelect = false;
            this.customListView7.Name = "customListView7";
            this.customListView7.Size = new System.Drawing.Size(275, 437);
            this.customListView7.TabIndex = 36;
            this.customListView7.UseCompatibleStateImageBehavior = false;
            this.customListView7.View = System.Windows.Forms.View.Details;
            // 
            // customListView4
            // 
            this.customListView4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.customListView4.FullRowSelect = true;
            this.customListView4.GridLines = true;
            this.customListView4.Location = new System.Drawing.Point(313, 51);
            this.customListView4.MultiSelect = false;
            this.customListView4.Name = "customListView4";
            this.customListView4.Size = new System.Drawing.Size(275, 437);
            this.customListView4.TabIndex = 35;
            this.customListView4.UseCompatibleStateImageBehavior = false;
            this.customListView4.View = System.Windows.Forms.View.Details;
            this.customListView4.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.customListView4_ItemSelectionChanged);
            this.customListView4.Enter += new System.EventHandler(this.customListView4_Enter);
            this.customListView4.Leave += new System.EventHandler(this.customListView4_Leave);
            // 
            // customListView1
            // 
            this.customListView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.customListView1.FullRowSelect = true;
            this.customListView1.GridLines = true;
            this.customListView1.Location = new System.Drawing.Point(17, 51);
            this.customListView1.MultiSelect = false;
            this.customListView1.Name = "customListView1";
            this.customListView1.Size = new System.Drawing.Size(275, 437);
            this.customListView1.TabIndex = 34;
            this.customListView1.UseCompatibleStateImageBehavior = false;
            this.customListView1.View = System.Windows.Forms.View.Details;
            this.customListView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.customListView1_ItemSelectionChanged);
            this.customListView1.Enter += new System.EventHandler(this.customListView1_Enter);
            this.customListView1.Leave += new System.EventHandler(this.customListView1_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label17.Location = new System.Drawing.Point(684, 20);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(126, 18);
            this.label17.TabIndex = 30;
            this.label17.Text = "Lista studentów";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label18.Location = new System.Drawing.Point(382, 20);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(123, 18);
            this.label18.TabIndex = 29;
            this.label18.Text = "Lista projektów";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label19.Location = new System.Drawing.Point(77, 20);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(146, 18);
            this.label19.TabIndex = 28;
            this.label19.Text = "Lista przedmiotów";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.customListView9);
            this.groupBox2.Controls.Add(this.button13);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Location = new System.Drawing.Point(927, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(189, 437);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Wyszukiwanie użytkownika";
            // 
            // customListView9
            // 
            this.customListView9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.customListView9.FullRowSelect = true;
            this.customListView9.GridLines = true;
            this.customListView9.Location = new System.Drawing.Point(9, 158);
            this.customListView9.MultiSelect = false;
            this.customListView9.Name = "customListView9";
            this.customListView9.Size = new System.Drawing.Size(174, 265);
            this.customListView9.TabIndex = 4;
            this.customListView9.UseCompatibleStateImageBehavior = false;
            this.customListView9.View = System.Windows.Forms.View.Details;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(47, 100);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(95, 35);
            this.button13.TabIndex = 2;
            this.button13.Text = "Szukaj";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 61);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(174, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label20.Location = new System.Drawing.Point(6, 43);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(176, 13);
            this.label20.TabIndex = 0;
            this.label20.Text = "Podaj login lub jego fragment:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.customListView8);
            this.tabPage2.Controls.Add(this.customListView5);
            this.tabPage2.Controls.Add(this.customListView2);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1133, 514);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Moje przedmioty i projekty";
            // 
            // customListView8
            // 
            this.customListView8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.customListView8.FullRowSelect = true;
            this.customListView8.GridLines = true;
            this.customListView8.Location = new System.Drawing.Point(773, 52);
            this.customListView8.MultiSelect = false;
            this.customListView8.Name = "customListView8";
            this.customListView8.Size = new System.Drawing.Size(307, 382);
            this.customListView8.TabIndex = 29;
            this.customListView8.UseCompatibleStateImageBehavior = false;
            this.customListView8.View = System.Windows.Forms.View.Details;
            // 
            // customListView5
            // 
            this.customListView5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.customListView5.FullRowSelect = true;
            this.customListView5.GridLines = true;
            this.customListView5.Location = new System.Drawing.Point(411, 52);
            this.customListView5.MultiSelect = false;
            this.customListView5.Name = "customListView5";
            this.customListView5.Size = new System.Drawing.Size(307, 382);
            this.customListView5.TabIndex = 28;
            this.customListView5.UseCompatibleStateImageBehavior = false;
            this.customListView5.View = System.Windows.Forms.View.Details;
            this.customListView5.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.customListView5_ItemSelectionChanged);
            this.customListView5.Enter += new System.EventHandler(this.customListView5_Enter);
            this.customListView5.Leave += new System.EventHandler(this.customListView5_Leave);
            // 
            // customListView2
            // 
            this.customListView2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.customListView2.FullRowSelect = true;
            this.customListView2.GridLines = true;
            this.customListView2.Location = new System.Drawing.Point(44, 52);
            this.customListView2.MultiSelect = false;
            this.customListView2.Name = "customListView2";
            this.customListView2.Size = new System.Drawing.Size(307, 382);
            this.customListView2.TabIndex = 27;
            this.customListView2.UseCompatibleStateImageBehavior = false;
            this.customListView2.View = System.Windows.Forms.View.Details;
            this.customListView2.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.customListView2_ItemSelectionChanged);
            this.customListView2.Enter += new System.EventHandler(this.customListView2_Enter);
            this.customListView2.Leave += new System.EventHandler(this.customListView2_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(873, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 18);
            this.label4.TabIndex = 26;
            this.label4.Text = "Twoje oceny:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(499, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 18);
            this.label5.TabIndex = 25;
            this.label5.Text = "Wybierz projekt:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(121, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 18);
            this.label6.TabIndex = 24;
            this.label6.Text = "Wybierz przedmiot:";
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(484, 452);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(168, 36);
            this.button3.TabIndex = 21;
            this.button3.Text = "Wypisz się z projektu";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(104, 452);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(168, 36);
            this.button6.TabIndex = 18;
            this.button6.Text = "Wypisz się z przedmiotu";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 53);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1141, 540);
            this.tabControl1.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ProjektBD.Properties.Resources.logout;
            this.pictureBox2.Location = new System.Drawing.Point(1109, 11);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 40);
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(137, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Zalogowany jako:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(226, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "login";
            // 
            // StudentMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 605);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StudentMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StudentMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StudentMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StudentMain_FormClosed);
            this.Load += new System.EventHandler(this.StudentMain_Load);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button6;
        private Custom_Controls.customListView customListView1;
        private Custom_Controls.customListView customListView2;
        private Custom_Controls.customListView customListView3;
        private Custom_Controls.customListView customListView4;
        private Custom_Controls.customListView customListView5;
        private Custom_Controls.customListView customListView6;
        private Custom_Controls.customListView customListView7;
        private Custom_Controls.customListView customListView8;
        private Custom_Controls.customListView customListView9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}