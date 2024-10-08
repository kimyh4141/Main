using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WiseM.Browser
{
    public partial class JigPeriodMsg : Form
    {
        public string period1;
        public string period2;

        public JigPeriodMsg()
        {
            InitializeComponent();

            period1 = "";
            period2 = "";

            textBox1.Text = DateTime.Today.ToString("yyyy-MM-dd");
            textBox2.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = !textBox1.Enabled;
            textBox2.Enabled = !textBox2.Enabled;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                period1 = "";
                period2 = "";
            }
            else
            {
                period1 = textBox1.Text;
                period2 = textBox2.Text;
            }

            Close();
        }
    }
}
