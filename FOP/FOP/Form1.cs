using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FOP
{
    public partial class Form1 : Form
    {
        Timer time = new Timer();        // Existing timer for other purposes
        Timer fadeOutTimer = new Timer();  // Timer for fade-out animation
        public Point mouseLocation;

        public Form1()
        {
            InitializeComponent();

            // Set up the regular timer
            time.Tick += timertick;
            time.Start();

            // Set up the fade-out timer
            fadeOutTimer.Interval = 10;  // Interval in milliseconds for smooth fade
            fadeOutTimer.Tick += fadeOutTimer_Tick;  // Link fade timer to event handler
        }

        // Existing method to check Roblox status and update UI
        private void timertick(object sender, EventArgs e)
        {
            if (ForlornApi.Api.IsRobloxOpen())
            {
                robloxopen.Text = " ";
                robloxopen.ForeColor = Color.LightGreen;
            }
            else
            {
                robloxopen.Text = " ";
                robloxopen.ForeColor = Color.Red;
            }

            if (ForlornApi.Api.IsInjected())
            {
                status.Text = "Status: Injected!";
                status.ForeColor = Color.LightGreen;
            }
            else
            {
                status.Text = "Status: Not Injected!";
                status.ForeColor = Color.Red;
            }
        }

        // Fade-out logic to reduce opacity step-by-step
        private void fadeOutTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)  // Reduce opacity gradually
            {
                this.Opacity -= 0.05;  // Decrease opacity by 5% at each tick
            }
            else
            {
                fadeOutTimer.Stop();  // Stop the timer when opacity reaches 0
                this.Close();  // Close the form after fade-out
            }
        }

        // X button click event with fade-out effect
        private void X_Click(object sender, EventArgs e)
        {
            fadeOutTimer.Start();  // Start the fade-out animation
        }

        // Other methods (existing functionality)
        private void button2_Click(object sender, EventArgs e)
        {
            ForlornApi.Api.Inject();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ForlornApi.Api.ExecuteScript(Editor.Text);
        }

        private void status_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Editor.Text = File.ReadAllText($"./Scripts/{listBox1.SelectedItem}");
        }

        private void Button_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            functions.PopulateListBox(listBox1, "./Scripts", "*.lua");
            functions.PopulateListBox(listBox1, "./Scripts", "*.txt");
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void Editor_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void robloxopen_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }

    // Helper class for populating ListBox
    class functions
    {
        public static void PopulateListBox(System.Windows.Forms.ListBox lsb, string Folder, string FileType)
        {
            DirectoryInfo dinfo = new DirectoryInfo(Folder);
            FileInfo[] Files = dinfo.GetFiles(FileType);
            foreach (FileInfo file in Files)
            {
                lsb.Items.Add(file.Name);
            }
        }
    }
}
