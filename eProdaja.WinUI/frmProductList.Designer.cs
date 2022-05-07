namespace eProdaja.WinUI
{
    partial class frmProductList
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
            this.btnShowProductList = new System.Windows.Forms.Button();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // btnShowProductList
            // 
            this.btnShowProductList.Location = new System.Drawing.Point(713, 58);
            this.btnShowProductList.Name = "btnShowProductList";
            this.btnShowProductList.Size = new System.Drawing.Size(75, 23);
            this.btnShowProductList.TabIndex = 0;
            this.btnShowProductList.Text = "Show";
            this.btnShowProductList.UseVisualStyleBackColor = true;
            this.btnShowProductList.Click += new System.EventHandler(this.btnShowProductList_Click);
            // 
            // dgvProduct
            // 
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduct.Location = new System.Drawing.Point(12, 108);
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.RowTemplate.Height = 25;
            this.dgvProduct.Size = new System.Drawing.Size(776, 330);
            this.dgvProduct.TabIndex = 1;
            // 
            // frmProductList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvProduct);
            this.Controls.Add(this.btnShowProductList);
            this.Name = "frmProductList";
            this.Text = "frmProductList";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnShowProductList;
        private DataGridView dgvProduct;
    }
}