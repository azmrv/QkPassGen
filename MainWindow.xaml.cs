using QuickPassWordGenerator;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;


namespace QuickPassWordGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PassGen passwordGenerator;
        List<string> currentPasswordList = new List<string>();
        Dictionary<int, string> savedSettings = new Dictionary<int, string>();

        public MainWindow()
        {
            InitializeComponent();
            passwordGenerator = new PassGen();
            SetupPassWordData();
        }

 
        private void GetDataFromControls()
        {
            try
            {
                passwordGenerator.IsNumbers = bool.Parse(ChkBxIsNmbr.IsChecked.ToString());
                passwordGenerator.IsLowerCase = bool.Parse(ChkBxIsLwrCs.IsChecked.ToString());
                passwordGenerator.IsUpperCase = bool.Parse(ChkBxIsUpprCs.IsChecked.ToString());
                passwordGenerator.IsSpecial = bool.Parse(ChkBxIsSpcl.IsChecked.ToString());
                passwordGenerator.IsASCII = bool.Parse(ChkBxIsASCII.IsChecked.ToString());
                passwordGenerator.IsSimilar = bool.Parse(ChkBxIsSmlr.IsChecked.ToString());
                passwordGenerator.IsDuplicate = bool.Parse(ChkBxIsDplct.IsChecked.ToString());
                passwordGenerator.IsSequential = bool.Parse(ChkBxIsSqntl.IsChecked.ToString());
                passwordGenerator.IsBeginWithALetter = bool.Parse(ChkBxIsBWL.IsChecked.ToString());
                passwordGenerator.IsUSEABC = bool.Parse(ChkBxIsABC.IsChecked.ToString());
                passwordGenerator.IsLatn = bool.Parse(ChkBxIsLatn.IsChecked.ToString());
                passwordGenerator.IsCyrl = bool.Parse(ChkBxIsCyrl.IsChecked.ToString());

                passwordGenerator.PasswordLength = int.Parse(CmbBxStrPsswrdLength.Text.ToString());
                passwordGenerator.PasswordsQuantity = int.Parse(CBxQntt.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void SetupPassWordData()
        {
            GetDataFromControls();

            if (!savedSettings.ContainsKey(passwordGenerator.GetHashCode()))
            {
                savedSettings.Add(passwordGenerator.GetHashCode(), CmbBxStrPsswrdLength.Text + "|" + CBxQntt.Text);
                CmbBxStrPsswrdLength.Text = passwordGenerator.PasswordLength.ToString();
                CBxQntt.Text = passwordGenerator.PasswordsQuantity.ToString();
            }
            else
            {
                var settings = savedSettings[passwordGenerator.GetHashCode()].Split('|');
                CmbBxStrPsswrdLength.Text = settings[0];
                CBxQntt.Text = settings[1];
            }

            passwordGenerator.CustomDict = TxBxABC.Text.ToString();

            if (passwordGenerator.PasswordLength < 8)
            {
                passwordGenerator.PasswordLength = 8;
            }
            else if (passwordGenerator.PasswordLength > 255)
            {
                passwordGenerator.PasswordLength = 255;
            }

            CmbBxStrPsswrdLength.Text = passwordGenerator.PasswordLength.ToString();

            if (passwordGenerator.PasswordsQuantity < 1)
            {
                passwordGenerator.PasswordsQuantity = 1;
            }
            else if (passwordGenerator.PasswordsQuantity > 255)
            {
                passwordGenerator.PasswordsQuantity = 255;
            }

            CBxQntt.Text = passwordGenerator.PasswordsQuantity.ToString();
        }

        private void GeneratePassword()
        {
            currentPasswordList = passwordGenerator.PassWrdGen();

            foreach (var item in currentPasswordList)
            {
                RTxBxPswrdList.Document.Blocks.Add(new Paragraph(new Run(item.ToString())));
            }
        }

        private void BtnGnrtPsswrd_Click(object sender, RoutedEventArgs e)
        {
            SetupPassWordData();
            GeneratePassword();
        }

        private void CopyRndPasswordToClipboard()
        {
            if (currentPasswordList.Count > 0)
            {
                Random rnd = new Random();
                string cpsswrd = currentPasswordList[rnd.Next(0, passwordGenerator.PasswordsQuantity - 1)];
                Clipboard.SetText(cpsswrd);
                RTxBxPswrdList.Document.Blocks.Clear();
                RTxBxPswrdList.Document.Blocks.Add(new Paragraph(new Run(cpsswrd)));
            }
        }

        private void BtnCpTBffr_Click(object sender, RoutedEventArgs e)
        {
            if (currentPasswordList.Count > 0)
            {
                CopyRndPasswordToClipboard();
            }
            else
            {
                SetupPassWordData();
                GeneratePassword();
                CopyRndPasswordToClipboard();
            }
        }
    }
}