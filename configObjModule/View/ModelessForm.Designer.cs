using System.Drawing;
using configObjModule.ViewModel;

namespace configObjModule.View
{
      partial class ModelessForm
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
                  this.Export = new System.Windows.Forms.Button();
                  this.Select = new System.Windows.Forms.Button();
                  this.Cancel = new System.Windows.Forms.Button();
                  this.panel1 = new System.Windows.Forms.Panel();
                  this.panel1.SuspendLayout();
                  this.SuspendLayout();
                  // 
                  // Export
                  // 
                  this.Export.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
                  this.Export.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
                  this.Export.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                  this.Export.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                  this.Export.ForeColor = System.Drawing.Color.Gray;
                  this.Export.Location = new System.Drawing.Point(26, 221);
                  this.Export.Name = "Export";
                  this.Export.Size = new System.Drawing.Size(480, 108);
                  this.Export.TabIndex = 7;
                  this.Export.Text = "Export OBJ";
                  this.Export.UseVisualStyleBackColor = true;
                  this.Export.Click += new System.EventHandler(this.Export_Click);
                  // 
                  // Select
                  // 
                  this.Select.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
                  this.Select.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
                  this.Select.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
                  this.Select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                  this.Select.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                  this.Select.ForeColor = System.Drawing.Color.Gray;
                  this.Select.Location = new System.Drawing.Point(26, 81);
                  this.Select.Name = "Select";
                  this.Select.Size = new System.Drawing.Size(480, 112);
                  this.Select.TabIndex = 8;
                  this.Select.Text = "Select Element";
                  this.Select.UseVisualStyleBackColor = true;
                  this.Select.Click += new System.EventHandler(this.Select_Click_1);
                  // 
                  // Cancel
                  // 
                  this.Cancel.BackColor = System.Drawing.Color.Gainsboro;
                  this.Cancel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
                  this.Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
                  this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                  this.Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                  this.Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                  this.Cancel.Location = new System.Drawing.Point(373, 25);
                  this.Cancel.Name = "Cancel";
                  this.Cancel.Size = new System.Drawing.Size(140, 45);
                  this.Cancel.TabIndex = 2;
                  this.Cancel.Text = "Cancel";
                  this.Cancel.UseVisualStyleBackColor = false;
                  this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
                  // 
                  // panel1
                  // 
                  this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
                  this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                  this.panel1.Controls.Add(this.Cancel);
                  this.panel1.Location = new System.Drawing.Point(-8, 444);
                  this.panel1.Name = "panel1";
                  this.panel1.Size = new System.Drawing.Size(547, 105);
                  this.panel1.TabIndex = 9;
                  // 
                  // ModelessForm
                  // 
                  this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
                  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                  this.BackColor = System.Drawing.SystemColors.ControlLightLight;
                  this.ClientSize = new System.Drawing.Size(526, 536);
                  this.Controls.Add(this.panel1);
                  this.Controls.Add(this.Select);
                  this.Controls.Add(this.Export);
                  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                  this.Location = new System.Drawing.Point(400, 400);
                  this.MaximizeBox = false;
                  this.MinimizeBox = false;
                  this.Name = "ModelessForm";
                  this.ShowIcon = false;
                  this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                  this.Text = "Export OBJ";
                  this.Load += new System.EventHandler(this.ModelessForm_Load);
                  this.panel1.ResumeLayout(false);
                  this.ResumeLayout(false);

            }

            #endregion
            private System.Windows.Forms.Button Export;
            private new System.Windows.Forms.Button Select;
            private System.Windows.Forms.Button Cancel;
            private System.Windows.Forms.Panel panel1;
      }
}