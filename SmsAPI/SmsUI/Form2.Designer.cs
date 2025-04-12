
namespace SmsUI
{
    partial class Form2
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ckboxTwilio = new System.Windows.Forms.CheckBox();
            this.ckboxSmsApi = new System.Windows.Forms.CheckBox();
            this.ckboxSmsBulk = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 119);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1003, 343);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(474, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mensagens";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(477, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Mostrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ckboxTwilio
            // 
            this.ckboxTwilio.AutoSize = true;
            this.ckboxTwilio.Location = new System.Drawing.Point(395, 52);
            this.ckboxTwilio.Name = "ckboxTwilio";
            this.ckboxTwilio.Size = new System.Drawing.Size(53, 17);
            this.ckboxTwilio.TabIndex = 6;
            this.ckboxTwilio.Text = "Twilio";
            this.ckboxTwilio.UseVisualStyleBackColor = true;
            // 
            // ckboxSmsApi
            // 
            this.ckboxSmsApi.AutoSize = true;
            this.ckboxSmsApi.Location = new System.Drawing.Point(477, 52);
            this.ckboxSmsApi.Name = "ckboxSmsApi";
            this.ckboxSmsApi.Size = new System.Drawing.Size(61, 17);
            this.ckboxSmsApi.TabIndex = 7;
            this.ckboxSmsApi.Text = "SmsApi";
            this.ckboxSmsApi.UseVisualStyleBackColor = true;
            // 
            // ckboxSmsBulk
            // 
            this.ckboxSmsBulk.AutoSize = true;
            this.ckboxSmsBulk.Location = new System.Drawing.Point(564, 52);
            this.ckboxSmsBulk.Name = "ckboxSmsBulk";
            this.ckboxSmsBulk.Size = new System.Drawing.Size(67, 17);
            this.ckboxSmsBulk.TabIndex = 8;
            this.ckboxSmsBulk.Text = "SmsBulk";
            this.ckboxSmsBulk.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 474);
            this.Controls.Add(this.ckboxSmsBulk);
            this.Controls.Add(this.ckboxSmsApi);
            this.Controls.Add(this.ckboxTwilio);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox ckboxTwilio;
        private System.Windows.Forms.CheckBox ckboxSmsApi;
        private System.Windows.Forms.CheckBox ckboxSmsBulk;
    }
}