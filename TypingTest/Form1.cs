using System;
using System.Collections.Generic;
using System.Linq;
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

        //sample text as string so it can be changed easier if another passage is required all calcs
        //done in ** in textBox1_TextChanged method
        string sampleText = "When Mr. Bilbo Baggins of Bag End announced that he would shortly be celebrating his eleventy-first birthday with a party of special magnificence, there was much talk and excitement in Hobbiton.Bilbo was very rich and very peculiar, and had been the wonder of the Shire for sixty years, ever since his remarkable disappearance and unexpected return.";
        float wordsPerMin = 0;
        float numOfSec = 0;
        float numOfMs = 0;
        string charUsed = "";

        //This is for reset button, sets all values to 0 and clears text box
        private void button3_Click(object sender, EventArgs e)
        {
            ms = 0;
            sec = 0;
            min = 0;
            wordsPerMin = 0;
            numOfWords = 0;

            label1.Text = 0 + ":" + 0 + ":" + 0.ToString();
            WordsLabel.Text = "0";
            WordsPerMin.Text = "0";
            timer1.Stop();
            PercError.Text = "0";
            textBox1.Enabled = true;
            textBox1.Clear();
        }

        //This controls textbox and timer start and stop. 
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //list to store all chars user has submitted
            List<char> listOfChars = new List<char>();

            //counts the number of characters in real time using Linq to not count white spaces.
            var userInput = textBox1.Text;
            int charCount = userInput.Count(c => !Char.IsWhiteSpace(c));
            charCountOutput.Text = charCount.ToString();


            //This starts the timer once the user begins typing and changes status message
            if (textBox1.Modified)
            {
                timer1.Start();
                StatusMessage_Click(sender, e);
                
            }
           
            //these variables count the whitespace in user text and sample
            int sampleCount = sampleText.Count(y => Char.IsWhiteSpace(y));
            int userSpaceCount = userInput.Count(z => Char.IsWhiteSpace(z));
            //this counts the chars in sample text
            int sampleCharCount = sampleText.Count(x => !Char.IsWhiteSpace(x));

            if ( charCount == sampleCharCount || userSpaceCount == sampleCount + 3  )
            {
                timer1.Stop();
                WordsPerMin_Click(sender,e);
                textBox1.Enabled = false;
                StatusMessage.Text = "You're Done! check your score bellow";

                //once user is done this gather all unique chars and shows in bottom pane
                foreach ( char let in userInput )
                {
                    listOfChars.Add(let);
                }

                var uniqueList = listOfChars.Distinct();

                foreach ( char i in uniqueList)
                {
                    charUsed = charUsed + " " + i;
                }
                UniqueChar.Text = charUsed.ToString();

                SpellCheck(sender, e);
                
            }

        }

        //This counts the num of whitespace in the user textbox to count words
        //***Note extra space at end of sample text is left to correct for this method***

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Using linq I find all white spaces.The correction of plus 1 is in order to count
            //first word due to the fact that there is not whitespace prior to the first word
            var userInput = textBox1.Text;
            int wordCount = userInput.Count(c => Char.IsWhiteSpace(c));
            numOfWords = wordCount;
            numOfWords = numOfWords + 1;
            WordsLabel.Text = numOfWords.ToString();

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

        //This calculates the words per min taking into account seconds and ms
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
            wordsPerMin = (float)Math.Round(wordsPerMin, 2);
            WordsPerMin.Text = wordsPerMin.ToString();

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void charCountOutput_Click(object sender, EventArgs e)
        {

        }

        private void UniqueChar_Click(object sender, EventArgs e)
        {

        }

        private void StatusMessage_Click(object sender, EventArgs e)
        {
            StatusMessage.Text = " Test will end automatically, Good Luck!";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //This is manages the finctionality of stop watch
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = min + ":" + sec + ":" + ms.ToString();
            ms++;
            if (ms > 9)
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

     
        }

        //This calculates the percent error by comparing the input of the user to the sample text
        private void SpellCheck(object sender, EventArgs e)
        {
            var userInput = textBox1.Text;
            var sampleWords = sampleText.Split(' ');
            var userWords = userInput.Split(' ');
            var correctSpell = 0;

            foreach ( string word in userWords )
            {
                if (sampleWords.Contains(word))
                {
                    correctSpell++;
                    
                }
            }
            float correctCount = correctSpell;
            var rawError = correctCount / 57;
            float percentError =( rawError * 100);
            percentError = (float) Math.Round(percentError, 2);
            PercError.Text = percentError + " %".ToString();

        }
    }
}
