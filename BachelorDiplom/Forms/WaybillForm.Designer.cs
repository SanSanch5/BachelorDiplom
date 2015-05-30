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
            this.components = new System.ComponentModel.Container();
            this.ltpNewDriver = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblConsName = new System.Windows.Forms.Label();
            this.lblArr = new System.Windows.Forms.Label();
            this.cmbCons = new System.Windows.Forms.ComboBox();
            this.edtFrom = new System.Windows.Forms.TextBox();
            this.edtMid = new System.Windows.Forms.TextBox();
            this.edtTo = new System.Windows.Forms.TextBox();
            this.picFrom = new System.Windows.Forms.PictureBox();
            this.picTo = new System.Windows.Forms.PictureBox();
            this.btnMoreMid = new System.Windows.Forms.Button();
            this.picMid = new System.Windows.Forms.PictureBox();
            this.edtDriverPhoneNumber = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbGRZ = new System.Windows.Forms.ComboBox();
            this.picGrz = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.edtConsCapacity = new System.Windows.Forms.TextBox();
            this.btnNewWaybill = new System.Windows.Forms.Button();
            this.cmbDriverName = new System.Windows.Forms.ComboBox();
            this.ttForOk = new System.Windows.Forms.ToolTip(this.components);
            this.ltpNewDriver.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGrz)).BeginInit();
            this.SuspendLayout();
            // 
            // ltpNewDriver
            // 
            this.ltpNewDriver.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial;
            this.ltpNewDriver.ColumnCount = 6;
            this.ltpNewDriver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.9058F));
            this.ltpNewDriver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.47039F));
            this.ltpNewDriver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.56899F));
            this.ltpNewDriver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.010456F));
            this.ltpNewDriver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.027883F));
            this.ltpNewDriver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.016474F));
            this.ltpNewDriver.Controls.Add(this.label1, 0, 2);
            this.ltpNewDriver.Controls.Add(this.lblStart, 0, 1);
            this.ltpNewDriver.Controls.Add(this.lblConsName, 0, 0);
            this.ltpNewDriver.Controls.Add(this.lblArr, 0, 3);
            this.ltpNewDriver.Controls.Add(this.cmbCons, 1, 0);
            this.ltpNewDriver.Controls.Add(this.edtFrom, 1, 1);
            this.ltpNewDriver.Controls.Add(this.edtMid, 1, 2);
            this.ltpNewDriver.Controls.Add(this.edtTo, 1, 3);
            this.ltpNewDriver.Controls.Add(this.picFrom, 2, 1);
            this.ltpNewDriver.Controls.Add(this.picTo, 2, 3);
            this.ltpNewDriver.Controls.Add(this.btnMoreMid, 3, 2);
            this.ltpNewDriver.Controls.Add(this.picMid, 2, 2);
            this.ltpNewDriver.Controls.Add(this.edtDriverPhoneNumber, 3, 4);
            this.ltpNewDriver.Controls.Add(this.label3, 0, 4);
            this.ltpNewDriver.Controls.Add(this.label2, 0, 6);
            this.ltpNewDriver.Controls.Add(this.dtpStart, 1, 6);
            this.ltpNewDriver.Controls.Add(this.label4, 0, 5);
            this.ltpNewDriver.Controls.Add(this.cmbGRZ, 1, 5);
            this.ltpNewDriver.Controls.Add(this.picGrz, 2, 5);
            this.ltpNewDriver.Controls.Add(this.label6, 2, 4);
            this.ltpNewDriver.Controls.Add(this.label5, 5, 0);
            this.ltpNewDriver.Controls.Add(this.edtConsCapacity, 4, 0);
            this.ltpNewDriver.Controls.Add(this.btnNewWaybill, 2, 6);
            this.ltpNewDriver.Controls.Add(this.cmbDriverName, 1, 4);
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
            this.ltpNewDriver.Size = new System.Drawing.Size(819, 247);
            this.ltpNewDriver.TabIndex = 5;
            this.ltpNewDriver.Leave += new System.EventHandler(this.OnFocusOut);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(101, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 19);
            this.label1.TabIndex = 13;
            this.label1.Text = "Промежуточный пункт:";
            // 
            // lblStart
            // 
            this.lblStart.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblStart.AutoSize = true;
            this.lblStart.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStart.Location = new System.Drawing.Point(114, 41);
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
            this.lblConsName.Location = new System.Drawing.Point(31, 8);
            this.lblConsName.Name = "lblConsName";
            this.lblConsName.Size = new System.Drawing.Size(239, 19);
            this.lblConsName.TabIndex = 0;
            this.lblConsName.Text = "* Наименование груза (вещества):";
            // 
            // lblArr
            // 
            this.lblArr.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblArr.AutoSize = true;
            this.lblArr.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblArr.Location = new System.Drawing.Point(133, 107);
            this.lblArr.Name = "lblArr";
            this.lblArr.Size = new System.Drawing.Size(137, 19);
            this.lblArr.TabIndex = 2;
            this.lblArr.Text = "* Пункт прибытия:";
            // 
            // cmbCons
            // 
            this.cmbCons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCons.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCons.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ltpNewDriver.SetColumnSpan(this.cmbCons, 2);
            this.cmbCons.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbCons.FormattingEnabled = true;
            this.cmbCons.Location = new System.Drawing.Point(279, 6);
            this.cmbCons.Name = "cmbCons";
            this.cmbCons.Size = new System.Drawing.Size(380, 27);
            this.cmbCons.TabIndex = 18;
            // 
            // edtFrom
            // 
            this.edtFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ltpNewDriver.SetColumnSpan(this.edtFrom, 2);
            this.edtFrom.Location = new System.Drawing.Point(279, 39);
            this.edtFrom.Name = "edtFrom";
            this.edtFrom.Size = new System.Drawing.Size(380, 26);
            this.edtFrom.TabIndex = 19;
            this.edtFrom.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.edtFrom.Leave += new System.EventHandler(this.OnFocusOut);
            // 
            // edtMid
            // 
            this.edtMid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ltpNewDriver.SetColumnSpan(this.edtMid, 2);
            this.edtMid.Location = new System.Drawing.Point(279, 72);
            this.edtMid.Name = "edtMid";
            this.edtMid.Size = new System.Drawing.Size(380, 26);
            this.edtMid.TabIndex = 20;
            this.edtMid.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.edtMid.Leave += new System.EventHandler(this.OnFocusOut);
            // 
            // edtTo
            // 
            this.edtTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ltpNewDriver.SetColumnSpan(this.edtTo, 2);
            this.edtTo.Location = new System.Drawing.Point(279, 105);
            this.edtTo.Name = "edtTo";
            this.edtTo.Size = new System.Drawing.Size(380, 26);
            this.edtTo.TabIndex = 21;
            this.edtTo.TextChanged += new System.EventHandler(this.OnTextChanged);
            this.edtTo.Leave += new System.EventHandler(this.OnFocusOut);
            // 
            // picFrom
            // 
            this.picFrom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.picFrom.Location = new System.Drawing.Point(668, 43);
            this.picFrom.Name = "picFrom";
            this.picFrom.Size = new System.Drawing.Size(16, 16);
            this.picFrom.TabIndex = 22;
            this.picFrom.TabStop = false;
            // 
            // picTo
            // 
            this.picTo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.picTo.Location = new System.Drawing.Point(668, 109);
            this.picTo.Name = "picTo";
            this.picTo.Size = new System.Drawing.Size(16, 16);
            this.picTo.TabIndex = 23;
            this.picTo.TabStop = false;
            // 
            // btnMoreMid
            // 
            this.btnMoreMid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ltpNewDriver.SetColumnSpan(this.btnMoreMid, 2);
            this.btnMoreMid.Enabled = false;
            this.btnMoreMid.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMoreMid.Location = new System.Drawing.Point(695, 72);
            this.btnMoreMid.Name = "btnMoreMid";
            this.btnMoreMid.Size = new System.Drawing.Size(118, 24);
            this.btnMoreMid.TabIndex = 15;
            this.btnMoreMid.Text = "Добавить";
            this.btnMoreMid.UseVisualStyleBackColor = true;
            this.btnMoreMid.Click += new System.EventHandler(this.AddMoreMidCities);
            // 
            // picMid
            // 
            this.picMid.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.picMid.Location = new System.Drawing.Point(668, 76);
            this.picMid.Name = "picMid";
            this.picMid.Size = new System.Drawing.Size(16, 16);
            this.picMid.TabIndex = 24;
            this.picMid.TabStop = false;
            // 
            // edtDriverPhoneNumber
            // 
            this.edtDriverPhoneNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ltpNewDriver.SetColumnSpan(this.edtDriverPhoneNumber, 3);
            this.edtDriverPhoneNumber.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edtDriverPhoneNumber.Location = new System.Drawing.Point(668, 138);
            this.edtDriverPhoneNumber.Mask = "(999) 000-0000";
            this.edtDriverPhoneNumber.Name = "edtDriverPhoneNumber";
            this.edtDriverPhoneNumber.Size = new System.Drawing.Size(145, 26);
            this.edtDriverPhoneNumber.TabIndex = 8;
            this.edtDriverPhoneNumber.Leave += new System.EventHandler(this.edtDriverPhoneNumber_Leave);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(152, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 19);
            this.label3.TabIndex = 25;
            this.label3.Text = "* Имя водителя:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(123, 213);
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
            this.dtpStart.Location = new System.Drawing.Point(279, 209);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(229, 26);
            this.dtpStart.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(136, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 19);
            this.label4.TabIndex = 27;
            this.label4.Text = "* ГРЗ автомобиля:";
            // 
            // cmbGRZ
            // 
            this.cmbGRZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGRZ.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbGRZ.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGRZ.FormattingEnabled = true;
            this.cmbGRZ.Location = new System.Drawing.Point(279, 172);
            this.cmbGRZ.Name = "cmbGRZ";
            this.cmbGRZ.Size = new System.Drawing.Size(229, 27);
            this.cmbGRZ.TabIndex = 28;
            this.cmbGRZ.SelectedIndexChanged += new System.EventHandler(this.cmbGRZ_SelectedIndexChanged);
            // 
            // picGrz
            // 
            this.picGrz.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.picGrz.Location = new System.Drawing.Point(517, 175);
            this.picGrz.Name = "picGrz";
            this.picGrz.Size = new System.Drawing.Size(16, 16);
            this.picGrz.TabIndex = 29;
            this.picGrz.TabStop = false;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(579, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 19);
            this.label6.TabIndex = 31;
            this.label6.Text = "* Телефон:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(762, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 19);
            this.label5.TabIndex = 30;
            this.label5.Text = "тонн";
            // 
            // edtConsCapacity
            // 
            this.edtConsCapacity.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.edtConsCapacity.Location = new System.Drawing.Point(702, 6);
            this.edtConsCapacity.Name = "edtConsCapacity";
            this.edtConsCapacity.Size = new System.Drawing.Size(51, 26);
            this.edtConsCapacity.TabIndex = 32;
            // 
            // btnNewWaybill
            // 
            this.btnNewWaybill.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ltpNewDriver.SetColumnSpan(this.btnNewWaybill, 4);
            this.btnNewWaybill.Enabled = false;
            this.btnNewWaybill.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewWaybill.Location = new System.Drawing.Point(636, 204);
            this.btnNewWaybill.Name = "btnNewWaybill";
            this.btnNewWaybill.Size = new System.Drawing.Size(177, 37);
            this.btnNewWaybill.TabIndex = 10;
            this.btnNewWaybill.Text = "Добавить перевозку";
            this.btnNewWaybill.UseVisualStyleBackColor = true;
            this.btnNewWaybill.Click += new System.EventHandler(this.AddNewWaybill);
            // 
            // cmbDriverName
            // 
            this.cmbDriverName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDriverName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDriverName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDriverName.FormattingEnabled = true;
            this.cmbDriverName.Location = new System.Drawing.Point(279, 138);
            this.cmbDriverName.Name = "cmbDriverName";
            this.cmbDriverName.Size = new System.Drawing.Size(229, 27);
            this.cmbDriverName.TabIndex = 33;
            // 
            // WaybillForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 247);
            this.Controls.Add(this.ltpNewDriver);
            this.Name = "WaybillForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Заполнить путевой лист";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.WaybillForm_Enter);
            this.ltpNewDriver.ResumeLayout(false);
            this.ltpNewDriver.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGrz)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel ltpNewDriver;
        private Label lblArr;
        private Label lblStart;
        private Label lblConsName;
        private MaskedTextBox edtDriverPhoneNumber;
        private Button btnNewWaybill;
        private Label label1;
        private Button btnMoreMid;
        private Label label2;
        private DateTimePicker dtpStart;
        private ComboBox cmbCons;
        private TextBox edtFrom;
        private TextBox edtMid;
        private TextBox edtTo;
        private PictureBox picFrom;
        private PictureBox picTo;
        private ToolTip ttForOk;
        private PictureBox picMid;
        private Label label3;
        private Label label4;
        private ComboBox cmbGRZ;
        private PictureBox picGrz;
        private Label label5;
        private Label label6;
        private TextBox edtConsCapacity;
        private ComboBox cmbDriverName;
    }
}