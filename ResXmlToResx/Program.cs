using System;
using System.IO;
using System.Resources;
using System.Xml;

namespace ResXmlToResx
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            { 
                Console.WriteLine("Set 2 file names as arguments: source target");

                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Source file could not be found: " + args[0]);

                return;
            }

            if (File.Exists(args[1]))
            {
                Console.WriteLine("Target file exists: " + args[1]);

                return;
            }

            using (var reader = new XmlTextReader(args[0]))
            {
                using (var writer = new ResXResourceWriter(args[1]))
                {
                    try
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "string")
                                writer.AddResource(reader.GetAttribute("name"), reader.ReadString());
                        }

                        writer.Generate();

                        Console.WriteLine("Done!");
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Something went wrong: " + e.Message);
                    }
                }
            }
        }
    }
}
