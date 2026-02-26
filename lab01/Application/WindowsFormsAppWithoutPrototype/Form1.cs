using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSoftware_Click(object sender, EventArgs e)
        {

            string name = string.IsNullOrWhiteSpace(inputNameSoftware.Text) ? "Без названия" : inputNameSoftware.Text;
            string code = string.IsNullOrWhiteSpace(inputSourceCode.Text) ? "Не указан" : inputSourceCode.Text;
            string terms = string.IsNullOrWhiteSpace(inputTermsOfReference.Text) ? "Стандартное ТЗ" : inputTermsOfReference.Text;


            List<string> requirements = new List<string>();
            if (listReq.Items.Count > 0)
            {
                foreach (var item in listReq.Items)
                {
                    if (item != null)
                        requirements.Add(item.ToString());
                }
            }
            else
            {
                requirements.Add("Анализ требований");
                requirements.Add("Проектирование системы");
                requirements.Add("Сдача документации");
            }

            SoftwareContract contract = new SoftwareContract(name, code, terms, requirements);

            listContract.Items.Add(contract.GetInfo());
            MessageBox.Show("Договор создан!\n\n" + contract.GetInfo());

            listReq.Items.Clear();
            inputNameSoftware.Clear();
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            string type = inputLicenseType.Text;
            int duration = (int)inputDuration.Value;
            string territory = inputTerrtory.Text;
            decimal rate = (decimal)inputRoyality.Value;

            LicenseContract contract = new LicenseContract(type, duration, territory, rate);

            listContract.Items.Add(contract.GetInfo());
            MessageBox.Show("Договор создан!\n\n" + contract.GetInfo());
        }

        private void btnEployment_Click(object sender, EventArgs e)
        {
            string position = inputPosition.Text;
            decimal salary = (decimal)inputSalary.Value;
            string dept = inputDepartment.Text;
            int probation = (int)inputProbationPeriod.Value;

            EmploymentContract contract = new EmploymentContract(position, salary, dept, probation);

            listContract.Items.Add(contract.GetInfo());
            MessageBox.Show("Договор создан!\n\n" + contract.GetInfo());
        }

        private void btnAddReq_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(inputNewReq.Text))
            {
                listReq.Items.Add(inputNewReq.Text);
                inputNewReq.Clear();
                inputNewReq.Focus();
            }
        }

        private void btnDelReq_Click(object sender, EventArgs e)
        {
            if (listReq.SelectedIndex != -1)
            {
                listReq.Items.RemoveAt(listReq.SelectedIndex);
            }
        }

        private void listContract_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listContract.SelectedItems.Count > 0)
            {
                string info = listContract.SelectedItems[0].Text;
                MessageBox.Show(info, "Информация о договоре", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }

    class SoftwareContract
    {
        public string NameSoftware { get; }
        public string SourceCode { get; }
        public string TermsOfReference { get; }
        public List<string> Requirements { get; }

        public SoftwareContract(string name, string code, string terms, List<string> requirements)
        {
            NameSoftware = name;
            SourceCode = code;
            TermsOfReference = terms;
            this.Requirements = new List<string>(requirements);
        }

        public string GetInfo()
        {
            string reqText = "Нет";
            if (Requirements != null && Requirements.Count > 0)
            {
                // Формируем нумерованный список
                var numbered = Requirements.Select((r, i) => $"{i + 1}. {r}").ToList();
                reqText = string.Join("\n   ", numbered);
            }

            return $"[Договор разработки ПО]\n" +
                   $"------------------------\n" +
                   $"Проект:       {NameSoftware}\n" +
                   $"Исходный код: {SourceCode}\n" +
                   $"ТЗ:           {TermsOfReference}\n" +
                   $"Требования ({Requirements.Count}):\n   {reqText}";
        }
    }

    class LicenseContract
    {
        public string LicenseType { get; }
        public int Duration { get; }
        public string Territory { get; }
        public decimal RoyaltyRate { get; }

        public LicenseContract(string type, int duration, string territory, decimal rate)
        {
            LicenseType = type;
            Duration = duration;
            Territory = territory;
            RoyaltyRate = rate;
        }

        public string GetInfo() =>
            $"[Лицензия] Тип: {LicenseType}\n" +
            $"Срок: {Duration} мес.\n" +
            $"Территория: {Territory}\n" +
            $"Роялти: {RoyaltyRate:P2}";
    }

    class EmploymentContract
    {
        public string Position { get; }
        public decimal Salary { get; }
        public string Department { get; }
        public int ProbationPeriod { get; }

        public EmploymentContract(string position, decimal salary, string department, int probation)
        {
            Position = position;
            Salary = salary;
            Department = department;
            ProbationPeriod = probation;
        }

        public string GetInfo() =>
            $"[HR] Должность: {Position}\n" +
            $"Оклад: {Salary:C}\n" +
            $"Отдел: {Department}\n" +
            $"Исп. срок: {ProbationPeriod} дн.";
    }
}