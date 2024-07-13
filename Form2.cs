namespace luvGame
{
    public partial class Form2 : Form
    {
        bool goLeft, goRight, jumping, hasKey;

        int jumpSpeed = 10;
        int force = 8;
        int Score = 0;

        int playerSpeed = 10;
        int backgroundSpeed = 8;
        public Form2()
        {
            InitializeComponent();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "ຄະເເນນ: " + Score;
            player.Top += jumpSpeed;
            if (goLeft == true && player.Left > 60)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true && player.Left + (player.Width + 60) < this.ClientSize.Width)
            {
                player.Left += playerSpeed;
            }

            if (goLeft == true && background.Left < 0)
            {
                background.Left += backgroundSpeed;
                MoveGameElement("forward");
            }

            if (goRight == true && background.Left > -1000)
            {
                background.Left -= backgroundSpeed;
                MoveGameElement("back");
            }

            if (jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && jumping == false)
                    {
                        force = 8;
                        player.Top = x.Top - player.Height;
                        jumpSpeed = 0;
                    }

                    x.BringToFront();
                }
                if (x is PictureBox && (string)x.Tag == "coin")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                    {
                        x.Visible = false;
                        Score += 1;
                    }
                }

            }

            if (player.Bounds.IntersectsWith(Key.Bounds))
            {
                Key.Visible = false;
                hasKey = true;
            }

            if (player.Bounds.IntersectsWith(door.Bounds) && hasKey == true)
            {
                door.Image = Properties.Resources.open_door;
                GameTimer.Stop();
                MessageBox.Show("ເຮັດໄດ້ດີຫຼາຍ, ການເດີນທາງສົມບູນແບບ!" + Environment.NewLine + "ກົດ OK ເພື່ອຫລິ້ນອີກຄັ້ງ");
                RestartGame();
            }

            if (player.Top + player.Height > this.ClientSize.Height)
            {
                GameTimer.Stop();
                MessageBox.Show("ເສຍຊີວິດ!" + Environment.NewLine + "ກົດ OK ເພື່ອຫລິ້ນອີກຄັ້ງ");
                RestartGame();
            }

        }


        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (jumping == true)
            {
                jumping = false;
            }
        }

        private void CloseGame(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void RestartGame()
        {
            Form2 newWindow = new Form2();
            newWindow.Show();
            this.Hide();
        }

        private void MoveGameElement(string direction)
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform" || x is PictureBox && (string)x.Tag == "coin" || x is PictureBox && (string)x.Tag == "Key" || x is PictureBox && (string)x.Tag == "door")
                {
                    if (direction == "back")
                    {
                        x.Left -= backgroundSpeed;
                    }
                    if (direction == "forward")
                    {
                        x.Left += backgroundSpeed;
                    }
                }
            }

        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {

        }

        private void door_Click(object sender, EventArgs e)
        {

        }
    }
}