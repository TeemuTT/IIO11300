/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course project.
* Created: 11.2.2016
* Authors: Teemu Tuomela
*/

using System;
using System.IO;
using System.Xml;

namespace TyontekijatKonsoli
{
    class Program
    {
        static void ReadWorkersFromXML(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(filename);
                    XmlNodeList xnl = doc.SelectNodes("/tyontekijat/tyontekija");

                    foreach (XmlNode node in xnl)
                    {
                        //Console.WriteLine(node.InnerText);
                        foreach (XmlNode childNode in node.ChildNodes)
                        {
                            Console.WriteLine(childNode.InnerText);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static void ReadWorkerTotalSalary(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(filename);
                    XmlNodeList xnl = doc.SelectNodes("/tyontekijat/tyontekija[tyosuhde='vakituinen']/palkka");

                    int salary = 0;
                    foreach (XmlNode node in xnl)
                    {
                        salary += Convert.ToInt32(node.InnerText);
                    }
                    Console.WriteLine("Vakituisia on {0} ja heidän palkat yhteensa: {1} ", xnl.Count, salary);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static void Main(string[] args)
        {
            //ReadWorkersFromXML("d:\\H8705\\tyontekijat.xml");
            ReadWorkerTotalSalary("d:\\H8705\\tyontekijat.xml");
        }
    }
}
