﻿namespace ProjektBD.Forms
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
            this.listView2 = new System.Windows.Forms.ListView();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listView12 = new System.Windows.Forms.ListView();
            this.listView16 = new System.Windows.Forms.ListView();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listView17 = new System.Windows.Forms.ListView();
            this.button13 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.listView14 = new System.Windows.Forms.ListView();
            this.listView13 = new System.Windows.Forms.ListView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.customListView1 = new ProjektBD.Custom_Controls.customListView();
            this.customListView2 = new ProjektBD.Custom_Controls.customListView();
            this.customListView3 = new ProjektBD.Custom_Controls.customListView();
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
            this.tabPage1.Controls.Add(this.customListView3);
            this.tabPage1.Controls.Add(this.listView2);
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
            // listView2
            // 
            this.listView2.Location = new System.Drawing.Point(629, 72);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(386, 353);
            this.listView2.TabIndex = 9;
            this.listView2.UseCompatibleStateImageBehavior = false;
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
            this.button1.Location = new System.Drawing.Point(225, 448);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 40);
            this.button1.TabIndex = 2;
            this.button1.Text = "Zapisz";
            this.button1.UseVisualStyleBackColor = true;
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
            this.tabPage3.Controls.Add(this.customListView1);
            this.tabPage3.Controls.Add(this.listView12);
            this.tabPage3.Controls.Add(this.listView16);
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
            // listView12
            // 
            this.listView12.Location = new System.Drawing.Point(313, 51);
            this.listView12.Name = "listView12";
            this.listView12.Size = new System.Drawing.Size(275, 437);
            this.listView12.TabIndex = 32;
            this.listView12.UseCompatibleStateImageBehavior = false;
            // 
            // listView16
            // 
            this.listView16.Location = new System.Drawing.Point(609, 51);
            this.listView16.Name = "listView16";
            this.listView16.Size = new System.Drawing.Size(275, 437);
            this.listView16.TabIndex = 31;
            this.listView16.UseCompatibleStateImageBehavior = false;
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
            this.groupBox2.Controls.Add(this.listView17);
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
            // listView17
            // 
            this.listView17.Location = new System.Drawing.Point(9, 158);
            this.listView17.Name = "listView17";
            this.listView17.Size = new System.Drawing.Size(174, 265);
            this.listView17.TabIndex = 3;
            this.listView17.UseCompatibleStateImageBehavior = false;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(47, 100);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(95, 35);
            this.button13.TabIndex = 2;
            this.button13.Text = "Szukaj";
            this.button13.UseVisualStyleBackColor = true;
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
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label20.Location = new System.Drawing.Point(6, 43);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(167, 15);
            this.label20.TabIndex = 0;
            this.label20.Text = "Podaj imię lub nazwisko:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.customListView2);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.listView14);
            this.tabPage2.Controls.Add(this.listView13);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1133, 514);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Moje przedmioty i projekty";
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
            this.button3.Location = new System.Drawing.Point(484, 452);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(168, 36);
            this.button3.TabIndex = 21;
            this.button3.Text = "Wypisz się z projektu";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(104, 452);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(168, 36);
            this.button6.TabIndex = 18;
            this.button6.Text = "Wypisz się z przedmiotu";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // listView14
            // 
            this.listView14.Location = new System.Drawing.Point(773, 52);
            this.listView14.Name = "listView14";
            this.listView14.Size = new System.Drawing.Size(307, 382);
            this.listView14.TabIndex = 16;
            this.listView14.UseCompatibleStateImageBehavior = false;
            // 
            // listView13
            // 
            this.listView13.Location = new System.Drawing.Point(411, 52);
            this.listView13.Name = "listView13";
            this.listView13.Size = new System.Drawing.Size(307, 382);
            this.listView13.TabIndex = 15;
            this.listView13.UseCompatibleStateImageBehavior = false;
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
            // customListView1
            // 
            this.customListView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.customListView1.FullRowSelect = true;
            this.customListView1.GridLines = true;
            this.customListView1.Location = new System.Drawing.Point(17, 51);
            this.customListView1.Name = "customListView1";
            this.customListView1.Size = new System.Drawing.Size(275, 437);
            this.customListView1.TabIndex = 34;
            this.customListView1.UseCompatibleStateImageBehavior = false;
            this.customListView1.View = System.Windows.Forms.View.Details;
            // 
            // customListView2
            // 
            this.customListView2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.customListView2.FullRowSelect = true;
            this.customListView2.GridLines = true;
            this.customListView2.Location = new System.Drawing.Point(44, 52);
            this.customListView2.Name = "customListView2";
            this.customListView2.Size = new System.Drawing.Size(307, 382);
            this.customListView2.TabIndex = 27;
            this.customListView2.UseCompatibleStateImageBehavior = false;
            this.customListView2.View = System.Windows.Forms.View.Details;
            // 
            // customListView3
            // 
            this.customListView3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.customListView3.FullRowSelect = true;
            this.customListView3.GridLines = true;
            this.customListView3.Location = new System.Drawing.Point(107, 72);
            this.customListView3.Name = "customListView3";
            this.customListView3.Size = new System.Drawing.Size(386, 353);
            this.customListView3.TabIndex = 10;
            this.customListView3.UseCompatibleStateImageBehavior = false;
            this.customListView3.View = System.Windows.Forms.View.Details;
            // 
            // StudentMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 605);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StudentMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StudentMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegisterForm_FormClosing);
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
        private System.Windows.Forms.ListView listView12;
        private System.Windows.Forms.ListView listView16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView listView17;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ListView listView14;
        private System.Windows.Forms.ListView listView13;
        private System.Windows.Forms.ListView listView2;
        private Custom_Controls.customListView customListView1;
        private Custom_Controls.customListView customListView2;
        private Custom_Controls.customListView customListView3;
    }
}