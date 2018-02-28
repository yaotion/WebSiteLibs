namespace ObjectSyncTool
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnStartSync = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstUser = new System.Windows.Forms.ListView();
            this.客户端编号 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.客户端名称 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.关注对象 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.更新数量 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstObject = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnViewSum = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tbUpdateLog = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSyncManualLib = new System.Windows.Forms.Button();
            this.btnUnRegLib = new System.Windows.Forms.Button();
            this.btnRegLib = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_UserObjectName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUserID = new System.Windows.Forms.TextBox();
            this.btnSyncLib = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tbUpdateLogWeb = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnSyncManualWeb = new System.Windows.Forms.Button();
            this.btnUnRegWeb = new System.Windows.Forms.Button();
            this.btnRegWeb = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbObjectNameWeb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbUserNameWeb = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbUserIDWweb = new System.Windows.Forms.TextBox();
            this.btnSyncAutoWeb = new System.Windows.Forms.Button();
            this.pbWeb = new System.Windows.Forms.ProgressBar();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(995, 463);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(987, 434);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "手工同步";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(981, 428);
            this.panel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 47);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(981, 381);
            this.textBox1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnStartSync);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(981, 47);
            this.panel2.TabIndex = 0;
            // 
            // btnStartSync
            // 
            this.btnStartSync.Location = new System.Drawing.Point(227, 12);
            this.btnStartSync.Name = "btnStartSync";
            this.btnStartSync.Size = new System.Drawing.Size(75, 23);
            this.btnStartSync.TabIndex = 5;
            this.btnStartSync.Text = "开始同步";
            this.btnStartSync.UseVisualStyleBackColor = true;
            this.btnStartSync.Click += new System.EventHandler(this.btnStartSync_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "同步对象:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "人员信息"});
            this.comboBox1.Location = new System.Drawing.Point(79, 15);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(131, 20);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(987, 434);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "状态查看";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstUser);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 233);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(981, 258);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "客户端更新列表";
            // 
            // lstUser
            // 
            this.lstUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.客户端编号,
            this.客户端名称,
            this.关注对象,
            this.更新数量});
            this.lstUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUser.GridLines = true;
            this.lstUser.Location = new System.Drawing.Point(3, 17);
            this.lstUser.Name = "lstUser";
            this.lstUser.Size = new System.Drawing.Size(975, 238);
            this.lstUser.TabIndex = 1;
            this.lstUser.UseCompatibleStateImageBehavior = false;
            this.lstUser.View = System.Windows.Forms.View.Details;
            // 
            // 客户端编号
            // 
            this.客户端编号.Text = "客户端编号";
            this.客户端编号.Width = 100;
            // 
            // 客户端名称
            // 
            this.客户端名称.Text = "客户端名称";
            this.客户端名称.Width = 120;
            // 
            // 关注对象
            // 
            this.关注对象.Text = "关注对象";
            this.关注对象.Width = 120;
            // 
            // 更新数量
            // 
            this.更新数量.Text = "更新数量";
            this.更新数量.Width = 100;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstObject);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(981, 182);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "同步对象列表";
            // 
            // lstObject
            // 
            this.lstObject.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstObject.GridLines = true;
            this.lstObject.Location = new System.Drawing.Point(3, 17);
            this.lstObject.Name = "lstObject";
            this.lstObject.Size = new System.Drawing.Size(975, 162);
            this.lstObject.TabIndex = 0;
            this.lstObject.UseCompatibleStateImageBehavior = false;
            this.lstObject.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "对象名称";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "版本";
            this.columnHeader2.Width = 350;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "数量";
            this.columnHeader3.Width = 80;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnViewSum);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(981, 48);
            this.panel3.TabIndex = 1;
            // 
            // btnViewSum
            // 
            this.btnViewSum.Location = new System.Drawing.Point(27, 11);
            this.btnViewSum.Name = "btnViewSum";
            this.btnViewSum.Size = new System.Drawing.Size(75, 23);
            this.btnViewSum.TabIndex = 0;
            this.btnViewSum.Text = "刷新";
            this.btnViewSum.UseVisualStyleBackColor = true;
            this.btnViewSum.Click += new System.EventHandler(this.btnViewSum_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.progressBar1);
            this.tabPage3.Controls.Add(this.tbUpdateLog);
            this.tabPage3.Controls.Add(this.panel4);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(987, 434);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "客户端模拟(类库)";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(3, 408);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(981, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // tbUpdateLog
            // 
            this.tbUpdateLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbUpdateLog.Location = new System.Drawing.Point(3, 51);
            this.tbUpdateLog.Multiline = true;
            this.tbUpdateLog.Name = "tbUpdateLog";
            this.tbUpdateLog.Size = new System.Drawing.Size(981, 380);
            this.tbUpdateLog.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnSyncManualLib);
            this.panel4.Controls.Add(this.btnUnRegLib);
            this.panel4.Controls.Add(this.btnRegLib);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.tb_UserObjectName);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.tbUserName);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.tbUserID);
            this.panel4.Controls.Add(this.btnSyncLib);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(981, 48);
            this.panel4.TabIndex = 2;
            // 
            // btnSyncManualLib
            // 
            this.btnSyncManualLib.Location = new System.Drawing.Point(845, 13);
            this.btnSyncManualLib.Name = "btnSyncManualLib";
            this.btnSyncManualLib.Size = new System.Drawing.Size(75, 23);
            this.btnSyncManualLib.TabIndex = 12;
            this.btnSyncManualLib.Text = "手工同步";
            this.btnSyncManualLib.UseVisualStyleBackColor = true;
            this.btnSyncManualLib.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnUnRegLib
            // 
            this.btnUnRegLib.Location = new System.Drawing.Point(755, 13);
            this.btnUnRegLib.Name = "btnUnRegLib";
            this.btnUnRegLib.Size = new System.Drawing.Size(75, 23);
            this.btnUnRegLib.TabIndex = 11;
            this.btnUnRegLib.Text = "注销";
            this.btnUnRegLib.UseVisualStyleBackColor = true;
            this.btnUnRegLib.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnRegLib
            // 
            this.btnRegLib.Location = new System.Drawing.Point(665, 13);
            this.btnRegLib.Name = "btnRegLib";
            this.btnRegLib.Size = new System.Drawing.Size(75, 23);
            this.btnRegLib.TabIndex = 10;
            this.btnRegLib.Text = "注册";
            this.btnRegLib.UseVisualStyleBackColor = true;
            this.btnRegLib.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(389, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "更新对象";
            // 
            // tb_UserObjectName
            // 
            this.tb_UserObjectName.Location = new System.Drawing.Point(452, 15);
            this.tb_UserObjectName.Name = "tb_UserObjectName";
            this.tb_UserObjectName.Size = new System.Drawing.Size(100, 21);
            this.tb_UserObjectName.TabIndex = 8;
            this.tb_UserObjectName.Text = "User";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(202, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "客户端名称";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(273, 16);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(100, 21);
            this.tbUserName.TabIndex = 6;
            this.tbUserName.Text = "测试客户端";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "客户端编号";
            // 
            // tbUserID
            // 
            this.tbUserID.Location = new System.Drawing.Point(87, 16);
            this.tbUserID.Name = "tbUserID";
            this.tbUserID.Size = new System.Drawing.Size(100, 21);
            this.tbUserID.TabIndex = 4;
            this.tbUserID.Text = "9999999";
            // 
            // btnSyncLib
            // 
            this.btnSyncLib.Location = new System.Drawing.Point(574, 14);
            this.btnSyncLib.Name = "btnSyncLib";
            this.btnSyncLib.Size = new System.Drawing.Size(75, 23);
            this.btnSyncLib.TabIndex = 0;
            this.btnSyncLib.Text = "差异更新";
            this.btnSyncLib.UseVisualStyleBackColor = true;
            this.btnSyncLib.Click += new System.EventHandler(this.btnGetUpdate_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.pbWeb);
            this.tabPage4.Controls.Add(this.tbUpdateLogWeb);
            this.tabPage4.Controls.Add(this.panel5);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(987, 434);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "客户端模拟(WebAPI)";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tbUpdateLogWeb
            // 
            this.tbUpdateLogWeb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbUpdateLogWeb.Location = new System.Drawing.Point(3, 51);
            this.tbUpdateLogWeb.Multiline = true;
            this.tbUpdateLogWeb.Name = "tbUpdateLogWeb";
            this.tbUpdateLogWeb.Size = new System.Drawing.Size(981, 380);
            this.tbUpdateLogWeb.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnSyncManualWeb);
            this.panel5.Controls.Add(this.btnUnRegWeb);
            this.panel5.Controls.Add(this.btnRegWeb);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.tbObjectNameWeb);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.tbUserNameWeb);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.tbUserIDWweb);
            this.panel5.Controls.Add(this.btnSyncAutoWeb);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(981, 48);
            this.panel5.TabIndex = 4;
            // 
            // btnSyncManualWeb
            // 
            this.btnSyncManualWeb.Location = new System.Drawing.Point(845, 13);
            this.btnSyncManualWeb.Name = "btnSyncManualWeb";
            this.btnSyncManualWeb.Size = new System.Drawing.Size(75, 23);
            this.btnSyncManualWeb.TabIndex = 12;
            this.btnSyncManualWeb.Text = "手工同步";
            this.btnSyncManualWeb.UseVisualStyleBackColor = true;
            this.btnSyncManualWeb.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // btnUnRegWeb
            // 
            this.btnUnRegWeb.Location = new System.Drawing.Point(755, 13);
            this.btnUnRegWeb.Name = "btnUnRegWeb";
            this.btnUnRegWeb.Size = new System.Drawing.Size(75, 23);
            this.btnUnRegWeb.TabIndex = 11;
            this.btnUnRegWeb.Text = "注销";
            this.btnUnRegWeb.UseVisualStyleBackColor = true;
            this.btnUnRegWeb.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnRegWeb
            // 
            this.btnRegWeb.Location = new System.Drawing.Point(665, 13);
            this.btnRegWeb.Name = "btnRegWeb";
            this.btnRegWeb.Size = new System.Drawing.Size(75, 23);
            this.btnRegWeb.TabIndex = 10;
            this.btnRegWeb.Text = "注册";
            this.btnRegWeb.UseVisualStyleBackColor = true;
            this.btnRegWeb.Click += new System.EventHandler(this.button4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(389, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "更新对象";
            // 
            // tbObjectNameWeb
            // 
            this.tbObjectNameWeb.Location = new System.Drawing.Point(452, 15);
            this.tbObjectNameWeb.Name = "tbObjectNameWeb";
            this.tbObjectNameWeb.Size = new System.Drawing.Size(100, 21);
            this.tbObjectNameWeb.TabIndex = 8;
            this.tbObjectNameWeb.Text = "User";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(202, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "客户端名称";
            // 
            // tbUserNameWeb
            // 
            this.tbUserNameWeb.Location = new System.Drawing.Point(273, 16);
            this.tbUserNameWeb.Name = "tbUserNameWeb";
            this.tbUserNameWeb.Size = new System.Drawing.Size(100, 21);
            this.tbUserNameWeb.TabIndex = 6;
            this.tbUserNameWeb.Text = "测试客户端";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "客户端编号";
            // 
            // tbUserIDWweb
            // 
            this.tbUserIDWweb.Location = new System.Drawing.Point(87, 16);
            this.tbUserIDWweb.Name = "tbUserIDWweb";
            this.tbUserIDWweb.Size = new System.Drawing.Size(100, 21);
            this.tbUserIDWweb.TabIndex = 4;
            this.tbUserIDWweb.Text = "9999999";
            // 
            // btnSyncAutoWeb
            // 
            this.btnSyncAutoWeb.Location = new System.Drawing.Point(574, 14);
            this.btnSyncAutoWeb.Name = "btnSyncAutoWeb";
            this.btnSyncAutoWeb.Size = new System.Drawing.Size(75, 23);
            this.btnSyncAutoWeb.TabIndex = 0;
            this.btnSyncAutoWeb.Text = "差异更新";
            this.btnSyncAutoWeb.UseVisualStyleBackColor = true;
            this.btnSyncAutoWeb.Click += new System.EventHandler(this.button5_Click);
            // 
            // pbWeb
            // 
            this.pbWeb.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbWeb.Location = new System.Drawing.Point(3, 408);
            this.pbWeb.Name = "pbWeb";
            this.pbWeb.Size = new System.Drawing.Size(981, 23);
            this.pbWeb.TabIndex = 6;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 463);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.Text = "数据同步管理工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnStartSync;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnViewSum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lstUser;
        private System.Windows.Forms.ListView lstObject;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader 客户端编号;
        private System.Windows.Forms.ColumnHeader 客户端名称;
        private System.Windows.Forms.ColumnHeader 关注对象;
        private System.Windows.Forms.ColumnHeader 更新数量;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSyncLib;
        private System.Windows.Forms.TextBox tbUserID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_UserObjectName;
        private System.Windows.Forms.Button btnUnRegLib;
        private System.Windows.Forms.Button btnRegLib;
        private System.Windows.Forms.TextBox tbUpdateLog;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnSyncManualLib;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox tbUpdateLogWeb;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnSyncManualWeb;
        private System.Windows.Forms.Button btnUnRegWeb;
        private System.Windows.Forms.Button btnRegWeb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbObjectNameWeb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbUserNameWeb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbUserIDWweb;
        private System.Windows.Forms.Button btnSyncAutoWeb;
        private System.Windows.Forms.ProgressBar pbWeb;
    }
}

