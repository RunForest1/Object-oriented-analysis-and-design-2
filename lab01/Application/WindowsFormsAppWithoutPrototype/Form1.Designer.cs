namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Software = new System.Windows.Forms.TabPage();
            this.btnSoftware = new System.Windows.Forms.Button();
            this.inputTermsOfReference = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.inputSourceCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.inputNameSoftware = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.License = new System.Windows.Forms.TabPage();
            this.inputRoyality = new System.Windows.Forms.NumericUpDown();
            this.inputDuration = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.btnLicense = new System.Windows.Forms.Button();
            this.inputTerrtory = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.inputLicenseType = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Employment = new System.Windows.Forms.TabPage();
            this.inputProbationPeriod = new System.Windows.Forms.NumericUpDown();
            this.inputSalary = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.btnEployment = new System.Windows.Forms.Button();
            this.inputDepartment = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.inputPosition = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.listContract = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Software.SuspendLayout();
            this.License.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputRoyality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputDuration)).BeginInit();
            this.Employment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputProbationPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputSalary)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 42);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.label2.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(786, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Админ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "ContractPrototype";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Software);
            this.tabControl1.Controls.Add(this.License);
            this.tabControl1.Controls.Add(this.Employment);
            this.tabControl1.Font = new System.Drawing.Font("Noto Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.Location = new System.Drawing.Point(0, 48);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(548, 401);
            this.tabControl1.TabIndex = 1;
            // 
            // Software
            // 
            this.Software.Controls.Add(this.btnSoftware);
            this.Software.Controls.Add(this.inputTermsOfReference);
            this.Software.Controls.Add(this.label6);
            this.Software.Controls.Add(this.inputSourceCode);
            this.Software.Controls.Add(this.label5);
            this.Software.Controls.Add(this.inputNameSoftware);
            this.Software.Controls.Add(this.label4);
            this.Software.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Software.Location = new System.Drawing.Point(4, 29);
            this.Software.Name = "Software";
            this.Software.Padding = new System.Windows.Forms.Padding(3);
            this.Software.Size = new System.Drawing.Size(540, 368);
            this.Software.TabIndex = 0;
            this.Software.Text = "Договор разработки ПО";
            this.Software.UseVisualStyleBackColor = true;
            // 
            // btnSoftware
            // 
            this.btnSoftware.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSoftware.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSoftware.ForeColor = System.Drawing.Color.White;
            this.btnSoftware.Location = new System.Drawing.Point(429, 314);
            this.btnSoftware.Name = "btnSoftware";
            this.btnSoftware.Size = new System.Drawing.Size(91, 36);
            this.btnSoftware.TabIndex = 6;
            this.btnSoftware.Text = "Создать";
            this.btnSoftware.UseVisualStyleBackColor = false;
            this.btnSoftware.Click += new System.EventHandler(this.btnSoftware_Click);
            // 
            // inputTermsOfReference
            // 
            this.inputTermsOfReference.Location = new System.Drawing.Point(196, 96);
            this.inputTermsOfReference.Name = "inputTermsOfReference";
            this.inputTermsOfReference.Size = new System.Drawing.Size(124, 25);
            this.inputTermsOfReference.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(8, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(182, 24);
            this.label6.TabIndex = 4;
            this.label6.Text = "Техническое задание:";
            // 
            // inputSourceCode
            // 
            this.inputSourceCode.Location = new System.Drawing.Point(196, 55);
            this.inputSourceCode.Name = "inputSourceCode";
            this.inputSourceCode.Size = new System.Drawing.Size(124, 25);
            this.inputSourceCode.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(8, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 24);
            this.label5.TabIndex = 2;
            this.label5.Text = "Исходный код:";
            // 
            // inputNameSoftware
            // 
            this.inputNameSoftware.Location = new System.Drawing.Point(196, 13);
            this.inputNameSoftware.Name = "inputNameSoftware";
            this.inputNameSoftware.Size = new System.Drawing.Size(124, 25);
            this.inputNameSoftware.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(8, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 24);
            this.label4.TabIndex = 0;
            this.label4.Text = "Название ПО:";
            // 
            // License
            // 
            this.License.Controls.Add(this.inputRoyality);
            this.License.Controls.Add(this.inputDuration);
            this.License.Controls.Add(this.label10);
            this.License.Controls.Add(this.btnLicense);
            this.License.Controls.Add(this.inputTerrtory);
            this.License.Controls.Add(this.label7);
            this.License.Controls.Add(this.label111);
            this.License.Controls.Add(this.inputLicenseType);
            this.License.Controls.Add(this.label9);
            this.License.Location = new System.Drawing.Point(4, 29);
            this.License.Name = "License";
            this.License.Padding = new System.Windows.Forms.Padding(3);
            this.License.Size = new System.Drawing.Size(540, 368);
            this.License.TabIndex = 1;
            this.License.Text = "Лицензионный договор";
            this.License.UseVisualStyleBackColor = true;
            // 
            // inputRoyality
            // 
            this.inputRoyality.Location = new System.Drawing.Point(243, 136);
            this.inputRoyality.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.inputRoyality.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.inputRoyality.Name = "inputRoyality";
            this.inputRoyality.Size = new System.Drawing.Size(124, 25);
            this.inputRoyality.TabIndex = 17;
            this.inputRoyality.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // inputDuration
            // 
            this.inputDuration.Location = new System.Drawing.Point(243, 58);
            this.inputDuration.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.inputDuration.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.inputDuration.Name = "inputDuration";
            this.inputDuration.Size = new System.Drawing.Size(124, 25);
            this.inputDuration.TabIndex = 16;
            this.inputDuration.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(14, 135);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 24);
            this.label10.TabIndex = 14;
            this.label10.Text = "Пошлина:";
            // 
            // btnLicense
            // 
            this.btnLicense.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnLicense.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLicense.ForeColor = System.Drawing.Color.White;
            this.btnLicense.Location = new System.Drawing.Point(429, 316);
            this.btnLicense.Name = "btnLicense";
            this.btnLicense.Size = new System.Drawing.Size(91, 36);
            this.btnLicense.TabIndex = 13;
            this.btnLicense.Text = "Создать";
            this.btnLicense.UseVisualStyleBackColor = false;
            this.btnLicense.Click += new System.EventHandler(this.btnLicense_Click);
            // 
            // inputTerrtory
            // 
            this.inputTerrtory.Location = new System.Drawing.Point(243, 99);
            this.inputTerrtory.Name = "inputTerrtory";
            this.inputTerrtory.Size = new System.Drawing.Size(124, 25);
            this.inputTerrtory.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(14, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 24);
            this.label7.TabIndex = 11;
            this.label7.Text = "Территория:";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label111.Location = new System.Drawing.Point(14, 59);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(203, 24);
            this.label111.TabIndex = 9;
            this.label111.Text = "Длительность лицензии:";
            // 
            // inputLicenseType
            // 
            this.inputLicenseType.Location = new System.Drawing.Point(243, 16);
            this.inputLicenseType.Name = "inputLicenseType";
            this.inputLicenseType.Size = new System.Drawing.Size(124, 25);
            this.inputLicenseType.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(14, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 24);
            this.label9.TabIndex = 7;
            this.label9.Text = "Тип лицензии:";
            // 
            // Employment
            // 
            this.Employment.Controls.Add(this.inputProbationPeriod);
            this.Employment.Controls.Add(this.inputSalary);
            this.Employment.Controls.Add(this.label8);
            this.Employment.Controls.Add(this.btnEployment);
            this.Employment.Controls.Add(this.inputDepartment);
            this.Employment.Controls.Add(this.label11);
            this.Employment.Controls.Add(this.label12);
            this.Employment.Controls.Add(this.inputPosition);
            this.Employment.Controls.Add(this.label13);
            this.Employment.Location = new System.Drawing.Point(4, 29);
            this.Employment.Name = "Employment";
            this.Employment.Size = new System.Drawing.Size(540, 368);
            this.Employment.TabIndex = 2;
            this.Employment.Text = "Договор для приема на работу";
            this.Employment.UseVisualStyleBackColor = true;
            // 
            // inputProbationPeriod
            // 
            this.inputProbationPeriod.Location = new System.Drawing.Point(243, 136);
            this.inputProbationPeriod.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.inputProbationPeriod.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.inputProbationPeriod.Name = "inputProbationPeriod";
            this.inputProbationPeriod.Size = new System.Drawing.Size(124, 25);
            this.inputProbationPeriod.TabIndex = 26;
            this.inputProbationPeriod.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // inputSalary
            // 
            this.inputSalary.Location = new System.Drawing.Point(243, 58);
            this.inputSalary.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.inputSalary.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.inputSalary.Name = "inputSalary";
            this.inputSalary.Size = new System.Drawing.Size(124, 25);
            this.inputSalary.TabIndex = 25;
            this.inputSalary.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(14, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 24);
            this.label8.TabIndex = 23;
            this.label8.Text = "Пробный период:";
            // 
            // btnEployment
            // 
            this.btnEployment.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnEployment.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnEployment.ForeColor = System.Drawing.Color.White;
            this.btnEployment.Location = new System.Drawing.Point(429, 316);
            this.btnEployment.Name = "btnEployment";
            this.btnEployment.Size = new System.Drawing.Size(91, 36);
            this.btnEployment.TabIndex = 22;
            this.btnEployment.Text = "Создать";
            this.btnEployment.UseVisualStyleBackColor = false;
            this.btnEployment.Click += new System.EventHandler(this.btnEployment_Click);
            // 
            // inputDepartment
            // 
            this.inputDepartment.Location = new System.Drawing.Point(243, 99);
            this.inputDepartment.Name = "inputDepartment";
            this.inputDepartment.Size = new System.Drawing.Size(124, 25);
            this.inputDepartment.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(14, 99);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 24);
            this.label11.TabIndex = 20;
            this.label11.Text = "Отдел:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(14, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 24);
            this.label12.TabIndex = 19;
            this.label12.Text = "Зарплата:";
            // 
            // inputPosition
            // 
            this.inputPosition.Location = new System.Drawing.Point(243, 16);
            this.inputPosition.Name = "inputPosition";
            this.inputPosition.Size = new System.Drawing.Size(124, 25);
            this.inputPosition.TabIndex = 18;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(14, 17);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 24);
            this.label13.TabIndex = 17;
            this.label13.Text = "Позиция:";
            // 
            // listContract
            // 
            listViewGroup1.Header = "ListViewGroup";
            listViewGroup1.Name = "listViewGroup1";
            this.listContract.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.listContract.HideSelection = false;
            this.listContract.Location = new System.Drawing.Point(554, 78);
            this.listContract.Name = "listContract";
            this.listContract.Size = new System.Drawing.Size(306, 371);
            this.listContract.TabIndex = 2;
            this.listContract.UseCompatibleStateImageBehavior = false;
            this.listContract.View = System.Windows.Forms.View.List;
            this.listContract.SelectedIndexChanged += new System.EventHandler(this.listContract_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Noto Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(673, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "Договора";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listContract);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.Software.ResumeLayout(false);
            this.Software.PerformLayout();
            this.License.ResumeLayout(false);
            this.License.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputRoyality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputDuration)).EndInit();
            this.Employment.ResumeLayout(false);
            this.Employment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputProbationPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputSalary)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Software;
        private System.Windows.Forms.TabPage License;
        private System.Windows.Forms.TabPage Employment;
        private System.Windows.Forms.TextBox inputNameSoftware;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listContract;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSoftware;
        private System.Windows.Forms.TextBox inputTermsOfReference;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox inputSourceCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnLicense;
        private System.Windows.Forms.TextBox inputTerrtory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.TextBox inputLicenseType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown inputDuration;
        private System.Windows.Forms.NumericUpDown inputRoyality;
        private System.Windows.Forms.NumericUpDown inputProbationPeriod;
        private System.Windows.Forms.NumericUpDown inputSalary;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnEployment;
        private System.Windows.Forms.TextBox inputDepartment;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox inputPosition;
        private System.Windows.Forms.Label label13;
    }
}

