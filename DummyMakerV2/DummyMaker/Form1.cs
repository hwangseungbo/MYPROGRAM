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

        int savePoint = 0;

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
            DirectoryInfo di1 = new DirectoryInfo(Environment.CurrentDirectory + @"\1보행자");
            DirectoryInfo di2 = new DirectoryInfo(Environment.CurrentDirectory + @"\2이동차량");
            DirectoryInfo di3 = new DirectoryInfo(Environment.CurrentDirectory + @"\3정지차량");

            try
            {
                if (!di0.Exists) Directory.CreateDirectory(Environment.CurrentDirectory + @"\0원본데이터");
                if (!di1.Exists) Directory.CreateDirectory(Environment.CurrentDirectory + @"\1보행자");
                if (!di2.Exists) Directory.CreateDirectory(Environment.CurrentDirectory + @"\2이동차량");
                if (!di3.Exists) Directory.CreateDirectory(Environment.CurrentDirectory + @"\3정지차량");
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
                pictureBox3.Image = Resize(list[IDX], 135, 108);
                pictureBox4.Image = Resize(list[IDX + 1], 135, 108);
                pictureBox5.Image = Resize(list[IDX + 2], 135, 108);
                pictureBox6.Image = Resize(list[IDX + 3], 135, 108);
                pictureBox7.Image = Resize(list[IDX + 4], 135, 108);
                pictureBox8.Image = Resize(list[IDX + 5], 135, 108);
                pictureBox9.Image = Resize(list[IDX + 6], 135, 108);
                pictureBox10.Image = Resize(list[IDX + 7], 135, 108);
                pictureBox11.Image = Resize(list[IDX + 8], 135, 108);
                pictureBox12.Image = Resize(list[IDX + 9], 135, 108);
            }
            catch
            { }

            try
            {

                pictureBox1.Image = Resize(list[IDX], 1520, 855);
                
                if (list[IDX] != null)
                {
                    panel2.BackColor = Color.Red;
                    panel3.BackColor = Color.Black;
                    panel4.BackColor = Color.Black;
                    panel5.BackColor = Color.Black;
                    panel6.BackColor = Color.Black;
                    panel7.BackColor = Color.Black;
                    panel8.BackColor = Color.Black;
                    panel9.BackColor = Color.Black;
                    panel10.BackColor = Color.Black;
                    panel11.BackColor = Color.Black;

                }

                textBox1.Text = Path.GetFileNameWithoutExtension(list[IDX]);
                path = list[IDX];
            }
            catch
            {}
            label10.Text = (IDX + 1).ToString();
            label11.Text = (list.Count).ToString();

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileInfo fileInfo = new FileInfo(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath));
            if (!fileInfo.Exists)
            {
                using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                {
                    sw.Close();
                }
            }

            //세이브포인트를 읽어와 이어서 시작
            string path = String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath);
            string textvalue = System.IO.File.ReadAllText(path);
            
            if(textvalue != null && textvalue != "" && String.IsNullOrWhiteSpace(textvalue) == false)
            {
                savePoint = Int32.Parse(textvalue);

                IDX = savePoint - 1;
                
                try
                {
                    pictureBox3.Image = null;
                    pictureBox4.Image = null;
                    pictureBox5.Image = null;
                    pictureBox6.Image = null;
                    pictureBox7.Image = null;
                    pictureBox8.Image = null;
                    pictureBox9.Image = null;
                    pictureBox10.Image = null;
                    pictureBox11.Image = null;
                    pictureBox12.Image = null;

                    pictureBox3.Image = Resize(list[IDX], 135, 108);
                    pictureBox4.Image = Resize(list[IDX + 1], 135, 108);
                    pictureBox5.Image = Resize(list[IDX + 2], 135, 108);
                    pictureBox6.Image = Resize(list[IDX + 3], 135, 108);
                    pictureBox7.Image = Resize(list[IDX + 4], 135, 108);
                    pictureBox8.Image = Resize(list[IDX + 5], 135, 108);
                    pictureBox9.Image = Resize(list[IDX + 6], 135, 108);
                    pictureBox10.Image = Resize(list[IDX + 7], 135, 108);
                    pictureBox11.Image = Resize(list[IDX + 8], 135, 108);
                    pictureBox12.Image = Resize(list[IDX + 9], 135, 108);
                }
                catch
                { }

                try
                {

                    pictureBox1.Image = Resize(list[IDX], 1520, 855);

                    if (list[IDX] != null)
                    {
                        panel2.BackColor = Color.Red;
                        panel3.BackColor = Color.Black;
                        panel4.BackColor = Color.Black;
                        panel5.BackColor = Color.Black;
                        panel6.BackColor = Color.Black;
                        panel7.BackColor = Color.Black;
                        panel8.BackColor = Color.Black;
                        panel9.BackColor = Color.Black;
                        panel10.BackColor = Color.Black;
                        panel11.BackColor = Color.Black;

                    }

                    textBox1.Text = Path.GetFileNameWithoutExtension(list[IDX]);
                    path = list[IDX];
                }
                catch
                { }
                label10.Text = (IDX + 1).ToString();
                label11.Text = (list.Count).ToString();
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int count = 0;

            try
            {
                count = Int32.Parse(label11.Text);
            }
            catch
            { }
            
            //오른쪽 방향키 입력 이벤트
            if (e.KeyCode == Keys.Right)
            {
                if (IDX < list.Count - 1)
                {
                    IDX++;
                    pictureBox1.Image = Resize(list[IDX], 1520, 855);
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

                //원본파일이 10개 미만일 경우
                if (count <= 10)
                {
                    if (panel2.BackColor == Color.Red && pictureBox4.Image != null)
                    {
                        panel2.BackColor = Color.Black;
                        panel3.BackColor = Color.Red;
                    }
                    else if (panel3.BackColor == Color.Red && pictureBox5.Image != null)
                    {
                        panel3.BackColor = Color.Black;
                        panel4.BackColor = Color.Red;
                    }
                    else if (panel4.BackColor == Color.Red && pictureBox6.Image != null)
                    {
                        panel4.BackColor = Color.Black;
                        panel5.BackColor = Color.Red;
                    }
                    else if (panel5.BackColor == Color.Red && pictureBox7.Image != null)
                    {
                        panel5.BackColor = Color.Black;
                        panel6.BackColor = Color.Red;
                    }
                    else if (panel6.BackColor == Color.Red && pictureBox8.Image != null)
                    {
                        panel6.BackColor = Color.Black;
                        panel7.BackColor = Color.Red;
                    }
                    else if (panel7.BackColor == Color.Red && pictureBox9.Image != null)
                    {
                        panel7.BackColor = Color.Black;
                        panel8.BackColor = Color.Red;
                    }
                    else if (panel8.BackColor == Color.Red && pictureBox10.Image != null)
                    {
                        panel8.BackColor = Color.Black;
                        panel9.BackColor = Color.Red;
                    }
                    else if (panel9.BackColor == Color.Red && pictureBox11.Image != null)
                    {
                        panel9.BackColor = Color.Black;
                        panel10.BackColor = Color.Red;
                    }
                    else if (panel10.BackColor == Color.Red && pictureBox12.Image != null)
                    {
                        panel10.BackColor = Color.Black;
                        panel11.BackColor = Color.Red;
                    }
                }
                //원본파일이 10개 이상일 경우
                if (count > 10)
                {
                    if (panel11.BackColor == Color.Red && list[IDX] != null)
                    {
                        try
                        {
                            pictureBox3.Image = null;
                            pictureBox4.Image = null;
                            pictureBox5.Image = null;
                            pictureBox6.Image = null;
                            pictureBox7.Image = null;
                            pictureBox8.Image = null;
                            pictureBox9.Image = null;
                            pictureBox10.Image = null;
                            pictureBox11.Image = null;
                            pictureBox12.Image = null;

                            pictureBox3.Image = Resize(list[IDX], 135, 108);
                            pictureBox4.Image = Resize(list[IDX + 1], 135, 108);
                            pictureBox5.Image = Resize(list[IDX + 2], 135, 108);
                            pictureBox6.Image = Resize(list[IDX + 3], 135, 108);
                            pictureBox7.Image = Resize(list[IDX + 4], 135, 108);
                            pictureBox8.Image = Resize(list[IDX + 5], 135, 108);
                            pictureBox9.Image = Resize(list[IDX + 6], 135, 108);
                            pictureBox10.Image = Resize(list[IDX + 7], 135, 108);
                            pictureBox11.Image = Resize(list[IDX + 8], 135, 108);
                            pictureBox12.Image = Resize(list[IDX + 9], 135, 108);
                        }
                        catch
                        {}
                    }

                    if (panel2.BackColor == Color.Red && pictureBox4.Image != null)
                    {
                        panel2.BackColor = Color.Black;
                        panel3.BackColor = Color.Red;
                    }
                    else if (panel3.BackColor == Color.Red && pictureBox5.Image != null)
                    {
                        panel3.BackColor = Color.Black;
                        panel4.BackColor = Color.Red;
                    }
                    else if (panel4.BackColor == Color.Red && pictureBox6.Image != null)
                    {
                        panel4.BackColor = Color.Black;
                        panel5.BackColor = Color.Red;
                    }
                    else if (panel5.BackColor == Color.Red && pictureBox7.Image != null)
                    {
                        panel5.BackColor = Color.Black;
                        panel6.BackColor = Color.Red;
                    }
                    else if (panel6.BackColor == Color.Red && pictureBox8.Image != null)
                    {
                        panel6.BackColor = Color.Black;
                        panel7.BackColor = Color.Red;
                    }
                    else if (panel7.BackColor == Color.Red && pictureBox9.Image != null)
                    {
                        panel7.BackColor = Color.Black;
                        panel8.BackColor = Color.Red;
                    }
                    else if (panel8.BackColor == Color.Red && pictureBox10.Image != null)
                    {
                        panel8.BackColor = Color.Black;
                        panel9.BackColor = Color.Red;
                    }
                    else if (panel9.BackColor == Color.Red && pictureBox11.Image != null)
                    {
                        panel9.BackColor = Color.Black;
                        panel10.BackColor = Color.Red;
                    }
                    else if (panel10.BackColor == Color.Red && pictureBox12.Image != null)
                    {
                        panel10.BackColor = Color.Black;
                        panel11.BackColor = Color.Red;
                    }
                    else if (panel11.BackColor == Color.Red && list[IDX] != null)
                    {
                        panel11.BackColor = Color.Black;
                        panel2.BackColor = Color.Red;
                    }

                    try
                    {
                        pictureBox1.Image = Resize(list[IDX], 1520, 855);
                    }
                    catch
                    { }
                }
            }

            //왼쪽 방향키 입력 이벤트
            else if (e.KeyCode == Keys.Left)
            {
                int IDX2= IDX;
                if (IDX == 0)
                {
                    SystemSounds.Beep.Play();
                }
                else if (IDX != 0)
                {

                    IDX--;
                    pictureBox1.Image = Resize(list[IDX], 1520, 855);
                    textBox1.Text = Path.GetFileNameWithoutExtension(list[IDX]);
                    path = list[IDX];
                    label10.Text = (IDX + 1).ToString();
                    label11.Text = (list.Count).ToString();
                    RRefresh();
                }
                //원본파일이 10개 미만일경우
                if (count <= 10)
                {
                    if (panel11.BackColor == Color.Red)
                    {
                        panel11.BackColor = Color.Black;
                        panel10.BackColor = Color.Red;
                    }
                    else if (panel10.BackColor == Color.Red)
                    {
                        panel10.BackColor = Color.Black;
                        panel9.BackColor = Color.Red;
                    }
                    else if (panel9.BackColor == Color.Red)
                    {
                        panel9.BackColor = Color.Black;
                        panel8.BackColor = Color.Red;
                    }
                    else if (panel8.BackColor == Color.Red)
                    {
                        panel8.BackColor = Color.Black;
                        panel7.BackColor = Color.Red;
                    }
                    else if (panel7.BackColor == Color.Red)
                    {
                        panel7.BackColor = Color.Black;
                        panel6.BackColor = Color.Red;
                    }
                    else if (panel6.BackColor == Color.Red)
                    {
                        panel6.BackColor = Color.Black;
                        panel5.BackColor = Color.Red;
                    }
                    else if (panel5.BackColor == Color.Red)
                    {
                        panel5.BackColor = Color.Black;
                        panel4.BackColor = Color.Red;
                    }
                    else if (panel4.BackColor == Color.Red)
                    {
                        panel4.BackColor = Color.Black;
                        panel3.BackColor = Color.Red;
                    }
                    else if (panel3.BackColor == Color.Red)
                    {
                        panel3.BackColor = Color.Black;
                        panel2.BackColor = Color.Red;
                    }
                }
                
                //원본파일이 10개 이상일 경우
                if (count > 10)
                {
                    if (panel2.BackColor == Color.Red && IDX2 != 0)
                    {
                        try
                        {
                            pictureBox3.Image = null;
                            pictureBox4.Image = null;
                            pictureBox5.Image = null;
                            pictureBox6.Image = null;
                            pictureBox7.Image = null;
                            pictureBox8.Image = null;
                            pictureBox9.Image = null;
                            pictureBox10.Image = null;
                            pictureBox11.Image = null;
                            pictureBox12.Image = null;

                            pictureBox12.Image = Resize(list[IDX], 135, 108);
                            pictureBox11.Image = Resize(list[IDX - 1], 135, 108);
                            pictureBox10.Image = Resize(list[IDX - 2], 135, 108);
                            pictureBox9.Image = Resize(list[IDX - 3], 135, 108);
                            pictureBox8.Image = Resize(list[IDX - 4], 135, 108);
                            pictureBox7.Image = Resize(list[IDX - 5], 135, 108);
                            pictureBox6.Image = Resize(list[IDX - 6], 135, 108);
                            pictureBox5.Image = Resize(list[IDX - 7], 135, 108);
                            pictureBox4.Image = Resize(list[IDX - 8], 135, 108);
                            pictureBox3.Image = Resize(list[IDX - 9], 135, 108);
                        }
                        catch
                        { }

                        try
                        {
                            pictureBox1.Image = Resize(list[IDX], 1520, 855);
                        }
                        catch
                        { }
                    }


                    if (panel11.BackColor == Color.Red && pictureBox11.Image != null && IDX2 != 0)
                    {
                        panel11.BackColor = Color.Black;
                        panel10.BackColor = Color.Red;
                    }
                    else if (panel10.BackColor == Color.Red && pictureBox10.Image != null && IDX2 != 0)
                    {
                        panel10.BackColor = Color.Black;
                        panel9.BackColor = Color.Red;
                    }
                    else if (panel9.BackColor == Color.Red && pictureBox9.Image != null && IDX2 != 0)
                    {
                        panel9.BackColor = Color.Black;
                        panel8.BackColor = Color.Red;
                    }
                    else if (panel8.BackColor == Color.Red && pictureBox8.Image != null && IDX2 != 0)
                    {
                        panel8.BackColor = Color.Black;
                        panel7.BackColor = Color.Red;
                    }
                    else if (panel7.BackColor == Color.Red && pictureBox7.Image != null && IDX2 != 0)
                    {
                        panel7.BackColor = Color.Black;
                        panel6.BackColor = Color.Red;
                    }
                    else if (panel6.BackColor == Color.Red && pictureBox6.Image != null && IDX2 != 0)
                    {
                        panel6.BackColor = Color.Black;
                        panel5.BackColor = Color.Red;
                    }
                    else if (panel5.BackColor == Color.Red && pictureBox5.Image != null && IDX2 != 0)
                    {
                        panel5.BackColor = Color.Black;
                        panel4.BackColor = Color.Red;
                    }
                    else if (panel4.BackColor == Color.Red && pictureBox4.Image != null && IDX2 != 0)
                    {
                        panel4.BackColor = Color.Black;
                        panel3.BackColor = Color.Red;
                    }
                    else if (panel3.BackColor == Color.Red && pictureBox3.Image != null && IDX2 != 0)
                    {
                        panel3.BackColor = Color.Black;
                        panel2.BackColor = Color.Red;
                    }
                    else if (panel2.BackColor == Color.Red && IDX2 != 0)
                    {
                        panel2.BackColor = Color.Black;
                        panel11.BackColor = Color.Red;
                    }
                    IDX2--;
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
                g.FillRectangle(Brushes.Black, 0, 0, newImage.Width, newImage.Height);   //원본 16:9 (HD 1520 * 855)에서 가로 혹은 세로에 비율을 맞춘뒤 남는곳은 브러쉬 색으로 사각형을 만들어색칠함.
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

        //저장버튼 클릭 이벤트
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
                    string saveName1 = saveName.Insert(18, "2");
                    string saveName3 = saveName.Insert(18, "3");
                    FileInfo fileInfo1 = new FileInfo(String.Format(@"{0}\2이동차량\{1}", System.Windows.Forms.Application.StartupPath, saveName1));

                    if (!fileInfo1.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\2이동차량\{1}", System.Windows.Forms.Application.StartupPath, saveName1));
                    }

                    FileInfo fileInfo3 = new FileInfo(String.Format(@"{0}\3정지차량\{1}", System.Windows.Forms.Application.StartupPath, saveName3));

                    if (!fileInfo3.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\3정지차량\{1}", System.Windows.Forms.Application.StartupPath, saveName3));
                    }
                    SystemSounds.Beep.Play();
                    //savePoint
                    savePoint = Int32.Parse(label10.Text);
                    savePoint++;
                    using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                    {
                        sw.WriteLine(savePoint.ToString());
                        sw.Close();
                    }
                    if (label10.Text == label11.Text)
                    {
                        using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                        {
                            sw.WriteLine("");
                            sw.Close();
                        }
                    }
                }


                //MessageBox.Show(saveName);
                if (eventNumber == "1" && textBox3.Text != "")
                {
                    string[] separate = saveName.Split('_');
                    separate[4] = "보행자";
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

                    FileInfo fileInfo = new FileInfo(String.Format(@"{0}\1보행자\{1}", System.Windows.Forms.Application.StartupPath, Name));

                    if (!fileInfo.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\1보행자\{1}", System.Windows.Forms.Application.StartupPath, Name));
                        SystemSounds.Beep.Play();
                        //savePoint
                        savePoint = Int32.Parse(label10.Text);
                        savePoint++;
                        using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                        {
                            sw.WriteLine(savePoint.ToString());
                            sw.Close();
                        }
                        if (label10.Text == label11.Text)
                        {
                            using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                            {
                                sw.WriteLine("");
                                sw.Close();
                            }
                        }
                    }
                }
                else if (eventNumber == "2" && textBox3.Text != "")
                {
                    FileInfo fileInfo = new FileInfo(String.Format(@"{0}\2이동차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));

                    if (!fileInfo.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\2이동차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));
                        SystemSounds.Beep.Play();
                        //savePoint
                        savePoint = Int32.Parse(label10.Text);
                        savePoint++;
                        using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                        {
                            sw.WriteLine(savePoint.ToString());
                            sw.Close();
                        }
                        if (label10.Text == label11.Text)
                        {
                            using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                            {
                                sw.WriteLine("");
                                sw.Close();
                            }
                        }
                    }
                }
                else if (eventNumber == "3" && textBox3.Text != "")
                {
                    FileInfo fileInfo = new FileInfo(String.Format(@"{0}\3정지차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));

                    if (!fileInfo.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\3정지차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));
                        SystemSounds.Beep.Play();
                        //savePoint
                        savePoint = Int32.Parse(label10.Text);
                        savePoint++;
                        using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                        {
                            sw.WriteLine(savePoint.ToString());
                            sw.Close();
                        }
                        if (label10.Text == label11.Text)
                        {
                            using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                            {
                                sw.WriteLine("");
                                sw.Close();
                            }
                        }
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
                    string saveName1 = saveName.Insert(18, "2");
                    string saveName3 = saveName.Insert(18, "3");

                    FileInfo fileInfo1 = new FileInfo(String.Format(@"{0}\2이동차량\{1}", System.Windows.Forms.Application.StartupPath, saveName1));

                    if (!fileInfo1.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\2이동차량\{1}", System.Windows.Forms.Application.StartupPath, saveName1));
                    }

                    FileInfo fileInfo3 = new FileInfo(String.Format(@"{0}\3정지차량\{1}", System.Windows.Forms.Application.StartupPath, saveName3));

                    if (!fileInfo3.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\3정지차량\{1}", System.Windows.Forms.Application.StartupPath, saveName3));
                    }
                    SystemSounds.Beep.Play();
                    //savePoint
                    savePoint = Int32.Parse(label10.Text);
                    savePoint++;
                    using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                    {
                        sw.WriteLine(savePoint.ToString());
                        sw.Close();
                    }
                    if (label10.Text == label11.Text)
                    {
                        using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                        {
                            sw.WriteLine("");
                            sw.Close();
                        }
                    }
                }


                if (eventNumber == "1" && textBox3.Text != "")
                {
                    string[] separate = saveName.Split('_');
                    separate[4] = "보행자";
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

                    FileInfo fileInfo = new FileInfo(String.Format(@"{0}\1보행자\{1}", System.Windows.Forms.Application.StartupPath, Name));

                    if (!fileInfo.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\1보행자\{1}", System.Windows.Forms.Application.StartupPath, Name));
                        SystemSounds.Beep.Play();
                        //savePoint
                        savePoint = Int32.Parse(label10.Text);
                        savePoint++;
                        using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                        {
                            sw.WriteLine(savePoint.ToString());
                            sw.Close();
                        }
                        if (label10.Text == label11.Text)
                        {
                            using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                            {
                                sw.WriteLine("");
                                sw.Close();
                            }
                        }
                    }
                }
                else if (eventNumber == "2" && textBox3.Text != "")
                {
                    FileInfo fileInfo = new FileInfo(String.Format(@"{0}\2이동차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));

                    if (!fileInfo.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\2이동차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));
                        SystemSounds.Beep.Play();
                        //savePoint
                        savePoint = Int32.Parse(label10.Text);
                        savePoint++;
                        using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                        {
                            sw.WriteLine(savePoint.ToString());
                            sw.Close();
                        }
                        if (label10.Text == label11.Text)
                        {
                            using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                            {
                                sw.WriteLine("");
                                sw.Close();
                            }
                        }
                    }
                }
                else if (eventNumber == "3" && textBox3.Text != "")
                {
                    FileInfo fileInfo = new FileInfo(String.Format(@"{0}\3정지차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));

                    if (!fileInfo.Exists)
                    {
                        System.IO.File.Copy(path, String.Format(@"{0}\3정지차량\{1}", System.Windows.Forms.Application.StartupPath, saveName));
                        SystemSounds.Beep.Play();
                        //savePoint
                        savePoint = Int32.Parse(label10.Text);
                        savePoint++;
                        using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                        {
                            sw.WriteLine(savePoint.ToString());
                            sw.Close();
                        }
                        if (label10.Text == label11.Text)
                        {
                            using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                            {
                                sw.WriteLine("");
                                sw.Close();
                            }
                        }
                    }
                }
            }
        }
    }
}
