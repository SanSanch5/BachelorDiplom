namespace BachelorLibAPI.Forms
{
    partial class EndingStatusForm
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
            this.ltpNewDriver = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpArr = new System.Windows.Forms.DateTimePicker();
            this.lblTelephoneNumber = new System.Windows.Forms.Label();
            this.edtDriverPhoneNumber = new System.Windows.Forms.MaskedTextBox();
            this.btnSetEndingStatus = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.ltpNewDriver.SuspendLayout();
            this.SuspendLayout();
            // 
            // ltpNewDriver
            // 
            this.ltpNewDriver.ColumnCount = 2;
            this.ltpNewDriver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.25F));
            this.ltpNewDriver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.75F));
            this.ltpNewDriver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ltpNewDriver.Controls.Add(this.label1, 0, 2);
            this.ltpNewDriver.Controls.Add(this.dtpArr, 1, 2);
            this.ltpNewDriver.Controls.Add(this.lblTelephoneNumber, 0, 0);
            this.ltpNewDriver.Controls.Add(this.edtDriverPhoneNumber, 1, 0);
            this.ltpNewDriver.Controls.Add(this.btnSetEndingStatus, 1, 3);
            this.ltpNewDriver.Controls.Add(this.label2, 0, 1);
            this.ltpNewDriver.Controls.Add(this.dtpStart, 1, 1);
            this.ltpNewDriver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ltpNewDriver.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ltpNewDriver.Location = new System.Drawing.Point(0, 0);
            this.ltpNewDriver.Name = "ltpNewDriver";
            this.ltpNewDriver.RowCount = 4;
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ltpNewDriver.Size = new System.Drawing.Size(463, 160);
            this.ltpNewDriver.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(129, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 19);
            this.label1.TabIndex = 19;
            this.label1.Text = "* Дата прибытия:";
            // 
            // dtpArr
            // 
            this.dtpArr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpArr.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpArr.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpArr.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpArr.Location = new System.Drawing.Point(263, 87);
            this.dtpArr.Name = "dtpArr";
            this.dtpArr.Size = new System.Drawing.Size(197, 26);
            this.dtpArr.TabIndex = 20;
            // 
            // lblTelephoneNumber
            // 
            this.lblTelephoneNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTelephoneNumber.AutoSize = true;
            this.lblTelephoneNumber.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTelephoneNumber.Location = new System.Drawing.Point(111, 10);
            this.lblTelephoneNumber.Name = "lblTelephoneNumber";
            this.lblTelephoneNumber.Size = new System.Drawing.Size(146, 19);
            this.lblTelephoneNumber.TabIndex = 3;
            this.lblTelephoneNumber.Text = "* Телефон водителя:";
            // 
            // edtDriverPhoneNumber
            // 
            this.edtDriverPhoneNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.edtDriverPhoneNumber.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edtDriverPhoneNumber.Location = new System.Drawing.Point(263, 7);
            this.edtDriverPhoneNumber.Mask = "(999) 000-0000";
            this.edtDriverPhoneNumber.Name = "edtDriverPhoneNumber";
            this.edtDriverPhoneNumber.Size = new System.Drawing.Size(136, 26);
            this.edtDriverPhoneNumber.TabIndex = 8;
            // 
            // btnSetEndingStatus
            // 
            this.btnSetEndingStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetEndingStatus.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSetEndingStatus.Location = new System.Drawing.Point(324, 123);
            this.btnSetEndingStatus.Name = "btnSetEndingStatus";
            this.btnSetEndingStatus.Size = new System.Drawing.Size(136, 34);
            this.btnSetEndingStatus.TabIndex = 18;
            this.btnSetEndingStatus.Text = "Установить";
            this.btnSetEndingStatus.UseVisualStyleBackColor = true;
            this.btnSetEndingStatus.Click += new System.EventHandler(this.SetEndingStatusClick);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(110, 50);
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
            this.dtpStart.Location = new System.Drawing.Point(263, 47);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(197, 26);
            this.dtpStart.TabIndex = 17;
            // 
            // EndingStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 160);
            this.Controls.Add(this.ltpNewDriver);
            this.Name = "EndingStatusForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Установить статус завершения для перевозки";
            this.ltpNewDriver.ResumeLayout(false);
            this.ltpNewDriver.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ltpNewDriver;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label lblTelephoneNumber;
        private System.Windows.Forms.MaskedTextBox edtDriverPhoneNumber;
        private System.Windows.Forms.Button btnSetEndingStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpArr;

    }
}