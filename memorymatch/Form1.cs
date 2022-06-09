using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memorymatch
{
    /* This application is a memory matching game in which a pattern is displayed
     * for 3 seconds. The user must remember this pattern and, once the pattern
     * is removed from the screen, click the buttons and recreate the pattern
     * within 5 seconds. For each pattern the user correctly recreates, the score
     * (in the top right label) increases by 1. (There is no penalty for incorrectly
     * recreating the pattern other than the shame of the text colour of the score
     * being red.)
     * There is a pause function which is activated when the user clicks the
     * countdown timer. The patterns and timers will reset, but the score remains.
     * The user can click the button which says "unpause" that appears when the 
     * game is paused to unpause the game.
     * An interesting part of my learning from this assignment is the exploration 
     * of the properties and parts of code I could manipulate to create a visually
     * appealing application. 
     * The biggest challenge was adding function to all of the 25 buttons and
     * ensuring that each button remains seperate (the colour change and pattern
     * booleans).
     */

    public partial class Form1 : Form
    {
        // Timer for game component
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        int timerInterval = 3000;

        // Timer for countdown component
        static System.Windows.Forms.Timer countdownTimer = new System.Windows.Forms.Timer();
        int secondsPassed = 0;

        // Boolean that is true when the pattern is displayed and false when the pattern is not displayed.
        static bool patternOn = true;

        // Booleans for if each button is on or off in the pattern
        bool oneSet;
        bool twoSet;
        bool threeSet;
        bool fourSet;
        bool fiveSet;
        bool sixSet;
        bool sevenSet;
        bool eightSet;
        bool nineSet;
        bool tenSet;
        bool oneBSet;
        bool twoBSet;
        bool threeBSet;
        bool fourBSet;
        bool fiveBSet;
        bool sixBSet;
        bool sevenBSet;
        bool eightBSet;
        bool nineBSet;
        bool tenBSet;
        bool oneCSet;
        bool twoCSet;
        bool threeCSet;
        bool fourCSet;
        bool fiveCSet;

        // Booleans for each button if the user clicked the button
        bool oneClick = false;
        bool twoClick = false;
        bool threeClick = false;
        bool fourClick = false;
        bool fiveClick = false;
        bool sixClick = false;
        bool sevenClick = false;
        bool eightClick = false;
        bool nineClick = false;
        bool tenClick = false;
        bool oneBClick = false;
        bool twoBClick = false;
        bool threeBClick = false;
        bool fourBClick = false;
        bool fiveBClick = false;
        bool sixBClick = false;
        bool sevenBClick = false;
        bool eightBClick = false;
        bool nineBClick = false;
        bool tenBClick = false;
        bool oneCClick = false;
        bool twoCClick = false;
        bool threeCClick = false;
        bool fourCClick = false;
        bool fiveCClick = false;

        // An integer that stores how many clicked buttons correspond to the pattern.
        int incorrect;

        // An integer that stores how many times the user correctly clicks the pattern.
        int totalCorrect = 0;

        // A boolean that is true when the game is paused and false when the game is not paused.
        bool pause = false;

        public Form1()
        {
            InitializeComponent();

            // Add a method to run every time Application is idle:
            Application.Idle += HandleApplicationIdle;

            /* Adds the event and the event handler for the method that will 
             * process the timer event to the timer. 
             */
            myTimer.Tick += new EventHandler(TimerEventProcessor);

            // Sets the timer interval to 4 seconds.
            myTimer.Interval = timerInterval;
            myTimer.Start();

            // Sets the countdown timer.
            countdownTimer.Tick += new EventHandler(Countdown);
            countdownTimer.Interval = 1000;
            countdownTimer.Start();

            setPattern();
            resetColours();
            setPatternColour();
        }

        // This is the method to run when the timer is raised.
        private void TimerEventProcessor(Object sender, EventArgs e)
        {
            myTimer.Stop();
            countdownTimer.Stop();

            // Restarts the timer.
            myTimer.Enabled = true;
            countdownTimer.Enabled = true;

            /* This changes the time for each section, 3000 for pattern
             * display, 5000 for pattern choosing, and 2000 for the answers.
             */
            if (timerInterval == 3000)
            {
                // 6 seconds for user pattern choosing.
                timerInterval = 6000;
                patternOn = false;

                resetColours();
            }
            else if (timerInterval == 6000)
            {
                // 2 seconds for answers and displaying correct or incorrect.
                timerInterval = 2000;
                patternOn = true;

                resetColours();
                setAnswerColour();

                if (incorrect > 0)
                {
                    //this.BackColor = Color.FromArgb(85, 53, 71); // red
                    label2.Text = "X";
                    label2.ForeColor = Color.FromArgb(255, 105, 122);
                    label2.Location = new Point(106, 16);

                    label3.ForeColor = Color.FromArgb(255, 105, 122);
                }
                else
                {
                    //this.BackColor = Color.FromArgb(31, 67, 57); // green
                    label2.Text = "O";
                    label2.ForeColor = Color.FromArgb(90, 255, 145);
                    label2.Location = new Point(108, 16);

                    totalCorrect++;

                    label3.ForeColor = Color.FromArgb(90, 255, 145);
                }

                label3.Text = "" + totalCorrect;

                incorrect = 0;
            }
            else if (timerInterval == 2000)
            {
                // 3 seconds for displaying the pattern.
                timerInterval = 3000;
                patternOn = true;

                this.BackColor = Color.FromArgb(41, 41, 41); // grey
                label2.Text = "";

                setPattern();
                resetColours();
                setPatternColour();
                resetClicks();
            }

            myTimer.Interval = timerInterval;

            // Resets the countdown display.
            secondsPassed = 0;
        }

        private void Countdown(Object sender, EventArgs e)
        {
            secondsPassed++;
        }

        // Method that will be called every time program is finished running events
        private void HandleApplicationIdle(object sender, EventArgs e)
        {
            // Displays the countdown label 
            label1.Text = "" + (timerInterval / 1000 - secondsPassed);
        }

        // Sets the pattern of the buttons.
        private void setPattern()
        {
            System.Random rnd = new System.Random();

            // Generates random booleans.
            oneSet = (rnd.NextDouble() >= 0.5);
            twoSet = (rnd.NextDouble() >= 0.5);
            threeSet = (rnd.NextDouble() >= 0.5);
            fourSet = (rnd.NextDouble() >= 0.5);
            fiveSet = (rnd.NextDouble() >= 0.5);
            sixSet = (rnd.NextDouble() >= 0.5);
            sevenSet = (rnd.NextDouble() >= 0.5);
            eightSet = (rnd.NextDouble() >= 0.5);
            nineSet = (rnd.NextDouble() >= 0.5);
            tenSet = (rnd.NextDouble() >= 0.5);
            oneBSet = (rnd.NextDouble() >= 0.5);
            twoBSet = (rnd.NextDouble() >= 0.5);
            threeBSet = (rnd.NextDouble() >= 0.5);
            fourBSet = (rnd.NextDouble() >= 0.5);
            fiveBSet = (rnd.NextDouble() >= 0.5);
            sixBSet = (rnd.NextDouble() >= 0.5);
            sevenBSet = (rnd.NextDouble() >= 0.5);
            eightBSet = (rnd.NextDouble() >= 0.5);
            nineBSet = (rnd.NextDouble() >= 0.5);
            tenBSet = (rnd.NextDouble() >= 0.5);
            oneCSet = (rnd.NextDouble() >= 0.5);
            twoCSet = (rnd.NextDouble() >= 0.5);
            threeCSet = (rnd.NextDouble() >= 0.5);
            fourCSet = (rnd.NextDouble() >= 0.5);
            fiveCSet = (rnd.NextDouble() >= 0.5);
        }

        // Sets the colours of the buttons corresponding to the pattern.
        private void setPatternColour()
        {
            if (oneSet == true)
            {
                button1.BackColor = Color.FromArgb(233, 180, 90); // orange
            }
            if (twoSet == true)
            {
                button2.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (threeSet == true)
            {
                button3.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (fourSet == true)
            {
                button4.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (fiveSet == true)
            {
                button5.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (sixSet == true)
            {
                button6.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (sevenSet == true)
            {
                button7.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (eightSet == true)
            {
                button8.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (nineSet == true)
            {
                button9.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (tenSet == true)
            {
                button10.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (oneBSet == true)
            {
                button11.BackColor = Color.FromArgb(233, 180, 90); // orange
            }
            if (twoBSet == true)
            {
                button12.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (threeBSet == true)
            {
                button13.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (fourBSet == true)
            {
                button14.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (fiveBSet == true)
            {
                button15.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (sixBSet == true)
            {
                button16.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (sevenBSet == true)
            {
                button17.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (eightBSet == true)
            {
                button18.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (nineBSet == true)
            {
                button19.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (tenBSet == true)
            {
                button20.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (oneCSet == true)
            {
                button21.BackColor = Color.FromArgb(233, 180, 90); // orange
            }
            if (twoCSet == true)
            {
                button22.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (threeCSet == true)
            {
                button23.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (fourCSet == true)
            {
                button24.BackColor = Color.FromArgb(233, 180, 90);
            }
            if (fiveCSet == true)
            {
                button25.BackColor = Color.FromArgb(233, 180, 90);
            }
        }

        // Sets the colours of the buttons corresponding to the pattern.
        private void setAnswerColour()
        {
            if (oneSet == oneClick)
            {
                button1.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button1.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (twoSet == twoClick)
            {
                button2.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button2.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (threeSet == threeClick)
            {
                button3.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button3.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (fourSet == fourClick)
            {
                button4.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button4.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (fiveSet == fiveClick)
            {
                button5.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button5.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (sixSet == sixClick)
            {
                button6.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button6.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (sevenSet == sevenClick)
            {
                button7.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button7.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (eightSet == eightClick)
            {
                button8.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button8.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (nineSet == nineClick)
            {
                button9.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button9.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (tenSet == tenClick)
            {
                button10.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button10.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (oneBSet == oneBClick)
            {
                button11.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button11.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (twoBSet == twoBClick)
            {
                button12.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button12.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (threeBSet == threeBClick)
            {
                button13.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button13.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (fourBSet == fourBClick)
            {
                button14.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button14.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }
            if (fiveBSet == fiveBClick)
            {
                button15.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button15.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (sixBSet == sixBClick)
            {
                button16.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button16.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (sevenBSet == sevenBClick)
            {
                button17.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button17.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (eightBSet == eightBClick)
            {
                button18.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button18.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (nineBSet == nineBClick)
            {
                button19.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button19.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (tenBSet == tenBClick)
            {
                button20.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button20.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (oneCSet == oneCClick)
            {
                button21.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button21.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (twoCSet == twoCClick)
            {
                button22.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button22.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (threeCSet == threeCClick)
            {
                button23.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button23.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (fourCSet == fourCClick)
            {
                button24.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button24.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }

            if (fiveCSet == fiveCClick)
            {
                button25.BackColor = Color.FromArgb(94, 185, 151); // green
            }
            else
            {
                button25.BackColor = Color.FromArgb(215, 104, 104); // red
                incorrect++;
            }
        }

        // This method resets the colours of all the buttons back to grey.
        private void resetColours()
        {
            button1.BackColor = Color.FromArgb(27, 27, 27); // grey
            button2.BackColor = Color.FromArgb(27, 27, 27); 
            button3.BackColor = Color.FromArgb(27, 27, 27); 
            button4.BackColor = Color.FromArgb(27, 27, 27); 
            button5.BackColor = Color.FromArgb(27, 27, 27); 
            button6.BackColor = Color.FromArgb(27, 27, 27); 
            button7.BackColor = Color.FromArgb(27, 27, 27); 
            button8.BackColor = Color.FromArgb(27, 27, 27);
            button9.BackColor = Color.FromArgb(27, 27, 27);
            button10.BackColor = Color.FromArgb(27, 27, 27);
            button11.BackColor = Color.FromArgb(27, 27, 27);
            button12.BackColor = Color.FromArgb(27, 27, 27);
            button13.BackColor = Color.FromArgb(27, 27, 27);
            button14.BackColor = Color.FromArgb(27, 27, 27);
            button15.BackColor = Color.FromArgb(27, 27, 27);
            button16.BackColor = Color.FromArgb(27, 27, 27);
            button17.BackColor = Color.FromArgb(27, 27, 27);
            button18.BackColor = Color.FromArgb(27, 27, 27);
            button19.BackColor = Color.FromArgb(27, 27, 27);
            button20.BackColor = Color.FromArgb(27, 27, 27);
            button21.BackColor = Color.FromArgb(27, 27, 27);
            button22.BackColor = Color.FromArgb(27, 27, 27);
            button23.BackColor = Color.FromArgb(27, 27, 27);
            button24.BackColor = Color.FromArgb(27, 27, 27);
            button25.BackColor = Color.FromArgb(27, 27, 27);
        }

        private void resetClicks()
        {
            // Resets the click booleans.
            oneClick = false;
            twoClick = false;
            threeClick = false;
            fourClick = false;
            fiveClick = false;
            sixClick = false;
            sevenClick = false;
            eightClick = false;
            nineClick = false;
            tenClick = false;
            oneBClick = false;
            twoBClick = false;
            threeBClick = false;
            fourBClick = false;
            fiveBClick = false;
            sixBClick = false;
            sevenBClick = false;
            eightBClick = false;
            nineBClick = false;
            tenBClick = false;
            oneCClick = false;
            twoCClick = false;
            threeCClick = false;
            fourCClick = false;
            fiveCClick = false;
        }

        // The following methods occur once the buttons are clicked.
        private void button1_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                // Changes colour of button from a grey to an orange or from orange to grey.
                if (oneClick == false)
                {
                    button1.BackColor = Color.FromArgb(240, 155, 115); // orange
                    oneClick = true;
                }
                else
                {
                    button1.BackColor = Color.FromArgb(27, 27, 27); // grey
                    oneClick = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (twoClick == false)
                {
                    button2.BackColor = Color.FromArgb(240, 155, 115); // orange
                    twoClick = true;
                }
                else
                {
                    button2.BackColor = Color.FromArgb(27, 27, 27); // grey
                    twoClick = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (threeClick == false)
                {
                    button3.BackColor = Color.FromArgb(240, 155, 115); // orange
                    threeClick = true;
                }
                else
                {
                    button3.BackColor = Color.FromArgb(27, 27, 27); // grey
                    threeClick = false;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (fourClick == false)
                {
                    button4.BackColor = Color.FromArgb(240, 155, 115); // orange
                    fourClick = true;
                }
                else
                {
                    button4.BackColor = Color.FromArgb(27, 27, 27); // grey
                    fourClick = false;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (fiveClick == false)
                {
                    button5.BackColor = Color.FromArgb(240, 155, 115); // orange
                    fiveClick = true;
                }
                else
                {
                    button5.BackColor = Color.FromArgb(27, 27, 27); // grey
                    fiveClick = false;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (sixClick == false)
                {
                    button6.BackColor = Color.FromArgb(240, 155, 115); // orange
                    sixClick = true;
                }
                else
                {
                    button6.BackColor = Color.FromArgb(27, 27, 27); // grey
                    sixClick = false;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (sevenClick == false)
                {
                    button7.BackColor = Color.FromArgb(240, 155, 115); // orange
                    sevenClick = true;
                }
                else
                {
                    button7.BackColor = Color.FromArgb(27, 27, 27); // grey
                    sevenClick = false;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (eightClick == false)
                {
                    button8.BackColor = Color.FromArgb(240, 155, 115); // orange
                    eightClick = true;
                }
                else
                {
                    button8.BackColor = Color.FromArgb(27, 27, 27); // grey
                    eightClick = false;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (nineClick == false)
                {
                    button9.BackColor = Color.FromArgb(240, 155, 115); // orange
                    nineClick = true;
                }
                else
                {
                    button9.BackColor = Color.FromArgb(27, 27, 27); // grey
                    nineClick = false;
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (tenClick == false)
                {
                    button10.BackColor = Color.FromArgb(240, 155, 115); // orange
                    tenClick = true;
                }
                else
                {
                    button10.BackColor = Color.FromArgb(27, 27, 27); // grey
                    tenClick = false;
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (oneBClick == false)
                {
                    button11.BackColor = Color.FromArgb(240, 155, 115); // orange
                    oneBClick = true;
                }
                else
                {
                    button11.BackColor = Color.FromArgb(27, 27, 27); // grey
                    oneBClick = false;
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (twoBClick == false)
                {
                    button12.BackColor = Color.FromArgb(240, 155, 115); // orange
                    twoBClick = true;
                }
                else
                {
                    button12.BackColor = Color.FromArgb(27, 27, 27); // grey
                    twoBClick = false;
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (threeBClick == false)
                {
                    button13.BackColor = Color.FromArgb(240, 155, 115); // orange
                    threeBClick = true;
                }
                else
                {
                    button13.BackColor = Color.FromArgb(27, 27, 27); // grey
                    threeBClick = false;
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (fourBClick == false)
                {
                    button14.BackColor = Color.FromArgb(240, 155, 115); // orange
                    fourBClick = true;
                }
                else
                {
                    button14.BackColor = Color.FromArgb(27, 27, 27); // grey
                    fourBClick = false;
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (fiveBClick == false)
                {
                    button15.BackColor = Color.FromArgb(240, 155, 115); // orange
                    fiveBClick = true;
                }
                else
                {
                    button15.BackColor = Color.FromArgb(27, 27, 27); // grey
                    fiveBClick = false;
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (sixBClick == false)
                {
                    button16.BackColor = Color.FromArgb(240, 155, 115); // orange
                    sixBClick = true;
                }
                else
                {
                    button16.BackColor = Color.FromArgb(27, 27, 27); // grey
                    sixBClick = false;
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (sevenBClick == false)
                {
                    button17.BackColor = Color.FromArgb(240, 155, 115); // orange
                    sevenBClick = true;
                }
                else
                {
                    button17.BackColor = Color.FromArgb(27, 27, 27); // grey
                    sevenBClick = false;
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (eightBClick == false)
                {
                    button18.BackColor = Color.FromArgb(240, 155, 115); // orange
                    eightBClick = true;
                }
                else
                {
                    button18.BackColor = Color.FromArgb(27, 27, 27); // grey
                    eightBClick = false;
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (nineBClick == false)
                {
                    button19.BackColor = Color.FromArgb(240, 155, 115); // orange
                    nineBClick = true;
                }
                else
                {
                    button19.BackColor = Color.FromArgb(27, 27, 27); // grey
                    nineBClick = false;
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (tenBClick == false)
                {
                    button20.BackColor = Color.FromArgb(240, 155, 115); // orange
                    tenBClick = true;
                }
                else
                {
                    button20.BackColor = Color.FromArgb(27, 27, 27); // grey
                    tenBClick = false;
                }
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (oneCClick == false)
                {
                    button21.BackColor = Color.FromArgb(240, 155, 115); // orange
                    oneCClick = true;
                }
                else
                {
                    button21.BackColor = Color.FromArgb(27, 27, 27); // grey
                    oneCClick = false;
                }
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (twoCClick == false)
                {
                    button22.BackColor = Color.FromArgb(240, 155, 115); // orange
                    twoCClick = true;
                }
                else
                {
                    button22.BackColor = Color.FromArgb(27, 27, 27); // grey
                    twoCClick = false;
                }
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (threeCClick == false)
                {
                    button23.BackColor = Color.FromArgb(240, 155, 115); // orange
                    threeCClick = true;
                }
                else
                {
                    button23.BackColor = Color.FromArgb(27, 27, 27); // grey
                    threeCClick = false;
                }
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (fourCClick == false)
                {
                    button24.BackColor = Color.FromArgb(240, 155, 115); // orange
                    fourCClick = true;
                }
                else
                {
                    button24.BackColor = Color.FromArgb(27, 27, 27); // grey
                    fourCClick = false;
                }
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (patternOn == false)
            {
                if (fiveCClick == false)
                {
                    button25.BackColor = Color.FromArgb(240, 155, 115); // orange
                    fiveCClick = true;
                }
                else
                {
                    button25.BackColor = Color.FromArgb(27, 27, 27); // grey
                    fiveCClick = false;
                }
            }
        }

        // Pauses the game when the timer label is clicked.
        private void label1_Click(object sender, EventArgs e)
        {
            if (pause == false)
            {
                pause = true;

                // Stops the time.
                myTimer.Stop();
                countdownTimer.Stop();

                // 3 seconds for displaying the pattern.
                timerInterval = 3000;
                patternOn = true;

                // Clears the "X" or "O" label in the top middle.
                label2.Text = "";
                // Resets the colours of the buttons
                resetColours();
                //Resets the user's recreated pattern
                resetClicks();

                myTimer.Interval = timerInterval;

                // Resets the countdown display.
                secondsPassed = 0;

                // Shows the unpause button and allows it to be clicked.
                label4.Visible = true;
                label4.Enabled = true;
            }
        }

        //The unpause button, clicking it starts the game again.
        private void label4_Click(object sender, EventArgs e)
        {
            if (pause == true)
            {
                pause = false;

                // Restarts the timers
                myTimer.Enabled = true;
                countdownTimer.Enabled = true;

                // Sets the pattern again
                setPattern();
                setPatternColour();

                // Makes the unpause button invisible and unclickable.
                label4.Visible = false;
                label4.Enabled = false;
            }
        }
    }
}
