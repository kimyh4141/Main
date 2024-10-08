using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WiseM.Browser
{
    public partial class JigSpecItem : UserControl
    {
        public delegate void JigSpecItemEventHandler(object sender);
        public event JigSpecItemEventHandler DeleteButtonClicked;

        public string Spec
        {
            get { return this.lbl_Spec.Text; }
            set { this.lbl_Spec.Text = value; }
        }

        public string MinValue
        {
            get { return this.tb_MinValue.Text; }
            set { this.tb_MinValue.Text = value; }
        }

        public string MaxValue
        {
            get { return this.tb_MaxValue.Text; }
            set { this.tb_MaxValue.Text = value; }
        }

        private bool _DelMode = false;

        public bool DelMode
        {
            get { return this._DelMode; }
            set { this._DelMode = this.btn_Del.Visible = value; }
        }

        public JigSpecItem(string spec, string minValue, string maxValue)
        {
            InitializeComponent();

            this.btn_Del.Visible = false;

            this.lbl_Spec.Text = spec;
            this.tb_MinValue.Text = minValue;
            this.tb_MaxValue.Text = maxValue;
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            if (this.DeleteButtonClicked != null)
            {
                this.DeleteButtonClicked.Invoke(this);
            }
        }
    }
}
