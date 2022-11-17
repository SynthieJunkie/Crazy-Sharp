
namespace CrazySharp
{
	partial class Form1
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.button_mix = new System.Windows.Forms.Button();
			this.button_new = new System.Windows.Forms.Button();
			this.label_time = new System.Windows.Forms.Label();
			this.label_points = new System.Windows.Forms.Label();
			this.label_info = new System.Windows.Forms.Label();
			this.label_combinations = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button_mix
			// 
			this.button_mix.Location = new System.Drawing.Point(170, 10);
			this.button_mix.Name = "button_mix";
			this.button_mix.Size = new System.Drawing.Size(150, 30);
			this.button_mix.TabIndex = 1;
			this.button_mix.Text = "Mix";
			this.button_mix.UseVisualStyleBackColor = true;
			// 
			// button_new
			// 
			this.button_new.Location = new System.Drawing.Point(10, 10);
			this.button_new.Name = "button_new";
			this.button_new.Size = new System.Drawing.Size(150, 30);
			this.button_new.TabIndex = 1;
			this.button_new.Text = "New";
			this.button_new.UseVisualStyleBackColor = true;
			// 
			// label_time
			// 
			this.label_time.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label_time.Location = new System.Drawing.Point(330, 10);
			this.label_time.Name = "label_time";
			this.label_time.Size = new System.Drawing.Size(150, 30);
			this.label_time.TabIndex = 2;
			this.label_time.Text = "Time: 00:00:00";
			this.label_time.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label_points
			// 
			this.label_points.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label_points.Location = new System.Drawing.Point(490, 10);
			this.label_points.Name = "label_points";
			this.label_points.Size = new System.Drawing.Size(150, 30);
			this.label_points.TabIndex = 2;
			this.label_points.Text = "Points: 0";
			this.label_points.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label_info
			// 
			this.label_info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label_info.Location = new System.Drawing.Point(10, 690);
			this.label_info.Name = "label_info";
			this.label_info.Size = new System.Drawing.Size(310, 30);
			this.label_info.TabIndex = 2;
			this.label_info.Text = "Welcome to Crazy Sharp.";
			this.label_info.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label_combinations
			// 
			this.label_combinations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label_combinations.Location = new System.Drawing.Point(330, 690);
			this.label_combinations.Name = "label_combinations";
			this.label_combinations.Size = new System.Drawing.Size(310, 30);
			this.label_combinations.TabIndex = 2;
			this.label_combinations.Text = "Combinations: 0";
			this.label_combinations.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Form1
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(650, 730);
			this.Controls.Add(this.label_points);
			this.Controls.Add(this.label_combinations);
			this.Controls.Add(this.label_info);
			this.Controls.Add(this.label_time);
			this.Controls.Add(this.button_new);
			this.Controls.Add(this.button_mix);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Crazy Sharp";
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button button_mix;
		private System.Windows.Forms.Button button_new;
		private System.Windows.Forms.Label label_time;
		private System.Windows.Forms.Label label_points;
		private System.Windows.Forms.Label label_info;
		private System.Windows.Forms.Label label_combinations;
	}
}

