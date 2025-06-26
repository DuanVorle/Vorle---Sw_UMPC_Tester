namespace UMPC_Production
{
    partial class frmHome
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpHome = new System.Windows.Forms.TableLayoutPanel();
            this.tlpTestador = new System.Windows.Forms.TableLayoutPanel();
            this.cbxTestador = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTestados = new System.Windows.Forms.Label();
            this.tlpTestes = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblAprovados = new System.Windows.Forms.Label();
            this.lblReprovados = new System.Windows.Forms.Label();
            this.tlpTeste = new System.Windows.Forms.TableLayoutPanel();
            this.btnTeste = new System.Windows.Forms.Button();
            this.pbxStatus = new System.Windows.Forms.PictureBox();
            this.tlpEmTeste = new System.Windows.Forms.TableLayoutPanel();
            this.cbxEmTeste = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rtbDebug = new System.Windows.Forms.RichTextBox();
            this.tlpHome.SuspendLayout();
            this.tlpTestador.SuspendLayout();
            this.tlpTestes.SuspendLayout();
            this.tlpTeste.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxStatus)).BeginInit();
            this.tlpEmTeste.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpHome
            // 
            this.tlpHome.AutoScroll = true;
            this.tlpHome.AutoSize = true;
            this.tlpHome.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpHome.ColumnCount = 3;
            this.tlpHome.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpHome.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpHome.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpHome.Controls.Add(this.tlpTestador, 0, 0);
            this.tlpHome.Controls.Add(this.tlpTeste, 1, 0);
            this.tlpHome.Controls.Add(this.tlpEmTeste, 2, 0);
            this.tlpHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHome.Location = new System.Drawing.Point(0, 0);
            this.tlpHome.Name = "tlpHome";
            this.tlpHome.RowCount = 1;
            this.tlpHome.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHome.Size = new System.Drawing.Size(800, 450);
            this.tlpHome.TabIndex = 0;
            // 
            // tlpTestador
            // 
            this.tlpTestador.AutoSize = true;
            this.tlpTestador.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpTestador.ColumnCount = 1;
            this.tlpTestador.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTestador.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpTestador.Controls.Add(this.cbxTestador, 0, 1);
            this.tlpTestador.Controls.Add(this.label1, 0, 0);
            this.tlpTestador.Controls.Add(this.label2, 0, 3);
            this.tlpTestador.Controls.Add(this.lblTestados, 0, 4);
            this.tlpTestador.Controls.Add(this.tlpTestes, 0, 5);
            this.tlpTestador.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTestador.Location = new System.Drawing.Point(3, 3);
            this.tlpTestador.Name = "tlpTestador";
            this.tlpTestador.RowCount = 6;
            this.tlpTestador.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpTestador.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpTestador.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTestador.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpTestador.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpTestador.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpTestador.Size = new System.Drawing.Size(234, 444);
            this.tlpTestador.TabIndex = 0;
            // 
            // cbxTestador
            // 
            this.cbxTestador.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxTestador.FormattingEnabled = true;
            this.cbxTestador.Location = new System.Drawing.Point(3, 53);
            this.cbxTestador.Name = "cbxTestador";
            this.cbxTestador.Size = new System.Drawing.Size(228, 24);
            this.cbxTestador.TabIndex = 0;
            this.cbxTestador.DropDown += new System.EventHandler(this.cbxTestador_DropDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 50);
            this.label1.TabIndex = 1;
            this.label1.Text = "Testador";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 50);
            this.label2.TabIndex = 2;
            this.label2.Text = "Testados";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTestados
            // 
            this.lblTestados.AutoSize = true;
            this.lblTestados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTestados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestados.Location = new System.Drawing.Point(3, 294);
            this.lblTestados.Name = "lblTestados";
            this.lblTestados.Size = new System.Drawing.Size(228, 50);
            this.lblTestados.TabIndex = 3;
            this.lblTestados.Text = "0";
            this.lblTestados.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpTestes
            // 
            this.tlpTestes.AutoScroll = true;
            this.tlpTestes.AutoSize = true;
            this.tlpTestes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpTestes.ColumnCount = 2;
            this.tlpTestes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTestes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTestes.Controls.Add(this.label5, 0, 0);
            this.tlpTestes.Controls.Add(this.label6, 1, 0);
            this.tlpTestes.Controls.Add(this.lblAprovados, 0, 1);
            this.tlpTestes.Controls.Add(this.lblReprovados, 1, 1);
            this.tlpTestes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTestes.Location = new System.Drawing.Point(3, 347);
            this.tlpTestes.Name = "tlpTestes";
            this.tlpTestes.RowCount = 2;
            this.tlpTestes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTestes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTestes.Size = new System.Drawing.Size(228, 94);
            this.tlpTestes.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 47);
            this.label5.TabIndex = 0;
            this.label5.Text = "Aprovados";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(117, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 47);
            this.label6.TabIndex = 1;
            this.label6.Text = "Reprovados";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAprovados
            // 
            this.lblAprovados.AutoSize = true;
            this.lblAprovados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAprovados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAprovados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblAprovados.Location = new System.Drawing.Point(3, 47);
            this.lblAprovados.Name = "lblAprovados";
            this.lblAprovados.Size = new System.Drawing.Size(108, 47);
            this.lblAprovados.TabIndex = 2;
            this.lblAprovados.Text = "0";
            this.lblAprovados.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReprovados
            // 
            this.lblReprovados.AutoSize = true;
            this.lblReprovados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReprovados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReprovados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblReprovados.Location = new System.Drawing.Point(117, 47);
            this.lblReprovados.Name = "lblReprovados";
            this.lblReprovados.Size = new System.Drawing.Size(108, 47);
            this.lblReprovados.TabIndex = 3;
            this.lblReprovados.Text = "0";
            this.lblReprovados.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpTeste
            // 
            this.tlpTeste.AutoSize = true;
            this.tlpTeste.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpTeste.ColumnCount = 1;
            this.tlpTeste.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTeste.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpTeste.Controls.Add(this.btnTeste, 0, 1);
            this.tlpTeste.Controls.Add(this.pbxStatus, 0, 0);
            this.tlpTeste.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTeste.Location = new System.Drawing.Point(243, 3);
            this.tlpTeste.Name = "tlpTeste";
            this.tlpTeste.RowCount = 2;
            this.tlpTeste.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpTeste.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpTeste.Size = new System.Drawing.Size(274, 444);
            this.tlpTeste.TabIndex = 1;
            // 
            // btnTeste
            // 
            this.btnTeste.AutoSize = true;
            this.btnTeste.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnTeste.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTeste.Location = new System.Drawing.Point(3, 313);
            this.btnTeste.Name = "btnTeste";
            this.btnTeste.Size = new System.Drawing.Size(268, 128);
            this.btnTeste.TabIndex = 0;
            this.btnTeste.Text = "Testar";
            this.btnTeste.UseVisualStyleBackColor = true;
            this.btnTeste.Click += new System.EventHandler(this.btnTeste_Click);
            // 
            // pbxStatus
            // 
            this.pbxStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxStatus.Image = global::UMPC_Production.Properties.Resources.Aguardando;
            this.pbxStatus.Location = new System.Drawing.Point(10, 10);
            this.pbxStatus.Margin = new System.Windows.Forms.Padding(10);
            this.pbxStatus.Name = "pbxStatus";
            this.pbxStatus.Size = new System.Drawing.Size(254, 290);
            this.pbxStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxStatus.TabIndex = 1;
            this.pbxStatus.TabStop = false;
            // 
            // tlpEmTeste
            // 
            this.tlpEmTeste.AutoSize = true;
            this.tlpEmTeste.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpEmTeste.ColumnCount = 1;
            this.tlpEmTeste.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEmTeste.Controls.Add(this.cbxEmTeste, 0, 1);
            this.tlpEmTeste.Controls.Add(this.label4, 0, 0);
            this.tlpEmTeste.Controls.Add(this.rtbDebug, 0, 2);
            this.tlpEmTeste.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEmTeste.Location = new System.Drawing.Point(523, 3);
            this.tlpEmTeste.Name = "tlpEmTeste";
            this.tlpEmTeste.RowCount = 4;
            this.tlpEmTeste.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpEmTeste.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpEmTeste.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEmTeste.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpEmTeste.Size = new System.Drawing.Size(274, 444);
            this.tlpEmTeste.TabIndex = 2;
            // 
            // cbxEmTeste
            // 
            this.cbxEmTeste.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxEmTeste.FormattingEnabled = true;
            this.cbxEmTeste.Location = new System.Drawing.Point(3, 53);
            this.cbxEmTeste.Name = "cbxEmTeste";
            this.cbxEmTeste.Size = new System.Drawing.Size(268, 24);
            this.cbxEmTeste.TabIndex = 0;
            this.cbxEmTeste.DropDown += new System.EventHandler(this.cbxEmTeste_DropDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(268, 50);
            this.label4.TabIndex = 1;
            this.label4.Text = "Em Teste";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtbDebug
            // 
            this.rtbDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDebug.Location = new System.Drawing.Point(3, 103);
            this.rtbDebug.Name = "rtbDebug";
            this.rtbDebug.Size = new System.Drawing.Size(268, 318);
            this.rtbDebug.TabIndex = 2;
            this.rtbDebug.Text = "";
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlpHome);
            this.Name = "frmHome";
            this.Text = "UMPC Tester";
            this.tlpHome.ResumeLayout(false);
            this.tlpHome.PerformLayout();
            this.tlpTestador.ResumeLayout(false);
            this.tlpTestador.PerformLayout();
            this.tlpTestes.ResumeLayout(false);
            this.tlpTestes.PerformLayout();
            this.tlpTeste.ResumeLayout(false);
            this.tlpTeste.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxStatus)).EndInit();
            this.tlpEmTeste.ResumeLayout(false);
            this.tlpEmTeste.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpHome;
        private System.Windows.Forms.TableLayoutPanel tlpTestador;
        private System.Windows.Forms.ComboBox cbxTestador;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTestados;
        private System.Windows.Forms.TableLayoutPanel tlpTestes;
        private System.Windows.Forms.TableLayoutPanel tlpTeste;
        private System.Windows.Forms.Button btnTeste;
        private System.Windows.Forms.PictureBox pbxStatus;
        private System.Windows.Forms.TableLayoutPanel tlpEmTeste;
        private System.Windows.Forms.ComboBox cbxEmTeste;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblAprovados;
        private System.Windows.Forms.Label lblReprovados;
        private System.Windows.Forms.RichTextBox rtbDebug;
    }
}

