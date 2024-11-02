using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace QuickPassWrodGennerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PassGen passwordGenerator;
        List<string> currentPasswordList = new List<string>();

        public MainWindow()
        {
            InitializeComponent(); 
            passwordGenerator = new PassGen();
            SetupPassWordData();
        }

        private void GetDataFromControls()
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

            passwordGenerator.PasswordLenght = int.Parse(CmbBxStrPsswrdLenght.Text.ToString());
            passwordGenerator.PasswordsQuantity = int.Parse(CBxQntt.Text.ToString());
        }


        private void SetupPassWordData()
        {
            GetDataFromControls();
            passwordGenerator.CustomDict = TxBxABC.Text.ToString();
            if (passwordGenerator.PasswordLenght < 8)
            {
                passwordGenerator.PasswordLenght = 8;
            }
            else if (passwordGenerator.PasswordLenght > 255)
            {
                passwordGenerator.PasswordLenght = 255;
            }
            CmbBxStrPsswrdLenght.Text = passwordGenerator.PasswordLenght.ToString();

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
            if ((bool)ChkBxClrPswrdsLst.IsChecked)
            {
                RTxBxPswrdList.Document.Blocks.Clear();
            }
            foreach (var item in currentPasswordList)
            {
                RTxBxPswrdList.Document.Blocks.Add(new Paragraph(new Run(item.ToString())));
            }
        }

        private void BtnGnrtPsswrd_Click(object sender, RoutedEventArgs e)
        {
            GetDataFromControls();
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
