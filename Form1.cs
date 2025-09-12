using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tac_Tic_Toe_Game.Properties;

namespace Tic_Tac_Toe_Game
{
    public partial class Form1 : Form
    {
        stGameStatus GameStatus;
        enPlayer PlayerTurn = enPlayer.Player1;
        enum enPlayer
        {
            Player1,
            Player2
        }

        enum enWinner
        {
            Player1,
            Player2,
            Draw,
            GameInProgress
        }

        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;

        }

        public Form1()
        {
            InitializeComponent();

        }
        int Rand(int from, int to)
        {
            Random rnd = new Random();
            return rnd.Next(from, to + 1);
        }
        void EndGame()
        {

            playerlb.Text = "Game Over";
            switch (GameStatus.Winner)
            {

                case enWinner.Player1:

                    winnerlb.Text = "Player1";
                    break;

                case enWinner.Player2:

                    winnerlb.Text = "Player2";
                    break;

                default:

                    winnerlb.Text = "Draw";
                    break;

            }
            GameStatus.GameOver = true;
            MessageBox.Show("GameOver", "GameOver", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void RestButton(PictureBox btn)
        {
            btn.Image = Resources.question_mark_96;
            btn.Tag = "?";
            btn.BackColor = Color.Transparent;

        }
        public bool CheckValues(PictureBox btn1,PictureBox btn2,PictureBox btn3)
        {
            if (btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn1.Tag.ToString() == btn3.Tag.ToString())
            {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;
                if(btn1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }

            }
            GameStatus.GameOver = false;
            return false;
        }
        void updateimagebox(PictureBox imgboxname)
        {

            if (imgboxname.Tag.ToString() == "?")
            {
                switch (PlayerTurn)
                {
                    case enPlayer.Player1:
                        imgboxname.Image = Resources.X;
                        PlayerTurn = enPlayer.Player2;
                        playerlb.Text = "Player 2";
                        GameStatus.PlayCount++;
                        imgboxname.Tag = "X";
                        CheckWinner();
                        break;
                    case enPlayer.Player2:
                        imgboxname.Image = Resources.O;
                        PlayerTurn = enPlayer.Player1;
                        playerlb.Text = "Player 1";
                        GameStatus.PlayCount++;
                        imgboxname.Tag = "O";
                        CheckWinner();
                        break;
                }
            }

            else

            {
                MessageBox.Show("Wrong Choice", "Worng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (GameStatus.PlayCount == 9 && !GameStatus.GameOver)
            {
                GameStatus.GameOver = true;
                GameStatus.Winner = enWinner.Draw;
                EndGame();
            }

        }
        void CheckWinner()
        {
            if (CheckValues(PB1,PB2,PB3))
                return;

            if (CheckValues(PB4,PB5,PB6))
                return;

            if (CheckValues(PB7,PB8,PB9))
                return;

            if (CheckValues(PB1,PB4,PB7))
                return;

            if (CheckValues(PB2,PB5,PB8))
                return;

            if (CheckValues(PB3,PB6,PB9))
                return;

            if (CheckValues(PB1,PB5,PB9))
                return;

            if (CheckValues(PB3,PB5,PB7))
                return;

        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color white = Color.White;
            Pen pen = new Pen(white);
            pen.Width = 10;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            e.Graphics.DrawLine(pen, 300, 300, 700, 300);
            e.Graphics.DrawLine(pen, 300, 200, 700, 200);
            e.Graphics.DrawLine(pen, 400, 120, 400, 400);
            e.Graphics.DrawLine(pen, 600, 120, 600, 400);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            PictureBox[] boxes = { PB1, PB2, PB3, PB4, PB5, PB6, PB7, PB8, PB9 };

            foreach (var pb in boxes)
            {
                pb.BackColor = Color.Transparent;
                winnerlb.BackColor = Color.Transparent;
                playerlb.BackColor = Color.Transparent;
                label1.BackColor = Color.Transparent;
                label2.BackColor = Color.Transparent;
                label4.BackColor = Color.Transparent;
            }
        }
        private void PB_Click(object sender, EventArgs e)
        {
            updateimagebox((PictureBox)sender);
        }

        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {
            Color white = Color.White;
            Pen pen = new Pen(white);
            pen.Width = 15;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            //Horizontal
            e.Graphics.DrawLine(pen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(pen, 400, 460, 1050, 460);
            //Vertical
            e.Graphics.DrawLine(pen, 610, 140, 610, 620);
            e.Graphics.DrawLine(pen, 840, 140, 840, 620);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PictureBox[] boxes = { PB1, PB2, PB3, PB4, PB5, PB6, PB7, PB8, PB9 };

            foreach (var pb in boxes)
            {
                RestButton(pb);
            }
            PlayerTurn = enPlayer.Player1;
            playerlb.Text = "Player 1";
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            GameStatus.Winner = enWinner.GameInProgress;
            winnerlb.Text = "In Progress";
            this.BackgroundImage = Resources.seaback2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image[] imgs = { Resources.seaback2,
                Resources.wallpaperflare_com_wallpaper__2_,Resources.dark,
                Resources.view,Resources.space,Resources.Beach };
            this.BackgroundImage = imgs[Rand(0, imgs.Length-1)];
        }
    }
}
