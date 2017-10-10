using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public partial class Form1 : Form
    {

        bool goleft;
        bool goright;
        int speed = 5;
        int score = 0;
        bool isPressed;
        int totalEnemies = 24;
        int playerSpeed = 6;
        int ammo = 50;


        public Form1()
        {
            InitializeComponent();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            //movement for spaceship
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }

            //shoot
            if(e.KeyCode == Keys.Space && !isPressed)
            {
                isPressed = true;
                makeLaser();
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            //movement for when the button isn't pressed - therefore is false as no instruction is given
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }

            if (isPressed)
            {
                isPressed = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(goleft)
            {
                player.Left -= playerSpeed;
            }

            else if (goright)
            {
                player.Left += playerSpeed;
            }

        //end of player moving left and right

        //enemies moving on the form

            foreach (Control x in this.Controls)
            {
                //identifies that the picture box is of tag invader, this allows all oicture boxes with this tag 
                if (x is PictureBox &&
                    (string)x.Tag == "invader")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        gamerOver();
                    }

                    ((PictureBox)x).Left += speed;

                    if (((PictureBox)x).Left > 720)
                    {
                        ((PictureBox)x).Top += ((PictureBox)x).Height + 10;
                        ((PictureBox)x).Left = -50;

                    }
                }
            }
            //end of enemies moving

            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && (string)y.Tag == "bullet")
                {
                    y.Top -= 20;
                    if (((PictureBox)y).Top < this.Height - 490)
                    {
                        this.Controls.Remove(y);
                    }
                }
            }

            foreach (Control i in this.Controls)
            {
                foreach (Control j in this.Controls)
                {
                    if (i is PictureBox && (string)i.Tag == "invader")
                    {
                        if (j is PictureBox && (string)j.Tag == "bullet")
                        {
                            if (i.Bounds.IntersectsWith(j.Bounds))
                            {
                                score++;
                                this.Controls.Remove(i);
                                this.Controls.Remove(j);
                            }
                        }
                    }
                }
            }
            //bullet and enemy collison ends
            label1.Text = "Score :" + score;

            if( score>= totalEnemies)
            {
                gameWin();
            }
        }

        private void makeLaser()
        {
            //create a picturebox with the laser picture in it. If it is greater then or equal to one then the user can shoot
            if (ammo >= 1)
            {
                PictureBox bullet = new PictureBox();
                bullet.Image = Properties.Resources.bullet;
                bullet.Size = new Size(2, 20);
                bullet.Tag = "bullet";
                bullet.Left = player.Left + player.Width / 2;
                bullet.Top = player.Top - 20;
                this.Controls.Add(bullet);
                bullet.BringToFront();
                ammo--;
                label2.Text = "Ammo :" + ammo;
            }
            else if (ammo == 0)
            {
                gamerOverAmmo();
            }

        }

        private void gamerOver()
        {
            timer1.Stop();
            label3.Visible = true;
            MessageBox.Show("The aliens invaded!");
            Close();
        }

        private void gamerOverAmmo()
        {
            timer1.Stop();
            label3.Visible = true;
            MessageBox.Show("You ran out of ammo!");
            Close();
        }

        private void gameWin()
        {
            timer1.Stop();
            label4.Visible = true;
            MessageBox.Show("You beat them!");
            Close();
        }
    }
}
