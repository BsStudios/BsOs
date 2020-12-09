using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
//using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Sys = Cosmos.System;

namespace CosmosKernelTest
{
    public class Kernel : Sys.Kernel
    {
        string currentdir = "0:/";
        Sys.FileSystem.CosmosVFS fs = null;

        protected override void BeforeRun()
        {
            fs = new Sys.FileSystem.CosmosVFS();
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
                Regex reg1 = new Regex("cd*");
                Regex reg2 = new Regex("ls*");
                Regex reg3 = new Regex("mkdir*");
                if (input.ToLower() == "shutdown")
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
                else if (reg1.IsMatch(input.ToLower())) // cd command
                {
                    
                }
                else if (reg2.IsMatch(input.ToLower())) // ls command
                {
                    string[] temp = input.Split(' ');
                    if(temp.Length > 1)
                    {
                        Console.WriteLine();
                        foreach (var file in fs.GetDirectoryListing(currentdir))
                        {
                            Console.WriteLine(file.mName);
                        }
                        Console.WriteLine();
                    }
                    else if (temp.Length > 2)
                    {
                        Console.WriteLine("\nTo Many Paramaters Found\n");
                        continue;
                    }
                    else
                    {
                        if (temp[1].StartsWith('.') && !temp[1].StartsWith(".."))
                        {
                            Console.WriteLine();
                            foreach (var file in fs.GetDirectoryListing(currentdir + temp[1].Remove(0,2)))
                            {
                                Console.WriteLine(file.mName);
                            }
                            Console.WriteLine();
                        }
                        else if (temp[1].StartsWith(".."))
                        {
                            Console.WriteLine();
                            foreach (var file in fs.GetDirectoryListing(currentdir + temp[1]))
                            {
                                Console.WriteLine(file.mName);
                            }
                            Console.WriteLine();
                        }
                        else if (temp[1].StartsWith("/"))
                        {
                            Console.WriteLine();
                            foreach (var file in fs.GetDirectoryListing(temp[1]))
                            {
                                Console.WriteLine(file.mName);
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine();
                            foreach (var file in fs.GetDirectoryListing(currentdir + temp[1]))
                            {
                                Console.WriteLine(file.mName);
                            }
                            Console.WriteLine();
                        }
                    }
                }
                else if (reg3.IsMatch(input.ToLower())) // mkdir command
                {
                    string[] temp = input.Split(' ');
                    if(temp.Length > 2)
                    {
                        Console.WriteLine("\nTo Many Paramaters Found\n");
                        continue;
                    }
                    else if(temp.Length == 1)
                    {
                        Console.WriteLine("\nTo Little Paramaters Found\n");
                        continue;
                    }
                    else
                    {
                        fs.CreateDirectory(currentdir + temp[1]);
                    }
                }
                else
                {
                    Console.WriteLine($"\nCommand {input} Not Found\n");
                }
            }
        }
    }
}
