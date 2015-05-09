using System.ComponentModel;
using System.Windows.Forms;

namespace BachelorLibAPI.TestsGenerator
{
    partial class TestsGeneratorForm
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
            this.btnGenAndAdd = new System.Windows.Forms.Button();
            this.pbTransits = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnGenAndAdd
            // 
            this.btnGenAndAdd.Location = new System.Drawing.Point(124, 12);
            this.btnGenAndAdd.Name = "btnGenAndAdd";
            this.btnGenAndAdd.Size = new System.Drawing.Size(273, 70);
            this.btnGenAndAdd.TabIndex = 0;
            this.btnGenAndAdd.Text = "Сгенерировать и добавить";
            this.btnGenAndAdd.UseVisualStyleBackColor = true;
            this.btnGenAndAdd.Click += new System.EventHandler(this.GenAndAddClick);
            // 
            // pbTransits
            // 
            this.pbTransits.Location = new System.Drawing.Point(124, 110);
            this.pbTransits.Name = "pbTransits";
            this.pbTransits.Size = new System.Drawing.Size(273, 38);
            this.pbTransits.TabIndex = 1;
            // 
            // TestsGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 178);
            this.Controls.Add(this.pbTransits);
            this.Controls.Add(this.btnGenAndAdd);
            this.Name = "TestsGeneratorForm";
            this.Text = "Сгенерировать и добавить тестовые данные";
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnGenAndAdd;
        private ProgressBar pbTransits;
    }
}