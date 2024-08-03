using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;
using Microsoft.Win32;


namespace DVLD.Classes
{
    internal static class clsGlobal
    {
        private static string _KeyName = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";
        private static string _UserNameValueName = "Username";
        private static string _PasswordValueName = "Password";



        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            try
            {
                //this will get the current project directory folder.
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();


                // Define the path to the text file where you want to save the data
                string filePath = currentDirectory + "\\data.txt";

                //incase the username is empty, delete the file
                if (Username == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;

                }

                // concatonate username and passwrod withe seperator.
                string dataToSave = Username + "#//#" + Password;

                // Create a StreamWriter to write to the file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the data to the file
                    writer.WriteLine(dataToSave);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                //gets the current project's directory
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Path for the file that contains the credential.
                string filePath = currentDirectory + "\\data.txt";

                // Check if the file exists before attempting to read it
                if (File.Exists(filePath))
                {
                    // Create a StreamReader to read from the file
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Read data line by line until the end of the file
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line); // Output each line of data to the console
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
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
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool RememberUsernameAndPasswordInRegistry(string username, string password)
        {
            bool IsSuccess = false;

            try
            {
                Registry.SetValue(_KeyName, _UserNameValueName, username, RegistryValueKind.String);
                Registry.SetValue(_KeyName, _PasswordValueName, password, RegistryValueKind.String);
                IsSuccess = true;

            }
            catch(Exception ex)
            {
                IsSuccess = false;
            }

            return IsSuccess;
        }

        public static bool GetStoredCredentialFromRegistry(ref string username, ref string password)
        {
            bool IsStored = false;

            try
            {
                username = Registry.GetValue(_KeyName, _UserNameValueName, null) as string;
                password = Registry.GetValue(_KeyName, _PasswordValueName, null) as string;
                IsStored = true;
            
            }
            catch(Exception ex)
            {
                IsStored = false;
            }

            return IsStored;
        }

    }
}
