/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course project.
* Created: 11.2.2016
* Modified: 16.2.2016
* Authors: Teemu Tuomela
*/

namespace SMLiiga
{
    public class BLPelaaja
    {
        #region PROPERTIES
        public string Etunimi { get; set; } = "";
        public string Sukunimi { get; set; } = "";
        public string Seura { get; set; } = "";
        public decimal Siirtohinta { get; set; } = 0;
        public string KokoNimi
        {
            get
            {
                return Sukunimi + " " + Etunimi;
            }
        }
        public string Esitysnimi
        {
            get
            {
                return Etunimi + " " + Sukunimi + ", " + Seura;
            }
        }
        public string TekstiMuoto
        {
            get
            {
                return Etunimi + "|" + Sukunimi + "|" + Seura + "|" + Siirtohinta.ToString();
            }
        }
        #endregion

        #region CONSTRUCTORS
        public BLPelaaja()
        {
        }

        public BLPelaaja(string enimi, string snimi, string seura, decimal hinta)
        {
            this.Etunimi = enimi;
            this.Sukunimi = snimi;
            this.Seura = seura;
            this.Siirtohinta = hinta;
        }
        #endregion

        #region OVERRIDES
        public override string ToString()
        {
            return Esitysnimi;
        }
        #endregion
    }
}
