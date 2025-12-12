using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLogic;

namespace CodeGeneratorDAL
{
    public class LoginInfo
    {


        private static ClsServerConnection _SelectedLoginInfo;


        public static ClsServerConnection SelectedLoginInfo { get { return _SelectedLoginInfo; } }


        public static void Set(ClsServerConnection ServerInfo)
        {

            _SelectedLoginInfo = ServerInfo;

        }

        private static string Encrypt(string Txt, int EncryptionShift = 2)
        {

            string Result = "";

            for (short i = 0; i < Txt.Length; i++)
            {

                Result += (char)((int)Txt[i] + EncryptionShift);

            }
            return Result;
        }

        private static string Decrypt(string Txt, int DecryptionShift = 2)
        {

            string Result = "";

            for (int i = 0; i < Txt.Length; i++)
            {

                Result += (char)((int)Txt[i] - DecryptionShift);

            }
            return Result;

        }

        public static bool RememberLoginServerInfo(string ServerNAme, string UserName, string Password)
        {

            try
            {

                string currentDirec = System.IO.Directory.GetCurrentDirectory();

                string FilePath = currentDirec + "\\data.txt";

                if (UserName == "" && File.Exists(FilePath))
                {

                    File.Delete(FilePath);
                    return true;

                }

                string dataToSave = ServerNAme + "#//#" + UserName + "#//#" + Encrypt(Password);

                using (StreamWriter writer = new StreamWriter(FilePath))
                {

                    writer.WriteLine(dataToSave);
                    return true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An Error :  {ex.Message}");

                return false;

            }

        }

        public static bool GetStoredInfo(ref string ServerName ,ref string UserName, ref string Password)
        {

            try
            {

                string currntDirec = System.IO.Directory.GetCurrentDirectory();

                string FilePath = currntDirec + "\\data.txt";

                if (File.Exists(FilePath))
                {

                    using (StreamReader reader = new StreamReader(FilePath))
                    {

                        string Line;

                        while ((Line = reader.ReadLine()) != null)
                        {

                            Console.WriteLine(Line);

                            string[] result = Line.Split(new string[] { "#//#" }, StringSplitOptions.None);


                            ServerName = result[0];
                            UserName = result[1];
                            Password = Decrypt(result[2]);

                        }

                        return true;

                    }

                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show($"An Error :  {ex.Message}");

                return false;
            }



        }
    }
}
