using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace itxSync
{
	public partial class Form1 : Form
	{
		public string settingFile;

		public Form1()
		{
			InitializeComponent();
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			string path = Directory.GetCurrentDirectory();
			settingFile = path + "\\itxSync.txt";

			if (!File.Exists(settingFile))
			{
				File.Create(settingFile);
			}
			else
			{
				string[] lines = File.ReadAllLines(settingFile);
				for (var i = 0; i < lines.Count(); i++)
				{
					var line = lines[i].Trim().Split('|');
					textBox1.Text += line[0].Trim() + "\n";
					textBox2.Text += line[1].Trim() + "\n";
				}
				
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			
			var text1 = textBox1.Text.Trim().Split('\n');
			var text2 = textBox2.Text.Trim().Split('\n');
			
			for (var i=0; i<text1.Count(); i++)
			{
				if (!string.IsNullOrEmpty(text1[i]) && !string.IsNullOrEmpty(text2[i]))
				{

					string logs = "";
					string file1 = text1[i].Trim();
					string file2 = text2[i].Trim();

					try
					{
						var tmp = file1.Split('\\');
						var fname = tmp[tmp.Length - 1];
						file2 = file2 + "\\" + fname;

						File.Copy(file1, file2, true);
					}
					catch(Exception ex)
					{
						logs = "ERROR - " + ex.Message + " - ";
					}

					logs += DateTime.Now.ToString("u").ToString().Replace("Z", "") + " - " + file1 + " --> " + file2 + "\r\n";
					textBox3.Text = logs + textBox3.Text;

				}
			}

		}
		

		private void button2_Click(object sender, EventArgs e)
		{
			textBox3.Text = "";
		}

		private void button3_Click(object sender, EventArgs e)
		{
			textBox1.Text = "";
			textBox2.Text = "";
			textBox3.Text = "";
		}

		private void browse1_Click(object sender, EventArgs e)
		{
			
			DialogResult result = openFileDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				textBox1.Text += openFileDialog1.FileName + "\n";
			}
			
		}
		private void browse2_Click(object sender, EventArgs e)
		{
			DialogResult result = folderBrowserDialog1.ShowDialog();
			if (result == DialogResult.OK)
			{
				textBox2.Text += folderBrowserDialog1.SelectedPath + "\n";
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			var text1 = textBox1.Text.Trim().Split('\n');
			var text2 = textBox2.Text.Trim().Split('\n');

			string lines = "";
			for (var i = 0; i < text1.Count(); i++)
			{
				if (!string.IsNullOrEmpty(text1[i].Trim()) && !string.IsNullOrEmpty(text2[i].Trim()))
				{
					lines += text1[i].Trim() + "|" + text2[i].Trim() + "\r\n";
				}
			}

			File.WriteAllText(settingFile, lines);
			textBox3.Text = "Saved";
		}

	}
}
