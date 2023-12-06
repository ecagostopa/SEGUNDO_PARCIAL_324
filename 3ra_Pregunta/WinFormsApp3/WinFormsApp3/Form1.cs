using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.IO;


namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        int pR, pG, pB;
        int mR = 0; int mG = 0; int mB = 0;

        List<int> colorRGB = new List<int>();
        List<int> mediaRGB = new List<int>();
        List<int> auxcolorRGB = new List<int>();

        string rutaArchivo = "C:\\Users\\carol\\OneDrive\\Escritorio\\Archivo.txt";


        public Form1()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        int contar_img = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            contar_img++; 
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "Archivos JPG|*.jpg|Archivos BMP|*.bmp";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != string.Empty)
            {
                bmp = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = bmp;
            }
        }
        int contar_click = 0;
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            contar_click++; 
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

            auxcolorRGB.Add(pR);
            auxcolorRGB.Add(pG);
            auxcolorRGB.Add(pB);
        }
        int indice = 0;
        int limite = 0;
        int aux = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            limite = 3 * (contar_click - 1); // img = 1 ; clicks= 3 ; lim= 6
                                             // img = 2 ; clicks= 4 ; lim= 9
            if (contar_img < 2)
            {
                if (textBox1.Text.Length == 0 && textBox2.Text.Length == 0 && textBox3.Text.Length == 0)
                {
                    pintar(limite, indice);
                }
                else
                {
                    pintar(limite, indice);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
            }
            else
            {
                if (textBox1.Text.Length == 0 && textBox2.Text.Length == 0 && textBox3.Text.Length == 0)
                {
                    pintar(limite, indice);
                }
                else
                {
                    pintar(limite, aux);
                    contar_img = 0;
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }

            }
            aux = limite;
        }
        public void pintar1(int colorR, int colorG, int colorB, int auxR, int auxG, int auxB)
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
                    if ((auxR - 100 <= mR && mR <= auxR + 100) && (auxG - 100 <= mG && mG <= auxG + 100) && (auxB - 100 <= mB && mB <= auxB + 100))
                    {
                        for (int ki = i; ki < i + 10; ki++)
                        {
                            for (int kj = j; kj < j + 10; kj++)
                            {
                                bmp1.SetPixel(ki, kj, Color.FromArgb(colorR, colorG, colorB));
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
            pictureBox2.Image = bmp1;
            mediaRGB.Add(mR);
            mediaRGB.Add(mG);
            mediaRGB.Add(mB);
        }
        public void pintar2(int colorR, int colorG, int colorB, int auxR, int auxG, int auxB)
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
                    if ((auxR - 100 <= mR && mR <= auxR + 100) && (auxG - 100 <= mG && mG <= auxG + 100) && (auxB - 10 <= mB && mB <= auxB + 100))
                    {
                        for (int ki = i; ki < i + 10; ki++)
                        {
                            for (int kj = j; kj < j + 10; kj++)
                            {
                                bmp1.SetPixel(ki, kj, Color.FromArgb(colorR, colorG, colorB));
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
            pictureBox2.Image = bmp1;
            //mediaRGB.Add(mR);
            //mediaRGB.Add(mG);
            //mediaRGB.Add(mB);
            //colorRGB.Clear();
        }
        public void pintar(int limite, int indice)
        {
            for (int i = indice; i <= limite; i = i + 3)//  0  3
            {
                if (i == 0)
                {
                    pintar1(colorRGB[i], colorRGB[i + 1], colorRGB[i + 2], auxcolorRGB[i], auxcolorRGB[i + 1], auxcolorRGB[i + 2]);
                }
                else
                {
                    pintar2(colorRGB[i], colorRGB[i + 1], colorRGB[i + 2], auxcolorRGB[i], auxcolorRGB[i + 1], auxcolorRGB[i + 2]);
                }
            }
        }
        public void pintar_nuevo(int limite, int indice)
        {
            for (int i = indice; i <= limite; i = i + 3)//  0  3
            {
                if (i == 0)
                {
                    pintar1(colorRGB[i], colorRGB[i + 1], colorRGB[i + 2], auxcolorRGB[i], auxcolorRGB[i + 1], auxcolorRGB[i + 2]);
                }
                else
                {
                    pintar2(colorRGB[i], colorRGB[i + 1], colorRGB[i + 2], auxcolorRGB[i], auxcolorRGB[i + 1], auxcolorRGB[i + 2]);
                }
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e) { }
    }
}
