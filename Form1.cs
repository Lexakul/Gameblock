using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GameWinForm
{
    public partial class Form1 : Form
    {
        Random rand;
        int Npol;
        List<Point> list;
        const int pWidth = 200;
        const int pHeight = 50;

        public Form1()
        {
            InitializeComponent();
            // 
            rand = new Random();
            Npol = 5;
            list = new List<Point>();
            InitialCreate();

            timer1.Interval = 750;
            timer1.Start();
        }

        private void InitialCreate()
        {
            for (int i = 0; i < Npol; i++)
            {
                AddObject();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Объект для рисования
            Graphics g = this.CreateGraphics();

            for (int i = 0; i < list.Count; i++)
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(rand.Next(256),
                    rand.Next(256), rand.Next(256)));
                g.FillRectangle(brush, list[i].X, list[i].Y, pWidth, pHeight);
                g.DrawRectangle(Pens.Blue, list[i].X, list[i].Y, pWidth, pHeight);
            }
            if(list.Count >= 10)  //каждый раз проверяем выйграли мы или проиграли
            {
                timer1.Stop();
                DialogResult result = MessageBox.Show(
                    "Поражение :(",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.OK)
                {
                    this.Close();
                }
            }
           
            if(list.Count == 0)
            { 
                timer1.Stop();
                DialogResult result = MessageBox.Show(
                    "Победа! :)",
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);

            }
                
            
        }

      
            

        private void timer1_Tick(object sender, EventArgs e)
        {
            AddObject();
            this.Invalidate();
        }

        private void AddObject()
        {
            int x = rand.Next(this.Width);
            int y = rand.Next(this.Height);
            list.Add(new Point(x, y));
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int index = FindObject(e.X, e.Y);
            if (index < 0)
                return;

            list.RemoveAt(index);
            Invalidate();
        }

        private int FindObject(int x, int y)
        {
            int index = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (x < list[i].X || x > list[i].X + pWidth)
                    continue;
                if (y < list[i].Y || y > list[i].Y + pHeight)
                    continue;
                
                if (Overlap(i))
                    continue;
                index = i;
            }
            return index; // НЕ попал :(
        }

        private bool Overlap(int i)
        {
            Rectangle r1 = new Rectangle(list[i].X, list[i].Y,
                pWidth, pHeight);

            for (i++; i < list.Count; i++)
            {
                Rectangle r2 = new Rectangle(list[i].X, list[i].Y,
                                            pWidth, pHeight);
                if (r1.IntersectsWith(r2))
                    return true;
            }
            return false;
        }

        private void Form1_Load(object sender,EventArgs e)
        {

        }
    }
}
