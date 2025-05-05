namespace CheeseChase
{
    partial class formPartita
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formPartita));
            this.btnChiudi = new System.Windows.Forms.Button();
            this.lblMenu1 = new System.Windows.Forms.Label();
            this.lblMenu2 = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.lblVite = new System.Windows.Forms.Label();
            this.pbCliente1 = new System.Windows.Forms.PictureBox();
            this.scandisciSecondi = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbCliente1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnChiudi
            // 
            this.btnChiudi.BackColor = System.Drawing.Color.Maroon;
            this.btnChiudi.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChiudi.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnChiudi.Location = new System.Drawing.Point(1319, 214);
            this.btnChiudi.Name = "btnChiudi";
            this.btnChiudi.Size = new System.Drawing.Size(147, 138);
            this.btnChiudi.TabIndex = 0;
            this.btnChiudi.Text = "SERRANDA";
            this.btnChiudi.UseVisualStyleBackColor = false;
            this.btnChiudi.Click += new System.EventHandler(this.btnChiudi_Click);
            // 
            // lblMenu1
            // 
            this.lblMenu1.AutoSize = true;
            this.lblMenu1.BackColor = System.Drawing.Color.Transparent;
            this.lblMenu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenu1.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMenu1.Location = new System.Drawing.Point(116, 26);
            this.lblMenu1.Name = "lblMenu1";
            this.lblMenu1.Size = new System.Drawing.Size(69, 31);
            this.lblMenu1.TabIndex = 1;
            this.lblMenu1.Text = "Vite:";
            // 
            // lblMenu2
            // 
            this.lblMenu2.AutoSize = true;
            this.lblMenu2.BackColor = System.Drawing.Color.Transparent;
            this.lblMenu2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenu2.ForeColor = System.Drawing.SystemColors.Control;
            this.lblMenu2.Location = new System.Drawing.Point(315, 26);
            this.lblMenu2.Name = "lblMenu2";
            this.lblMenu2.Size = new System.Drawing.Size(250, 31);
            this.lblMenu2.TabIndex = 2;
            this.lblMenu2.Text = "Carretto abusivo di:";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.BackColor = System.Drawing.Color.Transparent;
            this.lblNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNome.ForeColor = System.Drawing.SystemColors.Control;
            this.lblNome.Location = new System.Drawing.Point(559, 26);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(81, 31);
            this.lblNome.TabIndex = 3;
            this.lblNome.Text = "nome";
            // 
            // lblVite
            // 
            this.lblVite.AutoSize = true;
            this.lblVite.BackColor = System.Drawing.Color.Transparent;
            this.lblVite.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVite.ForeColor = System.Drawing.SystemColors.Control;
            this.lblVite.Location = new System.Drawing.Point(174, 26);
            this.lblVite.Name = "lblVite";
            this.lblVite.Size = new System.Drawing.Size(38, 31);
            this.lblVite.TabIndex = 4;
            this.lblVite.Text = "-1";
            // 
            // pbCliente1
            // 
            this.pbCliente1.Image = ((System.Drawing.Image)(resources.GetObject("pbCliente1.Image")));
            this.pbCliente1.Location = new System.Drawing.Point(255, 196);
            this.pbCliente1.Name = "pbCliente1";
            this.pbCliente1.Size = new System.Drawing.Size(310, 262);
            this.pbCliente1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCliente1.TabIndex = 5;
            this.pbCliente1.TabStop = false;
            // 
            // scandisciSecondi
            // 
            this.scandisciSecondi.Interval = 1000;
            this.scandisciSecondi.Tick += new System.EventHandler(this.scandisciSecondi_Tick);
            // 
            // formPartita
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1526, 917);
            this.Controls.Add(this.pbCliente1);
            this.Controls.Add(this.lblVite);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.lblMenu2);
            this.Controls.Add(this.lblMenu1);
            this.Controls.Add(this.btnChiudi);
            this.Name = "formPartita";
            this.Text = "CheesChase";
            ((System.ComponentModel.ISupportInitialize)(this.pbCliente1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChiudi;
        private System.Windows.Forms.Label lblMenu1;
        private System.Windows.Forms.Label lblMenu2;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblVite;
        private System.Windows.Forms.PictureBox pbCliente1;
        private System.Windows.Forms.Timer scandisciSecondi;
    }
}