using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using GMap.NET.WindowsForms;

namespace BachelorLibAPI.Forms
{
    partial class MainForm
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
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.программаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.анализОпасностиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.новыйПутевойЛистToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ltMainOptions = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpPrecTime = new System.Windows.Forms.DateTimePicker();
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.mapMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.ltMapPanel = new System.Windows.Forms.TableLayoutPanel();
            this.edtCrashPlace = new System.Windows.Forms.TextBox();
            this.btnSetCrashPlace = new System.Windows.Forms.Button();
            this.picCheck = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.edtLat = new System.Windows.Forms.MaskedTextBox();
            this.edtLong = new System.Windows.Forms.MaskedTextBox();
            this.menuMain.SuspendLayout();
            this.ltMainOptions.SuspendLayout();
            this.mapMenu.SuspendLayout();
            this.ltMapPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.программаToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuMain.Size = new System.Drawing.Size(1108, 25);
            this.menuMain.TabIndex = 1;
            this.menuMain.Text = "Главное меню";
            // 
            // программаToolStripMenuItem
            // 
            this.программаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.анализОпасностиToolStripMenuItem,
            this.toolStripSeparator5,
            this.новыйПутевойЛистToolStripMenuItem});
            this.программаToolStripMenuItem.Name = "программаToolStripMenuItem";
            this.программаToolStripMenuItem.Size = new System.Drawing.Size(84, 19);
            this.программаToolStripMenuItem.Text = "&Программа";
            // 
            // анализОпасностиToolStripMenuItem
            // 
            this.анализОпасностиToolStripMenuItem.Name = "анализОпасностиToolStripMenuItem";
            this.анализОпасностиToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
            this.анализОпасностиToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.анализОпасностиToolStripMenuItem.Text = "Анализ опасности";
            this.анализОпасностиToolStripMenuItem.Click += new System.EventHandler(this.DangerAnalyseClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(224, 6);
            // 
            // новыйПутевойЛистToolStripMenuItem
            // 
            this.новыйПутевойЛистToolStripMenuItem.Name = "новыйПутевойЛистToolStripMenuItem";
            this.новыйПутевойЛистToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.новыйПутевойЛистToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.новыйПутевойЛистToolStripMenuItem.Text = "Новый путевой лист";
            this.новыйПутевойЛистToolStripMenuItem.Click += new System.EventHandler(this.NewWaybillClick);
            // 
            // ltMainOptions
            // 
            this.ltMainOptions.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ltMainOptions.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.ltMainOptions.ColumnCount = 22;
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.545455F));
            this.ltMainOptions.Controls.Add(this.label1, 0, 0);
            this.ltMainOptions.Controls.Add(this.dtpPrecTime, 2, 0);
            this.ltMainOptions.Controls.Add(this.btnSetCrashPlace, 18, 1);
            this.ltMainOptions.Controls.Add(this.picCheck, 17, 1);
            this.ltMainOptions.Controls.Add(this.label2, 0, 1);
            this.ltMainOptions.Controls.Add(this.edtCrashPlace, 2, 1);
            this.ltMainOptions.Controls.Add(this.edtLong, 19, 0);
            this.ltMainOptions.Controls.Add(this.label5, 17, 0);
            this.ltMainOptions.Controls.Add(this.label3, 8, 0);
            this.ltMainOptions.Controls.Add(this.label4, 12, 0);
            this.ltMainOptions.Controls.Add(this.edtLat, 14, 0);
            this.ltMainOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.ltMainOptions.Location = new System.Drawing.Point(0, 25);
            this.ltMainOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ltMainOptions.MaximumSize = new System.Drawing.Size(890, 76);
            this.ltMainOptions.MinimumSize = new System.Drawing.Size(890, 76);
            this.ltMainOptions.Name = "ltMainOptions";
            this.ltMainOptions.RowCount = 2;
            this.ltMainOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.ltMainOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
            this.ltMainOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ltMainOptions.Size = new System.Drawing.Size(890, 76);
            this.ltMainOptions.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.ltMainOptions.SetColumnSpan(this.label2, 2);
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(19, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Адрес: ";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.ltMainOptions.SetColumnSpan(this.label1, 3);
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Время аварии: ";
            // 
            // dtpPrecTime
            // 
            this.dtpPrecTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ltMainOptions.SetColumnSpan(this.dtpPrecTime, 4);
            this.dtpPrecTime.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpPrecTime.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpPrecTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPrecTime.Location = new System.Drawing.Point(127, 7);
            this.dtpPrecTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpPrecTime.Name = "dtpPrecTime";
            this.dtpPrecTime.Size = new System.Drawing.Size(149, 26);
            this.dtpPrecTime.TabIndex = 1;
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.ContextMenuStrip = this.mapMenu;
            this.gmap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemmory = 5;
            this.gmap.Location = new System.Drawing.Point(4, 4);
            this.gmap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 18;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(1100, 727);
            this.gmap.TabIndex = 13;
            this.gmap.Zoom = 0D;
            this.gmap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseDoubleClick);
            // 
            // mapMenu
            // 
            this.mapMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripSeparator1,
            this.toolStripMenuItem3,
            this.toolStripMenuItem5,
            this.toolStripMenuItem4});
            this.mapMenu.Name = "mapMenu";
            this.mapMenu.Size = new System.Drawing.Size(278, 98);
            this.mapMenu.Opening += new System.ComponentModel.CancelEventHandler(this.mapMenu_Opening);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(277, 22);
            this.toolStripMenuItem2.Text = "Добавить перевозку";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.NewWaybillClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(274, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(277, 22);
            this.toolStripMenuItem3.Text = "Отметить как пункт отправления";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.MarkStartPointClick);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(277, 22);
            this.toolStripMenuItem5.Text = "Отметить как промежуточную точку";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.MarkMiddlePointClick);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(277, 22);
            this.toolStripMenuItem4.Text = "Отметить как пункт назначения";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.MarkEndPointClick);
            // 
            // ltMapPanel
            // 
            this.ltMapPanel.ColumnCount = 1;
            this.ltMapPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ltMapPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.ltMapPanel.Controls.Add(this.gmap, 0, 0);
            this.ltMapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ltMapPanel.Location = new System.Drawing.Point(0, 25);
            this.ltMapPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ltMapPanel.Name = "ltMapPanel";
            this.ltMapPanel.RowCount = 1;
            this.ltMapPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ltMapPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 727F));
            this.ltMapPanel.Size = new System.Drawing.Size(1108, 735);
            this.ltMapPanel.TabIndex = 7;
            // 
            // edtCrashPlace
            // 
            this.edtCrashPlace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ltMainOptions.SetColumnSpan(this.edtCrashPlace, 16);
            this.edtCrashPlace.Location = new System.Drawing.Point(87, 43);
            this.edtCrashPlace.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.edtCrashPlace.Name = "edtCrashPlace";
            this.edtCrashPlace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.edtCrashPlace.Size = new System.Drawing.Size(629, 26);
            this.edtCrashPlace.TabIndex = 26;
            // 
            // btnSetCrashPlace
            // 
            this.btnSetCrashPlace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ltMainOptions.SetColumnSpan(this.btnSetCrashPlace, 3);
            this.btnSetCrashPlace.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSetCrashPlace.Location = new System.Drawing.Point(767, 43);
            this.btnSetCrashPlace.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSetCrashPlace.Name = "btnSetCrashPlace";
            this.btnSetCrashPlace.Size = new System.Drawing.Size(116, 26);
            this.btnSetCrashPlace.TabIndex = 25;
            this.btnSetCrashPlace.Text = "Установить";
            this.btnSetCrashPlace.UseVisualStyleBackColor = true;
            this.btnSetCrashPlace.Click += new System.EventHandler(this.btnSetCrashPlace_Click);
            // 
            // picCheck
            // 
            this.picCheck.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.picCheck.Location = new System.Drawing.Point(727, 44);
            this.picCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picCheck.Name = "picCheck";
            this.picCheck.Size = new System.Drawing.Size(24, 23);
            this.picCheck.TabIndex = 27;
            this.picCheck.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.ltMainOptions.SetColumnSpan(this.label3, 4);
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(327, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 19);
            this.label3.TabIndex = 28;
            this.label3.Text = "Координаты аварии: ";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.ltMainOptions.SetColumnSpan(this.label4, 2);
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(491, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 19);
            this.label4.TabIndex = 29;
            this.label4.Text = "широта";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.ltMainOptions.SetColumnSpan(this.label5, 2);
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(690, 10);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 19);
            this.label5.TabIndex = 30;
            this.label5.Text = "долгота";
            // 
            // edtLat
            // 
            this.edtLat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ltMainOptions.SetColumnSpan(this.edtLat, 3);
            this.edtLat.Location = new System.Drawing.Point(566, 6);
            this.edtLat.Mask = "000°00′00″?";
            this.edtLat.Name = "edtLat";
            this.edtLat.Size = new System.Drawing.Size(111, 26);
            this.edtLat.TabIndex = 31;
            // 
            // edtLong
            // 
            this.edtLong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ltMainOptions.SetColumnSpan(this.edtLong, 3);
            this.edtLong.Location = new System.Drawing.Point(766, 6);
            this.edtLong.Mask = "000°00′00″?";
            this.edtLong.Name = "edtLong";
            this.edtLong.Size = new System.Drawing.Size(118, 26);
            this.edtLong.TabIndex = 32;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 760);
            this.Controls.Add(this.ltMainOptions);
            this.Controls.Add(this.ltMapPanel);
            this.Controls.Add(this.menuMain);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Анализ опасности при ЧП";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ltMainOptions.ResumeLayout(false);
            this.ltMainOptions.PerformLayout();
            this.mapMenu.ResumeLayout(false);
            this.ltMapPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCheck)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuMain;
        private ToolStripMenuItem программаToolStripMenuItem;
        private ToolStripMenuItem новыйПутевойЛистToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem анализОпасностиToolStripMenuItem;
        private TableLayoutPanel ltMainOptions;
        private Label label1;
        private DateTimePicker dtpPrecTime;
        private GMapControl gmap;
        private TableLayoutPanel ltMapPanel;
        private ToolStripMenuItem toolStripMenuItem1;
        private ContextMenuStrip mapMenu;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolStripMenuItem5;
        private Label label2;
        private TextBox edtCrashPlace;
        private Button btnSetCrashPlace;
        private PictureBox picCheck;
        private MaskedTextBox edtLong;
        private Label label3;
        private Label label4;
        private MaskedTextBox edtLat;
        private Label label5;
    }
}