namespace BachelorLibAPI.Forms
{
    partial class ConsignmentAddForm
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
            this.ltpNewCons = new System.Windows.Forms.TableLayoutPanel();
            this.lblCrashActions = new System.Windows.Forms.Label();
            this.lblDangerDegree = new System.Windows.Forms.Label();
            this.lblConsName = new System.Windows.Forms.Label();
            this.edtDriverLastName = new System.Windows.Forms.TextBox();
            this.edtCrashActions = new System.Windows.Forms.TextBox();
            this.btnNewCons = new System.Windows.Forms.Button();
            this.cmbDangerDegrees = new System.Windows.Forms.ComboBox();
            this.ltpNewCons.SuspendLayout();
            this.SuspendLayout();
            // 
            // ltpNewCons
            // 
            this.ltpNewCons.ColumnCount = 2;
            this.ltpNewCons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.ltpNewCons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.ltpNewCons.Controls.Add(this.lblCrashActions, 0, 2);
            this.ltpNewCons.Controls.Add(this.lblDangerDegree, 0, 1);
            this.ltpNewCons.Controls.Add(this.lblConsName, 0, 0);
            this.ltpNewCons.Controls.Add(this.edtDriverLastName, 1, 0);
            this.ltpNewCons.Controls.Add(this.edtCrashActions, 1, 2);
            this.ltpNewCons.Controls.Add(this.btnNewCons, 1, 3);
            this.ltpNewCons.Controls.Add(this.cmbDangerDegrees, 1, 1);
            this.ltpNewCons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ltpNewCons.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ltpNewCons.Location = new System.Drawing.Point(0, 0);
            this.ltpNewCons.Name = "ltpNewCons";
            this.ltpNewCons.RowCount = 4;
            this.ltpNewCons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.ltpNewCons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.ltpNewCons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ltpNewCons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.ltpNewCons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ltpNewCons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ltpNewCons.Size = new System.Drawing.Size(554, 193);
            this.ltpNewCons.TabIndex = 5;
            // 
            // lblCrashActions
            // 
            this.lblCrashActions.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCrashActions.AutoSize = true;
            this.lblCrashActions.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCrashActions.Location = new System.Drawing.Point(75, 107);
            this.lblCrashActions.Name = "lblCrashActions";
            this.lblCrashActions.Size = new System.Drawing.Size(171, 19);
            this.lblCrashActions.TabIndex = 2;
            this.lblCrashActions.Text = "* Действия при аварии:";
            // 
            // lblDangerDegree
            // 
            this.lblDangerDegree.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDangerDegree.AutoSize = true;
            this.lblDangerDegree.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDangerDegree.Location = new System.Drawing.Point(107, 50);
            this.lblDangerDegree.Name = "lblDangerDegree";
            this.lblDangerDegree.Size = new System.Drawing.Size(139, 19);
            this.lblDangerDegree.TabIndex = 1;
            this.lblDangerDegree.Text = "* Класс опасности:";
            // 
            // lblConsName
            // 
            this.lblConsName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblConsName.AutoSize = true;
            this.lblConsName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblConsName.Location = new System.Drawing.Point(44, 10);
            this.lblConsName.Name = "lblConsName";
            this.lblConsName.Size = new System.Drawing.Size(202, 19);
            this.lblConsName.TabIndex = 0;
            this.lblConsName.Text = "* Название груза (вещества):";
            // 
            // edtDriverLastName
            // 
            this.edtDriverLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.edtDriverLastName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edtDriverLastName.Location = new System.Drawing.Point(252, 7);
            this.edtDriverLastName.Name = "edtDriverLastName";
            this.edtDriverLastName.Size = new System.Drawing.Size(299, 26);
            this.edtDriverLastName.TabIndex = 5;
            // 
            // edtCrashActions
            // 
            this.edtCrashActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edtCrashActions.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edtCrashActions.Location = new System.Drawing.Point(252, 83);
            this.edtCrashActions.Multiline = true;
            this.edtCrashActions.Name = "edtCrashActions";
            this.edtCrashActions.Size = new System.Drawing.Size(299, 67);
            this.edtCrashActions.TabIndex = 7;
            // 
            // btnNewCons
            // 
            this.btnNewCons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewCons.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewCons.Location = new System.Drawing.Point(405, 156);
            this.btnNewCons.Name = "btnNewCons";
            this.btnNewCons.Size = new System.Drawing.Size(146, 34);
            this.btnNewCons.TabIndex = 10;
            this.btnNewCons.Text = "Добавить";
            this.btnNewCons.UseVisualStyleBackColor = true;
            this.btnNewCons.Click += new System.EventHandler(this.AddNewConsignmentClick);
            // 
            // cmbDangerDegrees
            // 
            this.cmbDangerDegrees.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDangerDegrees.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDangerDegrees.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbDangerDegrees.FormattingEnabled = true;
            this.cmbDangerDegrees.Items.AddRange(new object[] {
            "1. Чрезвычайно опасные",
            "2. Высокоопасные",
            "3. Умеренно опасные",
            "4. Малоопасные"});
            this.cmbDangerDegrees.Location = new System.Drawing.Point(252, 46);
            this.cmbDangerDegrees.Name = "cmbDangerDegrees";
            this.cmbDangerDegrees.Size = new System.Drawing.Size(299, 27);
            this.cmbDangerDegrees.TabIndex = 11;
            // 
            // ConsignmentAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 193);
            this.Controls.Add(this.ltpNewCons);
            this.Name = "ConsignmentAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить новый груз";
            this.ltpNewCons.ResumeLayout(false);
            this.ltpNewCons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ltpNewCons;
        private System.Windows.Forms.Label lblCrashActions;
        private System.Windows.Forms.Label lblDangerDegree;
        private System.Windows.Forms.Label lblConsName;
        private System.Windows.Forms.TextBox edtDriverLastName;
        private System.Windows.Forms.TextBox edtCrashActions;
        private System.Windows.Forms.Button btnNewCons;
        private System.Windows.Forms.ComboBox cmbDangerDegrees;
    }
}