using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace WindowsFormsApplication9
{
    public partial class Form1 : Form
    {
        string [] portlar = SerialPort.GetPortNames();
        string[] baudRates = new string[] { "115200", "57600", "38400", "9600" };
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            foreach (string port in portlar)
            {
                comboBox1.Items.Add(port);
                comboBox1.SelectedItem = 0;
            }

            comboBox1.SelectedIndex = 1;
            richTextBox2.Text = DateTime.Now + ":    " + "Program started";
            checkBox_time.Checked = true;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (button1.Text == "Connect" && !serialPort1.IsOpen)
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                    //serialPort1.DataReceived += serialport1_datareceived;
                    serialPort1.Open();

                    button1.Text = "Disconnect";
                    label2.Text = "Status: Connected";
                    label2.BackColor = Color.GreenYellow;
                }
                else if (button1.Text == "Disconnect" && serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    button1.Text = "Connect";
                    label2.Text = "Status: None";
                    label2.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            serialPort1.Write(richTextBox1.Text);
            richTextBox1.Clear();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string buffer = sp.ReadExisting();


            
            if (checkBox_time.Checked)
            {
                richTextBox2.Text += DateTime.Now + buffer + Environment.NewLine;
            }
            else
            {
                richTextBox2.Text += buffer + Environment.NewLine;
            }
        }
    }
}
