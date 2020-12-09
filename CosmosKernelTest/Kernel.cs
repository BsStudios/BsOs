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
        string currentdir = @"0:\";
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
                else if (input.ToLower().StartsWith("cd"))
                {
                    string[] temp = input.Split(' ');
                    if (temp.Length == 1)
                    {
                        Console.WriteLine("\nPlease Specify A Directory\n");
                    }
                    else
                    {
                        string tempcurrentdir = currentdir;
                        if (temp[1].StartsWith(@".\") && !temp[1].StartsWith(@"..\"))
                        {
                            currentdir += temp[1].Remove(0, 1);
                        }
                        else if (temp[1].StartsWith(@"..\"))
                        {
                            string[] temp2 = currentdir.Split('\\');
                            temp2[temp2.Length - 1] = "";
                            string fintemp = temp2[0];
                            foreach(string tempp in temp2)
                            {
                                fintemp += @"\"+tempp;
                            }
                            currentdir = fintemp;
                            currentdir += temp[1].Remove(0, 2);
                        }
                        else if (temp[1].StartsWith(@"0:\") || temp[1].StartsWith(@"1:\") || temp[1].StartsWith(@"2:\") || temp[1].StartsWith(@"3:\"))
                        {
                            currentdir = temp[1];
                        }
                        else
                        {
                            currentdir += temp[1];
                        }

                        
                    }
                }
                else if (input.ToLower().StartsWith("ls")) // ls command
                {
                    string[] temp = input.Split(' ');
                    if(temp.Length == 1)
                    {
                        Console.WriteLine();
                        foreach (var file in fs.GetDirectoryListing(currentdir))
                        {
                            Console.WriteLine(file.mName);
                        }
                        Console.WriteLine();
                    }
                    //else if (temp.Length > 2)
                    //{
                    //    Console.WriteLine("\nTo Many Paramaters Found\n");
                    //    continue;
                    //}
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
                        else if (temp[1].StartsWith(@"\"))
                        {
                            Console.WriteLine();
                            foreach (var file in fs.GetDirectoryListing(temp[1]))
                            {
                                Console.WriteLine(file.mName);
                            }
                            Console.WriteLine();
                        }
                        else if (temp[1].StartsWith(@"0:\") || temp[1].StartsWith(@"1:\") || temp[1].StartsWith(@"2:\") || temp[1].StartsWith(@"3:\"))
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
                else if (input.ToLower().StartsWith("mkdir")) // mkdir command
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
