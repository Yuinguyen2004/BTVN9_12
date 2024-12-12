using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTVN9_12
{
    public partial class Form1 : Form
    {

        private string currentFilePath = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxFont.Text = "Tahoma";
            comboBoxSize.Text = "14";
            foreach (FontFamily font in new InstalledFontCollection().Families)
            {
                comboBoxFont.Items.Add(font.Name);
            }
            List<int> listSize = new List<int> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach (var s in listSize)
            {
                comboBoxSize.Items.Add(s);
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private int CountWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            var wordSeparators = new[] { ' ', '\t', '\r', '\n' };
            return text.Split(wordSeparators, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (toolStripStatusLabel1 != null)
            {
                int wordCount = CountWords(richTextBox1.Text);
                toolStripStatusLabel1.Text = $"Tổng số từ: {wordCount}";
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void địnhDạngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = true;
            fontDlg.ShowApply = true;
            fontDlg.ShowEffects = true;
            fontDlg.ShowHelp = true;
            if (fontDlg.ShowDialog() != DialogResult.Cancel)
            {
                richTextBox1.ForeColor = fontDlg.Color;
                richTextBox1.Font = fontDlg.Font;
            }
        }

        private void newDocument()
        {
            richTextBox1.Clear();

            richTextBox1.Font = new Font("Tahoma", 14);
            richTextBox1.ForeColor = Color.Black;

            comboBoxFont.SelectedItem = "Tahoma";
            comboBoxSize.SelectedItem = "14";

            toolStripStatusLabel1.Text = "Tổng số từ: 0";
        }

        private void tạoMớiVănBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newDocument();
        }

        private void mởThôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|Rich Text Files (*.rtf)|*.rtf";
                openFileDialog.Title = "Mở tập tin văn bản";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = openFileDialog.FileName;
                        string extension = System.IO.Path.GetExtension(filePath);

                        if (extension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                        {
                            richTextBox1.Text = System.IO.File.ReadAllText(filePath);
                        }
                        else if (extension.Equals(".rtf", StringComparison.OrdinalIgnoreCase))
                        {
                            richTextBox1.LoadFile(filePath, RichTextBoxStreamType.RichText);
                        }

                        currentFilePath = filePath;
                        toolStripStatusLabel1.Text = $"Tập tin đã mở: {filePath}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Không thể mở tập tin: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void lưuVănBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {                
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Rich Text Files (*.rtf)|*.rtf";
                    saveFileDialog.Title = "Lưu tập tin văn bản";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                            currentFilePath = saveFileDialog.FileName; 
                            MessageBox.Show("Lưu văn bản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Không thể lưu tập tin: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                try
                {
                    richTextBox1.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                    MessageBox.Show("Lưu văn bản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể lưu tập tin: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle = currentFont.Style;

                if (currentFont.Bold)
                    newFontStyle &= ~FontStyle.Bold; 
                else
                    newFontStyle |= FontStyle.Bold; 

                richTextBox1.SelectionFont = new Font(currentFont, newFontStyle);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle = currentFont.Style;

                if (currentFont.Italic)
                    newFontStyle &= ~FontStyle.Italic; 
                else
                    newFontStyle |= FontStyle.Italic; 

                richTextBox1.SelectionFont = new Font(currentFont, newFontStyle);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle = currentFont.Style;

                if (currentFont.Underline)
                    newFontStyle &= ~FontStyle.Underline; 
                else
                    newFontStyle |= FontStyle.Underline; 

                richTextBox1.SelectionFont = new Font(currentFont, newFontStyle);
            }
        }
    }
}
