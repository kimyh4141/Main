using System.Windows.Forms;

namespace MES_Monitoring.Forms
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
            var version = Application.ProductVersion;
        }
    }
}