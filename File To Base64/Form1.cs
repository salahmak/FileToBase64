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
        string base64 { get; set; }   //decalres base64 string to use in other methods
        bool ConvertToBase64()
        {
            bool xd = false;
            string newpath = textBox1.Text;  //gets path from text box if changed
            if (!File.Exists(newpath))
            {
                MessageBox.Show("File Not Found\nPlease Check Again");
            }
            else
            {
                byte[] bytearray = File.ReadAllBytes(newpath);       //reads file 
                base64 = Convert.ToBase64String(bytearray);          //converts it to base64 
                xd = true;
            }
            return xd;
            
        }
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
            if (ConvertToBase64() == true)
            {
                Clipboard.SetText(base64);                           //sets clipboard to base64 of the file
                MessageBox.Show("Clipboard Set To Base64 of The File");
            } 
                   
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            if (ConvertToBase64() == true)
            {
                string newpath = textBox1.Text;    //gets path from text box if changed
                string dir = Path.GetDirectoryName(newpath);     //gets directory of filepath (initial save dir)

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
                    MessageBox.Show("File Not Saved.");
                }
            }
        }


    

        private void SaveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (ConvertToBase64() == true)
            {
                textBox5.Clear();                                   //clears textbox if its not
                textBox5.Paste(Convert.ToString(base64.Length));    //pastes the string length
            }
            else
            {
                textBox5.Clear();            //clears file if its not
                textBox5.Paste("not found"); //paste this
            }
             
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {

            textBox3.Clear();       //clears first half of string textbox 
            textBox4.Clear();       //clears second half of string textbox 
            try
            {
                if (ConvertToBase64() == true)
                {
                    int base1 = 0;            //base num for counter in foreach loop (firsthalf)
                    int base2 = 0;            //base num for counter in foreach loop (secondhalf)
                    int max = base64.Length;     //pastes the string length
                    int split = int.Parse(textBox2.Text);
                    


                    string[] firsthalf = new string[split];   //declares first half of splitter
                    string[] secondhalf = new string[max - split];   ////declares second half of splitter


                    foreach (char letter in base64)     //pastes each char of base64 string in either firsthalf or second half
                    {
                        if (base1 < split)   //first half
                        {
                            string a = Convert.ToString(letter);
                            firsthalf[base1] = a;
                            base1++;
                        }
                        else    //second half
                        {
                            string a = Convert.ToString(letter);
                            secondhalf[base2] = a;
                            base2++;
                        }
                    }
                    string output1 = string.Join("", firsthalf);  //joins string[] to make it string
                    string output2 = string.Join("", secondhalf); //joins string[] to make it string
                    textBox3.Paste(output1);       //pastes output to textbox
                    textBox4.Paste(output2);       //pastes output to textbox
                }
            }
            catch (Exception)
            {
                MessageBox.Show("fucking enter a real number in split box");
            }
            
    

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox3.Text);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox4.Text);
        }
    }
}
