/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course project.
* Created: 11.2.2016
* Modified: 16.2.2016
* Authors: Teemu Tuomela
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SMLiiga
{
    class BLSerialisoija
    {
        #region STATIC METHODS
        public static void TallennaXml(ICollection pelaajat, string tiedosto)
        {
            XmlSerializer xs = new XmlSerializer(pelaajat.GetType());
            TextWriter tw = new StreamWriter(tiedosto);

            try
            {
                xs.Serialize(tw, pelaajat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                tw.Close();
            }
        }

        public static List<BLPelaaja> LataaXml(string tiedosto)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<BLPelaaja>));
            TextReader tr = new StreamReader(tiedosto);
            List<BLPelaaja> list;

            try
            {
                list = (List<BLPelaaja>)xs.Deserialize(tr);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                tr.Close();
            }
            return list;
        }

        public static void TallennaTxt(List<BLPelaaja> pelaajat, string tiedosto)
        {
            try
            {
                using (StreamWriter streamWriter = File.CreateText(tiedosto))
                {
                    foreach (var pelaaja in pelaajat)
                    {
                        streamWriter.WriteLine(pelaaja.TekstiMuoto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<BLPelaaja> LataaTxt(string tiedosto)
        {
            List<BLPelaaja> luetut = new List<BLPelaaja>();
            try
            {
                using (StreamReader sr = File.OpenText(tiedosto))
                {
                    string rivi = "";
                    while ((rivi = sr.ReadLine()) != null)
                    {
                        string[] jako = rivi.Split('|');
                        luetut.Add(new BLPelaaja(jako[0], jako[1], jako[2], Convert.ToDecimal(jako[3])));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return luetut;
        }
        #endregion
    }
}
