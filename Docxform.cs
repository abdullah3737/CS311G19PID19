using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Plagiarism_Checker_Tool
{
    public partial class Docxform : Form
    {
        public Docxform()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Plagform obj = new Plagform();

            obj.Show();
        }
        static void plag_doc(string path1, string path2, string filename1, string filename2)
        {
            // File Reading 1
            string[] data1 = new string[100];
            int z = 0;
            int count1 = 0;
            // Open the text file using a stream reader.
            using (StreamReader sr = new StreamReader(@path1))
            {

                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();

                string[] sentences = line.Split('.');

                using (StreamWriter newFile = new StreamWriter(filename1))

                    for (int i = 0; i < sentences.Length; i++)
                    {
                        if (sentences[i].Length != 0)
                        {
                            string outString = sentences[i].Trim() + ".";
                            newFile.WriteLine(outString);
                            data1[z] = outString;
                            /*Console.WriteLine(data1[z]);*/
                            z++;
                            count1++;
                        }
                    }
            }
            //File Reading 2
            string[] data2 = new string[100];
            int y = 0;
            int count2 = 0;
            // Open the text file using a stream reader.
            using (StreamReader sr = new StreamReader(@path2))
            {

                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();

                string[] sentences = line.Split('.');

                using (StreamWriter newFile = new StreamWriter(filename2))

                    for (int i = 0; i < sentences.Length; i++)
                    {
                        if (sentences[i].Length != 0)
                        {
                            string outString = sentences[i].Trim() + ".";
                            newFile.WriteLine(outString);
                            data2[y] = outString;
                            /*Console.WriteLine(data2[y]);*/
                            y++;
                            count2++;
                        }
                    }
                long length1 = new FileInfo(filename1).Length;
                long length2 = 0;
               /* for (int i = 0; i < count1; i++)
                {
                    int val = 0;
                    val = data1[i].Length;
                    length1 += val;
                }*/
                string[] plag_lcs_final = new string[100];
                Console.Write(Environment.NewLine);
                /*Console.WriteLine("Computed Plagarised Sentences Found");*/

                Console.WriteLine("===========================");
                Console.Write(Environment.NewLine);

                //Creating a text file to store plagarised text on dektop
                string fileName3 = @"C:\Users\jahangir\Desktop\plagarism.txt";

                try
                {
                    // Check if file already exists. If yes, delete it.     
                    if (File.Exists(fileName3))
                    {
                        File.Delete(fileName3);
                    }

                    // Create a new file     
                    using (FileStream fs = File.Create(fileName3));
                   

                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.ToString());
                }

               
                //1 copy or plagarised file data1
                for (int x1 = 0; x1 < count1; x1++)
                {
                    for (int x2 = 0; x2 < count2; x2++)
                    {


                        int y1 = 0, y2 = 0;
                        string C = data1[x1];
                        string D = data2[x2];

                        y1 = C.Length;
                        y2 = D.Length;



                        int[,] lc = new int[y1 + 1, y2 + 1];


                        for (int i = 0; i <= y1; i++)
                        {
                            for (int j = 0; j <= y2; j++)
                            {
                                if (i == 0 || j == 0)
                                    lc[i, j] = 0;
                                else if (C[i - 1] == D[j - 1])
                                {
                                    lc[i, j] = lc[i - 1, j - 1] + 1;

                                }
                                else
                                    lc[i, j] = Math.Max(lc[i - 1, j], lc[i, j - 1]);
                            }
                        }

                        // Following code is used to print LCS 
                        int idx = lc[y1, y2];
                        int p = idx;

                        // Create a character array  
                        // to store the lcs string 
                        char[] final_lcs = new char[idx + 1];

                        // Set the terminating character 
                        final_lcs[idx] = '\0';

                        // Start from the right-most-bottom-most corner 
                        // and one by one store characters in lcs[] 
                        int c = y1, t = y2;
                        while (c > 0 && t > 0)
                        {
                            // If current character in X[] and Y  
                            // are same, then current character  
                            // is part of LCS 
                            if (C[c - 1] == D[t - 1])
                            {
                                // Put current character in result 
                                final_lcs[idx - 1] = C[c - 1];

                                // reduce values of i, j and index 
                                c--;
                                t--;
                                idx--;
                            }

                            // If not same, then find the larger of two and 
                            // go in the direction of larger value 
                            else if (lc[c - 1, t] > lc[c, t - 1])
                                c--;
                            else
                                t--;
                        }


       



                
                            
                           /* string result1 = new string(final_lcs);*/

                            // Print the lcs 
                            if (p == C.Length)
                            {
                            /* Console.Write(" String1: " + C + Environment.NewLine);
                             Console.Write(" String2: " + D + Environment.NewLine);*/
                            string a ;
                                for (int r = 0; r <= p; r++)
                                {
                                    using (StreamWriter writer = new StreamWriter(@"C: \Users\jahangir\Desktop\plagarism.txt"))
                                    
                                    {
                                  /*a= final_lcs[r].ToString();
                                    writer.WriteLine("Hello World");*/

                                    }
                                }
                            
                                /*
                                {
                                    Console.Write(final_lcs[r]);
                                   
                                 }

                                /* Console.Write(Environment.NewLine);
                                 Console.Write(" Percentage of Plagiarism found is 100 %" + Environment.NewLine);*/
                            }
                     
                        
                    }
                }
                if (new FileInfo(fileName3).Length == 0)
                {
                    MessageBox.Show("Plagiarism Not Found");
                }
                else
                {
                    MessageBox.Show("Document Plagarism Found");
                    long percentage;
                    length2 = new FileInfo(fileName3).Length;
                    percentage = (length2 / length1) * 100;
                    MessageBox.Show("Percentage Found is " + percentage);
                }
            }

            }
        

        private void btncheck_Click(object sender, EventArgs e)
        {
            string path1= textBox1.Text;
            string filename1 = textBox3.Text;
            string path2 = textBox2.Text;
            string filename2 = textBox4.Text;
            plag_doc(path1,path2,filename1,filename2);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Docxform_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
