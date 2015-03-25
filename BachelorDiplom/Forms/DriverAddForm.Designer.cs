namespace BachelorLibAPI.Forms
{
    partial class DriverAddForm
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
            this.lblDriverMidName = new System.Windows.Forms.Label();
            this.lblTelephoneNumber = new System.Windows.Forms.Label();
            this.lblAddTN = new System.Windows.Forms.Label();
            this.lblDriverName = new System.Windows.Forms.Label();
            this.lblDriverLastName = new System.Windows.Forms.Label();
            this.edtDriverLastName = new System.Windows.Forms.TextBox();
            this.edtDriverName = new System.Windows.Forms.TextBox();
            this.edtDriverMidName = new System.Windows.Forms.TextBox();
            this.edtMainPhoneNumber = new System.Windows.Forms.MaskedTextBox();
            this.edtAddPhoneNumber = new System.Windows.Forms.MaskedTextBox();
            this.btnNewDriver = new System.Windows.Forms.Button();
            this.ltpNewDriver.SuspendLayout();
            this.SuspendLayout();
            // 
            // ltpNewDriver
            // 
            this.ltpNewDriver.ColumnCount = 2;
            this.ltpNewDriver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.ltpNewDriver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.ltpNewDriver.Controls.Add(this.lblDriverMidName, 0, 2);
            this.ltpNewDriver.Controls.Add(this.lblTelephoneNumber, 0, 3);
            this.ltpNewDriver.Controls.Add(this.lblAddTN, 0, 4);
            this.ltpNewDriver.Controls.Add(this.lblDriverName, 0, 1);
            this.ltpNewDriver.Controls.Add(this.lblDriverLastName, 0, 0);
            this.ltpNewDriver.Controls.Add(this.edtDriverLastName, 1, 0);
            this.ltpNewDriver.Controls.Add(this.edtDriverName, 1, 1);
            this.ltpNewDriver.Controls.Add(this.edtDriverMidName, 1, 2);
            this.ltpNewDriver.Controls.Add(this.edtMainPhoneNumber, 1, 3);
            this.ltpNewDriver.Controls.Add(this.edtAddPhoneNumber, 1, 4);
            this.ltpNewDriver.Controls.Add(this.btnNewDriver, 1, 5);
            this.ltpNewDriver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ltpNewDriver.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ltpNewDriver.Location = new System.Drawing.Point(0, 0);
            this.ltpNewDriver.Name = "ltpNewDriver";
            this.ltpNewDriver.RowCount = 6;
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.ltpNewDriver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.ltpNewDriver.Size = new System.Drawing.Size(505, 199);
            this.ltpNewDriver.TabIndex = 4;
            // 
            // lblDriverMidName
            // 
            this.lblDriverMidName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDriverMidName.AutoSize = true;
            this.lblDriverMidName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDriverMidName.Location = new System.Drawing.Point(148, 68);
            this.lblDriverMidName.Name = "lblDriverMidName";
            this.lblDriverMidName.Size = new System.Drawing.Size(76, 19);
            this.lblDriverMidName.TabIndex = 2;
            this.lblDriverMidName.Text = "Отчество:";
            // 
            // lblTelephoneNumber
            // 
            this.lblTelephoneNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTelephoneNumber.AutoSize = true;
            this.lblTelephoneNumber.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTelephoneNumber.Location = new System.Drawing.Point(90, 99);
            this.lblTelephoneNumber.Name = "lblTelephoneNumber";
            this.lblTelephoneNumber.Size = new System.Drawing.Size(134, 19);
            this.lblTelephoneNumber.TabIndex = 3;
            this.lblTelephoneNumber.Text = "* Номер телефона:";
            // 
            // lblAddTN
            // 
            this.lblAddTN.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAddTN.AutoSize = true;
            this.lblAddTN.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAddTN.Location = new System.Drawing.Point(68, 130);
            this.lblAddTN.Name = "lblAddTN";
            this.lblAddTN.Size = new System.Drawing.Size(156, 19);
            this.lblAddTN.TabIndex = 4;
            this.lblAddTN.Text = "Доп. номер телефона:";
            // 
            // lblDriverName
            // 
            this.lblDriverName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDriverName.AutoSize = true;
            this.lblDriverName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDriverName.Location = new System.Drawing.Point(172, 37);
            this.lblDriverName.Name = "lblDriverName";
            this.lblDriverName.Size = new System.Drawing.Size(52, 19);
            this.lblDriverName.TabIndex = 1;
            this.lblDriverName.Text = "* Имя:";
            // 
            // lblDriverLastName
            // 
            this.lblDriverLastName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDriverLastName.AutoSize = true;
            this.lblDriverLastName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDriverLastName.Location = new System.Drawing.Point(137, 6);
            this.lblDriverLastName.Name = "lblDriverLastName";
            this.lblDriverLastName.Size = new System.Drawing.Size(87, 19);
            this.lblDriverLastName.TabIndex = 0;
            this.lblDriverLastName.Text = "* Фамилия:";
            // 
            // edtDriverLastName
            // 
            this.edtDriverLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.edtDriverLastName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edtDriverLastName.Location = new System.Drawing.Point(230, 3);
            this.edtDriverLastName.Name = "edtDriverLastName";
            this.edtDriverLastName.Size = new System.Drawing.Size(272, 26);
            this.edtDriverLastName.TabIndex = 5;
            // 
            // edtDriverName
            // 
            this.edtDriverName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.edtDriverName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edtDriverName.Location = new System.Drawing.Point(230, 34);
            this.edtDriverName.Name = "edtDriverName";
            this.edtDriverName.Size = new System.Drawing.Size(272, 26);
            this.edtDriverName.TabIndex = 6;
            // 
            // edtDriverMidName
            // 
            this.edtDriverMidName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.edtDriverMidName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edtDriverMidName.Location = new System.Drawing.Point(230, 65);
            this.edtDriverMidName.Name = "edtDriverMidName";
            this.edtDriverMidName.Size = new System.Drawing.Size(272, 26);
            this.edtDriverMidName.TabIndex = 7;
            // 
            // edtMainPhoneNumber
            // 
            this.edtMainPhoneNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.edtMainPhoneNumber.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edtMainPhoneNumber.Location = new System.Drawing.Point(230, 96);
            this.edtMainPhoneNumber.Mask = "(999) 000-0000";
            this.edtMainPhoneNumber.Name = "edtMainPhoneNumber";
            this.edtMainPhoneNumber.Size = new System.Drawing.Size(136, 26);
            this.edtMainPhoneNumber.TabIndex = 8;
            // 
            // edtAddPhoneNumber
            // 
            this.edtAddPhoneNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.edtAddPhoneNumber.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edtAddPhoneNumber.Location = new System.Drawing.Point(230, 127);
            this.edtAddPhoneNumber.Mask = "(999) 000-0000";
            this.edtAddPhoneNumber.Name = "edtAddPhoneNumber";
            this.edtAddPhoneNumber.Size = new System.Drawing.Size(136, 26);
            this.edtAddPhoneNumber.TabIndex = 9;
            // 
            // btnNewDriver
            // 
            this.btnNewDriver.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnNewDriver.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewDriver.Location = new System.Drawing.Point(356, 159);
            this.btnNewDriver.Name = "btnNewDriver";
            this.btnNewDriver.Size = new System.Drawing.Size(146, 35);
            this.btnNewDriver.TabIndex = 10;
            this.btnNewDriver.Text = "Добавить";
            this.btnNewDriver.UseVisualStyleBackColor = true;
            this.btnNewDriver.Click += new System.EventHandler(this.AddDriverBtnClick);
            // 
            // DriverAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 199);
            this.Controls.Add(this.ltpNewDriver);
            this.Name = "DriverAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить нового водителя";
            this.ltpNewDriver.ResumeLayout(false);
            this.ltpNewDriver.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ltpNewDriver;
        private System.Windows.Forms.Label lblDriverMidName;
        private System.Windows.Forms.Label lblTelephoneNumber;
        private System.Windows.Forms.Label lblAddTN;
        private System.Windows.Forms.Label lblDriverName;
        private System.Windows.Forms.Label lblDriverLastName;
        private System.Windows.Forms.TextBox edtDriverLastName;
        private System.Windows.Forms.TextBox edtDriverName;
        private System.Windows.Forms.TextBox edtDriverMidName;
        private System.Windows.Forms.MaskedTextBox edtMainPhoneNumber;
        private System.Windows.Forms.MaskedTextBox edtAddPhoneNumber;
        private System.Windows.Forms.Button btnNewDriver;
    }
}