using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        System.IO.StreamWriter file;
        SaveFileDialog saveFileDialog1;
        string fontName;
        FontFamily fontFamily;
        FontStyle fontStyle;
        float fontSize;
        int col = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetTheme(Color.FromArgb(39, 39, 39), Color.FromArgb(44, 44, 44), Color.White);
            using (InstalledFontCollection col = new InstalledFontCollection())
            {
                foreach (FontFamily fa in col.Families)
                {
                    comboBox1.Items.Add(fa.Name);
                }
            }
            comboBox2.Items.Add("Regular");
            comboBox2.Items.Add("Italic");
            comboBox2.Items.Add("Bold");
            comboBox2.Items.Add("Bold Italic");

            for (int i = 0; i < 5; i++)
            {
                comboBox3.Items.Add(i + 7);
            }
            for (int i = 5; i + 7 < 28; i += 2)
            {
                comboBox3.Items.Add(i + 7);
            }
            comboBox3.Items.Add(36);
            comboBox3.Items.Add(49);
            comboBox3.Items.Add(72);
            comboBox1.Text = "Consolas";
            comboBox2.Text = "Regular";
            comboBox3.Text = "11";
            comboBox1.SelectedItem = "Consolas";
            comboBox2.SelectedItem = "Regular";
            comboBox3.SelectedItem = 11;

            panel1.BorderStyle = BorderStyle.None;
            panel2.BorderStyle = BorderStyle.None;
            panel3.BorderStyle = BorderStyle.None;
            toolStripSeparator2.BackColor = Color.FromArgb(44, 44, 44);
            toolStripSeparator1.BackColor = Color.FromArgb(44, 44, 44);
            label8.Text = "UTF-8";
        }

        private void GetItems(ToolStripMenuItem item)
        {
            Console.WriteLine(item.GetType());
        }

        private void enregistrerSousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void SaveAs()
        {
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "All files (*.*.*)|*.txt|txt files (*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = new System.IO.StreamWriter(saveFileDialog1.FileName.ToString());
                file.WriteLine(richTextBox1.Text);
                file.Close();
            }
        }

        private void enregistrerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file != null)
            {
                file = new System.IO.StreamWriter(saveFileDialog1.FileName.ToString());
                file.WriteLine(richTextBox1.Text);
                file.Close();
            }
            else
            {
                SaveAs();
            }
        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of the specified file
                    filePath = openFileDialog.FileName;

                    // Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                        richTextBox1.Text = fileContent;
                    }
                }
            }
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void fermerLaFenêtreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void nouvelleFenêtreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = @"TextEditor.exe";
            myProcess.Start();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (file != null)
            {
                file = new System.IO.StreamWriter(saveFileDialog1.FileName.ToString());
                file.WriteLine(richTextBox1.Text);
                file.Close();
            }
            else
            {
                SaveAs();
            }
        }

        private void zoomAvantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ZoomFactor += 0.5f;
            float zoomFactor = richTextBox1.ZoomFactor * 100;
            label7.Text = zoomFactor.ToString() + "%";
        }

        private void zoomArrièreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ZoomFactor -= 0.5f;
            float zoomFactor = richTextBox1.ZoomFactor * 100;
            label7.Text = zoomFactor.ToString() + "%";
        }

        private void restaurerLeZoomParDéfaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ZoomFactor = 1f;
            float zoomFactor = richTextBox1.ZoomFactor * 100;
            label7.Text = zoomFactor.ToString() + "%";
        }

        private void collerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String Text = Clipboard.GetText();
            richTextBox1.Text += Text;
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
        }

        private void copierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String Text = richTextBox1.SelectedText;
            if (Text != null)
            {
                Clipboard.SetText(Text);
            }
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
        }

        private void couperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String Text = richTextBox1.SelectedText;
            Clipboard.SetText(Text);
            richTextBox1.SelectedText = "";
        }

        private void supprimerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void toutSéléctionnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectionLength = richTextBox1.Text.Length;
        }

        private void fichierToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            // fichierToolStripMenuItem.BackColor = Color.FromArgb(44,44,44);
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            panel4.BackColor = Color.FromArgb(65, 65, 65);
            panel4.BorderStyle = BorderStyle.None;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            panel4.BackColor = Color.FromArgb(44, 44, 44);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel5.Visible = true;
            panel6.Visible = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel5.Visible = false;
            panel6.Visible = false;
        }

        private void SetFont()
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox3.SelectedItem != null && float.TryParse(comboBox3.SelectedItem.ToString(), out fontSize))
            {
                fontName = comboBox1.SelectedItem.ToString();
                fontFamily = new FontFamily(fontName);
                fontStyle = comboBox2.SelectedItem.ToString().ToFontStyle();
                Font font = new Font(fontFamily, fontSize, fontStyle);
                richTextBox1.SelectionFont = font;
                richTextBox1.Font = font;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFont();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFont();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFont();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            label9.Text = "Col" + richTextBox1.Text.Length;
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            float zoomFactor = richTextBox1.ZoomFactor * 100;
            label7.Text = zoomFactor.ToString() + "%";

            if (e.KeyCode == Keys.Enter)
            {
                col++;
            }
            label6.Text = "Ln " + col + ",";
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem.ToString() == "Clair")
            {
                SetTheme(Color.White, Color.LightGray, Color.Black);
            }
            else
            {
                SetTheme(Color.FromArgb(39, 39, 39), Color.FromArgb(44, 44, 44), Color.White);
            }
        }

        private void SetTheme(Color Main, Color Menu, Color text)
        {
            menuStrip1.BackColor = Menu;
            panel1.BackColor = Menu;
            panel4.BackColor = Menu;
            panel8.BackColor = Menu;
            panel9.BackColor = Menu;
            panel7.BackColor = Menu;
            panel5.BackColor = Menu;
            pictureBox1.BackColor = Menu;

            panel2.BackColor = Main;
            panel3.BackColor = Main;
            panel6.BackColor = Main;
            richTextBox1.BackColor = Main;

            richTextBox1.ForeColor = text;
            ForeColor = text;
            fichierToolStripMenuItem.ForeColor = text;
            ouvrirToolStripMenuItem.ForeColor = text;
            affichageToolStripMenuItem.ForeColor = text;
            modifierToolStripMenuItem.ForeColor = text;

            foreach (ToolStripMenuItem toolItem in menuStrip1.Items)
            {
                toolItem.BackColor = Menu;
                GetItems(toolItem);
                for (int i = 0; i < toolItem.DropDownItems.Count; i++)
                {
                    toolItem.DropDownItems[i].BackColor = Menu;
                    toolItem.DropDownItems[i].ForeColor = text;
                }
            }
        }
    }

    public static class Extensions
    {
        public static FontStyle ToFontStyle(this string a)
        {
            if (a == "Regular")
            {
                return FontStyle.Regular;
            }
            else if (a == "Bold")
            {
                return FontStyle.Bold;
            }
            else if (a == "Italic")
            {
                return FontStyle.Italic;
            }
            else if (a == "Bold Italic")
            {
                return FontStyle.Bold | FontStyle.Italic;
            }
            else
            {
                return FontStyle.Regular;
            }
        }
    }
}
