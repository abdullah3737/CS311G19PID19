using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plagiarism_Checker_Tool
{
    public partial class Stringform : Form
    {
        public Stringform()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Plagform obj = new Plagform();

            obj.Show();
        }
        static void lcs_string(String C, String D, int m1, int m2)
        {
            int[,] lc = new int[m1 + 1, m2 + 1];


            for (int i = 0; i <= m1; i++)
            {
                for (int j = 0; j <= m2; j++)
                {
                    if (i == 0 || j == 0)
                        lc[i, j] = 0;
                    else if (C[i - 1] == D[j - 1])
                        lc[i, j] = lc[i - 1, j - 1] + 1;
                    else
                        lc[i, j] = Math.Max(lc[i - 1, j], lc[i, j - 1]);
                }
            }

            // Following code is used to print LCS 
            int idx = lc[m1, m2];
            int p = idx;

            // Create a character array  
            // to store the lcs string 
            char[] final_lcs = new char[idx + 1];

            // Set the terminating character 
            final_lcs[idx] = '\0';

            // Start from the right-most-bottom-most corner 
            // and one by one store characters in lcs[] 
            int c = m1, t = m2;
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

            // Print the lcs 
            if (p == C.Length)
            {
                /*Console.Write(" String1: " + C + Environment.NewLine);
                Console.Write(" String2: " + D + Environment.NewLine);
                Console.Write(" Computed LCS = ");*/
                string result = new string(final_lcs);

                for (int r = 0; r <= p; r++)
                {
                    Console.Write(final_lcs[r]);
                    Console.WriteLine($"STRING: {result}");

                }
                MessageBox.Show(" Percentage of Plagiarism found is 100 % ");
                MessageBox.Show($"Plagarised String found = {result}");
                /*Console.Write(Environment.NewLine);
                MessageBox.Show(Environment.NewLine);
                Console.Write(" Percentage of Plagiarism found is 100 %" + Environment.NewLine);*/

                /* MessageBox.Show(Environment.NewLine);*/
            }
            else
            {
                /* Console.Write(" Percentage of Plagiarism found is 0 %");*/
                MessageBox.Show(" Percentage of Plagiarism found is 0 %");
            }
        }

        private void btncheck_Click_1(object sender, EventArgs e)
        {
            string C = richTextBox1.Text;
            int m = C.Length;
            string D = richTextBox2.Text;
            int n = D.Length;
            lcs_string(C, D, m, n);
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

      
    }
}
