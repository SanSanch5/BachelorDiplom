using System.Windows.Forms;

namespace BachelorLibAPI.Forms
{
    public partial class ProgressBarForm : Form
    {
        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        public void Progress(int count = 1)
        {
            progressBar.Value += count;
        }

        public ProgressBarForm(string processName, int count)
        {
            Text = processName;
            InitializeComponent();
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
