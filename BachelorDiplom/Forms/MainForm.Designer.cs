namespace BachelorLibAPI.Forms
{
    partial class MainForm
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
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.программаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.анализОпасностиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.новыйПутевойЛистToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.установитьСтатусЗавершенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.картаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьВнутреннююКартупоУмолчаниюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.базаДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.водителяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.грузToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.водителяToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.завершённыеПеревозкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.перевозкиРанееToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.найтиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.информациюОВодителеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.loadTestData = new System.Windows.Forms.ToolStripMenuItem();
            this.ltMainOptions = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.chbTimeInterval = new System.Windows.Forms.CheckBox();
            this.lblSince = new System.Windows.Forms.Label();
            this.lblUntil = new System.Windows.Forms.Label();
            this.dtpSince = new System.Windows.Forms.DateTimePicker();
            this.dtpUntil = new System.Windows.Forms.DateTimePicker();
            this.dtpPrecTime = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtRegion = new System.Windows.Forms.RadioButton();
            this.rbtCity = new System.Windows.Forms.RadioButton();
            this.cmbCrashPlace = new System.Windows.Forms.ComboBox();
            this.btnAnalyse = new System.Windows.Forms.Button();
            this.pbAnalyse = new System.Windows.Forms.ProgressBar();
            this.ltMapPanel = new System.Windows.Forms.TableLayoutPanel();
            this.sharpMap = new SharpMap.Forms.MapBox();
            this.menuMain.SuspendLayout();
            this.ltMainOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.ltMapPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.программаToolStripMenuItem,
            this.картаToolStripMenuItem,
            this.базаДанныхToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(914, 24);
            this.menuMain.TabIndex = 1;
            this.menuMain.Text = "Главное меню";
            // 
            // программаToolStripMenuItem
            // 
            this.программаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.анализОпасностиToolStripMenuItem,
            this.toolStripSeparator5,
            this.новыйПутевойЛистToolStripMenuItem,
            this.установитьСтатусЗавершенияToolStripMenuItem});
            this.программаToolStripMenuItem.Name = "программаToolStripMenuItem";
            this.программаToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.программаToolStripMenuItem.Text = "&Программа";
            // 
            // анализОпасностиToolStripMenuItem
            // 
            this.анализОпасностиToolStripMenuItem.Name = "анализОпасностиToolStripMenuItem";
            this.анализОпасностиToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
            this.анализОпасностиToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.анализОпасностиToolStripMenuItem.Text = "Анализ опасности";
            this.анализОпасностиToolStripMenuItem.Click += new System.EventHandler(this.DangerAnalyseClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(280, 6);
            // 
            // новыйПутевойЛистToolStripMenuItem
            // 
            this.новыйПутевойЛистToolStripMenuItem.Name = "новыйПутевойЛистToolStripMenuItem";
            this.новыйПутевойЛистToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.новыйПутевойЛистToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.новыйПутевойЛистToolStripMenuItem.Text = "Новый путевой лист";
            this.новыйПутевойЛистToolStripMenuItem.Click += new System.EventHandler(this.NewWaybillClick);
            // 
            // установитьСтатусЗавершенияToolStripMenuItem
            // 
            this.установитьСтатусЗавершенияToolStripMenuItem.Name = "установитьСтатусЗавершенияToolStripMenuItem";
            this.установитьСтатусЗавершенияToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.установитьСтатусЗавершенияToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.установитьСтатусЗавершенияToolStripMenuItem.Text = "Установить статус завершения";
            this.установитьСтатусЗавершенияToolStripMenuItem.Click += new System.EventHandler(this.EndingStatusClick);
            // 
            // картаToolStripMenuItem
            // 
            this.картаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьВнутреннююКартупоУмолчаниюToolStripMenuItem,
            this.видToolStripMenuItem});
            this.картаToolStripMenuItem.Name = "картаToolStripMenuItem";
            this.картаToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.картаToolStripMenuItem.Text = "&Карта";
            // 
            // загрузитьВнутреннююКартупоУмолчаниюToolStripMenuItem
            // 
            this.загрузитьВнутреннююКартупоУмолчаниюToolStripMenuItem.Name = "загрузитьВнутреннююКартупоУмолчаниюToolStripMenuItem";
            this.загрузитьВнутреннююКартупоУмолчаниюToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.загрузитьВнутреннююКартупоУмолчаниюToolStripMenuItem.Size = new System.Drawing.Size(374, 22);
            this.загрузитьВнутреннююКартупоУмолчаниюToolStripMenuItem.Text = "Загрузить внутреннюю карту (по умолчанию)";
            this.загрузитьВнутреннююКартупоУмолчаниюToolStripMenuItem.Click += new System.EventHandler(this.LoadBuildInMapClick);
            // 
            // видToolStripMenuItem
            // 
            this.видToolStripMenuItem.Name = "видToolStripMenuItem";
            this.видToolStripMenuItem.Size = new System.Drawing.Size(374, 22);
            this.видToolStripMenuItem.Text = "Вид";
            // 
            // базаДанныхToolStripMenuItem
            // 
            this.базаДанныхToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.удалитьToolStripMenuItem,
            this.toolStripSeparator8,
            this.найтиToolStripMenuItem,
            this.toolStripSeparator3,
            this.loadTestData});
            this.базаДанныхToolStripMenuItem.Name = "базаДанныхToolStripMenuItem";
            this.базаДанныхToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.базаДанныхToolStripMenuItem.Text = "&База данных";
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.водителяToolStripMenuItem,
            this.грузToolStripMenuItem,
            this.toolStripSeparator2});
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.добавитьToolStripMenuItem.Text = "Добавить";
            // 
            // водителяToolStripMenuItem
            // 
            this.водителяToolStripMenuItem.Name = "водителяToolStripMenuItem";
            this.водителяToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.водителяToolStripMenuItem.Text = "Водителя";
            this.водителяToolStripMenuItem.Click += new System.EventHandler(this.AddDriverClick);
            // 
            // грузToolStripMenuItem
            // 
            this.грузToolStripMenuItem.Name = "грузToolStripMenuItem";
            this.грузToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.грузToolStripMenuItem.Text = "Груз";
            this.грузToolStripMenuItem.Click += new System.EventHandler(this.AddConsignmentClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(122, 6);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.водителяToolStripMenuItem1,
            this.toolStripSeparator7,
            this.завершённыеПеревозкиToolStripMenuItem,
            this.перевозкиРанееToolStripMenuItem});
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            // 
            // водителяToolStripMenuItem1
            // 
            this.водителяToolStripMenuItem1.Name = "водителяToolStripMenuItem1";
            this.водителяToolStripMenuItem1.Size = new System.Drawing.Size(212, 22);
            this.водителяToolStripMenuItem1.Text = "Водителя";
            this.водителяToolStripMenuItem1.Click += new System.EventHandler(this.DelDriverClick);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(209, 6);
            // 
            // завершённыеПеревозкиToolStripMenuItem
            // 
            this.завершённыеПеревозкиToolStripMenuItem.Name = "завершённыеПеревозкиToolStripMenuItem";
            this.завершённыеПеревозкиToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.завершённыеПеревозкиToolStripMenuItem.Text = "Завершённые перевозки";
            this.завершённыеПеревозкиToolStripMenuItem.Click += new System.EventHandler(this.DelEndedTransits);
            // 
            // перевозкиРанееToolStripMenuItem
            // 
            this.перевозкиРанееToolStripMenuItem.Name = "перевозкиРанееToolStripMenuItem";
            this.перевозкиРанееToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.перевозкиРанееToolStripMenuItem.Text = "Перевозки ранее...";
            this.перевозкиРанееToolStripMenuItem.Click += new System.EventHandler(this.DelTransitsBefore);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(222, 6);
            // 
            // найтиToolStripMenuItem
            // 
            this.найтиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.информациюОВодителеToolStripMenuItem});
            this.найтиToolStripMenuItem.Name = "найтиToolStripMenuItem";
            this.найтиToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.найтиToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.найтиToolStripMenuItem.Text = "Найти";
            // 
            // информациюОВодителеToolStripMenuItem
            // 
            this.информациюОВодителеToolStripMenuItem.Name = "информациюОВодителеToolStripMenuItem";
            this.информациюОВодителеToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.информациюОВодителеToolStripMenuItem.Text = "Информацию о водителе";
            this.информациюОВодителеToolStripMenuItem.Click += new System.EventHandler(this.FindDriverInfoClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(222, 6);
            // 
            // loadTestData
            // 
            this.loadTestData.Name = "loadTestData";
            this.loadTestData.Size = new System.Drawing.Size(225, 22);
            this.loadTestData.Text = "Загрузить тестовые данные";
            this.loadTestData.Click += new System.EventHandler(this.LoadTestDataClick);
            // 
            // ltMainOptions
            // 
            this.ltMainOptions.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ltMainOptions.ColumnCount = 6;
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.90801F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.85019F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.412991F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.20791F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.412991F));
            this.ltMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.20791F));
            this.ltMainOptions.Controls.Add(this.label1, 0, 0);
            this.ltMainOptions.Controls.Add(this.chbTimeInterval, 0, 1);
            this.ltMainOptions.Controls.Add(this.lblSince, 1, 1);
            this.ltMainOptions.Controls.Add(this.lblUntil, 3, 1);
            this.ltMainOptions.Controls.Add(this.dtpSince, 2, 1);
            this.ltMainOptions.Controls.Add(this.dtpUntil, 4, 1);
            this.ltMainOptions.Controls.Add(this.dtpPrecTime, 3, 0);
            this.ltMainOptions.Controls.Add(this.groupBox1, 1, 2);
            this.ltMainOptions.Controls.Add(this.cmbCrashPlace, 2, 2);
            this.ltMainOptions.Controls.Add(this.btnAnalyse, 5, 3);
            this.ltMainOptions.Controls.Add(this.pbAnalyse, 5, 2);
            this.ltMainOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.ltMainOptions.Location = new System.Drawing.Point(0, 24);
            this.ltMainOptions.MaximumSize = new System.Drawing.Size(755, 151);
            this.ltMainOptions.MinimumSize = new System.Drawing.Size(755, 151);
            this.ltMainOptions.Name = "ltMainOptions";
            this.ltMainOptions.RowCount = 4;
            this.ltMainOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00001F));
            this.ltMainOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ltMainOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ltMainOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ltMainOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ltMainOptions.Size = new System.Drawing.Size(755, 151);
            this.ltMainOptions.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.ltMainOptions.SetColumnSpan(this.label1, 2);
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(178, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Время аварии: ";
            // 
            // chbTimeInterval
            // 
            this.chbTimeInterval.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chbTimeInterval.AutoSize = true;
            this.ltMainOptions.SetColumnSpan(this.chbTimeInterval, 2);
            this.chbTimeInterval.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chbTimeInterval.Location = new System.Drawing.Point(55, 44);
            this.chbTimeInterval.Name = "chbTimeInterval";
            this.chbTimeInterval.Size = new System.Drawing.Size(233, 23);
            this.chbTimeInterval.TabIndex = 2;
            this.chbTimeInterval.Text = "Промежуток времени аварии: ";
            this.chbTimeInterval.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chbTimeInterval.UseVisualStyleBackColor = true;
            this.chbTimeInterval.CheckedChanged += new System.EventHandler(this.chbTimeInterval_CheckedChanged);
            // 
            // lblSince
            // 
            this.lblSince.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSince.AutoSize = true;
            this.lblSince.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSince.Location = new System.Drawing.Point(323, 46);
            this.lblSince.Name = "lblSince";
            this.lblSince.Size = new System.Drawing.Size(20, 19);
            this.lblSince.TabIndex = 3;
            this.lblSince.Text = "с ";
            this.lblSince.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUntil
            // 
            this.lblUntil.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUntil.AutoSize = true;
            this.lblUntil.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblUntil.Location = new System.Drawing.Point(533, 46);
            this.lblUntil.Name = "lblUntil";
            this.lblUntil.Size = new System.Drawing.Size(30, 19);
            this.lblUntil.TabIndex = 4;
            this.lblUntil.Text = "по ";
            // 
            // dtpSince
            // 
            this.dtpSince.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpSince.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpSince.Enabled = false;
            this.dtpSince.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSince.Location = new System.Drawing.Point(349, 42);
            this.dtpSince.Name = "dtpSince";
            this.dtpSince.Size = new System.Drawing.Size(169, 26);
            this.dtpSince.TabIndex = 5;
            // 
            // dtpUntil
            // 
            this.dtpUntil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpUntil.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpUntil.Enabled = false;
            this.dtpUntil.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpUntil.Location = new System.Drawing.Point(579, 42);
            this.dtpUntil.Name = "dtpUntil";
            this.dtpUntil.Size = new System.Drawing.Size(173, 26);
            this.dtpUntil.TabIndex = 6;
            // 
            // dtpPrecTime
            // 
            this.dtpPrecTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpPrecTime.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpPrecTime.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpPrecTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPrecTime.Location = new System.Drawing.Point(349, 5);
            this.dtpPrecTime.Name = "dtpPrecTime";
            this.dtpPrecTime.Size = new System.Drawing.Size(169, 26);
            this.dtpPrecTime.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.groupBox1.Controls.Add(this.rbtRegion);
            this.groupBox1.Controls.Add(this.rbtCity);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(146, 77);
            this.groupBox1.Name = "groupBox1";
            this.ltMainOptions.SetRowSpan(this.groupBox1, 2);
            this.groupBox1.Size = new System.Drawing.Size(142, 71);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Место аварии";
            // 
            // rbtRegion
            // 
            this.rbtRegion.AutoSize = true;
            this.rbtRegion.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbtRegion.Location = new System.Drawing.Point(31, 47);
            this.rbtRegion.Name = "rbtRegion";
            this.rbtRegion.Size = new System.Drawing.Size(75, 23);
            this.rbtRegion.TabIndex = 1;
            this.rbtRegion.Text = "Регион";
            this.rbtRegion.UseVisualStyleBackColor = true;
            this.rbtRegion.CheckedChanged += new System.EventHandler(this.rbtRegion_Click);
            // 
            // rbtCity
            // 
            this.rbtCity.AutoSize = true;
            this.rbtCity.Checked = true;
            this.rbtCity.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbtCity.Location = new System.Drawing.Point(31, 22);
            this.rbtCity.Name = "rbtCity";
            this.rbtCity.Size = new System.Drawing.Size(67, 23);
            this.rbtCity.TabIndex = 0;
            this.rbtCity.TabStop = true;
            this.rbtCity.Text = "Город";
            this.rbtCity.UseVisualStyleBackColor = true;
            this.rbtCity.CheckedChanged += new System.EventHandler(this.rbtCity_Click);
            // 
            // cmbCrashPlace
            // 
            this.cmbCrashPlace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCrashPlace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCrashPlace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ltMainOptions.SetColumnSpan(this.cmbCrashPlace, 2);
            this.cmbCrashPlace.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbCrashPlace.FormattingEnabled = true;
            this.cmbCrashPlace.Location = new System.Drawing.Point(294, 99);
            this.cmbCrashPlace.Name = "cmbCrashPlace";
            this.ltMainOptions.SetRowSpan(this.cmbCrashPlace, 2);
            this.cmbCrashPlace.Size = new System.Drawing.Size(224, 27);
            this.cmbCrashPlace.TabIndex = 8;
            // 
            // btnAnalyse
            // 
            this.btnAnalyse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalyse.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAnalyse.Location = new System.Drawing.Point(579, 114);
            this.btnAnalyse.Name = "btnAnalyse";
            this.btnAnalyse.Size = new System.Drawing.Size(173, 34);
            this.btnAnalyse.TabIndex = 10;
            this.btnAnalyse.Text = "Анализ";
            this.btnAnalyse.UseVisualStyleBackColor = true;
            this.btnAnalyse.Click += new System.EventHandler(this.DangerAnalyseClick);
            // 
            // pbAnalyse
            // 
            this.pbAnalyse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbAnalyse.Location = new System.Drawing.Point(579, 77);
            this.pbAnalyse.Name = "pbAnalyse";
            this.pbAnalyse.Size = new System.Drawing.Size(173, 31);
            this.pbAnalyse.TabIndex = 11;
            this.pbAnalyse.Visible = false;
            // 
            // ltMapPanel
            // 
            this.ltMapPanel.ColumnCount = 1;
            this.ltMapPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ltMapPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ltMapPanel.Controls.Add(this.sharpMap, 0, 0);
            this.ltMapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ltMapPanel.Location = new System.Drawing.Point(0, 24);
            this.ltMapPanel.Name = "ltMapPanel";
            this.ltMapPanel.RowCount = 1;
            this.ltMapPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ltMapPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 391F));
            this.ltMapPanel.Size = new System.Drawing.Size(914, 496);
            this.ltMapPanel.TabIndex = 7;
            // 
            // sharpMap
            // 
            this.sharpMap.ActiveTool = SharpMap.Forms.MapBox.Tools.None;
            this.sharpMap.BackColor = System.Drawing.Color.White;
            this.sharpMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sharpMap.FineZoomFactor = 10D;
            this.sharpMap.Location = new System.Drawing.Point(3, 3);
            this.sharpMap.MapQueryMode = SharpMap.Forms.MapBox.MapQueryType.LayerByIndex;
            this.sharpMap.Name = "sharpMap";
            this.sharpMap.QueryGrowFactor = 5F;
            this.sharpMap.QueryLayerIndex = 0;
            this.sharpMap.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.sharpMap.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.sharpMap.ShowProgressUpdate = false;
            this.sharpMap.Size = new System.Drawing.Size(908, 490);
            this.sharpMap.TabIndex = 0;
            this.sharpMap.Text = "mapBox1";
            this.sharpMap.WheelZoomMagnitude = -2D;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 520);
            this.Controls.Add(this.ltMainOptions);
            this.Controls.Add(this.ltMapPanel);
            this.Controls.Add(this.menuMain);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Анализ опасности при ЧП";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.fMain_Activated);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ltMainOptions.ResumeLayout(false);
            this.ltMainOptions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ltMapPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem программаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новыйПутевойЛистToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem установитьСтатусЗавершенияToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem анализОпасностиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem картаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьВнутреннююКартупоУмолчаниюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem базаДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem водителяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem грузToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem водителяToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem найтиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem информациюОВодителеToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem loadTestData;
        private System.Windows.Forms.ToolStripMenuItem перевозкиРанееToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem завершённыеПеревозкиToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel ltMainOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chbTimeInterval;
        private System.Windows.Forms.Label lblSince;
        private System.Windows.Forms.Label lblUntil;
        private System.Windows.Forms.DateTimePicker dtpSince;
        private System.Windows.Forms.DateTimePicker dtpUntil;
        private System.Windows.Forms.DateTimePicker dtpPrecTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtRegion;
        private System.Windows.Forms.RadioButton rbtCity;
        private System.Windows.Forms.ComboBox cmbCrashPlace;
        private System.Windows.Forms.Button btnAnalyse;
        private System.Windows.Forms.ProgressBar pbAnalyse;
        private System.Windows.Forms.TableLayoutPanel ltMapPanel;
        private System.Windows.Forms.ToolStripMenuItem видToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private SharpMap.Forms.MapBox sharpMap;
    }
}