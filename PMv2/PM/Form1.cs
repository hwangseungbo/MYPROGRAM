using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Security.Principal;
using System.IO;
using System.Collections;
using Microsoft.Win32;
using System.Media;
using System.Linq.Expressions;
using System.Security.Permissions;
using System.Xml.Schema;
using System.Management;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;

namespace PM
{
    public partial class Form1 : Form
    {
        bool looptriger = true;
        Thread[] Tpro = new Thread[100];    // 관리 프로세스 마다 갖게될 쓰레드
        int Tindex = 0;                     // Tpro객체 인덱스
        Process[] Proc;                     // 전체 프로세스 정보를 담을 객체
        private Process[] myProcess = new Process[100];
        //private TaskCompletionSource<bool> eventHandled;
        string[] procName = new string[500];        //전체 프로세스 명을 담을 스트링 배열
        private object lockObject = new object();   //lock문에 사용될 객체.
        string[] startTime = new string[100];   // 시작시간을 저장하고있는 스트링배열
        bool On;
        Point Pos;

        //설정파일 저장해보자
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        //숨겨진창 활성화시 최상단으로 부르기위함
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;

        //쿼리를 통해 주소받아오기
        [DllImport("Kernel32.dll")]
        static extern uint QueryFullProcessImageName(IntPtr hProcess, uint flags, StringBuilder text, out uint size);

        public Form1()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Visible = false;

            ////ini파일 쓰기
            ////ini파일은 생성도 해주는 구나.
            //WritePrivateProfileString("PM", "path", "true", "C:\\Setting.ini");
            //WritePrivateProfileString("PM", "value", "3", "C:\\Setting.ini");
            //WritePrivateProfileString("PM", "vision", "false", "C:\\Setting.ini"); //3줄이 쓰이는게 아니라 섹션과 키가 동일하기에 그 키의 밸류값만 변경됨 즉 vision=false

            ////ini파일 읽기
            //StringBuilder Value = new StringBuilder();
            //StringBuilder Vision = new StringBuilder();

            ////Vision과 Value 변수에 저장이 되는구나.
            //GetPrivateProfileString("PM", "value", "", Value, Value.Capacity, "C:\\Setting.ini");
            //GetPrivateProfileString("PM", "vision", "", Vision, Vision.Capacity, "C:\\Setting.ini");

            //// StringBuilder 변수를 .ToString() 으로 출력이 가능하구나.
            //MessageBox.Show(Value.ToString());
            //MessageBox.Show(Vision.ToString());



            //List.txt파일이 없다면 현재 디렉토리에 생성해줌
            FileInfo fileInfo = new FileInfo(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath));
            //System.Windows.Forms.Application.StartupPath  ==  C:\hwangseungbo\Csharp\Observer3\Observer\bin\Debug 즉 실행파일이 잇는 위치
            if (!fileInfo.Exists)
            {
                using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                {
                    sw.Close();
                }
            }
            //마지막 종료시간 로그 폴더 및 파일이 없으면 만들어줌
            string DirPath = Environment.CurrentDirectory + @"\ExitLog";
            string FilePath = DirPath + "\\ExitLog" + ".log";

            DirectoryInfo di = new DirectoryInfo(DirPath);
            FileInfo fi = new FileInfo(FilePath);

            try
            {
                if (!di.Exists) Directory.CreateDirectory(DirPath);
                if (!fi.Exists)
                {
                    using (StreamWriter sw = new StreamWriter(FilePath))
                    {
                        sw.Close();
                    }
                    Thread.Sleep(100);
                }
            }
            catch { }


            MouseDown += (o, e) => { if (e.Button == MouseButtons.Left) { On = true; Pos = e.Location; } };
            MouseMove += (o, e) => { if (On) Location = new Point(Location.X + (e.X - Pos.X), Location.Y + (e.Y - Pos.Y)); };
            MouseUp += (o, e) => { if (e.Button == MouseButtons.Left) { On = false; Pos = e.Location; } };

            btnRevise.Enabled = false;
            btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48__1_;
            btnRevise.BackColor = Color.Transparent;
            btnAdd.Enabled = false;
            btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48__1_;
            btnAdd.BackColor = Color.Transparent;
            btnDel.Enabled = false;
            btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48__1_;
            btnDel.BackColor = Color.Transparent;
            btnOpen.Enabled = false;
            btnOpen.Image = PM.Properties.Resources.iconmonstr_cloud_32_48__1_;
            btnOpen.BackColor = Color.Transparent;

            //시작프로그램 등록여부 판별하여 버튼 활성화
            string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            RegistryKey strUpKey = Registry.LocalMachine.OpenSubKey(runKey);
            if (strUpKey.GetValue("PM") == null)
            {
                btnSPD.Enabled = false;
                btnSPD.BackColor = Color.Transparent;
                btnSPD.Image = PM.Properties.Resources.iconmonstr_eraser_2_48__1_;
            }
            else if (strUpKey.GetValue("PM") != null)
            {
                btnSPR.Enabled = false;
                btnSPR.BackColor = Color.Transparent;
                btnSPR.Image = PM.Properties.Resources.iconmonstr_clipboard_13_48__1_;
            }

            //대기시간없이 리스트 뷰 뛰우기위해 여기에 추가했다.--------------------------------------------------------------
            Process[] proc = Process.GetProcesses();

            listView1.Items.Clear();
            string path = String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath);
            string[] textvalue = System.IO.File.ReadAllLines(path);
            
            string ExitLogPath = Environment.CurrentDirectory + @"\ExitLog\ExitLog.log";

            for (int i = 0; i <= (textvalue.Length - 2); i = i + 2)
            {
                string[] LVItem = new string[7];
                LVItem[0] = "";
                LVItem[1] = Path.GetFileNameWithoutExtension(textvalue[i]);
                if (textvalue[i] == @"C:\Windows\System32\calc.exe")
                {
                    LVItem[1] = "Calculator";
                }
                LVItem[2] = textvalue[i];

                for (int j = 0; j <= proc.Length - 1; j++)
                {
                    if (LVItem[1] == proc[j].ProcessName)
                    {
                        LVItem[6] = "동작중";
                        break;
                    }
                    else if (j == proc.Length - 1 && LVItem[1] != proc[j].ProcessName)
                    {
                        LVItem[4] = "현재 실행중이지 않습니다.";
                        LVItem[6] = "종료됨";
                    }
                }
                LVItem[3] = textvalue[i + 1];
                string[] textvalue2 = System.IO.File.ReadAllLines(ExitLogPath);
                for (int k = textvalue2.Length - 1; k >= 0; k--)
                {
                    if (textvalue2[k].Contains(LVItem[1]))
                    {
                        LVItem[5] = textvalue2[k].Substring(0, 22);
                        break;
                    }
                    else if (k == 0 && textvalue[k] != LVItem[1])
                    {
                        LVItem[5] = "마지막 종료 정보가 없습니다.";
                        break;
                    }
                }
                ListViewItem lvi = new ListViewItem(LVItem);
                listView1.Items.Add(lvi);
            }
            lblProcNum.Text = "관리중인 프로그램 수 : " + (textvalue.Length / 2).ToString();
            listView1.CheckBoxes = true;
            for (int i = 0; i <= listView1.Items.Count - 1; i++)
            {
                listView1.Items[i].Checked = true;
            }
            //---------------------------------------------------------------------------------------------------------------
        }

        //pid를 통하여 프로세스의  경로를 구하는 함수. ver1
        public static string GetProcessPath(int processId)
        {
            string MethodResult = "";

            //MessageBox.Show(processId.ToString());

            try
            {
                string Query = "SELECT ExecutablePath FROM Win32_Process WHERE ProcessId = " + processId;

                using (ManagementObjectSearcher mos = new ManagementObjectSearcher(Query))
                {
                    using (ManagementObjectCollection moc = mos.Get())
                    {
                        if ((from mo in moc.Cast<ManagementObject>() select mo["ExecutablePath"]).First() != null)
                        {
                            string ExecutablePath = (from mo in moc.Cast<ManagementObject>() select mo["ExecutablePath"]).First().ToString();
                            MethodResult = ExecutablePath;
                        }
                        else
                        {
                            MethodResult = "경로를 확인할 수 없습니다.";
                        }
                    }
                }
            }
            catch//(Exception e)
            {
                //ex.HandleException();
            }
            return MethodResult;
        }
        
        //pid를 통하여 프로세스의  경로를 구하는 함수. ver2
        public string GetPathToApp(Process proc)
        {
            string pathToExe = string.Empty;

            if (null != proc)
            {
                uint nChars = 256;
                StringBuilder Buff = new StringBuilder((int)nChars);

                uint success = QueryFullProcessImageName(proc.Handle, 0, Buff, out nChars);

                if (0 != success)
                {
                    pathToExe = Buff.ToString();
                }
                else
                {
                    int error = Marshal.GetLastWin32Error();
                    pathToExe = ("Error = " + error + " when calling GetProcessImageFileName");
                }
            }

            return pathToExe;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //timer2.Interval = 1000;
            //timer2.Start();
            dmStopbtn.Enabled = false;
            dmStopbtn.Image = PM.Properties.Resources.iconmonstr_media_control_50_48__1_;
            dmStopbtn.BackColor = Color.Transparent;
            CheckForIllegalCrossThreadCalls = false;
            Proc = Process.GetProcesses();
            //List.txt파일이 없다면 현재 디렉토리에 생성해줌
            FileInfo fileInfo = new FileInfo(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath));
            //System.Windows.Forms.Application.StartupPath  ==  C:\hwangseungbo\Csharp\Observer3\Observer\bin\Debug 즉 실행파일이 잇는 위치
            if (!fileInfo.Exists)
            {
                using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                {
                    sw.Close();
                }
            }

            FileInfo fi = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\DM.txt");
            if (fi.Exists)
            {
                try
                {
                    string[] value = System.IO.File.ReadAllLines(fi.ToString());
                    textBox1.Text = value[0];
                    ScrollBar.Value = Int32.Parse(value[1]);
                    ScrollBar2.Value = Int32.Parse(value[2]);
                    Percentlbl.Text = value[1] + " %";
                    Percentlbl2.Text = value[1] + " %";
                    drivelbl.Text = "   "+textBox1.Text.Substring(0, 1);

                    Daylbl.Text = value[2] + " 일";
                    if(value[3]=="1")
                    {
                        dmStartbtn_Click(sender,e);
                    }
                }
                catch
                { }
            }

            LogWrite("PM프로그램이 실행되었습니다.");

            string path = String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath);
            string[] textvalue = System.IO.File.ReadAllLines(path);
            int num = (textvalue.Length / 2); // 프로그램 구동시 관리리스트에 프로그램이 등록되어있을경우 구동하기위해 등록된 갯수를 구함.

            //관리자 권한 확인
            bool right = IsRunningAsLocalAdmin();
            if (right) //관리자 권한인지 확인하여 맞으면 타이틀에 Administrator를 붙여줌
            {
                this.Text += " " + "(Administrator)";
                label1.Text += " (Administrator)";
            }

            if (num > 0) // 관리하는 프로그램이 1개라도 있다면
            {
                for (int i = 0; i <= (textvalue.Length - 2); i = i + 2)
                {
                    int idx = i;
                    int peri = int.Parse(textvalue[idx + 1]);
                    Tpro[Tindex] = new Thread(() => BackWork(textvalue[idx], peri));
                    Tpro[Tindex].IsBackground = true;
                    Tpro[Tindex].Start();
                    Tindex++;
                }
            }

            Thread ud = new Thread(new ThreadStart(usingdrive));
            ud.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            getListview();
        }

        //List.txt를 읽어들여 listview에 정보를 올려주는 함수
        void getListview()
        {
            bool[] check = new bool[100];
            bool[] check2 = new bool[100];
            Process[] proc = Process.GetProcesses();
            


            //리스트뷰 각 행의 체크박스가 체크되있는지 내용을 저장함
            for (int i = 0; i <= listView1.Items.Count - 1; i++)
            {
                if (listView1.Items[i].Checked == true)
                {
                    check[i] = true;
                }
                else if (listView1.Items[i].Checked == false)
                {
                    check[i] = false;
                }
                startTime[i] = listView1.Items[i].SubItems[4].Text;
            }


            //리스트뷰 각 아이템이 선택되어있는지 내용을 저장함
            for (int i = 0; i <= listView1.Items.Count - 1; i++)
            {
                if (listView1.Items[i].Selected == true)
                {
                    check2[i] = true;
                }
                else if (listView1.Items[i].Selected == false)
                {
                    check2[i] = false;
                }
            }

            //리스트뷰의 스크롤바의 위치를 작업중인 위치로 고정
            int topItemIndex = 0;
            if (listView1.Items.Count > 0)
            {
                try
                {
                    topItemIndex = listView1.TopItem.Index;
                }
                catch
                { }
            }
            

            listView1.Items.Clear();

            string path = String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath);
            string[] textvalue = System.IO.File.ReadAllLines(path);
            string ExitLogPath = Environment.CurrentDirectory + @"\ExitLog\ExitLog.log";
            for (int i = 0; i <= (textvalue.Length - 2); i = i + 2)
            {
                int x = 0;
                string[] LVItem = new string[7];
                LVItem[0] = "";
                LVItem[1] = Path.GetFileNameWithoutExtension(textvalue[i]);
                if(textvalue[i] == @"C:\Windows\System32\calc.exe")
                {
                    LVItem[1] = "Calculator";
                }
                LVItem[2] = textvalue[i];

                for (int j = 0; j <= proc.Length - 1; j++)
                {

                    if (LVItem[1] == proc[j].ProcessName)
                    {
                        LVItem[4] = proc[j].StartTime.ToString();
                        LVItem[6] = "동작중";
                        startTime[x] = proc[j].StartTime.ToString();
                        x++;
                        break;
                    }
                    else if (j == proc.Length - 1 && LVItem[1] != proc[j].ProcessName)
                    {
                        LVItem[4] = startTime[i / 2];
                        LVItem[6] = "종료됨";
                    }
                }
                LVItem[3] = textvalue[i + 1];
                string[] textvalue2 = System.IO.File.ReadAllLines(ExitLogPath);
                if (textvalue2.Length != 0)
                {
                    for (int k = textvalue2.Length - 1; k >= 0; k--)
                    {
                        if (textvalue2[k].Contains(LVItem[1]))
                        {
                            LVItem[5] = textvalue2[k].Substring(0, 22);
                            break;
                        }
                        else if (k == 0 && textvalue[k] != LVItem[1])
                        {
                            LVItem[5] = "마지막 종료 정보가 없습니다.";
                            break;
                        }
                    }
                }
                else if (textvalue2.Length == 0)
                {
                    LVItem[5] = "마지막 종료 정보가 없습니다.";
                }
                ListViewItem lvi = new ListViewItem(LVItem);
                listView1.Items.Add(lvi);

                for (int g = 0; g <= listView1.Items.Count - 1; g++)
                {
                    if (check[g] == true)
                    {
                        listView1.Items[g].Checked = true;
                    }
                    else if (check[g] == false)
                    {
                        listView1.Items[g].Checked = false;
                    }
                }

                for (int j = 0; j <= listView1.Items.Count - 1; j++)
                {
                    if (check2[j] == true)
                    {
                        listView1.Items[j].Selected = true;
                    }
                    else if (check2[j] == false)
                    {
                        listView1.Items[j].Selected = false;
                    }

                }
            }

            try
            {
                listView1.TopItem = listView1.Items[topItemIndex];
            }
            catch 
            { }
            lblProcNum.Text = "관리중인 프로그램 수 : " + (textvalue.Length / 2).ToString();
        }

        //폼의 빈공간 클릭시 이벤트
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tboxPath.Text) == true || String.IsNullOrWhiteSpace(tboxPeriod.Text) == true)
            {
                tboxPath.Text = "";
                tboxPeriod.Text = "";
            }
            //리스트뷰가 선택되어있으면 해제한다.
            if (listView1.SelectedItems.Count == 1)
            {
                listView1.SelectedItems[0].Selected = false;
                btnRevise.Enabled = false;
                btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48__1_;
                btnRevise.BackColor = Color.Transparent;
                btnAdd.Enabled = false;
                btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48__1_;
                btnAdd.BackColor = Color.Transparent;
                btnDel.Enabled = false;
                btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48__1_;
                btnDel.BackColor = Color.Transparent;
                btnOpen.Enabled = false;
                btnOpen.Image = PM.Properties.Resources.iconmonstr_cloud_32_48__1_;
                btnOpen.BackColor = Color.Transparent;

                tboxPath.Text = "";
                tboxPeriod.Text = "";
            }
            if (listView1.SelectedItems.Count > 1)
            {
                for (int i = listView1.SelectedItems.Count - 1; i >= 0; i--)
                {
                    listView1.SelectedItems[i].Selected = false;
                }
                btnRevise.Enabled = false;
                btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48__1_;
                btnRevise.BackColor = Color.Transparent;
                btnAdd.Enabled = false;
                btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48__1_;
                btnAdd.BackColor = Color.Transparent;
                btnDel.Enabled = false;
                btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48__1_;
                btnDel.BackColor = Color.Transparent;
                tboxPath.Text = "";
                tboxPeriod.Text = "";
            }
        }

        //관리자 권한으로 실행되는지 확인하기 위한 함수
        public bool IsRunningAsLocalAdmin()
        {
            WindowsIdentity cur = WindowsIdentity.GetCurrent();
            foreach (IdentityReference role in cur.Groups)
            {
                if (role.IsValidTargetType(typeof(SecurityIdentifier)))
                {
                    SecurityIdentifier sid = (SecurityIdentifier)role.Translate(typeof(SecurityIdentifier));
                    if (sid.IsWellKnown(WellKnownSidType.AccountAdministratorSid) || sid.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //관리 프로세스들을 지속적으로 감시하는 쓰레드를 통해 돌아가는 루프함수
        void BackWork(string path, int period)
        {
            looptriger = true;
            string procName = Path.GetFileNameWithoutExtension(path);
            bool flag = true;

            //계산기 예외로 추가함(프로세스명이 확장자명을 제외한 이름과 다를경우 이런식으로 추가만 해준다.)
            if(procName == "calc")
            {
                procName = "Calculator";
            }
            
            while (true)
            {
                int idx = 0;

                lock (lockObject)
                {
                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                        if (listView1.Items[i].SubItems[2].Text == path)
                        {
                            idx = i;
                            break;
                        }
                    }
                }

                try
                {
                    if (listView1.Items[idx].Checked == false)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                }
                catch
                {
                    Thread.Sleep(1000);
                    continue;
                }
                

                lock (lockObject)
                {
                    Proc = Process.GetProcesses();
                    Thread.Sleep(1000);

                    for (int i = 0; i <= (Proc.Length - 1); i++)
                    {
                        if (procName == Proc[i].ProcessName) // 정상 동작중이므로 할게 없다.      procName == Proc[i].ProcessName
                        {
                            //아래의 코딩 통해 프로세스를 종료이벤트에 등록함, 종료시 LogWrite 함수를 호출하여 정확한 종료시간을 기록한다.
                            //프로세스를 실행할 때 넣어주는게 좋으나 정확한 프로세스 객체가 필요하므로 이처럼 정상동작이 확인되었을 때 이 그인덱스를 빌려 쉽게 구현하였다.
                            //이렇게 되면 처음 실행이 되고 한 주기 동안 정상동작한 이후에 종료이벤트를 구독하는 문제가 있으나 타협.    // 이 함수 마지막 주기 설정부분을 플래그로 감싸며 해결
                            if (flag == true)
                            {
                                //MessageBox.Show(string.Format("{0}가 종료이벤트를 구독합니다.", procName));
                                if (Proc[i].EnableRaisingEvents == false)
                                {
                                    Proc[i].EnableRaisingEvents = true;
                                }
                                Proc[i].Exited += (sender, e) =>
                                {
                                    bool exit = Proc[i].WaitForExit(5000);
                                    LogWrite(string.Format("{0}이 종료되었습니다.", procName));
                                    ExitLogWrite(string.Format("{0}이 종료되었습니다.", procName));
                                    flag = true;
                                };
                                flag = false;
                            }
                            break;
                        }
                        else if (i == Proc.Length - 1 && procName != Proc[Proc.Length - 1].ProcessName) // 프로세스배열 끝까지 이름비교해도 없으면 동작중이 아니므로 실행시킨다.
                        {
                            FileInfo fi = new FileInfo(path);

                            if (fi.Exists)  //경로에 파일이 존재하는지 확인
                            {
                                LogWrite(path + "이(가) 실행중이지 않아 시작합니다.");
                                //아이센의 경우 인자값을 전달하여 로그인과정을 생략한다.
                                if (procName == "Eyesen")
                                {
                                    Process.Start(path, "/PM");             //재실행시의 인자값 전달.
                                }
                                else
                                {
                                    Process.Start(path);
                                }
                                Thread.Sleep(500);
                                break;
                            }
                            else
                            {
                                for(int j =0; j <= listView1.Items.Count -1; j++)
                                {
                                    if(path == listView1.Items[j].SubItems[2].Text)
                                    {
                                        listView1.Items[j].SubItems[2].Text = "해당경로상에 디렉토리 혹은 파일이 존재하지 않습니다.";
                                        break;
                                    }
                                }
                            }
                        }
                        if (looptriger == false)
                        {
                            break;
                        }
                    }
                }
                //원래는 한주기 후에 종료이벤트 구독했었는데 이 코드 덕에 해결. 이젠 프로세스 실행되고 얼마않있어 바로 종료이벤트 구독함.
                if (flag == false)
                {
                    Thread.Sleep(period * 1000);
                }
            }
        }

        
        //칼럼 클릭을 통한 정렬 ------------------------------------------------------------
        Boolean m_ColumnclickASC = true;
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //if (m_ColumnclickASC == true)
            //    ((ListView)sender).ListViewItemSorter = new ListViewItemSortASC(e.Column);
            //else
            //    ((ListView)sender).ListViewItemSorter = new ListViewItemSortDESC(e.Column);

            //m_ColumnclickASC = !m_ColumnclickASC;

            //if (listView1.SelectedItems.Count != 0)
            //{
            //    listView1.SelectedItems.Clear();
            //}
        }

        class ListViewItemSort : IComparer
        {
            private int col;

            public ListViewItemSort()
            {
                col = 0;
            }

            public ListViewItemSort(int column)
            {
                col = column;
            }

            public int Compare(object x, object y)
            {
                return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }

        class ListViewItemSortASC : IComparer
        {
            private int col;

            public ListViewItemSortASC()
            {
                col = 0;
            }

            public ListViewItemSortASC(int column)
            {
                col = column;
            }

            public int Compare(object x, object y)
            {
                try
                {
                    if (Convert.ToInt32(((ListViewItem)x).SubItems[col].Text) > Convert.ToInt32(((ListViewItem)y).SubItems[col].Text))
                        return 1;
                    else
                        return -1;
                }
                catch (Exception)
                {
                    if (1 != String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text))
                        return -1;
                    else
                        return 1;
                }
            }
        }
        class ListViewItemSortDESC : IComparer
        {
            private int col;

            public ListViewItemSortDESC()
            {
                col = 0;
            }

            public ListViewItemSortDESC(int column)
            {
                col = column;
            }

            public int Compare(object x, object y)
            {
                try
                {
                    if (Convert.ToInt32(((ListViewItem)x).SubItems[col].Text) < Convert.ToInt32(((ListViewItem)y).SubItems[col].Text))
                        return 1;
                    else
                        return -1;
                }

                catch (Exception)
                {
                    if (String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text) == 1)
                        return -1;
                    else
                        return 1;
                }
            }
        }
        //---------------------------------------------------------------------------------

        //"찾기"버튼 클릭시 이벤트
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                int len = Proc.Length;

                for (int i = 0; i <= len - 1; i++)
                {
                    procName[i] = Proc[i].ProcessName;  //지역변수로 procName[]을 선언하니 "할당안된변수 사용불가"라 하여 전역변수로 선언함
                }

                OpenFileDialog openProcessFile = new OpenFileDialog();
                openProcessFile.Filter = "실행파일(*.exe)|*.exe";

                if (openProcessFile.ShowDialog() == DialogResult.OK)
                {
                    var list = new List<string>();
                    list.AddRange(procName);
                    string item = Path.GetFileNameWithoutExtension(openProcessFile.FileName); //프로세스 명(ex: mspaint)을 반환받아 item 변수에 저장한다.
                    string path = openProcessFile.FileName;        //바로가기 lnk 파일을 대상으로해도 실제 찐경로가 반환된다.
                    
                    tboxPath.Text = path;
                    tboxPeriod.Text = "10";
                    if (listView1.Items.Count != 0) //관리 등록 프로그램이 하나라도잇다면(현재 테이블에서 선택한 항목없이 찾기버튼 통해 들어옴)
                    {
                        for (int i = 0; i <= listView1.Items.Count - 1; i++)
                        {
                            if (path == listView1.Items[i].SubItems[2].Text)    //경로 비교를 통해 이미등록된 프로그램을 찾기버튼통해 선택하였는지 판별한다.(만약 같은경로가존재할경우)
                            {
                                SystemSounds.Beep.Play();
                                MessageBox.Show("이미 등록되어있는 프로그램입니다.");
                                tboxPath.Text = "";
                                tboxPeriod.Text = "";
                                btnRevise.Enabled = false;
                                btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48__1_;
                                btnAdd.Enabled = false;
                                btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48__1_;
                                btnDel.Enabled = false;
                                btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48__1_;
                            }
                            else if (i == listView1.Items.Count - 1 && path != listView1.Items[i].SubItems[2].Text)
                            {
                                btnRevise.Enabled = false;
                                btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48__1_;
                                btnAdd.Enabled = true;
                                btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48;
                                btnAdd.BackColor = Color.FromArgb(44, 43, 60);
                                btnDel.Enabled = false;
                                btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48__1_;
                            }
                        }
                    }
                    else if (listView1.Items.Count == 0)
                    {
                        btnRevise.Enabled = false;
                        btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48__1_;
                        btnRevise.BackColor = Color.Transparent;
                        btnAdd.Enabled = true;
                        btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48;
                        btnAdd.BackColor = Color.FromArgb(44, 43, 60);
                        btnDel.Enabled = false;
                        btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48__1_;
                        btnDel.BackColor = Color.Transparent;
                    }
                }
            }
            else if (listView1.SelectedItems.Count != 0)
            {
                int len = Proc.Length;

                for (int i = 0; i <= (len - 1); i++)
                {
                    procName[i] = Proc[i].ProcessName;  //지역변수로 procName[]을 선언하니 "할당안된변수 사용불가"라 하여 전역변수로 선언함
                }

                OpenFileDialog openProcessFile = new OpenFileDialog();
                openProcessFile.Filter = "실행파일(*.exe)|*.exe";
                if (openProcessFile.ShowDialog() == DialogResult.OK)
                {
                    var list = new List<string>();
                    list.AddRange(procName);
                    string item = Path.GetFileNameWithoutExtension(openProcessFile.FileName); //프로세스 명(ex: mspaint)을 반환받아 item 변수에 저장한다.
                    string path = openProcessFile.FileName;        //바로가기 lnk 파일을 대상으로해도 실제 찐경로가 반환된다.

                    tboxPath.Text = path;
                    tboxPeriod.Text = listView1.SelectedItems[0].SubItems[3].Text;

                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                        if (path == listView1.Items[i].SubItems[2].Text)
                        {
                            SystemSounds.Beep.Play();
                            MessageBox.Show("이미 등록되어있는 프로그램입니다.");
                            break;
                        }
                        else if (i == listView1.Items.Count - 1 && path != listView1.Items[i].SubItems[2].Text)
                        {
                            if (listView1.SelectedItems.Count == 1)
                            {
                                tboxPath.Text = path;
                                btnRevise.Enabled = true;
                                btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48;
                                btnRevise.BackColor = Color.FromArgb(44, 43, 60);
                                btnAdd.Enabled = true;
                                btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48;
                                btnAdd.BackColor = Color.FromArgb(44, 43, 60);
                                btnDel.Enabled = false;
                                btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48__1_;
                                btnDel.BackColor = Color.Transparent;
                            }
                            else if (listView1.SelectedItems.Count > 1)
                            {
                                listView1.SelectedItems.Clear();
                                tboxPath.Text = path;
                                btnRevise.Enabled = false;
                                btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48__1_;
                                btnRevise.BackColor = Color.Transparent;
                                btnAdd.Enabled = true;
                                btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48;
                                btnAdd.BackColor = Color.FromArgb(44, 43, 60);
                                btnDel.Enabled = false;
                                btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48__1_;
                                btnDel.BackColor = Color.Transparent;
                            }
                        }
                    }
                }
            }
        }

        //"추가"버튼 클릭시 이벤트
        private void btnAdd_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            int a;

            if (listView1.SelectedItems.Count == 0)
            {
                if (tboxPath.Text == "")
                {
                    MessageBox.Show("찾기 버튼을 통해 실행파일을 선택해주세요.");
                }
                else if(!Int32.TryParse(tboxPeriod.Text, out a))
                {
                    MessageBox.Show("주기는 숫자만 입력가능 합니다.");
                }
                else if(int.Parse(tboxPeriod.Text.ToString()) < 5)
                {
                    MessageBox.Show("주기는 5초 이상으로만 설정 가능합니다.");
                }
                else if (tboxPath.Text != "" && tboxPeriod.Text == "")
                {
                    MessageBox.Show("주기를 입력해주세요.");
                }
                else if (!(int.TryParse(tboxPeriod.Text, out int result)))
                {
                    MessageBox.Show("주기는 숫자만 입력해주세요.");
                }
                else
                {
                    string path = tboxPath.Text;
                    ResistProcess(path);
                    tboxPath.Text = "";
                    tboxPeriod.Text = "";
                    StopAndDoWork();
                }
            }
            else if (listView1.SelectedItems.Count != 0)
            {
                if (tboxPath.Text == "")
                {
                    MessageBox.Show("찾기 버튼을 통해 실행파일을 선택해주세요.");
                }
                else if (tboxPath.Text != "" && tboxPeriod.Text == "")
                {
                    MessageBox.Show("주기를 입력해주세요.");
                }
                else if (!(int.TryParse(tboxPeriod.Text, out int result)))
                {
                    MessageBox.Show("주기는 숫자만 입력해주세요.");
                }
                else
                {
                    string path = tboxPath.Text;
                    ResistProcess(path);
                    getListview();
                    StopAndDoWork();
                }
                listView1.SelectedItems.Clear();
            }
            timer1.Start();
        }
        //"추가"버튼을 통한 관리 프로세스 등록함수
        private void ResistProcess(string path)
        {
            //timer1.Stop(); //어차피 추가버튼 클릭이벤에 들어가잇음
            int len = Proc.Length;
            string procName = Path.GetFileNameWithoutExtension(path);
            int leng = listView1.Items.Count;

            FileInfo fileInfo = new FileInfo(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath));
            try
            {
                if (!fileInfo.Exists)   //List.txt파일이 없다면 현재 디렉토리에 생성해주고 등록
                {
                    using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                    {
                        sw.WriteLine(path);
                        sw.WriteLine(tboxPeriod.Text);
                        sw.Close();
                    }
                    btnRevise.Enabled = true;
                    btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48;
                    btnRevise.BackColor = Color.FromArgb(44, 43, 60);
                    btnAdd.Enabled = false;
                    btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48__1_;
                    btnAdd.BackColor = Color.Transparent;
                    btnDel.Enabled = true;
                    btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48;
                    btnDel.BackColor = Color.FromArgb(44, 43, 60);
                    //StopAndDoWork(); //어차피 추가버튼 클릭이벤에 들어가잇음
                    Thread.Sleep(10);
                }
                else   //List.txt 존재시
                {
                    var lines = new List<string>();
                    lines.AddRange(File.ReadAllLines(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)));
                    if (lines.Count != 0) //관리로 등록된 프로그램이 하나라도 있을경우
                    {
                        for (int i = 0; i < lines.Count; i++)
                        {
                            if (path == lines[i]) //관리 등록된게 내가지금 등록하려는것과 같다면
                            {
                                SystemSounds.Beep.Play();
                                MessageBox.Show("이미 등록된 프로그램 입니다.");
                                Thread.Sleep(10);
                                break;
                            }
                            else if (path != lines[i] && i == lines.Count - 1)//관리 등록된게 내가 지금 등록하려는것과 다르며 현재 인덱스가 마지막 인덱스일경우
                            {
                                using (StreamWriter sw = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath), true))
                                {
                                    sw.WriteLine(path);
                                    sw.WriteLine(tboxPeriod.Text);
                                    sw.Close();
                                }
                                btnRevise.Enabled = true;
                                btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48;
                                btnRevise.BackColor = Color.FromArgb(44, 43, 60);
                                btnAdd.Enabled = false;
                                btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48__1_;
                                btnAdd.BackColor = Color.Transparent;
                                btnDel.Enabled = true;
                                btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48;
                                btnDel.BackColor = Color.FromArgb(44, 43, 60);
                                getListview();
                                //StopAndDoWork(); //어차피 추가버튼 클릭이벤에 들어가잇음
                                Thread.Sleep(10);
                                break;
                            }
                        }
                    }
                    else    //List.txt파일은 있으면서 관리 등록된게 하나도 없을경우
                    {
                        using (StreamWriter sw = File.AppendText(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                        {
                            sw.WriteLine(path);
                            sw.WriteLine(tboxPeriod.Text);
                            sw.Close();
                        }
                        btnRevise.Enabled = true;
                        btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48;
                        btnRevise.BackColor = Color.FromArgb(44, 43, 60);
                        btnAdd.Enabled = false;
                        btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48__1_;
                        btnAdd.BackColor = Color.Transparent;
                        btnDel.Enabled = true;
                        btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48;
                        btnDel.BackColor = Color.FromArgb(44, 43, 60);
                        //getListview();
                        //StopAndDoWork(); //어차피 추가버튼 클릭이벤에 들어가잇음
                        Thread.Sleep(10);
                    }
                    getListview();
                    listView1.Items[listView1.Items.Count - 1].Checked = true;
                }
            }
            catch { }
            //timer1.Start(); //어차피 추가버튼 클릭이벤에 들어가잇음
        }

        //"수정"버튼 클릭 이벤트
        private void btnRevise_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            try
            {
                if (listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].SubItems[2].Text == tboxPath.Text)
                {
                    if (!(int.TryParse(tboxPeriod.Text, out int result)))
                    {
                        MessageBox.Show("주기는 숫자만 입력해주세요.");
                    }
                    else if (int.Parse(tboxPeriod.Text.ToString()) < 5)
                    {
                        MessageBox.Show("주기는 5초 이상으로만 설정 가능합니다.");
                    }
                    else if (String.IsNullOrWhiteSpace(tboxPath.Text) == true || String.IsNullOrWhiteSpace(tboxPeriod.Text) == true)
                    {
                        MessageBox.Show("테이블에서 항목을 클릭해 주세요");
                    }
                    else if (String.IsNullOrWhiteSpace(tboxPeriod.Text) == false) //textBox1은 사용자가 글자를 입력할 수 없으므로 조건을 확인할 필요가 없다.
                    {
                        listView1.SelectedItems[0].SubItems[3].Text = tboxPeriod.Text.ToString();
                        var lines = new List<string>();
                        lines.AddRange(File.ReadAllLines(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)));

                        for (int i = 0; i < lines.Count; i++)
                        {
                            if (tboxPath.Text == lines[i])
                            {
                                lines[i + 1] = tboxPeriod.Text.ToString();

                                using (StreamWriter outputFile = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                                {
                                    for (int j = 0; j < lines.Count; j++)
                                    {
                                        outputFile.WriteLine(lines[j]);
                                    }
                                    outputFile.Close();
                                }
                            }
                        }
                        StopAndDoWork();
                    }
                }
                else if (listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].SubItems[2].Text != tboxPath.Text)
                {
                    var lines = new List<string>();
                    lines.AddRange(File.ReadAllLines(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)));

                    for (int i = 0; i <= lines.Count - 1; i++)
                    {
                        if (listView1.SelectedItems[0].SubItems[2].Text == lines[i])
                        {
                            lines[i] = tboxPath.Text;
                            lines[i + 1] = tboxPeriod.Text;
                            listView1.SelectedItems[0].SubItems[0].Text = "";
                            listView1.SelectedItems[0].SubItems[1].Text = Path.GetFileNameWithoutExtension(tboxPath.Text);
                            listView1.SelectedItems[0].SubItems[2].Text = tboxPath.Text;
                            listView1.SelectedItems[0].SubItems[3].Text = tboxPeriod.Text;
                            listView1.SelectedItems[0].SubItems[4].Text = "정보 불러오는 중";
                            listView1.SelectedItems[0].SubItems[5].Text = "정보 불러오는 중";
                            listView1.SelectedItems[0].SubItems[6].Text = "";


                            using (StreamWriter outputFile = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                            {
                                for (int j = 0; j < lines.Count; j++)
                                {
                                    outputFile.WriteLine(lines[j]);
                                }
                                outputFile.Close();
                            }

                            SystemSounds.Beep.Play();
                            MessageBox.Show("정상적으로 수정되었습니다.");
                            StopAndDoWork();
                            break;
                        }
                    }
                }
            }
            catch { }
            timer1.Start();
        }

        //모든 쓰레드의 동작을 멈추고 죽였다가 다시 실행시키는 함수
        private void StopAndDoWork()
        {
            for (int i = 0; i <= Proc.Length - 1; i++)  // 구독중인 종료이벤트 구독취소
            {
                Proc[i].Exited -=  null;
                if (Proc[i].EnableRaisingEvents == true)
                {
                    Proc[i].EnableRaisingEvents = false;
                }
            }

            for (int i = 0; i < Tindex; i++)    // 모든 쓰레드 종료
            {
                Tpro[i].Abort();
            }

            Tindex = 0;
            
            string path = String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath);
            string[] textvalue = System.IO.File.ReadAllLines(path);

            for (int i = 0; i <= (textvalue.Length - 2); i = i + 2)
            {
                int idx = i;
                int peri = int.Parse(textvalue[idx + 1]);
                Tpro[Tindex] = new Thread(() => BackWork(textvalue[idx], peri));
                Tpro[Tindex].IsBackground = true;
                Tpro[Tindex].Start();
                Tindex++;
            }
        }

        //로그디렉토리 및 로그파일 생성
        public void LogWrite(string str)
        {
            lock (lockObject)
            {
                string DirPath = Environment.CurrentDirectory + @"\Log";
                string FilePath = DirPath + "\\Log_" + DateTime.Today.ToString("yyyyMMdd") + ".log";
                string temp;

                DirectoryInfo di = new DirectoryInfo(DirPath);
                FileInfo fi = new FileInfo(FilePath);

                try
                {
                    if (!di.Exists) Directory.CreateDirectory(DirPath);
                    if (!fi.Exists)
                    {
                        using (StreamWriter sw = new StreamWriter(FilePath))
                        {
                            temp = string.Format("{0} {1}", DateTime.Now, str); //2020-08-05 오후 7:45:45 이런형식과 함수로 전달받은 str 문자열을 기록으로 남기게된다.
                            sw.WriteLine(temp);
                            sw.Close();
                        }
                        Thread.Sleep(100);
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(FilePath))
                        {
                            temp = string.Format("{0} {1}", DateTime.Now, str);
                            sw.WriteLine(temp);
                            sw.Close();
                        }
                        Thread.Sleep(100);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        //마지막 종료 로그
        public void ExitLogWrite(string str)
        {
            lock (lockObject)
            {


                string DirPath = Environment.CurrentDirectory + @"\ExitLog";
                string FilePath = DirPath + "\\ExitLog" + ".log";
                string temp;

                DirectoryInfo di = new DirectoryInfo(DirPath);
                FileInfo fi = new FileInfo(FilePath);

                try
                {
                    if (!di.Exists) Directory.CreateDirectory(DirPath);
                    if (!fi.Exists)
                    {
                        using (StreamWriter sw = new StreamWriter(FilePath))
                        {
                            temp = string.Format("{0} {1}", DateTime.Now, str);
                            sw.WriteLine(temp);
                            sw.Close();
                        }
                        Thread.Sleep(100);
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(FilePath))
                        {
                            temp = string.Format("{0} {1}", DateTime.Now, str);
                            sw.WriteLine(temp);
                            sw.Close();
                        }
                        Thread.Sleep(100);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        //DM 삭제 로그
        public void DeleteLogWrite(string str)
        {
            lock (lockObject)
            {


                string DirPath = Environment.CurrentDirectory + @"\DeleteLog";
                string FilePath = DirPath + "\\DeleteLog "+ DateTime.Today.ToString("yyyyMMdd") + ".log";
                string temp;

                DirectoryInfo di = new DirectoryInfo(DirPath);
                FileInfo fi = new FileInfo(FilePath);

                try
                {
                    if (!di.Exists) Directory.CreateDirectory(DirPath);
                    if (!fi.Exists)
                    {
                        using (StreamWriter sw = new StreamWriter(FilePath))
                        {
                            temp = string.Format("{0} {1}", DateTime.Now, str);
                            sw.WriteLine(temp);
                            sw.Close();
                        }
                        Thread.Sleep(1);
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(FilePath))
                        {
                            temp = string.Format("{0} {1}", DateTime.Now, str);
                            sw.WriteLine(temp);
                            sw.Close();
                        }
                        Thread.Sleep(1);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }



        //"삭제"버튼 클릭 이벤트
        private void btnDel_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            try
            {
                if (listView1.SelectedItems.Count == 0) // 선택된 항목이 없을경우
                {
                    MessageBox.Show("선택된 항목이 없습니다.");
                }
                else if (listView1.SelectedItems.Count != 0)   // 테이블에서 항목을 선택하고 삭제버튼을 눌렀다면
                {
                    var lines = new List<string>();
                    lines.AddRange(File.ReadAllLines(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)));
                    Thread.Sleep(10);

                    for (int i = 0; i <= lines.Count - 2; i = i + 2)
                    {
                        if (listView1.SelectedItems[0].SubItems[2].Text == lines[i])
                        {
                            LogWrite(lines[i] + "(을)를 관리 테이블에서 삭제합니다.");
                            lines.RemoveAt(i);
                            lines.RemoveAt(i);

                            using (StreamWriter outputFile = new StreamWriter(String.Format(@"{0}\List.txt", System.Windows.Forms.Application.StartupPath)))
                            {
                                for (int j = 0; j < lines.Count; j++)
                                {
                                    outputFile.WriteLine(lines[j]);
                                }
                                outputFile.Close();
                            }
                            SystemSounds.Beep.Play();
                            MessageBox.Show("정상적으로 삭제되었습니다.");
                            Thread.Sleep(10);
                            break;
                        }
                    }
                    listView1.SelectedItems[0].Remove();
                    tboxPath.Text = "";
                    tboxPeriod.Text = "";
                    btnRevise.Enabled = false;
                    btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48__1_;
                    btnRevise.BackColor = Color.Transparent;
                    btnAdd.Enabled = false;
                    btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48__1_;
                    btnAdd.BackColor = Color.Transparent;
                    btnDel.Enabled = false;
                    btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48__1_;
                    btnDel.BackColor = Color.Transparent;
                    StopAndDoWork();
                }
            }
            catch
            { }
            timer1.Start();
        }

        //시작프로그램 등록버튼
        private void btnSPR_Click(object sender, EventArgs e)
        {
            RegistrySet();
            btnSPR.Enabled = false;
            btnSPR.Image = PM.Properties.Resources.iconmonstr_clipboard_13_48__1_;
            btnSPR.BackColor = Color.Transparent;
            btnSPD.Enabled = true;
            btnSPD.Image = PM.Properties.Resources.iconmonstr_eraser_2_48;
            btnSPD.BackColor = Color.FromArgb(50, 49, 69);
        }
        //시작 프로그램 등록함수
        void RegistrySet()
        {
            try
            {
                // 시작프로그램 등록하는 레지스트리
                string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                RegistryKey strUpKey = Registry.LocalMachine.OpenSubKey(runKey);
                if (strUpKey.GetValue("PM") == null)
                {
                    strUpKey.Close();
                    strUpKey = Registry.LocalMachine.OpenSubKey(runKey, true);
                    // 시작프로그램 등록명과 exe경로를 레지스트리에 등록
                    strUpKey.SetValue("PM", Application.ExecutablePath);
                    SystemSounds.Beep.Play();
                    MessageBox.Show("시작 프로그램으로 등록합니다.");
                }
                else if (strUpKey.GetValue("PM") != null)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("이미 등록되어 있습니다..");
                }
            }
            catch
            {
                MessageBox.Show("Add Startup Fail");
            }
        }

        private void btnSPD_Click(object sender, EventArgs e)
        {
            RegistryDel();
            btnSPD.Enabled = false;
            btnSPD.Image = PM.Properties.Resources.iconmonstr_eraser_2_48__1_;
            btnSPD.BackColor = Color.Transparent;
            btnSPR.Enabled = true;
            btnSPR.Image = PM.Properties.Resources.iconmonstr_clipboard_13_48;
            btnSPR.BackColor = Color.FromArgb(50, 49, 69);
        }
        //시작프로그램 등록 삭제 함수
        void RegistryDel()
        {
            try
            {
                string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                RegistryKey strUpKey = Registry.LocalMachine.OpenSubKey(runKey, true);
                // 레지스트리값 제거
                strUpKey.DeleteValue("PM");
                SystemSounds.Beep.Play();
                MessageBox.Show("시작프로그램에서 제거되었습니다.");
            }
            catch
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("시작프로그램 리스트에 존재하지않습니다.");
            }
        }

        //타이틀 패널 드래그를 통한 이동가능하게 하는 함수
        private Point mCurrentPosition = new Point(0, 0);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mCurrentPosition = new Point(-e.X, -e.Y);
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(
                this.Location.X + (mCurrentPosition.X + e.X),
                this.Location.Y + (mCurrentPosition.Y + e.Y));
            }
        }

        //프로그램 위치열기 버튼
        private void btnOpen_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo(tboxPath.Text);
            

            if (fi.Exists)  //경로에 파일이 존재하는지 확인
            {
                Process.Start(Path.GetDirectoryName(tboxPath.Text));
            }
            else
            {
                try
                {
                    Process.Start(Path.GetDirectoryName(tboxPath.Text));
                    MessageBox.Show("해당경로상의 파일이 존재하지 않습니다.");
                }
                catch
                {
                    MessageBox.Show("해당경로상의 디렉토리 혹은 파일이 존재하지 않습니다.");
                }
            }
        }

        //프로그램 종료 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            //SystemSounds.Beep.Play();
            //if (MessageBox.Show("프로그램을 종료하시겠습니까?", "종료", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    try
            //    {
            //        for (int i = 0; i <= Proc.Length - 1; i++)
            //        {
            //            if (Proc[i].EnableRaisingEvents == true)
            //            {
            //                Proc[i].Exited -= null;
            //                Proc[i].EnableRaisingEvents = false;
            //            }
            //        }
            //    }
            //    catch { };
            //    LogWrite("PM프로그램이 종료되었습니다.");
            //    System.Diagnostics.Process.GetCurrentProcess().Kill();
            //}
        }

        //리스트뷰 구성요소 클릭 시 발생이벤트
        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].SubItems[6].Text != "종료됨")
            {
                btnRevise.Enabled = true;
                btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48;
                btnRevise.BackColor = Color.FromArgb(44, 43, 60);
                btnAdd.Enabled = false;
                btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48__1_;
                btnAdd.BackColor = Color.Transparent;
                btnDel.Enabled = true;
                btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48;
                btnDel.BackColor = Color.FromArgb(44, 43, 60);
                btnOpen.Enabled = true;
                btnOpen.Image = PM.Properties.Resources.iconmonstr_cloud_32_48;
                btnOpen.BackColor = Color.FromArgb(50, 49, 69);
                tboxPath.Text = listView1.SelectedItems[0].SubItems[2].Text;
                tboxPeriod.Text = listView1.SelectedItems[0].SubItems[3].Text;
            }
            if (listView1.SelectedItems.Count > 1)
            {
                btnRevise.Enabled = false;
                btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48__1_;
                btnRevise.BackColor = Color.Transparent;
                btnAdd.Enabled = false;
                btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48__1_;
                btnAdd.BackColor = Color.Transparent;
                btnDel.Enabled = false;
                btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48__1_;
                btnDel.BackColor = Color.Transparent;

                tboxPath.Text = "여러 항목을 한번에 수정하거나 삭제할 수 없습니다.";
                tboxPeriod.Text = "";
            }
            if (listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].SubItems[6].Text == "종료됨")
            {
                btnRevise.Enabled = true;
                btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48;
                btnRevise.BackColor = Color.FromArgb(44, 43, 60);
                btnAdd.Enabled = false;
                btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48__1_;
                btnAdd.BackColor = Color.Transparent;
                btnDel.Enabled = true;
                btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48;
                btnDel.BackColor = Color.FromArgb(44, 43, 60);
                btnOpen.Enabled = true;
                btnOpen.Image = PM.Properties.Resources.iconmonstr_cloud_32_48;
                btnOpen.BackColor = Color.FromArgb(50, 49, 69);
                tboxPath.Text = listView1.SelectedItems[0].SubItems[2].Text;
                tboxPeriod.Text = listView1.SelectedItems[0].SubItems[3].Text;
            }
        }

        //리스트뷰 구성요소가 없는 부분 클리시 발생이벤트
        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            //리스트뷰가 선택되어있으면 해제한다.
            if (listView1.SelectedItems.Count == 1)
            {
                listView1.SelectedItems[0].Selected = false;
                btnRevise.Enabled = false;
                btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48__1_;
                btnRevise.BackColor = Color.Transparent;
                btnAdd.Enabled = false;
                btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48__1_;
                btnAdd.BackColor = Color.Transparent;
                btnDel.Enabled = false;
                btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48__1_;
                btnDel.BackColor = Color.Transparent;
                btnOpen.Enabled = false;
                btnOpen.Image = PM.Properties.Resources.iconmonstr_cloud_32_48__1_;
                btnOpen.BackColor = Color.Transparent;
                tboxPath.Text = "";
                tboxPeriod.Text = "";
            }
            if (listView1.SelectedItems.Count > 1)
            {
                for (int i = listView1.SelectedItems.Count - 1; i >= 0; i--)
                {
                    listView1.SelectedItems[i].Selected = false;
                }
                btnRevise.Enabled = false;
                btnRevise.Image = PM.Properties.Resources.iconmonstr_edit_9_48__1_;
                btnRevise.BackColor = Color.Transparent;
                btnAdd.Enabled = false;
                btnAdd.Image = PM.Properties.Resources.iconmonstr_folder_24_48__1_;
                btnAdd.BackColor = Color.Transparent;
                btnDel.Enabled = false;
                btnDel.Image = PM.Properties.Resources.iconmonstr_folder_26_48__1_;
                btnDel.BackColor = Color.Transparent;
                tboxPath.Text = "";
                tboxPeriod.Text = "";
            }
        }

        //선택 프로그램 실행 버튼
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                for(int i=0;i<= listView1.Items.Count-1;i++)
                {
                    if(listView1.Items[i].SubItems[6].Text == "종료됨")
                    {
                        FileInfo fi = new FileInfo(listView1.Items[i].SubItems[2].Text);

                        if (fi.Exists && listView1.Items[i].Checked == true)  //경로에 파일이 존재하는지 확인 && 동작체크가 되어있으면 실행시킴
                        {
                            Process.Start(listView1.Items[i].SubItems[2].Text);
                            LogWrite(listView1.Items[i].SubItems[2].Text + "이(가) 실행중이지 않아 사용자의 시작요청에 의해 시작합니다.");
                        }
                        else if(!fi.Exists)
                        {
                            MessageBox.Show(listView1.Items[i].SubItems[2].Text + "이 경로가 존재하지 않거나 잘못되었습니다.");
                        }
                    }
                }
            }
        }

        //트레이 아이콘화를 위한 윈폼의 Resize 이벤트
        private void Form1_Resize(object sender, EventArgs e)
        {
            //윈도우 상태가 Minimized일 경우
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false; //창을 보이지 않게 한다.
                this.ShowIcon = false; //작업표시줄에서 제거.
                notifyIcon1.Visible = true; //트레이 아이콘을 표시한다.
            }
        }

        //트레이 아이콘 더블클릭시 다시 화면에 나타나는이벤트
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            //Notify Icon을 더블클릭했을시 일어나는 이벤트.
            this.Visible = true;
            this.ShowIcon = true;
            notifyIcon1.Visible = false; //트레이 아이콘을 숨긴다.

            IntPtr hWnd = FindWindow(null, "PM (Administrator)");
            SetForegroundWindow(hWnd);

            if (!hWnd.Equals(IntPtr.Zero))
            {
                // 윈도우가 최소화 되어 있다면 활성화 시킨다
                ShowWindowAsync(hWnd, SW_SHOWNORMAL);

                // 윈도우에 포커스를 줘서 최상위로 만든다
                SetForegroundWindow(hWnd);
            }
        }


        //트레이 아이콘일 때 우측  클릭 Show 메뉴 선택시 이벤트
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Notify Icon을 더블클릭했을시 일어나는 이벤트.
            this.Visible = true;
            this.ShowIcon = true;
            notifyIcon1.Visible = false; //트레이 아이콘을 숨긴다.

            IntPtr hWnd = FindWindow(null, "PM (Administrator)");
            SetForegroundWindow(hWnd);

            if (!hWnd.Equals(IntPtr.Zero))
            {
                // 윈도우가 최소화 되어 있다면 활성화 시킨다
                ShowWindowAsync(hWnd, SW_SHOWNORMAL);

                // 윈도우에 포커스를 줘서 최상위로 만든다
                SetForegroundWindow(hWnd);
            }
        }

        //트레이 아이콘일 때 우측  클릭 Exit 메뉴 선택시 이벤트
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();
            if (MessageBox.Show("프로그램을 종료하시겠습니까?", "종료", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i <= Proc.Length - 1; i++)
                {
                    Proc[i].Exited -= null; 
                    Proc[i].EnableRaisingEvents = false;
                }
                LogWrite("PM프로그램이 종료되었습니다.");
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string syswowpath = "C:\\Windows\\SysWOW64\\ExitLog";
            FileInfo fi = new FileInfo(syswowpath);

            try
            {
                Process.Start(syswowpath);
            }
            catch
            {
                string filepath = System.Windows.Forms.Application.StartupPath;
                Process.Start(filepath);
            }
                                    
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            timer1.Stop();
            
            if (listView1.SelectedItems.Count != 0) // 현재 탭이 프로세스 탭이면서 리스트뷰에 클릭되어진게 있을 때
            {
                try
                {
                    foreach (var process in Process.GetProcessesByName(listView1.SelectedItems[0].SubItems[1].Text))
                    {
                        process.Kill();
                    }
                    MessageBox.Show("정상적으로 종료되었습니다.", "Message", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
            else if(listView1.SelectedItems.Count <= 0 )
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("종료시킬 작업을 선택해 주세요.");
            }
           

            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            string select_path = dialog.SelectedPath;
            if(!(select_path==""))
            {
                dmStopbtn_Click(sender,e);
                textBox1.Text = select_path;
            }

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Percentlbl.Text = ScrollBar.Value.ToString() + " %";
            Percentlbl2.Text = ScrollBar.Value.ToString() + " %";
        }
        private void hScrollBar1_Scroll_1(object sender, ScrollEventArgs e)
        {
            Daylbl.Text = ScrollBar2.Value.ToString() + " 일";
        }
      
        //DM탭 클릭시
        private void tabPage2_Enter(object sender, EventArgs e)
        {
        }

        //DM설정저장 버튼 클릭 이벤트
        private void button3_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text) == false)
            {
                if (true)
                {
                    //현재의 DM 설정사항을 파일로 남겨서 저장시킨다.
                    string DirPath = Environment.CurrentDirectory;
                    string FilePath = DirPath + "\\DM" + ".txt";

                    DirectoryInfo di = new DirectoryInfo(DirPath);
                    FileInfo fi = new FileInfo(FilePath);

                    try
                    {
                        if (!di.Exists) Directory.CreateDirectory(DirPath);
                        if (!fi.Exists)
                        {
                            using (StreamWriter sw = new StreamWriter(FilePath))
                            {
                                sw.WriteLine(textBox1.Text);
                                sw.WriteLine(ScrollBar.Value.ToString());
                                sw.WriteLine(ScrollBar2.Value.ToString());
                                sw.WriteLine("0");
                                sw.Close();
                            }

                            dmStopbtn.Enabled = false;
                            dmStopbtn.Image = PM.Properties.Resources.iconmonstr_media_control_50_48__1_;
                            dmStopbtn.BackColor = Color.Transparent;
                            dmStartbtn.Enabled = true;
                            dmStartbtn.Image = PM.Properties.Resources.iconmonstr_media_control_48_48__1_;
                            dmStartbtn.BackColor = Color.FromArgb(50, 49, 69);
                            drivelbl.Text = "   " + textBox1.Text.Substring(0, 1);

                            timer2.Stop();
                            dmflag = false;
                            Thread.Sleep(100);
                        }
                        else
                        {
                            using (StreamWriter sw = new StreamWriter(FilePath))
                            {
                                sw.WriteLine(textBox1.Text);
                                sw.WriteLine(ScrollBar.Value.ToString());
                                sw.WriteLine(ScrollBar2.Value.ToString());
                                sw.WriteLine("0");
                                sw.Close();
                            }

                            dmStopbtn.Enabled = false;
                            dmStopbtn.Image = PM.Properties.Resources.iconmonstr_media_control_50_48__1_;
                            dmStopbtn.BackColor = Color.Transparent;
                            dmStartbtn.Enabled = true;
                            dmStartbtn.Image = PM.Properties.Resources.iconmonstr_media_control_48_48__1_;
                            dmStartbtn.BackColor = Color.FromArgb(50, 49, 69);
                            drivelbl.Text = "   " + textBox1.Text.Substring(0, 1);

                            timer2.Stop();

                            Thread.Sleep(100);
                        }


                        SystemSounds.Beep.Play();
                        //MessageBox.Show("설정이 저장되었습니다.");
                    }
                    catch { }
                        
                }
                else
                {
                    //SystemSounds.Beep.Play();
                    //MessageBox.Show("저장방식을 설정해주세요..");
                }
            }
            else
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("관리할 폴더를 선택해주세요.");
            }
        }

        //드라이브 사용중인 용량을 표시하여 DM탭에 표시해주는 함수
        private void usingdrive()
        {
            while(true)
            {
                try
                {
                    var Cdrive = new DriveInfo("C");
                    int Cpercent = 0;
                    int Ctotal = (int)(Cdrive.TotalSize / 1000000);
                    int Cuse = (int)((Cdrive.TotalSize - Cdrive.AvailableFreeSpace) / 1000000);
                    Cpercent = (Cuse / (Ctotal / 100));

                    Clabel.Text = Cpercent.ToString() + " %";
                    progressBarC.Value = Cpercent;
                }
                catch
                {
                    Clabel.Text = "C드라이브가 존재하지 않습니다.";
                }

                try
                { 
                    var Ddrive = new DriveInfo("D");
                    int Dpercent = 0;
                    int Dtotal = (int)(Ddrive.TotalSize / 1000000);
                    int Duse = (int)((Ddrive.TotalSize - Ddrive.AvailableFreeSpace) / 1000000);
                    Dpercent = (Duse / (Dtotal / 100));

                    Dlabel.Text = Dpercent.ToString() + " %";
                    progressBarD.Value = Dpercent;
                }
                catch 
                {
                    Dlabel.Text = "D드라이브가 존재하지 않습니다.";
                }
                Thread.Sleep(2000);
            }
        }

        static int fcount;
        bool dmflag = false;
        public void dmStartbtn_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);

            var drive = new DriveInfo("C");
            int percent = 0;
            int total = (int)(drive.TotalSize / 1000000);
            int use = (int)((drive.TotalSize - drive.AvailableFreeSpace) / 1000000);
            percent = (use / (total/100));  //int형에서 작은수를 큰수로 나누면 0이 되므로 퍼센티지 구할 때 분모에 100을 먼저 나눠준다.
            progressBarC.Value = percent;

            int expire=9999;

            if (String.IsNullOrWhiteSpace(textBox1.Text) == false)
            {
                string DirPath = Environment.CurrentDirectory;
                string FilePath = DirPath + "\\DM" + ".txt";
                
                DirectoryInfo di = new DirectoryInfo(DirPath);
                FileInfo fi = new FileInfo(FilePath);

                string DMaddress = "";

                try
                {
                    if (!di.Exists) Directory.CreateDirectory(DirPath);
                    if (fi.Exists)
                    {
                        dmStartbtn.Enabled = false;
                        dmStartbtn.Image = PM.Properties.Resources.iconmonstr_media_control_48_48;
                        dmStartbtn.BackColor = Color.Transparent;
                        dmStopbtn.Enabled = true;
                        dmStopbtn.Image = PM.Properties.Resources.iconmonstr_media_control_50_48;
                        dmStopbtn.BackColor = Color.FromArgb(50, 49, 69);

                        //DM프로그램이 동작시에 수행되는 프로세스

                        //설정해둔 디렉토리의 하위 디렉토리와 파일들을 검색
                        fcount = 1;
                        //DirFileSearch(textBox1.Text, "jpg");

                        FileInfo fi2 = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\DM.txt");
                        if (fi2.Exists)
                        {
                            try
                            {
                                string[] value = System.IO.File.ReadAllLines(fi2.ToString());
                                DMaddress = value[0];
                                expire = Int32.Parse(value[2]);
                                
                                using (StreamWriter sw = new StreamWriter(FilePath))
                                {
                                    sw.WriteLine(value[0]);
                                    sw.WriteLine(value[1]);
                                    sw.WriteLine(value[2]);
                                    sw.WriteLine("1");
                                    sw.Close();
                                }
                            }
                            catch
                            { }
                        }

                        //용량단위로 삭제하는 쓰레드 실행
                        Thread t1 = new Thread(new ThreadStart(capacitydelete));
                        dmflag = true;
                        t1.Start();

                        timer2.Start();
                        //Thread DMthread = new Thread(() => DirFileSearch(DMaddress, "jpg", expire));
                        //DMthread.Start();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if(String.IsNullOrWhiteSpace(textBox1.Text))
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("관리폴더를 설정해주세요.");
            }
        }

        
        public void dmStopbtn_Click(object sender, EventArgs e)
        {
            string DirPath = Environment.CurrentDirectory;
            string FilePath = DirPath + "\\DM" + ".txt";

            dmStopbtn.Enabled = false;
            dmStopbtn.Image = PM.Properties.Resources.iconmonstr_media_control_50_48__1_;
            dmStopbtn.BackColor = Color.Transparent;
            dmStartbtn.Enabled = true;
            dmStartbtn.Image = PM.Properties.Resources.iconmonstr_media_control_48_48__1_;
            dmStartbtn.BackColor = Color.FromArgb(50, 49, 69);

            dmflag = false;
            timer2.Stop();

            try
            {
                string[] value = System.IO.File.ReadAllLines(FilePath.ToString());

                using (StreamWriter sw = new StreamWriter(FilePath))
                {
                    sw.WriteLine(value[0]);
                    sw.WriteLine(value[1]);
                    sw.WriteLine(value[2]);
                    sw.WriteLine("0");
                    sw.Close();
                }
            }
            catch { }

              
        }

        ////특정시간에 특정이벤트를 종료하기위한 이벤트
        //private void timer2_Tick(object sender, EventArgs e)
        //{
        //    DateTime date = DateTime.Now;
        //    if(date.ToString()=="2020-11-17 오후 2:29:00")  //자정은 "2020-11-18 오전 0:00:00" 이다
        //    {
        //        for(int i = 0; i < Proc.Length; i++)
        //        {
        //            if(Proc[i].ProcessName=="notepad")
        //            {
        //                Proc[i].Kill();
        //            }
        //        }
        //        MessageBox.Show("지정된시간이 되었습니다.");
        //    }
        //}


        //용량별, 기간별 삭제 함수
        List<string> dirList = new List<string>();
        public void capacitydelete()
        {
            while (dmflag==true)
            {
                Thread.Sleep(4000); //각 드라이브 용량정보 가져오는게 2초마다이므로 용량정보 가져오기전에 비교하면 다삭제해버리니 이렇게 안전장치를 설치한다.
                string DMaddress;

                FileInfo fi3 = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\DM.txt");
                if (fi3.Exists)
                {
                    try
                    {
                        string[] value = System.IO.File.ReadAllLines(fi3.ToString());
                        DMaddress = value[0];
                    }
                    catch { }
                }

                string oldestFolder = "";
                DirSearch(textBox1.Text);//최하위 폴더정보만 캐치
                DirectoryInfo dir1 = new DirectoryInfo(dirList[0]);

                if (dirList.Count > 1)
                {
                    for (int i = 0; i < dirList.Count - 2; i++)
                    {
                        DirectoryInfo dir2 = new DirectoryInfo(dirList[i + 1]);
                        if (DateTime.Compare(dir1.CreationTime, dir2.CreationTime) > 0)
                        {
                            dir1 = dir2;
                        }
                    }
                    oldestFolder = dir1.FullName;
                }
                else if (dirList.Count == 1)
                {
                    oldestFolder = dir1.FullName;
                }

                //경로 자신폴더마저 삭제하는 바람에 이러한 조건을 추가하였다.
                if(oldestFolder==textBox1.Text)
                {
                    dirList.Clear();
                    continue;
                }

                try
                {
                    FileInfo fi2 = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\DM.txt");
                    string[] value = System.IO.File.ReadAllLines(fi2.ToString());
                    int capacity = Int32.Parse(value[1]);
                    string sp = textBox1.Text;
                    string drive = (sp.Substring(0, 1));
                    string[] files = Directory.GetFiles(oldestFolder);//, $"*.jpg");

                    //MessageBox.Show(oldestFolder);

                    //기간단위 삭제방식 시작 ---------------------------------------
                    foreach (string f in files)
                    {
                        var info = new FileInfo(f);
                        //MessageBox.Show(info.Name);
                        DateTime now = DateTime.Now;
                        TimeSpan time = now - info.CreationTime;
                        //MessageBox.Show(time.Days.ToString());
                        if (Int32.Parse(time.Days.ToString()) > Int32.Parse(value[2]) && dmflag == true)//실험용으로 바로삭제시 조건은 ">= 0" 이며 원본 조건은  "> Int32.Parse(value[2])" 이다.
                        {
                            DeleteLogWrite(f+"를 삭제하였습니다.");

                            info.Delete();
                        }
                    }
                    //기간단위 삭제방식 끝 -----------------------------------------


                    //용량단위 삭제방식 시작 ---------------------------------------
                    if (drive == "C")
                    {
                        if (progressBarC.Value > capacity && dmflag == true)
                        {
                            DirectoryInfo di = new DirectoryInfo(oldestFolder);

                            foreach (string f in files)
                            {
                                DeleteLogWrite(f + "를 삭제하였습니다.");
                            }

                            di.Delete(true);
                        }
                    }
                    else if (drive == "D")
                    {
                        if (progressBarD.Value > capacity && dmflag == true)
                        {
                            DirectoryInfo di = new DirectoryInfo(oldestFolder);

                            foreach (string f in files)
                            {
                                DeleteLogWrite(f + "를 삭제하였습니다.");
                            }

                            di.Delete(true);
                        }
                    }
                    //용량단위 삭제방식 끝------------------------------------------

                }
                catch { }

                dirList.Clear();
            }
        }

        public void DirSearch(string path)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);

                if (dirs.Length > 0)
                {
                    foreach (string dir in dirs)
                    {
                        //MessageBox.Show(dir);
                        DirSearch(dir);
                    }
                }
                else if(dirs.Length == 0)
                {
                    DirectoryInfo di = new DirectoryInfo(path); //최하위 폴더정보
                    dirList.Add(di.FullName);
                }
            }
            catch//(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }


        //텅빈디렉토리 삭제 (폴더만들어진지 하루가 지낫지만 안에 내용물없으면 삭제시킴.)------------------------------------------------------------------------------------------------------------------------
        static void DirFileSearch(string path, string file,int expire)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                string[] files = Directory.GetFiles(path, $"*.{file}");

                for (int i = 0; i < dirs.Length; i++)
                {
                    //MessageBox.Show(i.ToString() + dirs[i]);
                    var d = new DirectoryInfo(dirs[i]);
                    try
                    {
                        DateTime now = DateTime.Now;
                        TimeSpan time = now - d.CreationTime;

                        //MessageBox.Show(time.Days.ToString());    //폴더만들어진지 몇일지났는지 확인하는 메세지

                        if(Int32.Parse(time.Days.ToString()) >= 1)  //폴더만들어진지 하루가 지낫지만 안에 내용물없으면 삭제시킴.
                        {
                            d.Delete();
                        }
                        //MessageBox.Show(d.Name + "디렉토리가 삭제되었습니다.");
                    }
                    catch//(Exception e)
                    {
                        //MessageBox.Show(e.Message);
                    }
                }


                //foreach (string f in files)
                //{
                    // 이 곳에 해당 파일을 찾아서 처리할 코드를 삽입하면 된다.   
                    //Console.WriteLine($"[{count++}] path - {f}");               
                    //MessageBox.Show($"[{fcount++}] path - {f}");

                    //var info = new FileInfo(f);
                    //DateTime now = DateTime.Now;
                    //TimeSpan time = now - info.CreationTime;

                    //if (Int32.Parse(time.Days.ToString()) > expire)
                    //{
                    //    info.Delete();
                    //}
                //}


                if (dirs.Length > 0)
                {
                    foreach (string dir in dirs)
                    {
                        //MessageBox.Show(dir);
                        DirFileSearch(dir, file, expire);
                    }
                }
                //else if(dirs.Length == 0)
                //{
                //    DirectoryInfo di = new DirectoryInfo(path); //최하위 폴더정보
                //}
            }
            catch//(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        
        //비어있는폴더 삭제함수가 돌아가는 타이머
        public void timer2_Tick(object sender, EventArgs e)
        {
            string DMaddress = "";
            int expire = 9999;

            FileInfo fi2 = new FileInfo(System.Windows.Forms.Application.StartupPath + @"\DM.txt");
            if (fi2.Exists)
            { 
                try
                {
                    string[] value = System.IO.File.ReadAllLines(fi2.ToString());
                    DMaddress = value[0];
                    expire = Int32.Parse(value[2]);
                }
                catch
                { }
            }
            DirFileSearch(DMaddress, "jpg", expire);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}