using System.Windows.Forms;

namespace BachelorLibAPI.Forms
{
    public partial class ProgressBarForm : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                var ret = base.CreateParams;
                ret.ExStyle |= (int)0x08000000L;
                return ret;
            }
        }

        public void Progress(int count = 1)
        {
            progressBar.Value += count;
        }

        public void Complete()
        {
            progressBar.Value = progressBar.Maximum-1;
        }

        public ProgressBarForm(string processName, int count)
        {
            InitializeComponent();
            Text = processName;
            Show();
            var screen = Screen.PrimaryScreen;
            Left = screen.WorkingArea.Right - Width;
            Top = screen.WorkingArea.Top + 50;

            progressBar.Maximum = count;
            progressBar.Minimum = 0;
            progressBar.Value = 0;
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
    }
}
