using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong
{
    public partial class Pong : Form
    {
        int cpuDirection = 5;
        int ballXCoordinate = 5;
        int ballYcoordinate = 5;

        
        int cpuScore = 0;
        int playerScore = 0;


        int bottomBoundary;
        int centerPoint;
        int xMidpoint;
        int yMidpoint;



        bool playerDetectedUp;
        bool playerDetectedDown;

        int spaceBarClicked = 0;


        public Pong()
        {
            InitializeComponent();
            bottomBoundary = ClientSize.Height - player1.Height;
            xMidpoint = ClientSize.Height / 2;
            yMidpoint = ClientSize.Width / 2;
        }







        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random newBallSpot = new Random();
            int newSpot = newBallSpot.Next(100, ClientSize.Height - 100);

            Ball.Top -= ballYcoordinate;
            Ball.Left -= ballXCoordinate;
            //making cpu move

            cpuPlayer.Top += cpuDirection;

            //cpu get Gud
            if(playerScore > 5)
            {
                cpuPlayer.Top = Ball.Top + 100;
            }


            //check if cpu has reached top or bottom
            if(cpuPlayer.Top < 0 || cpuPlayer.Top > bottomBoundary)
            {
                cpuDirection = -cpuDirection;
            }

            //check if ball has exited left side screen

            if(Ball.Left < 0 ) 
            {
                Ball.Left = xMidpoint;
                Ball.Top = newSpot;
                ballXCoordinate = -ballXCoordinate;
                if (playerScore > 5) { ballXCoordinate -= 1; }
                cpuScore++;
                score2.Text = cpuScore.ToString();
            }

            //Check if player has scored
            if(Ball.Left + Ball.Width > ClientSize.Width)
            {
                Ball.Left = xMidpoint;
                Ball.Top = newSpot;
                ballXCoordinate = -ballXCoordinate;
                if (playerScore < 5) { ballXCoordinate += 1; }
                playerScore++;
                score1.Text = playerScore.ToString();
            }

            //Ensuring the ball is withing screen
            if (Ball.Top < 0 || Ball.Top + Ball.Height > ClientSize.Height)
            {
                ballYcoordinate = -ballYcoordinate;
            }

            //checking if ball hits a paddle
            if (Ball.Bounds.IntersectsWith(player1.Bounds) || Ball.Bounds.IntersectsWith(cpuPlayer.Bounds))
            {
                ballXCoordinate = -ballXCoordinate;
            }

            //move player up
            if(playerDetectedUp == true && player1.Top > 0)
            {
                player1.Top -= 10;
            }

            //moving player down
            if (playerDetectedDown == true && player1.Top < bottomBoundary)
            {
                player1.Top += 10;
            }
            //checking for when
            if(playerScore >= 10)
            {
                pongTimer.Stop();
            }
        }

        private void Pong_KeyDown(object sender, KeyEventArgs e)
        {
            //if player presses the up button, move paddle upward
            if (e.KeyCode == Keys.Up) { playerDetectedUp = true; }
            //moving down
            if(e.KeyCode == Keys.Down) { playerDetectedDown = true; }

            if(e.KeyCode == Keys.Space)
            {
                if(spaceBarClicked % 2 == 0)
                {
                    pongTimer.Stop();
                }
                else
                {
                    pongTimer.Start();
                }
            }
            spaceBarClicked++;
        }

        private void Pong_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Up) { playerDetectedUp = false; }
            
            if (e.KeyCode == Keys.Down) { playerDetectedDown = false; }
        }
    }
}
