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
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using Microsoft.Win32;
using System.Windows.Forms.VisualStyles;

namespace xFFplay
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        Form2 form2 = new Form2();

        public Form1()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            this.DoubleBuffered = true;

            
        }
        public Process ffplay = new Process();
       

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            form2.Show();
            

            xxxFFplay();
        }
        private void xxxFFplay()
        {
            
             
           
            ffplay.StartInfo.FileName = "c:\\ffmpeg\\ffplay.exe";
            ffplay.StartInfo.Arguments = "-noborder c:\\ffmpeg\\bay.mp4";
            
            ffplay.StartInfo.CreateNoWindow = true;
            ffplay.StartInfo.RedirectStandardOutput = true;
            ffplay.StartInfo.UseShellExecute = false;
            

            ffplay.EnableRaisingEvents = true;
            ffplay.OutputDataReceived += (o, e) => Debug.WriteLine(e.Data ?? "NULL", "ffplay");
            ffplay.ErrorDataReceived += (o, e) => Debug.WriteLine(e.Data ?? "NULL", "ffplay");
            ffplay.Exited += (o, e) => Debug.WriteLine("Exited", "ffplay");
            ffplay.Start();

           // Thread.Sleep(5000); // you need to wait/check the process started, then...
           timer1.Enabled = true;
            // child, new parent
            // make 'this' the parent of ffmpeg (presuming you are in scope of a Form or Control)
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try { ffplay.Kill(); }
            catch { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            SetParent(ffplay.MainWindowHandle, form2.Handle);

            // window, x, y, width, height, repaint
            // move the ffplayer window to the top-left corner and set the size to 320x280
            MoveWindow(ffplay.MainWindowHandle, 0, 0, form2.Width, form2.Height, true);
            timer1.Enabled=false;
        }
    }
}
