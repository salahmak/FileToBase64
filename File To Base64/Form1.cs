using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_To_Base64
{
    //26/06/2019
    public partial class Form1 : Form
    {
        string path { get; set; }     //declares path to use it in other methods
        public Form1()
        {
            InitializeComponent();
        }

        private void Panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void Panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);  //gets draganddrop dropped file path in string[]
            
            foreach (string file in files)          //just gets path name
            {
                path = file;                        //declares path as string
                textBox1.Clear();                   //clears textbox before pasting it again if dragged&dropped again
            }
            textBox1.Paste(path);                   //pastes path in textbox


        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            
            
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string newpath = textBox1.Text;  //gets path from text box if changed
            if (!File.Exists(newpath))
            {
                MessageBox.Show("File Not Found\nPlease Check Again");
            }
            else
            {
                byte[] bytearray = File.ReadAllBytes(newpath);       //reads file 
                string base64 = Convert.ToBase64String(bytearray);   //converts it to base64 
                Clipboard.SetText(base64);                           //sets clipboard to base64 of the file
                MessageBox.Show("Clipboard Set To Base64 of The File");
            }
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string newpath = textBox1.Text;  //gets path from text box if changed
            if (!File.Exists(newpath))
            {
                MessageBox.Show("File Not Found\nPlease Check Again");
            }
            else
            {
                byte[] bytearray = File.ReadAllBytes(newpath);      //reads file 
                string base64 = Convert.ToBase64String(bytearray);  //converts it to base64

                string dir = Path.GetDirectoryName(newpath);     //gets directory of filepath (initial save dir)
                string filename = Path.GetFileName(newpath);     //gets file name of filepath (initial filename)

                saveFileDialog1.Title = "Save As";                         //dialog title
                saveFileDialog1.InitialDirectory = dir;                    //where to save file
                saveFileDialog1.DefaultExt = "txt";                        //default extension

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)       //open savefile dialog
                {
                    File.WriteAllText(saveFileDialog1.FileName, base64);   //writes base64 to filename
                }
                if (File.Exists(saveFileDialog1.FileName))
                {
                    MessageBox.Show("Saved File to Location  " + saveFileDialog1.FileName);
                }
                else
                {
                    MessageBox.Show("Something Went Wrong\nPlease Try Again");
                }
            }

            

        }

        private void SaveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }
    }
}
