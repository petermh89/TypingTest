using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TypingTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Variables for handling stopwatch, number of words and calculation of words per min
        int min, sec, ms = 0;
        float numOfWords = 0;
        float numOfMin = 0;
        string sampleText = "When Mr. Bilbo Baggins of Bag End announced that he would shortly be celebrating his eleventy-first birthday with a party of special magnificence, there was much talk and excitement in Hobbiton. Bilbo was very rich and very peculiar, and had been the wonder of the Shire for sixty years, ever since his remarkable disappearance and unexpected return. The riches he had brought back from his travels had now become a local legend, and it was popularly believed, whatever the old folk might say, that the Hill at Bag End was full of tunnels stuffed with treasure.And if that was not enough for fame, there was also his prolonged vigour to marvel at. Time wore on, but it seemed to have little effect on Mr. Baggins. At ninety he was much the same as at fifty. At ninety-nine they began to call him well-preserved; but unchanged would have been nearer the mark. There were some that shook their heads and thought this was too much of a good thing; it seemed unfair that anyone should possess (apparently) perpetual youth as well as (reputedly) inexhaustible wealth.  ";
        float wordsPerMin = 0;
        float numOfSec = 0;
        float numOfMs = 0;



        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ms = 0;
            sec = 0;
            min = 0;
            //hour = 0;
            wordsPerMin = 0;
            numOfWords = 0;

            label1.Text = 0 + ":" + 0 + ":" + 0.ToString();
            WordsLabel.Text = "0";
            WordsPerMin.Text = "0";
            textBox1.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int sampleCount = 0;
            for ( int i = 0; i < sampleText.Length; i++ )
            {
                if ( sampleText[i] == ' ')
                {
                    sampleCount++;
                }
            }

            if (textBox1.Modified)
            {
                timer1.Start();
                
            }

            if (numOfWords >= sampleCount)
            {
                timer1.Stop();
                WordsPerMin_Click(sender,e);
                textBox1.Enabled = false;
            }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsWhiteSpace(e.KeyChar))
            {
                numOfWords++;
                WordsLabel.Text = numOfWords.ToString();                
            }

            //if (e.KeyChar == (char)Keys.Back)
            //{
            //        numOfWords--;
            //        WordsLabel.Text = numOfWords.ToString();


            //}
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void WordsPerMin_Click(object sender, EventArgs e)
        {
            numOfSec = sec;
            numOfMs = ms;

            if ( sec != 0 && ms == 0 )
            {
                numOfSec = numOfSec / 60;
                numOfMin = min + numOfSec;               
            }

            if ( sec == 0 && ms != 0 )
            {
                numOfMs = numOfMs / 60;
                numOfSec = numOfMs / 60;
                numOfMin = min + numOfSec;               
            }

            if ( sec != 0 && ms != 0)
            {
                numOfMs = numOfMs / 60;
                numOfSec = (numOfSec + numOfMs) / 60; 
                numOfMin = min + numOfSec;               
            }

            wordsPerMin = numOfWords / numOfMin;
            WordsPerMin.Text = wordsPerMin.ToString();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = min + ":" + sec + ":" + ms.ToString();
            ms++;
            if (ms > 10)
            {
                sec++;
                ms = 0;
            }

            else
            {
                ms++;
            }

            if (sec > 59)
            {
                min++;
                sec = 0;
            }

            //if (min > 59)
            //{
            //    hour++;
            //    min = 0;
            //}
        }
    }
}
