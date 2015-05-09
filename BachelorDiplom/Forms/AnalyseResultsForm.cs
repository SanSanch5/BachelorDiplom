using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BachelorLibAPI.Data;

namespace BachelorLibAPI.Forms
{
    public partial class AnalyseResultsForm : Form
    {
        public readonly Dictionary<int, string> ConsignmentsRate = new Dictionary<int, string>()
        {
            {1, "Чрезвычайно опасный"},
            {2, "Высокоопасный"},
            {3, "Умеренно опасный"},
            {4, "Малоопасные"}
        };

        private List<AnalyseReturnType> _result;
        public AnalyseResultsForm(List<AnalyseReturnType> res)
        {
            InitializeComponent();
            _result = res;
            _result.Sort((a, b) => a.DangerDegree.CompareTo(b.DangerDegree));

            FillConsignments();
        }

        private void FillConsignments()
        {
            if (_result.Count != 0)
            {
                foreach (var res in _result)
                {
                    cmbRes.Items.Add(res.ConsName);
                }
                cmbRes.SelectedIndex = 0;
                FillFormWithIndex(0);
                Show();
            }
            else
            {
                MessageBox.Show("Анализ не выявил возможной опасности.", "Информация");
                Close();
            }
        }

        private void FillFormWithIndex(int index)
        {
            var curTrans = _result[index];
            lblDangerDegree.Text = curTrans.DangerDegree.ToString() + " (" + ConsignmentsRate[curTrans.DangerDegree] + ")";
            lblDriverName.Text = curTrans.DriversName;
            lblDriverSurname.Text = curTrans.DriversSurname;
            lblPlace.Text = curTrans.City;
            edtAfterCrash.Text = curTrans.AfterCrash;
            dtpLoc.Value = curTrans.Location;

            cmbDriverNumbers.Items.Clear();
            foreach (var number in curTrans.DriversNumbers)
                cmbDriverNumbers.Items.Add(number);

            cmbDriverNumbers.SelectedIndex = 0;

            var clr = GetColor(curTrans.DangerDegree);
            lblDangerDegree.ForeColor = clr;
            lblDriverName.ForeColor = clr;
            lblDriverSurname.ForeColor = clr;
            lblPlace.ForeColor = clr;
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

        private void cmbRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillFormWithIndex(cmbRes.SelectedIndex);
        }
    }
}
