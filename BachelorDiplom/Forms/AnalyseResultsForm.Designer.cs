using System.ComponentModel;
using System.Windows.Forms;

namespace BachelorLibAPI.Forms
{
    partial class AnalyseResultsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.edtAfterCrash = new System.Windows.Forms.TextBox();
            this.lblDriverSurname = new System.Windows.Forms.Label();
            this.lblDriverName = new System.Windows.Forms.Label();
            this.lblDangerDegree = new System.Windows.Forms.Label();
            this.lblPlace = new System.Windows.Forms.Label();
            this.cmbRes = new System.Windows.Forms.ComboBox();
            this.cmbDriverNumbers = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpLoc = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.edtAfterCrash, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblDriverSurname, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblDriverName, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblDangerDegree, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPlace, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbRes, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbDriverNumbers, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.label7, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.dtpLoc, 2, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.59313F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.59313F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.59313F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.65445F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.78308F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.78308F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(646, 248);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(14, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Уровень опасности:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label2, 2);
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(177, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Опасное вещество: ";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(84, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Действия";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(340, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 19);
            this.label4.TabIndex = 5;
            this.label4.Text = "Местонахождение: ";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(78, 168);
            this.label5.Name = "label5";
            this.tableLayoutPanel1.SetRowSpan(this.label5, 2);
            this.label5.Size = new System.Drawing.Size(80, 19);
            this.label5.TabIndex = 6;
            this.label5.Text = "Водитель: ";
            // 
            // edtAfterCrash
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.edtAfterCrash, 3);
            this.edtAfterCrash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtAfterCrash.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edtAfterCrash.Location = new System.Drawing.Point(164, 99);
            this.edtAfterCrash.Multiline = true;
            this.edtAfterCrash.Name = "edtAfterCrash";
            this.edtAfterCrash.ReadOnly = true;
            this.edtAfterCrash.Size = new System.Drawing.Size(479, 42);
            this.edtAfterCrash.TabIndex = 7;
            // 
            // lblDriverSurname
            // 
            this.lblDriverSurname.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDriverSurname.AutoSize = true;
            this.lblDriverSurname.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDriverSurname.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblDriverSurname.Location = new System.Drawing.Point(247, 168);
            this.lblDriverSurname.Name = "lblDriverSurname";
            this.tableLayoutPanel1.SetRowSpan(this.lblDriverSurname, 2);
            this.lblDriverSurname.Size = new System.Drawing.Size(72, 19);
            this.lblDriverSurname.TabIndex = 8;
            this.lblDriverSurname.Text = "Фамилия";
            // 
            // lblDriverName
            // 
            this.lblDriverName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDriverName.AutoSize = true;
            this.lblDriverName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDriverName.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblDriverName.Location = new System.Drawing.Point(325, 168);
            this.lblDriverName.Name = "lblDriverName";
            this.tableLayoutPanel1.SetRowSpan(this.lblDriverName, 2);
            this.lblDriverName.Size = new System.Drawing.Size(37, 19);
            this.lblDriverName.TabIndex = 9;
            this.lblDriverName.Text = "Имя";
            // 
            // lblDangerDegree
            // 
            this.lblDangerDegree.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDangerDegree.AutoSize = true;
            this.lblDangerDegree.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDangerDegree.Location = new System.Drawing.Point(208, 62);
            this.lblDangerDegree.Name = "lblDangerDegree";
            this.lblDangerDegree.Size = new System.Drawing.Size(66, 19);
            this.lblDangerDegree.TabIndex = 11;
            this.lblDangerDegree.Text = "Уровень";
            // 
            // lblPlace
            // 
            this.lblPlace.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPlace.AutoSize = true;
            this.lblPlace.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPlace.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblPlace.Location = new System.Drawing.Point(540, 62);
            this.lblPlace.Name = "lblPlace";
            this.lblPlace.Size = new System.Drawing.Size(49, 19);
            this.lblPlace.TabIndex = 12;
            this.lblPlace.Text = "Город";
            // 
            // cmbRes
            // 
            this.cmbRes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.cmbRes, 2);
            this.cmbRes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRes.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbRes.FormattingEnabled = true;
            this.cmbRes.Location = new System.Drawing.Point(325, 10);
            this.cmbRes.Name = "cmbRes";
            this.cmbRes.Size = new System.Drawing.Size(318, 27);
            this.cmbRes.TabIndex = 1;
            this.cmbRes.SelectedIndexChanged += new System.EventHandler(this.cmbRes_SelectedIndexChanged);
            // 
            // cmbDriverNumbers
            // 
            this.cmbDriverNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDriverNumbers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDriverNumbers.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbDriverNumbers.FormattingEnabled = true;
            this.cmbDriverNumbers.Location = new System.Drawing.Point(486, 180);
            this.cmbDriverNumbers.Name = "cmbDriverNumbers";
            this.cmbDriverNumbers.Size = new System.Drawing.Size(157, 27);
            this.cmbDriverNumbers.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(500, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 19);
            this.label7.TabIndex = 15;
            this.label7.Text = "Номера телефона:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label6, 2);
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(38, 220);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(281, 19);
            this.label6.TabIndex = 13;
            this.label6.Text = "Предположительное время нахождения:";
            // 
            // dtpLoc
            // 
            this.dtpLoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpLoc.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpLoc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpLoc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpLoc.Location = new System.Drawing.Point(325, 216);
            this.dtpLoc.Name = "dtpLoc";
            this.dtpLoc.Size = new System.Drawing.Size(155, 26);
            this.dtpLoc.TabIndex = 14;
            // 
            // AnalyseResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 248);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AnalyseResultsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Результаты анализа";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox cmbRes;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox edtAfterCrash;
        private Label lblDriverSurname;
        private Label lblDriverName;
        private ComboBox cmbDriverNumbers;
        private Label lblDangerDegree;
        private Label lblPlace;
        private Label label6;
        private DateTimePicker dtpLoc;
        private Label label7;

    }
}