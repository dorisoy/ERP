namespace CreateSysConfig {
	partial class Form1 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.txtSrc = new System.Windows.Forms.TextBox();
			this.btnCreate = new System.Windows.Forms.Button();
			this.btnSelectFile = new System.Windows.Forms.Button();
			this.rbIsTest1 = new System.Windows.Forms.RadioButton();
			this.rbIsTest2 = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.gbIsSingleWarehouse = new System.Windows.Forms.GroupBox();
			this.rbIsSingleWarehouse2 = new System.Windows.Forms.RadioButton();
			this.rbIsSingleWarehouse1 = new System.Windows.Forms.RadioButton();
			this.txtSystemTitle = new System.Windows.Forms.TextBox();
			this.lblSystemTitle = new System.Windows.Forms.Label();
			this.txtSystemVersion = new System.Windows.Forms.TextBox();
			this.lblSystemVesion = new System.Windows.Forms.Label();
			this.lblInstallTime = new System.Windows.Forms.Label();
			this.lblLastModifyTime = new System.Windows.Forms.Label();
			this.txtInstallTime = new System.Windows.Forms.TextBox();
			this.txtLastModifyTime = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.gbIsSingleWarehouse.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtSrc
			// 
			this.txtSrc.Location = new System.Drawing.Point(34, 24);
			this.txtSrc.Name = "txtSrc";
			this.txtSrc.ReadOnly = true;
			this.txtSrc.Size = new System.Drawing.Size(255, 21);
			this.txtSrc.TabIndex = 5;
			// 
			// btnCreate
			// 
			this.btnCreate.Location = new System.Drawing.Point(125, 278);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size(98, 23);
			this.btnCreate.TabIndex = 4;
			this.btnCreate.Text = "生成加密文件";
			this.btnCreate.UseVisualStyleBackColor = true;
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			// 
			// btnSelectFile
			// 
			this.btnSelectFile.Location = new System.Drawing.Point(295, 22);
			this.btnSelectFile.Name = "btnSelectFile";
			this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
			this.btnSelectFile.TabIndex = 3;
			this.btnSelectFile.Text = "选择文件";
			this.btnSelectFile.UseVisualStyleBackColor = true;
			this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
			// 
			// rbIsTest1
			// 
			this.rbIsTest1.AutoSize = true;
			this.rbIsTest1.Location = new System.Drawing.Point(6, 15);
			this.rbIsTest1.Name = "rbIsTest1";
			this.rbIsTest1.Size = new System.Drawing.Size(35, 16);
			this.rbIsTest1.TabIndex = 8;
			this.rbIsTest1.Text = "是";
			this.rbIsTest1.UseVisualStyleBackColor = true;
			// 
			// rbIsTest2
			// 
			this.rbIsTest2.AutoSize = true;
			this.rbIsTest2.Location = new System.Drawing.Point(47, 15);
			this.rbIsTest2.Name = "rbIsTest2";
			this.rbIsTest2.Size = new System.Drawing.Size(35, 16);
			this.rbIsTest2.TabIndex = 9;
			this.rbIsTest2.Text = "否";
			this.rbIsTest2.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rbIsTest2);
			this.groupBox1.Controls.Add(this.rbIsTest1);
			this.groupBox1.Location = new System.Drawing.Point(126, 208);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(88, 35);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "测试环境";
			// 
			// gbIsSingleWarehouse
			// 
			this.gbIsSingleWarehouse.Controls.Add(this.rbIsSingleWarehouse2);
			this.gbIsSingleWarehouse.Controls.Add(this.rbIsSingleWarehouse1);
			this.gbIsSingleWarehouse.Location = new System.Drawing.Point(125, 124);
			this.gbIsSingleWarehouse.Name = "gbIsSingleWarehouse";
			this.gbIsSingleWarehouse.Size = new System.Drawing.Size(88, 38);
			this.gbIsSingleWarehouse.TabIndex = 10;
			this.gbIsSingleWarehouse.TabStop = false;
			this.gbIsSingleWarehouse.Text = "是否单仓";
			// 
			// rbIsSingleWarehouse2
			// 
			this.rbIsSingleWarehouse2.AutoSize = true;
			this.rbIsSingleWarehouse2.Location = new System.Drawing.Point(47, 18);
			this.rbIsSingleWarehouse2.Name = "rbIsSingleWarehouse2";
			this.rbIsSingleWarehouse2.Size = new System.Drawing.Size(35, 16);
			this.rbIsSingleWarehouse2.TabIndex = 1;
			this.rbIsSingleWarehouse2.Text = "否";
			this.rbIsSingleWarehouse2.UseVisualStyleBackColor = true;
			// 
			// rbIsSingleWarehouse1
			// 
			this.rbIsSingleWarehouse1.AutoSize = true;
			this.rbIsSingleWarehouse1.Checked = true;
			this.rbIsSingleWarehouse1.Location = new System.Drawing.Point(6, 18);
			this.rbIsSingleWarehouse1.Name = "rbIsSingleWarehouse1";
			this.rbIsSingleWarehouse1.Size = new System.Drawing.Size(35, 16);
			this.rbIsSingleWarehouse1.TabIndex = 0;
			this.rbIsSingleWarehouse1.TabStop = true;
			this.rbIsSingleWarehouse1.Text = "是";
			this.rbIsSingleWarehouse1.UseVisualStyleBackColor = true;
			// 
			// txtSystemTitle
			// 
			this.txtSystemTitle.Location = new System.Drawing.Point(125, 49);
			this.txtSystemTitle.Name = "txtSystemTitle";
			this.txtSystemTitle.Size = new System.Drawing.Size(164, 21);
			this.txtSystemTitle.TabIndex = 11;
			// 
			// lblSystemTitle
			// 
			this.lblSystemTitle.AutoSize = true;
			this.lblSystemTitle.Location = new System.Drawing.Point(58, 54);
			this.lblSystemTitle.Name = "lblSystemTitle";
			this.lblSystemTitle.Size = new System.Drawing.Size(65, 12);
			this.lblSystemTitle.TabIndex = 12;
			this.lblSystemTitle.Text = "系统标题：";
			// 
			// txtSystemVersion
			// 
			this.txtSystemVersion.Location = new System.Drawing.Point(125, 89);
			this.txtSystemVersion.Name = "txtSystemVersion";
			this.txtSystemVersion.Size = new System.Drawing.Size(164, 21);
			this.txtSystemVersion.TabIndex = 13;
			// 
			// lblSystemVesion
			// 
			this.lblSystemVesion.AutoSize = true;
			this.lblSystemVesion.Location = new System.Drawing.Point(70, 97);
			this.lblSystemVesion.Name = "lblSystemVesion";
			this.lblSystemVesion.Size = new System.Drawing.Size(53, 12);
			this.lblSystemVesion.TabIndex = 14;
			this.lblSystemVesion.Text = "版本号：";
			// 
			// lblInstallTime
			// 
			this.lblInstallTime.AutoSize = true;
			this.lblInstallTime.Location = new System.Drawing.Point(34, 165);
			this.lblInstallTime.Name = "lblInstallTime";
			this.lblInstallTime.Size = new System.Drawing.Size(89, 12);
			this.lblInstallTime.TabIndex = 15;
			this.lblInstallTime.Text = "系统安装时间：";
			// 
			// lblLastModifyTime
			// 
			this.lblLastModifyTime.AutoSize = true;
			this.lblLastModifyTime.Location = new System.Drawing.Point(10, 187);
			this.lblLastModifyTime.Name = "lblLastModifyTime";
			this.lblLastModifyTime.Size = new System.Drawing.Size(113, 12);
			this.lblLastModifyTime.TabIndex = 16;
			this.lblLastModifyTime.Text = "最后一次更新时间：";
			// 
			// txtInstallTime
			// 
			this.txtInstallTime.Location = new System.Drawing.Point(125, 162);
			this.txtInstallTime.Name = "txtInstallTime";
			this.txtInstallTime.Size = new System.Drawing.Size(147, 21);
			this.txtInstallTime.TabIndex = 17;
			// 
			// txtLastModifyTime
			// 
			this.txtLastModifyTime.Location = new System.Drawing.Point(125, 184);
			this.txtLastModifyTime.Name = "txtLastModifyTime";
			this.txtLastModifyTime.ReadOnly = true;
			this.txtLastModifyTime.Size = new System.Drawing.Size(147, 21);
			this.txtLastModifyTime.TabIndex = 17;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.DarkRed;
			this.label1.Location = new System.Drawing.Point(123, 246);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(155, 12);
			this.label1.TabIndex = 18;
			this.label1.Text = "测试环境加密不绑定mac地址";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(403, 342);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtLastModifyTime);
			this.Controls.Add(this.txtInstallTime);
			this.Controls.Add(this.lblLastModifyTime);
			this.Controls.Add(this.lblInstallTime);
			this.Controls.Add(this.lblSystemVesion);
			this.Controls.Add(this.txtSystemVersion);
			this.Controls.Add(this.lblSystemTitle);
			this.Controls.Add(this.txtSystemTitle);
			this.Controls.Add(this.gbIsSingleWarehouse);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.txtSrc);
			this.Controls.Add(this.btnCreate);
			this.Controls.Add(this.btnSelectFile);
			this.Name = "Form1";
			this.Text = "Form1";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.gbIsSingleWarehouse.ResumeLayout(false);
			this.gbIsSingleWarehouse.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtSrc;
		private System.Windows.Forms.Button btnCreate;
		private System.Windows.Forms.Button btnSelectFile;
		private System.Windows.Forms.RadioButton rbIsTest1;
		private System.Windows.Forms.RadioButton rbIsTest2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox gbIsSingleWarehouse;
		private System.Windows.Forms.RadioButton rbIsSingleWarehouse1;
		private System.Windows.Forms.RadioButton rbIsSingleWarehouse2;
		private System.Windows.Forms.TextBox txtSystemTitle;
		private System.Windows.Forms.Label lblSystemTitle;
		private System.Windows.Forms.TextBox txtSystemVersion;
		private System.Windows.Forms.Label lblSystemVesion;
		private System.Windows.Forms.Label lblInstallTime;
		private System.Windows.Forms.Label lblLastModifyTime;
		private System.Windows.Forms.TextBox txtInstallTime;
		private System.Windows.Forms.TextBox txtLastModifyTime;
		private System.Windows.Forms.Label label1;
	}
}