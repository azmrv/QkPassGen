using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


namespace QuickPassWordGenerator
{
    internal class PassGen
    {
        bool isNumbers = true;
        bool isLowerCase = true;
        bool isUpperCase = true;
        bool isSpecial = false;
        bool isASCII = false;
        bool isSimilar = false;
        bool isDuplicate = false;
        bool isSequential = false;
        bool isBeginWithALetter = false;
        bool isUSEABC = false;
        bool isLatn = true;
        bool isCyrl = false;
        int passwordLength = 28;
        int passwordsQuantity = 1;
        string customDict = null;

        const string defaultString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
        const string ruletters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        List<string> currentPasswordList = null;
        string stringPasswordsDictionary = null;
        StringBuilder passwordsDictionary;

        public bool IsNumbers { get => isNumbers; set => isNumbers = value; }
        public bool IsLowerCase { get => isLowerCase; set => isLowerCase = value; }
        public bool IsUpperCase { get => isUpperCase; set => isUpperCase = value; }
        public bool IsSpecial { get => isSpecial; set => isSpecial = value; }
        public bool IsASCII { get => isASCII; set => isASCII = value; }
        public bool IsSimilar { get => isSimilar; set => isSimilar = value; }
        public bool IsDuplicate { get => isDuplicate; set => isDuplicate = value; }
        public bool IsSequential { get => isSequential; set => isSequential = value; }
        public bool IsBeginWithALetter { get => isBeginWithALetter; set => isBeginWithALetter = value; }
        public bool IsUSEABC { get => isUSEABC; set => isUSEABC = value; }
        public bool IsLatn { get => isLatn; set => isLatn = value; }
        public bool IsCyrl { get => isCyrl; set => isCyrl = value; }
        public int PasswordLength { get => passwordLength; set => passwordLength = value; }
        public int PasswordsQuantity { get => passwordsQuantity; set => passwordsQuantity = value; }
        public string CustomDict { get => customDict; set => customDict = value; }
        public List<string> CurrentPasswordList { get => currentPasswordList; set => currentPasswordList = value; }
        public string StringPasswordsDictionary { get => stringPasswordsDictionary; set => stringPasswordsDictionary = value; }
        public StringBuilder PasswordsDictionary { get => passwordsDictionary; set => passwordsDictionary = value; }

        public PassGen()
        {

        }


        static private int GetCryptographicallyRandomInt()
   		{
   			byte[] randomBytes = new byte[4];
   			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
   			rng.GetBytes(randomBytes);
            int randomInt = BitConverter.ToInt32(randomBytes, 0);
   			return randomInt;
   		}

        static public string GeneratePassword(Random r, int length, char[] allowableChars)
  		{
  			StringBuilder passwordBuilder = new StringBuilder((int)length);            

              for (int i = 0; i<length; i++)
  			{
  				int nextInt = r.Next(allowableChars.Length);
  				char c = allowableChars[nextInt];
  				passwordBuilder.Append(c);
  			}
  
  			return passwordBuilder.ToString();
  		}


        private void SetPasswordDictionary()
  		{
            PasswordsDictionary = new StringBuilder();
            if (!IsNumbers && !IsLowerCase && !IsLowerCase && !IsUpperCase && !IsSpecial && !IsASCII && !IsUSEABC && !IsCyrl)
            {
                PasswordsDictionary.Append(defaultString);
            }
            else
            {
                if (IsNumbers)
                {
                    for (int i = 48; i <= 57; i++)
                    {
                        PasswordsDictionary.Append(((char)i));                       
                    }                    
                }
                if (IsLowerCase || IsLatn)
                {
                    for (int i = 97; i <= 122; i++)
                    {
                        PasswordsDictionary.Append(((char)i));
                    }
                }
                if (IsUpperCase || IsLatn)
                {
                    for (int i = 65; i <= 90; i++)
                    {
                        PasswordsDictionary.Append(((char)i));
                    }
                }
                //if (IsLowerCase && IsCyrl)
                //{
                //    for (int i = 97; i <= 122; i++)
                //    {
                //        PasswordsDictionary.Append(((char)i));
                //    }
                //}
                if (IsCyrl)
                {
                    // add 2 string upper and lower chars and check for iscyrl
                    PasswordsDictionary.Append(ruletters);
                }
                if (IsSpecial)
                {
                    for (int i = 33; i <= 47; i++)
                    {
                        PasswordsDictionary.Append(((char)i));
                    }
                    for (int i = 58; i <= 64; i++)
                    {
                        PasswordsDictionary.Append(((char)i));
                    }
                    for (int i = 91; i <= 96; i++)
                    {
                        PasswordsDictionary.Append(((char)i));
                    }
                    for (int i = 123; i <= 126; i++)
                    {
                        PasswordsDictionary.Append(((char)i));
                    }
                }
                if (IsASCII)
                {
                    for (int i = 128; i <= 190; i++)
                    {
                        PasswordsDictionary.Append(((char)i));
                    }
                }
                if (IsUSEABC)
                {
                    if (CustomDict != null)
                    {
                        PasswordsDictionary.Append(CustomDict);
                    }
                    if (CustomDict != null)
                    {
                        PasswordsDictionary.Append(CustomDict);
                    }
                }
            }
        }

        //TODO
        private bool CheckPasswordCriteriaMatch()
        {
            //isSimilar
            //isDuplicate
            //isSequential
            //isBeginWithALetter
            return false;
        }

        //TODO
        private int CheckPasswordStrengthCheck()
        {
            int psswrdstrength = 0;    
            //isSimilar
            //isDuplicate
            //isSequential
            //isBeginWithALetter
            return psswrdstrength;
        }


        public List<string> PassWrdGen()
        {
            CurrentPasswordList = new List<string>(); ;
            SetPasswordDictionary();
            //isSimilar
            //isDuplicate
            //isSequential
            //isBeginWithALetter
            int seed = GetCryptographicallyRandomInt();
            Random rnd = new Random(seed);
            if (PasswordsDictionary.Length > 0)
            {
                for (int i = 0; i < PasswordsQuantity; i++)
                {
                    string psswrd = GeneratePassword(rnd, PasswordLength, PasswordsDictionary.ToString().ToCharArray());
                    CurrentPasswordList.Add(psswrd);
                }
            }
            return CurrentPasswordList;
        }

    }
}
