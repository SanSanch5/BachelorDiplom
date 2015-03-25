using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            _result.Sort((a, b) => a.dangerDegree.CompareTo(b.dangerDegree));

            FillConsignments();
        }

        private void FillConsignments()
        {
            if (_result.Count != 0)
            {
                foreach (var res in _result)
                {
                    cmbRes.Items.Add(res.consName);
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
            AnalyseReturnType curTrans = _result[index];
            lblDangerDegree.Text = curTrans.dangerDegree.ToString() + " (" + ConsignmentsRate[curTrans.dangerDegree] + ")";
            lblDriverName.Text = curTrans.driversName;
            lblDriverSurname.Text = curTrans.driversSurname;
            lblPlace.Text = curTrans.city;
            edtAfterCrash.Text = curTrans.afterCrash;
            dtpLoc.Value = curTrans.location;

            cmbDriverNumbers.Items.Clear();
            foreach (var number in curTrans.driversNumbers)
                cmbDriverNumbers.Items.Add(number);

            cmbDriverNumbers.SelectedIndex = 0;

            Color clr = GetColor(curTrans.dangerDegree);
            lblDangerDegree.ForeColor = clr;
            lblDriverName.ForeColor = clr;
            lblDriverSurname.ForeColor = clr;
            lblPlace.ForeColor = clr;
        }

        private Color GetColor(int dangerDegree)
        {
            Color clr = new Color();
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
