using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Globalization;
using System.Security.Cryptography;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            var hwid = GetHWID();
            CheckHWID(hwid);

        }

        private static string GetHWID() // Thanks For This Chunk HideakiAtsuyo <3
        {
            string location = @"SOFTWARE\Microsoft\Cryptography"; string name = "MachineGuid";

            using (RegistryKey localMachineX64View = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey rk = localMachineX64View.OpenSubKey(location))
                {
                    if (rk == null) throw new KeyNotFoundException(string.Format(location));
                    object HWID = rk.GetValue(name);
                    if (HWID == null) throw new IndexOutOfRangeException(string.Format(name));
                    return HWID.ToString();
                }
            }
        }

        static async void CheckHWID(string hwid)
        {
            var client = new HttpClient();
            var database = await client.GetAsync("https://pastebin.com/raw/6WZm9xr1").Result.Content.ReadAsStringAsync();
            if (database.Contains(hwid))
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("+");
                Console.ResetColor();
                Console.Write("] HWID Authorized!");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("+");
                Console.ResetColor();
                Console.Write("] HWID Unauthorized\n");
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("+");
                Console.ResetColor();
                Console.Write("] HWID: " + hwid + "\n");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

    }
}
