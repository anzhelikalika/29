using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReplaceWordInFile
{
    public partial class Form1 : Form
    {
        private string fileContent;
        private string filePath;

        private string replace;
        private string paste;
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            button1.Enabled = false;
            textBox2.Enabled = false;
            button2.Enabled = false;
            
            openFileDialog1.InitialDirectory = "c:\\";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                
                var fileStream = openFileDialog1.OpenFile();

                label1.ForeColor = Color.Black;

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }
                
                richTextBox1.Text = fileContent;
                textBox1.Enabled = true;
                button1.Enabled = true;

                label1.Text = "Файл открыт";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            replace = textBox1.Text;
            label1.Text = "Поиск...";
            if (!string.IsNullOrWhiteSpace(replace) && fileContent.ToLower().IndexOf(replace) != -1)
            {
                label1.ForeColor = Color.Green;
                label1.Text = "Слово найдено";
                textBox2.Enabled = true;
                button2.Enabled = true;
            }
            else
            {
                label1.ForeColor = Color.Red;
                label1.Text = "Слово не найдено";
                
                textBox2.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            paste = textBox2.Text;
            if (!string.IsNullOrEmpty(paste))
            {
                fileContent = fileContent = Regex.Replace(fileContent, replace, paste, RegexOptions.IgnoreCase); 
                richTextBox2.Text = fileContent;
                button4.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(fileContent);
                sw.Close();
                
                label1.Text = "Успешно!";
            }

            textBox2.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
        }
    }
}
