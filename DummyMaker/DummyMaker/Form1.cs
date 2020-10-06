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
using System.Threading;
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

        int orgGaro = 0;
        int orgSero = 0;
        int newWidth = 0;
        int newHeight = 0;

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
            DirectoryInfo di0 = new DirectoryInfo(Environment.CurrentDirectory + @"\0원본데이터");
            DirectoryInfo di1 = new DirectoryInfo(Environment.CurrentDirectory + @"\1주행차량");
            DirectoryInfo di2 = new DirectoryInfo(Environment.CurrentDirectory + @"\2보행자");
            DirectoryInfo di3 = new DirectoryInfo(Environment.CurrentDirectory + @"\3정지차량");
            DirectoryInfo di4 = new DirectoryInfo(Environment.CurrentDirectory + @"\4수배차량");
            DirectoryInfo di5 = new DirectoryInfo(Environment.CurrentDirectory + @"\5역주행차량");

            try
            {
                if (!di0.Exists) Directory.CreateDirectory(Environment.CurrentDirectory + @"\0원본데이터");
                if (!di1.Exists) Directory.CreateDirectory(Environment.CurrentDirectory + @"\1주행차량");
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
                pictureBox1.Image = Resize(list[IDX], 1280, 720);
                textBox1.Text = Path.GetFileNameWithoutExtension(list[IDX]);
                path = list[IDX];
            }
            catch
            {
                MessageBox.Show("데이터가 하나도 없습니다.");
            }
            label10.Text = (IDX + 1).ToString();
            label11.Text = (list.Count).ToString();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (IDX < list.Count - 1)
                {
                    IDX++;
                    pictureBox1.Image = Resize(list[IDX], 1280, 720);
                    textBox1.Text = Path.GetFileNameWithoutExtension(list[IDX]);
                    path = list[IDX];
                    label10.Text = (IDX + 1).ToString();
                    label11.Text = (list.Count).ToString();
                    RRefresh();
                }
                else if (IDX == list.Count - 1)
                {
                    SystemSounds.Beep.Play();
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (IDX == 0)
                {
                    SystemSounds.Beep.Play();
                }
                else if (IDX != 0)
                {
                    IDX--;
                    pictureBox1.Image = Resize(list[IDX], 1280, 720);
                    textBox1.Text = Path.GetFileNameWithoutExtension(list[IDX]);
                    path = list[IDX];
                    label10.Text = (IDX + 1).ToString();
                    label11.Text = (list.Count).ToString();
                    RRefresh();
                }
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                button2_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private Rectangle GetRect()
        {
            Rect = new Rectangle(); //Create Rectangle Object

            //사각형 만들기
            if (LocationX1Y1.Y < 0)
            {
                LocationX1Y1.Y = 0;
            }
            if (LocationX1Y1.X < 0)
            {
                LocationX1Y1.X = 0;
            }
            if (LocationX1Y1.X > orgGaro)
            {
                LocationX1Y1.X = orgGaro;
            }
            if (LocationX1Y1.Y > orgSero)
            {
                LocationX1Y1.Y = orgSero;
            }

            Rect.X = Math.Min(LocationXY.X, LocationX1Y1.X);
            Rect.Y = Math.Min(LocationXY.Y, LocationX1Y1.Y);
            Rect.Width = Math.Abs(LocationXY.X - LocationX1Y1.X);
            Rect.Height = Math.Abs(LocationXY.Y - LocationX1Y1.Y);

            if (Rect.X + Rect.Width > newWidth)
            {
                Rect.Width = newWidth - Rect.X - 1;
            }
            if (Rect.Y + Rect.Height > newHeight)
            {
                Rect.Height = newHeight - Rect.Y - 1;
            }

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

                if (a + c > orgGaro)
                {
                    c = orgGaro - a;
                    lblWith.Text = string.Format("Width : {0}", c.ToString());
                }
                if (b + d > orgSero)
                {
                    d = orgSero - b;
                    lblHeight.Text = string.Format("Height : {0}", d.ToString());
                }

                textBox3.Text = a + "_" + b + "_" + c + "_" + d;

            }

            return Rect;
        }

        double ratio;
        public Bitmap Resize(string importPath, int targetX, int targetY)
        {
            Image originalImage = Image.FromFile(importPath);

            orgGaro = originalImage.Width;
            orgSero = originalImage.Height;

            double ratioX = targetX / (double)originalImage.Width;
            double ratioY = targetY / (double)originalImage.Height;

            ratio = Math.Min(ratioX, ratioY);

            newWidth = (int)(originalImage.Width * ratio);
            newHeight = (int)(originalImage.Height * ratio);

            Bitmap newImage = new Bitmap(targetX, targetY);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.FillRectangle(Brushes.BlanchedAlmond, 0, 0, newImage.Width, newImage.Height);   //원본 16:9 (HD 1280 * 720)에서 가로 혹은 세로에 비율을 맞춘뒤 남는곳은 브러쉬 색으로 사각형을 만들어색칠함.
                g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }

        //초기화하는 함수
        public void RRefresh()
        {
            LocationXY.X = 0;
            LocationXY.Y = 0;
            LocationX1Y1.X = 0;
            LocationX1Y1.Y = 0;
            IsMouseDown = false;
            Refresh();
            lblLeft.Text = "Left : 0";
            lblTop.Text = "Top : 0";
            lblWith.Text = "Width : 0";
            lblHeight.Text = "Height : 0";
            textBox3.Text = "";
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;

            LocationXY = e.Location;    //Get the Starting Point of X and Y

            if (e.Button == MouseButtons.Right)
            {
                RRefresh();
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown == true)
            {
                LocationX1Y1 = e.Location;  //Get the Current Point of X and Y

                Refresh();  //Refresh the Form
            }
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

            string orgName = textBox1.Text;
            string[] separates = orgName.Split('_');

            //다음 조건문은 파일 이름형식에 따른 저장을 나눈것이다.
            if (!int.TryParse(separates[2], out int result))
            {

                string saveName = null;
                string eventNumber = comboBox1.Text;
                separates[4] = separates[2];
                separates[3] = comboBox1.Text;
                separates[2] = comboBox2.Text;
                if (textBox2.Text.Length == 8)
                {
                    separates[0] = textBox2.Text;
                }
                for (int i = 0; i <= 4; i++)
                {
                    if (i == 0)
                    {
                        saveName = saveName + separates[i];
                    }
                    else
                    {
                        saveName = saveName + '_' + separates[i];
                    }
                }
                saveName = saveName + '_' + textBox3.Text + ".jpg";

                if (eventNumber == "0" && textBox3.Text != "")
                {
                    saveName = saveName.Remove(18, 1);
                    string saveName1 = saveName.Insert(18, "1");
                    string saveName3 = saveName.Insert(18, "3");
                    string saveName4 = saveName.Insert(18, "4");
                    string saveName5 = saveName.Insert(18, "5");

                    FileInfo fileInfo1 = new FileInfo(String.Format(@"{0}\1주행차량\{1}", System.Windows.Forms.Application.StartupPath, saveName1));

                    if (!fileInfo1.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\1주행차량\{1}", System.Windows.Forms.Application.StartupPath, saveName1));
                    }

                    FileInfo fileInfo3 = new FileInfo(String.Format(@"{0}\3정지차량\{1}", System.Windows.Forms.Application.StartupPath, saveName3));

                    if (!fileInfo3.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\3정지차량\{1}", System.Windows.Forms.Application.StartupPath, saveName3));
                    }

                    FileInfo fileInfo4 = new FileInfo(String.Format(@"{0}\4수배차량\{1}", System.Windows.Forms.Application.StartupPath, saveName4));

                    if (!fileInfo4.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\4수배차량\{1}", System.Windows.Forms.Application.StartupPath, saveName4));
                    }

                    FileInfo fileInfo5 = new FileInfo(String.Format(@"{0}\5역주행차량\{1}", System.Windows.Forms.Application.StartupPath, saveName5));

                    if (!fileInfo5.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\5역주행차량\{1}", System.Windows.Forms.Application.StartupPath, saveName5));
                        SystemSounds.Beep.Play();
                    }
                }


                //MessageBox.Show(saveName);
                if (eventNumber == "1" && textBox3.Text != "")
                {
                    FileInfo fileInfo = new FileInfo(String.Format(@"{0}\1주행차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));

                    if (!fileInfo.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\1주행차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));
                        SystemSounds.Beep.Play();
                    }
                }

                if (eventNumber == "2" && textBox3.Text != "")
                {
                    string[] separate = saveName.Split('_');
                    separate[4] = "Unknown";
                    string Name = null;
                    for (int i = 0; i <= 8; i++)
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
                        System.IO.File.Copy(path, String.Format(@"{0}\2보행자\{1}", System.Windows.Forms.Application.StartupPath, Name));
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
            else if (!int.TryParse(separates[4], out result))
            {
                string saveName = null;
                string eventNumber = comboBox1.Text;
                string carNumber = separates[4];

                if (textBox2.Text.Length == 8)
                {
                    separates[0] = textBox2.Text;
                }

                separates[2] = comboBox2.Text;
                separates[3] = comboBox1.Text;

                for (int i = 0; i <= 4; i++)
                {
                    if (i == 0)
                    {
                        saveName = saveName + separates[i];
                    }
                    else
                    {
                        saveName = saveName + '_' + separates[i];
                    }
                }
                saveName = saveName + '_' + textBox3.Text + ".jpg";
                
                if (eventNumber == "0" && textBox3.Text != "")  //이벤번호가 0일경우 2번제외 모든 이벤트 폴더에 저장을해줘야하므로
                {
                    saveName = saveName.Remove(18, 1);
                    string saveName1 = saveName.Insert(18, "1");
                    string saveName3 = saveName.Insert(18, "3");
                    string saveName4 = saveName.Insert(18, "4");
                    string saveName5 = saveName.Insert(18, "5");

                    FileInfo fileInfo1 = new FileInfo(String.Format(@"{0}\1주행차량\{1}", System.Windows.Forms.Application.StartupPath, saveName1));

                    if (!fileInfo1.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\1주행차량\{1}", System.Windows.Forms.Application.StartupPath, saveName1));
                    }

                    FileInfo fileInfo3 = new FileInfo(String.Format(@"{0}\3정지차량\{1}", System.Windows.Forms.Application.StartupPath, saveName3));

                    if (!fileInfo3.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\3정지차량\{1}", System.Windows.Forms.Application.StartupPath, saveName3));
                    }

                    FileInfo fileInfo4 = new FileInfo(String.Format(@"{0}\4수배차량\{1}", System.Windows.Forms.Application.StartupPath, saveName4));

                    if (!fileInfo4.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\4수배차량\{1}", System.Windows.Forms.Application.StartupPath, saveName4));
                    }

                    FileInfo fileInfo5 = new FileInfo(String.Format(@"{0}\5역주행차량\{1}", System.Windows.Forms.Application.StartupPath, saveName5));

                    if (!fileInfo5.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\5역주행차량\{1}", System.Windows.Forms.Application.StartupPath, saveName5));
                        SystemSounds.Beep.Play();
                    }
                }

                if (eventNumber == "1" && textBox3.Text != "")
                {
                    FileInfo fileInfo = new FileInfo(String.Format(@"{0}\1주행차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));

                    if (!fileInfo.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\1주행차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));
                        SystemSounds.Beep.Play();
                    }
                }

                if (eventNumber == "2" && textBox3.Text != "")
                {
                    string[] separate = saveName.Split('_');
                    separate[4] = "Unknown";
                    string Name = null;
                    for (int i = 0; i <= 8; i++)
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
                        System.IO.File.Copy(path, String.Format(@"{0}\2보행자\{1}", System.Windows.Forms.Application.StartupPath, Name));
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
}
