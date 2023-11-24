using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Web;


namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        int pR, pG, pB;
        int mR = 0; int mG = 0; int mB = 0;

        List<int> colorRGB = new List<int>();
        List<int> mediaRGB = new List<int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mediaRGB.Clear();
            colorRGB.Clear();
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "Archivos JPG|*.jpg|Archivos BMP|*.bmp";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != string.Empty)
            {
                bmp = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = bmp;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Color c = new Color();
            c = bmp.GetPixel(15, 15);
            textBox1.Text = c.R.ToString();
            textBox2.Text = c.G.ToString();
            textBox3.Text = c.B.ToString();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //OBTENER COLOR
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Color c1 = new Color();
            c1 = bmp.GetPixel(e.X, e.Y);
            textBox1.Text = c1.R.ToString();
            textBox2.Text = c1.G.ToString();
            textBox3.Text = c1.B.ToString();

            colorRGB.Add(c1.R);
            colorRGB.Add(c1.G);
            colorRGB.Add(c1.B);
//---------------------------------------------------------------------------------
            //OBTENER LA MEDIA
            Color c = new Color();
            pR = 0;
            pG = 0;
            pB = 0;
            for (int i = e.X; i < e.X + 10; i++)
            {
                for (int j = e.Y; j < e.Y + 10; j++)
                {
                    c = bmp.GetPixel(i, j);
                    pR = pR + c.R;
                    pG = pG + c.G;
                    pB = pB + c.B;
                }
            }
            pR = pR / 100;
            pG = pG / 100;
            pB = pB / 100;

            textBox1.Text = c.R.ToString();
            textBox2.Text = c.G.ToString();
            textBox3.Text = c.B.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //GUARDAR RANGO 
            Bitmap bmp0 = new Bitmap(pictureBox2.Image);
            Bitmap bmp1 = new Bitmap(bmp0.Width, bmp0.Height);
            Color c = new Color();
            for (int i = 0; i < bmp0.Width - 10; i = i + 10)
            {
                for (int j = 0; j < bmp0.Height - 10; j = j + 10)
                {
                    mR = 0; mG = 0; mB = 0;
                    for (int ki = i; ki < i + 10; ki++)
                    {
                        for (int kj = j; kj < j + 10; kj++)
                        {
                            c = bmp0.GetPixel(i, j);
                            mR = mR + c.R;
                            mG = mG + c.G;
                            mB = mB + c.B;
                        }
                    }
                    mR = mR / 100;
                    mG = mG / 100;
                    mB = mB / 100;

                    c = bmp0.GetPixel(i, j);
                    if ((pR - 10 <= mR && mR <= pR + 10) && (pG - 10 <= mG && mG <= pG + 10) && (pB - 10 <= mB && mB <= pB + 10))
                    {
                        for (int ki = i; ki < i + 10; ki++)
                        {
                            for (int kj = j; kj < j + 10; kj++)
                            {
                                bmp1.SetPixel(ki, kj, Color.FromArgb(colorRGB[0], colorRGB[1], colorRGB[2]));
                            }
                        }
                    }
                    else
                    {
                        for (int ki = i; ki < i + 10; ki++)
                        {
                            for (int kj = j; kj < j + 10; kj++)
                            {
                                c = bmp0.GetPixel(ki, kj);
                                bmp1.SetPixel(ki, kj, Color.FromArgb(c.R, c.G, c.B));
                            }
                        }

                    }
                }
            }
            //MessageBox.Show(mR + "-" + mG + "-" + mB);
            pictureBox2.Image = bmp1;
            bmp0 = bmp1;
            mediaRGB.Add(mR);
            mediaRGB.Add(mG);
            mediaRGB.Add(mB);
            colorRGB.Clear();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //GUARDAR RANGO 
            Bitmap bmp0 = new Bitmap(pictureBox1.Image);
            Bitmap bmp1 = new Bitmap(bmp0.Width, bmp0.Height);
            Color c = new Color();
            for (int i = 0; i < bmp0.Width - 10; i = i + 10)
            {
                for (int j = 0; j < bmp0.Height - 10; j = j + 10)
                {
                    mR = 0; mG = 0; mB = 0;
                    for (int ki = i; ki < i + 10; ki++)
                    {
                        for (int kj = j; kj < j + 10; kj++)
                        {
                                c = bmp0.GetPixel(i, j);
                                mR = mR + c.R;
                                mG = mG + c.G;
                                mB = mB + c.B;
                        }
                        }
                    mR = mR / 100;
                    mG = mG / 100;
                    mB = mB / 100;

                     c = bmp0.GetPixel(i, j);
                     if ((pR - 10 <= mR && mR <= pR + 10) && (pG - 10 <= mG && mG <= pG + 10) && (pB - 10 <= mB && mB <= pB + 10))
                     {
                         for (int ki = i; ki < i + 10; ki++)
                         {
                            for (int kj = j; kj < j + 10; kj++)
                            {
                                bmp1.SetPixel(ki, kj, Color.FromArgb(colorRGB[0], colorRGB[1], colorRGB[2]));
                            }
                         }
                     }
                    else
                    {
                        for (int ki = i; ki < i + 10; ki++)
                        {
                            for (int kj = j; kj < j + 10; kj++)
                            {
                                c = bmp0.GetPixel(ki, kj);
                                bmp1.SetPixel(ki, kj, Color.FromArgb(c.R, c.G, c.B));
                            }
                        }

                    }
                }
            }
            //MessageBox.Show(mR + "-" + mG + "-" + mB);
            pictureBox2.Image = bmp1;
            bmp0 = bmp1;
            mediaRGB.Add(mR);
            mediaRGB.Add(mG);
            mediaRGB.Add(mB);
            colorRGB.Clear();
        }
    }
}
