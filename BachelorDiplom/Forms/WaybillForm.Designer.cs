using System.ComponentModel;
using System.Windows.Forms;

namespace BachelorLibAPI.Forms
{
    partial class WaybillForm
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
            this.ltpNewDriver = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblConsName = new System.Windows.Forms.Label();
            this.cmbStart = new System.Windows.Forms.ComboBox();
            this.lblTelephoneNumber = new System.Windows.Forms.Label();
            this.edtDriverPhoneNumber = new System.Windows.Forms.MaskedTextBox();
            this.lblArr = new System.Windows.Forms.Label();
            this.cmbArr = new System.Windows.Forms.ComboBox();
            this.cmbMid = new System.Windows.Forms.ComboBox();
            this.btnMoreMid = new System.Windows.Forms.Button();
            this.btnNewDriver = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.cmbCons = new System.Windows.Forms.ComboBox();
            this.ltpNewDriver.SuspendLayout();
            this.SuspendLayout();
            // 
            // ltpNewDriver
            // 
            this.ltpNewDriver.ColumnCount = 3;
            this.ltpNewDriver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.ltpNewDriver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.ltpNewDriver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.ltpNewDriver.Controls.Add(this.label1, 0, 2);
            this.ltpNewDriver.Controls.Add(this.lblStart, 0, 1);
            this.ltpNewDriver.Controls.Add(this.lblConsName, 0, 0);
            this.ltpNewDriver.Controls.Add(this.cmbStart, 1, 1);
            this.ltpNewDriver.Controls.Add(this.lblTelephoneNumber, 0, 4);
            this.ltpNewDriver.Controls.Add(this.edtDriverPhoneNumber, 1, 4);
            this.ltpNewDriver.Controls.Add(this.lblArr, 0, 3);
            this.ltpNewDriver.Controls.Add(this.cmbArr, 1, 3);
            this.ltpNewDriver.Controls.Add(this.cmbMid, 1, 2);
            this.ltpNewDriver.Controls.Add(this.btnMoreMid, 2, 2);
            this.ltpNewDriver.Controls.Add(this.btnNewDriver, 2, 6);
            this.ltpNewDriver.Controls.Add(this.label2, 0, 5);
            this.ltpNewDriver.Controls.Add(this.dtpStart, 1, 5);
            this.ltpNewDriver.Controls.Add(this.cmbCons, 1, 0);
            this.ltpNewDriver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ltpNewDriver.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ltpNewDriver.Location = new System.Drawing.Point(0, 0);
            this.ltpNewDriver.Name = "ltpNewDriver";
            this.ltpNewDriver.RowCount = 7;
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.ltpNewDriver.Size = new System.Drawing.Size(575, 262);
            this.ltpNewDriver.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(77, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 19);
            this.label1.TabIndex = 13;
            this.label1.Text = "Промежуточные пункты:";
            // 
            // lblStart
            // 
            this.lblStart.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblStart.AutoSize = true;
            this.lblStart.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStart.Location = new System.Drawing.Point(99, 44);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(156, 19);
            this.lblStart.TabIndex = 1;
            this.lblStart.Text = "* Пункт отправления:";
            // 
            // lblConsName
            // 
            this.lblConsName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblConsName.AutoSize = true;
            this.lblConsName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblConsName.Location = new System.Drawing.Point(16, 8);
            this.lblConsName.Name = "lblConsName";
            this.lblConsName.Size = new System.Drawing.Size(239, 19);
            this.lblConsName.TabIndex = 0;
            this.lblConsName.Text = "* Наименование груза (вещества):";
            // 
            // cmbStart
            // 
            this.cmbStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStart.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbStart.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ltpNewDriver.SetColumnSpan(this.cmbStart, 2);
            this.cmbStart.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbStart.FormattingEnabled = true;
            this.cmbStart.Location = new System.Drawing.Point(261, 40);
            this.cmbStart.Name = "cmbStart";
            this.cmbStart.Size = new System.Drawing.Size(311, 27);
            this.cmbStart.TabIndex = 11;
            // 
            // lblTelephoneNumber
            // 
            this.lblTelephoneNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTelephoneNumber.AutoSize = true;
            this.lblTelephoneNumber.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTelephoneNumber.Location = new System.Drawing.Point(109, 152);
            this.lblTelephoneNumber.Name = "lblTelephoneNumber";
            this.lblTelephoneNumber.Size = new System.Drawing.Size(146, 19);
            this.lblTelephoneNumber.TabIndex = 3;
            this.lblTelephoneNumber.Text = "* Телефон водителя:";
            // 
            // edtDriverPhoneNumber
            // 
            this.edtDriverPhoneNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.edtDriverPhoneNumber.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edtDriverPhoneNumber.Location = new System.Drawing.Point(261, 149);
            this.edtDriverPhoneNumber.Mask = "(999) 000-0000";
            this.edtDriverPhoneNumber.Name = "edtDriverPhoneNumber";
            this.edtDriverPhoneNumber.Size = new System.Drawing.Size(136, 26);
            this.edtDriverPhoneNumber.TabIndex = 8;
            // 
            // lblArr
            // 
            this.lblArr.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblArr.AutoSize = true;
            this.lblArr.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblArr.Location = new System.Drawing.Point(118, 116);
            this.lblArr.Name = "lblArr";
            this.lblArr.Size = new System.Drawing.Size(137, 19);
            this.lblArr.TabIndex = 2;
            this.lblArr.Text = "* Пункт прибытия:";
            // 
            // cmbArr
            // 
            this.cmbArr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbArr.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbArr.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ltpNewDriver.SetColumnSpan(this.cmbArr, 2);
            this.cmbArr.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbArr.FormattingEnabled = true;
            this.cmbArr.Location = new System.Drawing.Point(261, 112);
            this.cmbArr.Name = "cmbArr";
            this.cmbArr.Size = new System.Drawing.Size(311, 27);
            this.cmbArr.TabIndex = 12;
            // 
            // cmbMid
            // 
            this.cmbMid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMid.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbMid.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbMid.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbMid.FormattingEnabled = true;
            this.cmbMid.Location = new System.Drawing.Point(261, 76);
            this.cmbMid.Name = "cmbMid";
            this.cmbMid.Size = new System.Drawing.Size(195, 27);
            this.cmbMid.TabIndex = 14;
            // 
            // btnMoreMid
            // 
            this.btnMoreMid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoreMid.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMoreMid.Location = new System.Drawing.Point(462, 75);
            this.btnMoreMid.Name = "btnMoreMid";
            this.btnMoreMid.Size = new System.Drawing.Size(110, 29);
            this.btnMoreMid.TabIndex = 15;
            this.btnMoreMid.Text = "Ещё";
            this.btnMoreMid.UseVisualStyleBackColor = true;
            this.btnMoreMid.Click += new System.EventHandler(this.AddMoreMidCities);
            // 
            // btnNewDriver
            // 
            this.btnNewDriver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewDriver.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewDriver.Location = new System.Drawing.Point(462, 219);
            this.btnNewDriver.Name = "btnNewDriver";
            this.btnNewDriver.Size = new System.Drawing.Size(110, 40);
            this.btnNewDriver.TabIndex = 10;
            this.btnNewDriver.Text = "Добавить";
            this.btnNewDriver.UseVisualStyleBackColor = true;
            this.btnNewDriver.Click += new System.EventHandler(this.AddNewWaybill);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(108, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 19);
            this.label2.TabIndex = 16;
            this.label2.Text = "* Дата отправления:";
            // 
            // dtpStart
            // 
            this.dtpStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStart.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpStart.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(261, 185);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(195, 26);
            this.dtpStart.TabIndex = 17;
            // 
            // cmbCons
            // 
            this.cmbCons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCons.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCons.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ltpNewDriver.SetColumnSpan(this.cmbCons, 2);
            this.cmbCons.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbCons.FormattingEnabled = true;
            this.cmbCons.Location = new System.Drawing.Point(261, 4);
            this.cmbCons.Name = "cmbCons";
            this.cmbCons.Size = new System.Drawing.Size(311, 27);
            this.cmbCons.TabIndex = 18;
            // 
            // WaybillForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 262);
            this.Controls.Add(this.ltpNewDriver);
            this.Name = "WaybillForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Заполнить путевой лист";
            this.ltpNewDriver.ResumeLayout(false);
            this.ltpNewDriver.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel ltpNewDriver;
        private Label lblArr;
        private Label lblTelephoneNumber;
        private Label lblStart;
        private Label lblConsName;
        private MaskedTextBox edtDriverPhoneNumber;
        private Button btnNewDriver;
        private ComboBox cmbStart;
        private ComboBox cmbArr;
        private Label label1;
        private ComboBox cmbMid;
        private Button btnMoreMid;
        private Label label2;
        private DateTimePicker dtpStart;
        private ComboBox cmbCons;
    }
}