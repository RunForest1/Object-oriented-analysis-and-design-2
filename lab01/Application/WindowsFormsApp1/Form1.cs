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
            manager.SetPrototype(_softwarePrototype);
            SoftwareContract clone = (SoftwareContract)manager.CreateContract();

            if (!string.IsNullOrWhiteSpace(inputNameSoftware.Text))
                clone.NameSoftware = inputNameSoftware.Text;

            if (!string.IsNullOrWhiteSpace(inputSourceCode.Text))
                clone.SourceCode = inputSourceCode.Text;

            if (!string.IsNullOrWhiteSpace(inputTermsOfReference.Text))
                clone.TermsOfReference = inputTermsOfReference.Text;

            if (listReq.Items.Count > 0)
            {
                clone.Requirements.Clear(); // Очищаем стандартные требования из шаблона

                foreach (var item in listReq.Items)
                {
                    // Добавляем каждое требование из ListBox в объект контракта
                    if (item != null)
                        clone.Requirements.Add(item.ToString());
                }
            }

            listContract.Items.Add(clone.GetInfo());
            MessageBox.Show("Договор разработки ПО создан!\n\n" + clone.GetInfo());
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            manager.SetPrototype(_licensePrototype);
            LicenseContract clone = (LicenseContract)manager.CreateContract();

            if (!string.IsNullOrWhiteSpace(inputLicenseType.Text))
                clone.LicenseType = inputLicenseType.Text;

            if (inputDuration.Value > 0)
                clone.Duration = (int)inputDuration.Value;

            if (!string.IsNullOrWhiteSpace(inputTerrtory.Text))
                clone.Territory = inputTerrtory.Text;

            if (inputRoyality.Value > 0)
                clone.RoyaltyRate = (decimal)inputRoyality.Value;

            listContract.Items.Add(clone.GetInfo());
            MessageBox.Show("Лицензионный договор создан!\n\n" + clone.GetInfo());
        }

        private void btnEployment_Click(object sender, EventArgs e)
        {
            manager.SetPrototype(_employmentPrototype);
            EmploymentContract clone = (EmploymentContract)manager.CreateContract();

            if (!string.IsNullOrWhiteSpace(inputPosition.Text))
                clone.Position = inputPosition.Text;

            if (inputSalary.Value > 0)
                clone.Salary = (decimal)inputSalary.Value;

            if (!string.IsNullOrWhiteSpace(inputDepartment.Text))
                clone.Department = inputDepartment.Text;

            if (inputProbationPeriod.Value > 0)
                clone.ProbationPeriod = (int)inputProbationPeriod.Value;

            listContract.Items.Add(clone.GetInfo());
            MessageBox.Show("Трудовой договор создан!\n\n" + clone.GetInfo());
        }

        private void btnAddReq_Click(object sender, EventArgs e)
        {
            listReq.Items.Add(inputNewReq.Text);
            inputNewReq.Clear();
        }

        private void btnDelReq_Click(object sender, EventArgs e)
        {
            if (listReq.SelectedIndex != -1)
            {
                listReq.Items.RemoveAt(listReq.SelectedIndex);
            }
        }

        private void listContract_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            // Проверяем, что элемент именно ВЫДЕЛИЛСЯ (а не снял выделение)
            if (e.IsSelected && e.Item != null)
            {

                string selectedText = e.Item.Text;
                MessageBox.Show(
                    $"Вы выбрали договор:\n\n{selectedText}",
                    "Информация о договоре",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
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

        // Список требований (ссылочный тип)
        public List<string> Requirements { get; set; }

        public SoftwareContract(string projectName)
        {
            NameSoftware = projectName;
            SourceCode = "Standard Code Base";
            TermsOfReference = "Standard Terms";

            // Инициализируем список требованиями по умолчанию
            Requirements = new List<string>
        {
            "Анализ требований",
            "Проектирование системы",
            "Сдача документации"
        };
        }

        private SoftwareContract(SoftwareContract source)
        {
            NameSoftware = source.NameSoftware;
            SourceCode = source.SourceCode;
            TermsOfReference = source.TermsOfReference;

            // cоздаем НОВЫЙ список и копируем элементы
            if (source.Requirements != null)
            {
                this.Requirements = new List<string>(source.Requirements);
            }
            else
            {
                this.Requirements = new List<string>();
            }
        }

        public IContractPrototype Clone() => new SoftwareContract(this);

        public string GetInfo()
        {
            string reqList = string.Join(", ", Requirements);
            return $"[Software] Проект: {NameSoftware} | Код: {SourceCode} | ТЗ: {TermsOfReference} | Требования: {reqList}";
        }
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
            Duration = 12;
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
            Salary = 50000m;
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