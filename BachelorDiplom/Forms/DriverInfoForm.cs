using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BachelorLibAPI.Program;

namespace BachelorLibAPI.Forms
{
    public partial class DriverInfoForm : Form
    {
        //List<DriverInfoType> _info;
        QueriesHandler _queriesHandler;
        public DriverInfoForm(QueriesHandler qh)
        {
            InitializeComponent();
            _queriesHandler = qh;

            FillDrivers();
        }

        private void FillDrivers()
        {
            var drivers = _queriesHandler.GetDriversFullNames();

            cmbDriver.Items.Clear();
            cmbDriver.Items.AddRange(drivers.Select(x => (object)x).ToArray());

            cmbDriver.SelectedIndex = -1;
            ClearForm();
        }

        private void ClearForm()
        {
            cmbNumbers.Items.Clear();
            dtpStart.Visible = false;
            dtpArr.Visible = false;
            lblArrTime.Text = "";
            lblConsName.Text = "";
            lblFrom.Text = "";
            lblTo.Text = "";
            lblProbableLocation.Visible = false;
            lblProbableLocSub.Visible = false;
            lblID.Text = "";
            lblStatus.Text = "";
        }

        private void FillFormWithIndex(int index)
        {
            try
            {
                var driverInfo = _queriesHandler.GetComboBoxedDriverInfo(index);

                lblID.Text = driverInfo.Id.ToString();
                cmbNumbers.Items.Clear();
                foreach (var num in driverInfo.Numbers)
                    cmbNumbers.Items.Add(num);
                cmbNumbers.SelectedIndex = 0;

                dtpArr.Visible = true;
                lblFrom.Text = driverInfo.StartLocation;
                lblTo.Text = driverInfo.GoalLocation;
                lblConsName.Text = driverInfo.ConsName;
                lblConsName.ForeColor = GetColor(driverInfo.DangerDegree);

                if (driverInfo.Status)
                {
                    lblProbableLocSub.Visible = false;
                    lblProbableLocation.Visible = false;
                    lblStatus.Text = "Завершена";
                    lblArrTime.Text = "Время прибытия:";
                    dtpArr.Value = driverInfo.Arrival;
                    lblStatus.ForeColor = Color.Green;
                }
                else
                {
                    dtpStart.Visible = true;
                    dtpStart.Value = driverInfo.Start;
                    lblProbableLocation.Text = driverInfo.ProbableLocation;
                    lblProbableLocSub.Visible = true;
                    lblProbableLocation.Visible = true;
                    lblStatus.Text = "Не завершена";
                    lblArrTime.Text = "Время прибытия (предположительное):";
                    dtpArr.Value = driverInfo.ProbableArrival;
                    lblStatus.ForeColor = Color.BlueViolet;
                }
            }
            catch (NullReferenceException ex)
            {
                ClearForm();
                var driverId = _queriesHandler.GetComboBoxedDriverId(index);
                lblID.Text = driverId.ToString();
                foreach (var num in _queriesHandler.GetDriverNumbers(driverId))
                    cmbNumbers.Items.Add(num);
                cmbNumbers.SelectedIndex = 0;

                MessageBox.Show(ex.Message);
            }
        }

        private Color GetColor(int dangerDegree)
        {
            var clr = new Color();
            switch (dangerDegree)
            {
                case 1:
                    clr = Color.Red;
                    break;
                case 2:
                    clr = Color.OrangeRed;
                    break;
                case 3:
                    clr = Color.YellowGreen;
                    break;
                case 4:
                    clr = Color.Green;
                    break;
            }
            return clr;
        }

        private void cmbDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearForm();
            FillFormWithIndex(cmbDriver.SelectedIndex);
        }
    }
}
