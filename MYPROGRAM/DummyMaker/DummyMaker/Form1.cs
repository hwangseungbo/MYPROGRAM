using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DummyMaker
{
    public partial class Form1 : Form
    {
        List<string> list = new List<string>();
        int IDX = 0;

        Rectangle Rect;     //Instance of Rectangle
        Point LocationXY;   //For Starting Point
        Point LocationX1Y1;  //For Ending Point
        int a = 0, b = 0, c = 0, d = 0;

        bool IsMouseDown = false;   //마우스 다운 이벤트 발생시 ture;
        string path;

        public Form1()
        {
            InitializeComponent();
            lblLeft.Text = "Left : 0";
            lblTop.Text = "Top : 0";
            lblWith.Text = "Width : 0";
            lblHeight.Text = "Height : 0";

            //이벤트별 폴더가 없으면 만들어줌
            DirectoryInfo di1 = new DirectoryInfo(Environment.CurrentDirectory + @"\0원본데이터");
            DirectoryInfo di2 = new DirectoryInfo(Environment.CurrentDirectory + @"\2보행자");
            DirectoryInfo di3 = new DirectoryInfo(Environment.CurrentDirectory + @"\3정지차량");
            DirectoryInfo di4 = new DirectoryInfo(Environment.CurrentDirectory + @"\4수배차량");
            DirectoryInfo di5 = new DirectoryInfo(Environment.CurrentDirectory + @"\5역주행차량");

            try
            {
                if (!di1.Exists) Directory.CreateDirectory(Environment.CurrentDirectory + @"\0원본데이터");
                if (!di2.Exists) Directory.CreateDirectory(Environment.CurrentDirectory + @"\2보행자");
                if (!di3.Exists) Directory.CreateDirectory(Environment.CurrentDirectory + @"\3정지차량");
                if (!di4.Exists) Directory.CreateDirectory(Environment.CurrentDirectory + @"\4수배차량");
                if (!di5.Exists) Directory.CreateDirectory(Environment.CurrentDirectory + @"\5역주행차량");
            }
            catch { }

            try
            {
                if (System.IO.Directory.Exists(Environment.CurrentDirectory + @"\0원본데이터"))
                {
                    foreach (string s in System.IO.Directory.GetFiles(Environment.CurrentDirectory + @"\0원본데이터"))
                    {
                        list.Add(s);
                    }
                }
            }
            catch
            {
                MessageBox.Show("데이터가 하나도 없습니다.");
            }

            try
            {
                pictureBox1.Image = Resize(list[IDX], 1920, 1080);
                textBox1.Text = Path.GetFileNameWithoutExtension(list[IDX]);
                path = list[IDX];
            }
            catch
            {
                MessageBox.Show("데이터가 하나도 없습니다.");
            }
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (IDX < list.Count-1)
                {
                    IDX++;
                    pictureBox1.Image = Resize(list[IDX], 1920, 1080);
                    textBox1.Text = Path.GetFileNameWithoutExtension(list[IDX]);
                    path = list[IDX];
                }
                else if(IDX == list.Count-1)
                {
                    SystemSounds.Beep.Play();
                }
            }
            else if(e.KeyCode == Keys.Left)
            {
                if(IDX == 0)
                {
                    SystemSounds.Beep.Play();
                }
                else if(IDX != 0)
                {
                    IDX--;
                    pictureBox1.Image = Resize(list[IDX], 1920, 1080);
                    textBox1.Text = Path.GetFileNameWithoutExtension(list[IDX]);
                    path = list[IDX];
                }
            }
            else if(e.Control && e.KeyCode == Keys.S)
            {
                button2_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private Rectangle GetRect()
        {
            Rect = new Rectangle(); //Create Rectangle Object

            //사각형 만들기
            Rect.X = Math.Min(LocationXY.X, LocationX1Y1.X);
            Rect.Y = Math.Min(LocationXY.Y, LocationX1Y1.Y);
            Rect.Width = Math.Abs(LocationXY.X - LocationX1Y1.X);
            Rect.Height = Math.Abs(LocationXY.Y - LocationX1Y1.Y);

            if (IsMouseDown == true && textBox1.Text != "")
            {
                lblLeft.Text = string.Format("Left : {0}", ((int)(Rect.X * (1 / ratio))).ToString());
                a = ((int)(Rect.X * (1 / ratio)));
                lblTop.Text = string.Format("Top : {0}", ((int)(Rect.Y * (1 / ratio))).ToString());
                b = ((int)(Rect.Y * (1 / ratio)));
                lblWith.Text = string.Format("Width : {0}", ((int)(Rect.Width * (1 / ratio))).ToString());
                c = ((int)(Rect.Width * (1 / ratio)));
                lblHeight.Text = string.Format("Height : {0}", ((int)(Rect.Height * (1 / ratio))).ToString());
                d = ((int)(Rect.Height * (1 / ratio)));

                textBox3.Text = a+"_"+b+"_"+c+"_"+d;

            }
            
            return Rect;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openImgFile = new OpenFileDialog();
            openImgFile.Filter = "Image Files(*.bmp, *.gif, *.jpg)|*.bmp;*.gif;*.jpg";
            
            
            if(openImgFile.ShowDialog() == DialogResult.OK)
            {
                //panel1.BackgroundImage = Image.FromFile(openImgFile.FileName, true);
                path = openImgFile.FileName;
                pictureBox1.Image = Resize(openImgFile.FileName, 1920, 1080);
                textBox1.Text = Path.GetFileNameWithoutExtension(openImgFile.FileName);
            }
        }


        double ratio;
        public  Bitmap Resize(string importPath, int targetX, int targetY)
        {
            Image originalImage = Image.FromFile(importPath);

            double ratioX = targetX / (double)originalImage.Width;
            double ratioY = targetY / (double)originalImage.Height;

            ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(originalImage.Width * ratio);
            int newHeight = (int)(originalImage.Height * ratio);

            Bitmap newImage = new Bitmap(targetX, targetY);
            using (Graphics g= Graphics.FromImage(newImage))
            {
                g.FillRectangle(Brushes.Black, 0, 0, newImage.Width, newImage.Height);
                g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;

            LocationXY = e.Location;    //Get the Starting Point of X and Y
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == true)
            {
                LocationX1Y1 = e.Location;  //Get the Current Point of X and Y

                Refresh();  //Refresh the Form
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == true)
            {
                LocationX1Y1 = e.Location;  //Get the Ending Point of X and Y

                IsMouseDown = false;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (Rect != null)    //Check If Rectangle is not Null
            {
                e.Graphics.DrawRectangle(Pens.Red, GetRect());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string saveName;
            string orgName = textBox1.Text;
            string eventNumber = comboBox1.Text;
            orgName = orgName.Remove(18, 1);
            saveName = orgName.Insert(18, eventNumber);
            saveName = saveName.Replace("0_0_0_0", textBox3.Text)+".jpg";
            //MessageBox.Show(saveName);


            if (eventNumber == "2" && textBox3.Text != "")
            {
                string[] separate = saveName.Split('_');
                separate[4] = "Unknown";
                string Name = null;
                for(int i=0; i<=8;i++)
                {
                    if (i == 0)
                    {
                        Name = Name + separate[i];
                    }
                    else
                    {
                        Name = Name + '_' + separate[i];
                    }
                }

                FileInfo fileInfo = new FileInfo(String.Format(@"{0}\2보행자\{1}", System.Windows.Forms.Application.StartupPath, Name));

                if (!fileInfo.Exists)
                {
                    System.IO.File.Copy(path , String.Format(@"{0}\2보행자\{1}", System.Windows.Forms.Application.StartupPath, Name));
                    SystemSounds.Beep.Play();
                }
            }
            else if (eventNumber == "3" && textBox3.Text != "")
            {
                FileInfo fileInfo = new FileInfo(String.Format(@"{0}\3정지차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));

                if (!fileInfo.Exists)
                {
                    System.IO.File.Copy(path, String.Format(@"{0}\3정지차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));
                    SystemSounds.Beep.Play();
                }
            }
            else if (eventNumber == "4" && textBox3.Text != "")
            {
                FileInfo fileInfo = new FileInfo(String.Format(@"{0}\4수배차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));

                if (!fileInfo.Exists)
                {
                    System.IO.File.Copy(path, String.Format(@"{0}\4수배차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));
                    SystemSounds.Beep.Play();
                }
            }
            else if (eventNumber == "5" && textBox3.Text != "")
            {
                FileInfo fileInfo = new FileInfo(String.Format(@"{0}\5역주행차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));

                if (!fileInfo.Exists)
                {
                    System.IO.File.Copy(path, String.Format(@"{0}\5역주행차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));
                    SystemSounds.Beep.Play();
                }
            }

        }
    }
}
