using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
//using System.IO;
using System.Text;
using Sys = Cosmos.System;

namespace CosmosKernelTest
{
    public class Kernel : Sys.Kernel
    {
        string currentdir = "0:/";

        protected override void BeforeRun()
        {
            Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.WriteLine("Bs Os Has Booted Succesfully.");
        }


        protected override void Run()
        {
            Console.Clear();

            Console.WriteLine("Bs Os Ver. 1.0\n");
            while (true)
            {
                Console.Write(currentdir+" >> ");
                string input = Console.ReadLine();
                if(input.ToLower() == "shutdown")
                {
                    Sys.Power.Shutdown();
                }
                else if (input.ToLower() == "reboot")
                {
                    Sys.Power.Reboot();
                }
                else if(input.ToLower() == "clear" || input.ToLower() == "cls")
                {
                    Console.Clear();
                }
                else
                {

                }
            }
        }
    }
}
