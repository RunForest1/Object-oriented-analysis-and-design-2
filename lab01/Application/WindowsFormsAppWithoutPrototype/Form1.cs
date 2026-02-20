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
            // Получаем данные из формы
            string name = inputNameSoftware.Text;
            string code = inputSourceCode.Text;
            string terms = inputTermsOfReference.Text;

            // Создаем объект ПРЯМО ЗДЕСЬ через конструктор
            SoftwareContract contract = new SoftwareContract(name, code, terms);

            // Отображаем результат
            listContract.Items.Add(contract.GetInfo());
            MessageBox.Show("Договор создан!\n\n" + contract.GetInfo());
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            // Получаем данные из формы
            string type = inputLicenseType.Text;
            int duration = (int)inputDuration.Value;
            string territory = inputTerrtory.Text;
            decimal rate = (decimal)inputRoyality.Value;

            // Создаем объект ПРЯМО ЗДЕСЬ через конструктор

            LicenseContract contract = new LicenseContract(type, duration, territory, rate);

            // Отображаем результат
            listContract.Items.Add(contract.GetInfo());
            MessageBox.Show("Договор создан!\n\n" + contract.GetInfo());
        }

        private void btnEployment_Click(object sender, EventArgs e)
        {
            // Получаем данные из формы
            string position = inputPosition.Text;
            decimal salary = (decimal)inputSalary.Value;
            string dept = inputDepartment.Text;
            int probation = (int)inputProbationPeriod.Value;

            // Создаем объект ПРЯМО ЗДЕСЬ через конструктор
            EmploymentContract contract = new EmploymentContract(position, salary, dept, probation);

            // Отображаем результат
            listContract.Items.Add(contract.GetInfo());
            MessageBox.Show("Договор создан!\n\n" + contract.GetInfo());
        }

        private void listContract_SelectedIndexChanged(object sender, EventArgs e) { }
    }

    class SoftwareContract
    {

        public string NameSoftware { get; }
        public string SourceCode { get; }
        public string TermsOfReference { get; }

        public SoftwareContract(string name, string code, string terms)
        {
            NameSoftware = name;
            SourceCode = code;
            TermsOfReference = terms;
        }

        public string GetInfo() => $"[Software] Проект: {NameSoftware}\nИсходный код: {SourceCode}\nТЗ: {TermsOfReference}";
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