/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course project.
* Created: 16.2.2016
* Authors: Teemu Tuomela
*/

using System;
using System.Collections.Generic;

namespace SMLiiga
{
    class BLLiiga
    {
        #region PROPERTIES
        public List<BLPelaaja> Pelaajat { get; private set; } = new List<BLPelaaja>();

        public string[] Seurat { get; } = { "Blues", "HIFK", "HPK", "Ilves",
            "JYP", "KalPa", "KooKoo", "Kärpät", "Lukko", "Pelicans", "Saipa",
            "Sport", "Tappara", "TPS", "Ässät" };
        #endregion

        #region CONSTRUCTORS
        public BLLiiga()
        {
        }
        #endregion

        #region METHODS
        public bool LisaaPelaaja(string enimi, string snimi, string seura, decimal siirtohinta)
        {
            if (EtsiPelaaja(enimi, snimi))
            {
                return false;
            }

            Pelaajat.Add(new BLPelaaja(enimi, snimi, seura, siirtohinta));
            return true;
        }

        public void PoistaPelaaja(int index)
        {
            Pelaajat.RemoveAt(index);
        }

        public void PaivitaPelaaja(int index, string enimi, string snimi, string seura, decimal siirtohinta)
        {
            Pelaajat[index].Etunimi = enimi;
            Pelaajat[index].Sukunimi = snimi;
            Pelaajat[index].Seura = seura;
            Pelaajat[index].Siirtohinta = siirtohinta;
        }

        public void LataaTiedostosta(string tiedosto, string tyyppi)
        {
            try
            {
                if (tyyppi == ".xml")
                {
                    List<BLPelaaja> luetut = BLSerialisoija.LataaXml(tiedosto);
                    Pelaajat = luetut;
                }
                else if (tyyppi == ".txt")
                {
                    List<BLPelaaja> luetut = BLSerialisoija.LataaTxt(tiedosto);
                    Pelaajat = luetut;
                }
                else
                {
                    throw new Exception("Tuntematon tiedostomuoto");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TallennaTiedostoon(string tiedosto, string tyyppi)
        {
            try
            {
                if (tyyppi == ".xml")
                {
                    BLSerialisoija.TallennaXml(Pelaajat, tiedosto);
                }
                else if (tyyppi == ".txt")
                {
                    BLSerialisoija.TallennaTxt(Pelaajat, tiedosto);
                }
                else
                {
                    throw new Exception("Tuntematon tiedostomuoto");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool EtsiPelaaja(string enimi, string snimi)
        {
            foreach (var pelaaja in Pelaajat)
            {
                if (pelaaja.Etunimi == enimi && pelaaja.Sukunimi == snimi)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

    }
}
