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
        // Менеджер для управления созданием контрактов
        Manager manager = new Manager();

        // ПОЛЯ ДЛЯ ХРАНЕНИЯ ПРОТОТИПОВ
        private SoftwareContract _softwarePrototype;
        private LicenseContract _licensePrototype;
        private EmploymentContract _employmentPrototype;

        public Form1()
        {
            InitializeComponent();

            // Шаблон для договора разработки ПО
            _softwarePrototype = new SoftwareContract("Типовой проект ПО");
            // Шаблон для лицензионного договора
            _licensePrototype = new LicenseContract("Стандартная лицензия");
            // Шаблон для трудового договора
            _employmentPrototype = new EmploymentContract("Стандартный сотрудник");

        }

        private void btnSoftware_Click(object sender, EventArgs e)
        {
            // Передаем готовый шаблон договора ПО
            manager.SetPrototype(_softwarePrototype);

            // Клонируем его
            SoftwareContract clone = (SoftwareContract)manager.CreateContract();

            // Заполняем клон уникальными данными из формы

        if (!string.IsNullOrWhiteSpace(inputNameSoftware.Text))
            clone.NameSoftware = inputNameSoftware.Text;

        if (!string.IsNullOrWhiteSpace(inputSourceCode.Text))
            clone.SourceCode = inputSourceCode.Text;

        if (!string.IsNullOrWhiteSpace(inputTermsOfReference.Text))
            clone.TermsOfReference = inputTermsOfReference.Text;


            // Отображаем результат
            listContract.Items.Add(clone.GetInfo());
            MessageBox.Show("Договор разработки ПО создан!\n\n" + clone.GetInfo());
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            // Передаем готовый шаблон договора лицензии
            manager.SetPrototype(_licensePrototype);

            // Клонируем его
            LicenseContract clone = (LicenseContract)manager.CreateContract();

            // Заполняем клон уникальными данными из формы
        if (!string.IsNullOrWhiteSpace(inputLicenseType.Text))
            clone.LicenseType = inputLicenseType.Text;

        // Проверяем, изменил ли пользователь значение (если 0, то оставляем дефолтное из прототипа)
        if (inputDuration.Value > 0)
            clone.Duration = (int)inputDuration.Value;

        if (!string.IsNullOrWhiteSpace(inputTerrtory.Text))
            clone.Territory = inputTerrtory.Text;

        if (inputRoyality.Value > 0)
            clone.RoyaltyRate = (decimal)inputRoyality.Value;

            // Отображаем результат
            listContract.Items.Add(clone.GetInfo());
            MessageBox.Show("Лицензионный договор создан!\n\n" + clone.GetInfo());
        }

        private void btnEployment_Click(object sender, EventArgs e)
        {
            // Передаем готовый шаблон трудового договора
            manager.SetPrototype(_employmentPrototype);

            // Клонируем его
            EmploymentContract clone = (EmploymentContract)manager.CreateContract();

            // Заполняем клон уникальными данными из формы
        if (!string.IsNullOrWhiteSpace(inputPosition.Text))
            clone.Position = inputPosition.Text;

        if (inputSalary.Value > 0)
            clone.Salary = (decimal)inputSalary.Value;

        if (!string.IsNullOrWhiteSpace(inputDepartment.Text))
            clone.Department = inputDepartment.Text;

        if (inputProbationPeriod.Value > 0)
            clone.ProbationPeriod = (int)inputProbationPeriod.Value;

            // Отображаем результат
            listContract.Items.Add(clone.GetInfo());
            MessageBox.Show("Трудовой договор создан!\n\n" + clone.GetInfo());
        }
    }


    interface IContractPrototype
    {
        IContractPrototype Clone();
        string GetInfo();
    }

    class SoftwareContract : IContractPrototype
    {
        public string NameSoftware { get; set; }
        public string SourceCode { get; set; }
        public string TermsOfReference { get; set; }

        public SoftwareContract(string projectName)
        {
            NameSoftware = projectName;
            SourceCode = "Standard Code Base";
            TermsOfReference = "Standard Terms";
        }

        private SoftwareContract(SoftwareContract source)
        {
            NameSoftware = source.NameSoftware;
            SourceCode = source.SourceCode;
            TermsOfReference = source.TermsOfReference;
        }

        public IContractPrototype Clone() => new SoftwareContract(this);

        public string GetInfo() => $"[Software] Проект: {NameSoftware} Исходный код: {SourceCode} ТЗ: {TermsOfReference}";
    }

    class LicenseContract : IContractPrototype
    {
        public string LicenseType { get; set; }
        public int Duration { get; set; }
        public string Territory { get; set; }
        public decimal RoyaltyRate { get; set; }

        public LicenseContract(string type)
        {
            LicenseType = type;
            Duration = 12;                // Значение по умолчанию
            Territory = "Российская Федерация";
            RoyaltyRate = 0.05m;
        }

        private LicenseContract(LicenseContract source)
        {
            LicenseType = source.LicenseType;
            Duration = source.Duration;
            Territory = source.Territory;
            RoyaltyRate = source.RoyaltyRate;
        }

        public IContractPrototype Clone() => new LicenseContract(this);

        public string GetInfo() =>
            $"[Лицензия] Тип: {LicenseType} " +
            $"Срок: {Duration} мес. " +
            $"Территория: {Territory} " +
            $"Роялти: {RoyaltyRate:P2}";
    }

    class EmploymentContract : IContractPrototype
    {
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public string Department { get; set; }
        public int ProbationPeriod { get; set; }

        public EmploymentContract(string position)
        {
            Position = position;
            Salary = 50000m;            // Значение по умолчанию
            Department = "Общий отдел";
            ProbationPeriod = 90;
        }

        private EmploymentContract(EmploymentContract source)
        {
            Position = source.Position;
            Salary = source.Salary;
            Department = source.Department;
            ProbationPeriod = source.ProbationPeriod;
        }

        public IContractPrototype Clone() => new EmploymentContract(this);

        public string GetInfo() =>
            $"[HR] Должность: {Position} " +
            $"Оклад: {Salary:C} " +
            $"Отдел: {Department} " +
            $"Исп. срок: {ProbationPeriod} дн.";
    }

    class Manager
    {
        private IContractPrototype _prototype;

        public void SetPrototype(IContractPrototype prototype)
        {
            _prototype = prototype;
        }

        public IContractPrototype CreateContract()
        {
            if (_prototype == null)
                throw new InvalidOperationException("Прототип не установлен.");

            return _prototype.Clone();
        }
    }
}