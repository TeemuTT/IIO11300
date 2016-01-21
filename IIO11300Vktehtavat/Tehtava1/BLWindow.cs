/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course project.
* Created: 14.1.2016
* Modified: 21.1.2016
* Authors: Teemu Tuomela
*/

namespace JAMK.IT.IIO11300
{
    public class Ikkuna
    {
        #region Muuttujat
        double korkeus;
        double leveys;
        #endregion

        #region Ominaisuudet
        public double Korkeus
        {
            get
            {
                return korkeus;
            }
            set
            {
                korkeus = value;
            }
        }
        public double Leveys
        {
            get
            {
                return leveys;
            }
            set
            {
                leveys = value;
            }
        }
        public double PintaAla
        {
            get { return leveys * korkeus; }
        }
        public float Hinta
        {
            get { return LaskeHinta(); }
        }
        #endregion

        #region Konstruktorit
        #endregion

        #region Metodit
        private float LaskeHinta()
        {
            // Hinta lasketaan työn, menekin ja katteen mukaan.
            float kate = 100F;
            float tyo = 200F;
            float materiaali = 100F * (float)(leveys * korkeus * 1E-6);
            return materiaali + kate + tyo;
        }
        #endregion
    }

    public class BusinessLogicWindow
    {
        public static double CalculatePerimeter(double width, double height, double frameWidth)
        {
            return 2 * ((width + 2 * frameWidth) + (height + 2 * frameWidth));
        }

        public static double CalculateWindowArea(double width, double height)
        {
            return width * height;
        }

        public static double CalculateFrameArea(double windowWidth, double windowHeight, double frameWidth)
        {
            return (windowWidth + 2 * frameWidth) * (windowHeight + 2 * frameWidth) - CalculateWindowArea(windowWidth, windowHeight);
        }
    }
}
