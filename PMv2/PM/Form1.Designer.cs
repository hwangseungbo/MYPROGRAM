namespace PM
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnLog = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSPD = new System.Windows.Forms.Button();
            this.btnSPR = new System.Windows.Forms.Button();
            this.tboxPath = new System.Windows.Forms.TextBox();
            this.tboxPeriod = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblProcNum = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnRevise = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.PMTab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.Dlabel = new System.Windows.Forms.Label();
            this.Clabel = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.progressBarD = new System.Windows.Forms.ProgressBar();
            this.progressBarC = new System.Windows.Forms.ProgressBar();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dmStopbtn = new System.Windows.Forms.Button();
            this.dmStartbtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.Daylbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Percentlbl = new System.Windows.Forms.Label();
            this.ScrollBar2 = new System.Windows.Forms.HScrollBar();
            this.ScrollBar = new System.Windows.Forms.HScrollBar();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.Percentlbl2 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.PMTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(75)))), ((int)(((byte)(105)))));
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1060, 31);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(1025, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(35, 31);
            this.btnExit.TabIndex = 14;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Constantia", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.AliceBlue;
            this.label1.Location = new System.Drawing.Point(22, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "PDM";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(5, 7);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(19, 21);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(77, 31);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.AliceBlue;
            this.label2.Location = new System.Drawing.Point(174, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "실행파일";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.AliceBlue;
            this.label3.Location = new System.Drawing.Point(204, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "주기";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
            this.panel2.Controls.Add(this.btnLog);
            this.panel2.Controls.Add(this.btnStart);
            this.panel2.Controls.Add(this.btnOpen);
            this.panel2.Controls.Add(this.btnSPD);
            this.panel2.Controls.Add(this.btnSPR);
            this.panel2.Location = new System.Drawing.Point(-3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(157, 539);
            this.panel2.TabIndex = 4;
            // 
            // btnLog
            // 
            this.btnLog.FlatAppearance.BorderSize = 0;
            this.btnLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLog.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLog.Image = global::PM.Properties.Resources.iconmonstr_school_15_48__1_;
            this.btnLog.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLog.Location = new System.Drawing.Point(0, 405);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(156, 82);
            this.btnLog.TabIndex = 15;
            this.btnLog.Text = "로그기록 확인";
            this.btnLog.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnStart
            // 
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Image = global::PM.Properties.Resources.iconmonstr_media_control_48_48__1_;
            this.btnStart.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStart.Location = new System.Drawing.Point(0, 229);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(156, 82);
            this.btnStart.TabIndex = 14;
            this.btnStart.Text = "종료된 프로그램 실행";
            this.btnStart.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.FlatAppearance.BorderSize = 0;
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Image = global::PM.Properties.Resources.iconmonstr_cloud_32_48;
            this.btnOpen.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpen.Location = new System.Drawing.Point(0, 317);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(156, 82);
            this.btnOpen.TabIndex = 11;
            this.btnOpen.Text = "프로그램 위치탐색";
            this.btnOpen.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSPD
            // 
            this.btnSPD.FlatAppearance.BorderSize = 0;
            this.btnSPD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSPD.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSPD.Image = global::PM.Properties.Resources.iconmonstr_eraser_2_48;
            this.btnSPD.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSPD.Location = new System.Drawing.Point(0, 141);
            this.btnSPD.Name = "btnSPD";
            this.btnSPD.Size = new System.Drawing.Size(156, 82);
            this.btnSPD.TabIndex = 10;
            this.btnSPD.Text = "시작프로그램에서 삭제";
            this.btnSPD.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSPD.UseVisualStyleBackColor = true;
            this.btnSPD.Click += new System.EventHandler(this.btnSPD_Click);
            // 
            // btnSPR
            // 
            this.btnSPR.FlatAppearance.BorderSize = 0;
            this.btnSPR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSPR.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSPR.Image = global::PM.Properties.Resources.iconmonstr_clipboard_13_48;
            this.btnSPR.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSPR.Location = new System.Drawing.Point(0, 53);
            this.btnSPR.Name = "btnSPR";
            this.btnSPR.Size = new System.Drawing.Size(156, 82);
            this.btnSPR.TabIndex = 9;
            this.btnSPR.Text = "시작프로그램에 등록";
            this.btnSPR.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSPR.UseVisualStyleBackColor = true;
            this.btnSPR.Click += new System.EventHandler(this.btnSPR_Click);
            // 
            // tboxPath
            // 
            this.tboxPath.Enabled = false;
            this.tboxPath.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tboxPath.ForeColor = System.Drawing.Color.AliceBlue;
            this.tboxPath.Location = new System.Drawing.Point(250, 30);
            this.tboxPath.Name = "tboxPath";
            this.tboxPath.Size = new System.Drawing.Size(647, 29);
            this.tboxPath.TabIndex = 5;
            // 
            // tboxPeriod
            // 
            this.tboxPeriod.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tboxPeriod.Location = new System.Drawing.Point(250, 83);
            this.tboxPeriod.Name = "tboxPeriod";
            this.tboxPeriod.Size = new System.Drawing.Size(89, 29);
            this.tboxPeriod.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.AliceBlue;
            this.label4.Location = new System.Drawing.Point(345, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "초";
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader7});
            this.listView1.Font = new System.Drawing.Font("굴림", 9F);
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(157, 186);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(887, 265);
            this.listView1.TabIndex = 12;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            this.listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "동작";
            this.columnHeader1.Width = 47;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "프로세스명";
            this.columnHeader2.Width = 84;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "프로세스경로";
            this.columnHeader3.Width = 237;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "주기(초)";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "시작시간";
            this.columnHeader4.Width = 189;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "마지막 종료시간";
            this.columnHeader6.Width = 197;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "상태";
            // 
            // lblProcNum
            // 
            this.lblProcNum.AutoSize = true;
            this.lblProcNum.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcNum.Location = new System.Drawing.Point(159, 468);
            this.lblProcNum.Name = "lblProcNum";
            this.lblProcNum.Size = new System.Drawing.Size(193, 23);
            this.lblProcNum.TabIndex = 13;
            this.lblProcNum.Text = "관리중인 프로그램 수 : 0";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "PM";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(105, 48);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // btnDel
            // 
            this.btnDel.FlatAppearance.BorderSize = 0;
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.Image = global::PM.Properties.Resources.iconmonstr_folder_26_48;
            this.btnDel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDel.Location = new System.Drawing.Point(740, 83);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(135, 82);
            this.btnDel.TabIndex = 11;
            this.btnDel.Text = "삭제";
            this.btnDel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnRevise
            // 
            this.btnRevise.FlatAppearance.BorderSize = 0;
            this.btnRevise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevise.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevise.Image = global::PM.Properties.Resources.iconmonstr_edit_9_48;
            this.btnRevise.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRevise.Location = new System.Drawing.Point(576, 83);
            this.btnRevise.Name = "btnRevise";
            this.btnRevise.Size = new System.Drawing.Size(135, 82);
            this.btnRevise.TabIndex = 10;
            this.btnRevise.Text = "수정";
            this.btnRevise.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRevise.UseVisualStyleBackColor = true;
            this.btnRevise.Click += new System.EventHandler(this.btnRevise_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::PM.Properties.Resources.iconmonstr_folder_24_48;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdd.Location = new System.Drawing.Point(409, 83);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(135, 82);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "추가";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::PM.Properties.Resources.iconmonstr_folder_30_48;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSearch.Location = new System.Drawing.Point(903, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(135, 82);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "찾기";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(948, 465);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 31);
            this.button1.TabIndex = 16;
            this.button1.Text = "작업 끝내기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // PMTab
            // 
            this.PMTab.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.PMTab.Controls.Add(this.tabPage1);
            this.PMTab.Controls.Add(this.tabPage2);
            this.PMTab.Font = new System.Drawing.Font("Constantia", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PMTab.ItemSize = new System.Drawing.Size(100, 30);
            this.PMTab.Location = new System.Drawing.Point(-3, 31);
            this.PMTab.Margin = new System.Windows.Forms.Padding(0);
            this.PMTab.Name = "PMTab";
            this.PMTab.Padding = new System.Drawing.Point(0, 0);
            this.PMTab.SelectedIndex = 0;
            this.PMTab.Size = new System.Drawing.Size(1073, 547);
            this.PMTab.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(43)))), ((int)(((byte)(60)))));
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.tboxPath);
            this.tabPage1.Controls.Add(this.lblProcNum);
            this.tabPage1.Controls.Add(this.btnSearch);
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.btnDel);
            this.tabPage1.Controls.Add(this.tboxPeriod);
            this.tabPage1.Controls.Add(this.btnRevise);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btnAdd);
            this.tabPage1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tabPage1.ForeColor = System.Drawing.Color.AliceBlue;
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1065, 509);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "   PM";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(43)))), ((int)(((byte)(60)))));
            this.tabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.Percentlbl2);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.Dlabel);
            this.tabPage2.Controls.Add(this.Clabel);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.progressBarD);
            this.tabPage2.Controls.Add(this.progressBarC);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.ScrollBar2);
            this.tabPage2.Controls.Add(this.ScrollBar);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1065, 509);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "   DM";
            this.tabPage2.Enter += new System.EventHandler(this.tabPage2_Enter);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.AliceBlue;
            this.label16.Location = new System.Drawing.Point(766, 472);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(272, 24);
            this.label16.TabIndex = 30;
            this.label16.Text = "※ 설정저장 시 DM동작이 중지됩니다.";
            // 
            // Dlabel
            // 
            this.Dlabel.AutoSize = true;
            this.Dlabel.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dlabel.ForeColor = System.Drawing.Color.AliceBlue;
            this.Dlabel.Location = new System.Drawing.Point(500, 426);
            this.Dlabel.Name = "Dlabel";
            this.Dlabel.Size = new System.Drawing.Size(34, 24);
            this.Dlabel.TabIndex = 29;
            this.Dlabel.Text = "0%";
            // 
            // Clabel
            // 
            this.Clabel.AutoSize = true;
            this.Clabel.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clabel.ForeColor = System.Drawing.Color.AliceBlue;
            this.Clabel.Location = new System.Drawing.Point(500, 390);
            this.Clabel.Name = "Clabel";
            this.Clabel.Size = new System.Drawing.Size(34, 24);
            this.Clabel.TabIndex = 28;
            this.Clabel.Text = "0%";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.AliceBlue;
            this.label15.Location = new System.Drawing.Point(305, 347);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(156, 29);
            this.label15.TabIndex = 27;
            this.label15.Text = "<사용중인 공간>";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.AliceBlue;
            this.label14.Location = new System.Drawing.Point(262, 426);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 24);
            this.label14.TabIndex = 26;
            this.label14.Text = "D Drive";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.AliceBlue;
            this.label13.Location = new System.Drawing.Point(262, 390);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 24);
            this.label13.TabIndex = 25;
            this.label13.Text = "C Drive";
            // 
            // progressBarD
            // 
            this.progressBarD.Location = new System.Drawing.Point(335, 426);
            this.progressBarD.Name = "progressBarD";
            this.progressBarD.Size = new System.Drawing.Size(159, 24);
            this.progressBarD.TabIndex = 24;
            // 
            // progressBarC
            // 
            this.progressBarC.Location = new System.Drawing.Point(335, 390);
            this.progressBarC.Name = "progressBarC";
            this.progressBarC.Size = new System.Drawing.Size(159, 24);
            this.progressBarC.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.AliceBlue;
            this.label10.Location = new System.Drawing.Point(233, 145);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(560, 24);
            this.label10.TabIndex = 22;
            this.label10.Text = "마찬가지로 지정일수가 채워지지 않아도 설정용량을 초과시에 삭제가 진행됩니다.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.AliceBlue;
            this.label9.Location = new System.Drawing.Point(594, 270);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(386, 24);
            this.label9.TabIndex = 21;
            this.label9.Text = "(설정 가능 범위)    용량 : 10 ~ 95%    일 : 10 ~ 150일";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.AliceBlue;
            this.label8.Location = new System.Drawing.Point(233, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(555, 24);
            this.label8.TabIndex = 20;
            this.label8.Text = "설정용량만큼 용량이 채워지지 않아도 설정한 일 수가 지나면 삭제가 진행됩니다.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.AliceBlue;
            this.label7.Location = new System.Drawing.Point(233, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(435, 24);
            this.label7.TabIndex = 19;
            this.label7.Text = "삭제방식은 혼합형으로 우선순위는 용량 퍼센티지 우선입니다. ";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(43)))), ((int)(((byte)(60)))));
            this.button3.Location = new System.Drawing.Point(770, 385);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(210, 80);
            this.button3.TabIndex = 18;
            this.button3.Text = "설정저장";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.dmStopbtn);
            this.panel3.Controls.Add(this.dmStartbtn);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.Daylbl);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.Percentlbl);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(157, 539);
            this.panel3.TabIndex = 17;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.AliceBlue;
            this.label12.Location = new System.Drawing.Point(30, 245);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 24);
            this.label12.TabIndex = 24;
            this.label12.Text = "기간";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.AliceBlue;
            this.label11.Location = new System.Drawing.Point(30, 190);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 24);
            this.label11.TabIndex = 23;
            this.label11.Text = "용량";
            // 
            // dmStopbtn
            // 
            this.dmStopbtn.FlatAppearance.BorderSize = 0;
            this.dmStopbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dmStopbtn.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dmStopbtn.Image = global::PM.Properties.Resources.iconmonstr_media_control_50_48;
            this.dmStopbtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.dmStopbtn.Location = new System.Drawing.Point(1, 385);
            this.dmStopbtn.Name = "dmStopbtn";
            this.dmStopbtn.Size = new System.Drawing.Size(156, 82);
            this.dmStopbtn.TabIndex = 20;
            this.dmStopbtn.Text = "정지";
            this.dmStopbtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.dmStopbtn.UseVisualStyleBackColor = true;
            this.dmStopbtn.Click += new System.EventHandler(this.dmStopbtn_Click);
            // 
            // dmStartbtn
            // 
            this.dmStartbtn.FlatAppearance.BorderSize = 0;
            this.dmStartbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dmStartbtn.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dmStartbtn.Image = global::PM.Properties.Resources.iconmonstr_media_control_48_48__1_;
            this.dmStartbtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.dmStartbtn.Location = new System.Drawing.Point(1, 289);
            this.dmStartbtn.Name = "dmStartbtn";
            this.dmStartbtn.Size = new System.Drawing.Size(156, 82);
            this.dmStartbtn.TabIndex = 19;
            this.dmStartbtn.Text = "동작";
            this.dmStartbtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.dmStartbtn.UseVisualStyleBackColor = true;
            this.dmStartbtn.Click += new System.EventHandler(this.dmStartbtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.AliceBlue;
            this.label5.Location = new System.Drawing.Point(27, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 24);
            this.label5.TabIndex = 3;
            this.label5.Text = "관리 폴더 경로";
            // 
            // Daylbl
            // 
            this.Daylbl.AutoSize = true;
            this.Daylbl.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Daylbl.ForeColor = System.Drawing.Color.AliceBlue;
            this.Daylbl.Location = new System.Drawing.Point(76, 245);
            this.Daylbl.Name = "Daylbl";
            this.Daylbl.Size = new System.Drawing.Size(48, 24);
            this.Daylbl.TabIndex = 16;
            this.Daylbl.Text = "10 일";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.AliceBlue;
            this.label6.Location = new System.Drawing.Point(39, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 24);
            this.label6.TabIndex = 11;
            this.label6.Text = "삭제 방식";
            // 
            // Percentlbl
            // 
            this.Percentlbl.AutoSize = true;
            this.Percentlbl.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Percentlbl.ForeColor = System.Drawing.Color.AliceBlue;
            this.Percentlbl.Location = new System.Drawing.Point(76, 190);
            this.Percentlbl.Name = "Percentlbl";
            this.Percentlbl.Size = new System.Drawing.Size(48, 24);
            this.Percentlbl.TabIndex = 14;
            this.Percentlbl.Text = "10 %";
            // 
            // ScrollBar2
            // 
            this.ScrollBar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ScrollBar2.Location = new System.Drawing.Point(237, 243);
            this.ScrollBar2.Maximum = 159;
            this.ScrollBar2.Minimum = 1;
            this.ScrollBar2.Name = "ScrollBar2";
            this.ScrollBar2.Size = new System.Drawing.Size(743, 26);
            this.ScrollBar2.TabIndex = 15;
            this.ScrollBar2.Value = 10;
            this.ScrollBar2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll_1);
            // 
            // ScrollBar
            // 
            this.ScrollBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ScrollBar.Location = new System.Drawing.Point(237, 188);
            this.ScrollBar.Maximum = 104;
            this.ScrollBar.Minimum = 10;
            this.ScrollBar.Name = "ScrollBar";
            this.ScrollBar.Size = new System.Drawing.Size(743, 26);
            this.ScrollBar.TabIndex = 13;
            this.ScrollBar.Value = 10;
            this.ScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::PM.Properties.Resources.iconmonstr_folder_30_48;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(903, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 82);
            this.button2.TabIndex = 9;
            this.button2.Text = "찾기";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox1.ForeColor = System.Drawing.Color.AliceBlue;
            this.textBox1.Location = new System.Drawing.Point(237, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(660, 29);
            this.textBox1.TabIndex = 6;
            // 
            // timer2
            // 
            this.timer2.Interval = 60000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Percentlbl2
            // 
            this.Percentlbl2.AutoSize = true;
            this.Percentlbl2.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Percentlbl2.ForeColor = System.Drawing.Color.Purple;
            this.Percentlbl2.Location = new System.Drawing.Point(805, 216);
            this.Percentlbl2.Name = "Percentlbl2";
            this.Percentlbl2.Size = new System.Drawing.Size(48, 24);
            this.Percentlbl2.TabIndex = 31;
            this.Percentlbl2.Text = "10 %";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.AliceBlue;
            this.label17.Location = new System.Drawing.Point(654, 216);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(155, 24);
            this.label17.TabIndex = 32;
            this.label17.Text = "관리 드라이브 용량이";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.AliceBlue;
            this.label18.Location = new System.Drawing.Point(850, 216);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(130, 24);
            this.label18.TabIndex = 33;
            this.label18.Text = "를 넘는 경우 삭제";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(43)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(1060, 570);
            this.Controls.Add(this.PMTab);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.AliceBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PM";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.PMTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tboxPath;
        private System.Windows.Forms.TextBox tboxPeriod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRevise;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label lblProcNum;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSPD;
        private System.Windows.Forms.Button btnSPR;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl PMTab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.HScrollBar ScrollBar;
        private System.Windows.Forms.Label Percentlbl;
        private System.Windows.Forms.Label Daylbl;
        private System.Windows.Forms.HScrollBar ScrollBar2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button dmStopbtn;
        private System.Windows.Forms.Button dmStartbtn;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ProgressBar progressBarD;
        private System.Windows.Forms.ProgressBar progressBarC;
        private System.Windows.Forms.Label Dlabel;
        private System.Windows.Forms.Label Clabel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label Percentlbl2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
    }
}

