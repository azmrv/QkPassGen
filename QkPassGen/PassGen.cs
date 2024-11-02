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
        HashSet<string> currentPasswordList = new HashSet<string>();
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
        private StringBuilder PasswordsDictionary { get => passwordsDictionary; set => passwordsDictionary = value; }
        private HashSet<string> CurrentPasswordList { get => currentPasswordList; set => currentPasswordList = value; }

        public PassGen()
        {
            SetPasswordDictionary();
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
                }
            }
        }

        // Генерация случайного числа
        static private int GetCryptographicallyRandomInt()
        {
            byte[] randomBytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            int randomInt = BitConverter.ToInt32(randomBytes, 0);
            return randomInt;
        }

        // Генерация пароля
        static public string GeneratePassword(Random r, int length, char[] allowableChars)
        {
            StringBuilder passwordBuilder = new StringBuilder((int)length);

            for (int i = 0; i < length; i++)
            {
                int nextInt = r.Next(allowableChars.Length);
                char c = allowableChars[nextInt];
                passwordBuilder.Append(c);
            }

            return passwordBuilder.ToString();
        }

        // Проверка сложности пароля
        private int CheckPasswordStrengthCheck()
        {
            // Оценка сложности пароля по определённым критериям
            int psswrdstrength = 0;
            //isSimilar
            //isDuplicate
            //isSequential
            //isBeginWithALetter
            return psswrdstrength;
        }

        // Генерация списка паролей
        public HashSet<string> PassWrdGen()
        {
            currentPasswordList.Clear();

            while (currentPasswordList.Count < PasswordsQuantity)
            {
                string psswrd = GeneratePassword(new Random(GetCryptographicallyRandomInt()), PasswordLenght, PasswordsDictionary.ToString().ToCharArray());

                if (!currentPasswordList.Contains(psswrd))
                {
                    currentPasswordList.Add(psswrd);
                }
            }

            return currentPasswordList;
        }
    }
}