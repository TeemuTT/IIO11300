/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course project.
* Created: 27.1.2016
* Authors: Teemu Tuomela
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Tehtava2_WinLotto
{

    class Lotto
    {
        #region Muuttujat
        private int primaryNumbers;
        private int secondaryNumbers;
        private int primaryLimit;
        private int secondaryLimit;
        private Random random;
        #endregion

        #region Konstruktorit
        public Lotto(string game)
        {
            switch (game)
            {
                case "Suomi":
                    Initialize(7, 2, 39, 39);
                    break;
                case "Viking Lotto":
                    Initialize(6, 0, 48, 0);
                    break;
                case "Eurojackpot":
                    Initialize(5, 2, 50, 8);
                    break;
            }
        }
        #endregion

        #region Metodit
        private void Initialize(int primaryNumbers, int secondaryNumbers, int primaryLimit, int secondaryLimit)
        {
            random = new Random();
            this.primaryNumbers = primaryNumbers;
            this.secondaryNumbers = secondaryNumbers;
            this.primaryLimit = primaryLimit;
            this.secondaryLimit = secondaryLimit;
        }

        public string Draw()
        {
            List<int> primary = new List<int>();
            List<int> secondary = new List<int>();

            // Arvo päänumerot.
            primary = GenerateLine(null, primaryNumbers, primaryLimit);

            // Arvo lisänumerot ehdolla:
            // Jos päänumeroiden ja lisänumeroiden raja on sama oletetaan,
            // että arvonta suoritetaan samasta joukosta.
            // Jos rajat ovat eri oletetaan, että arvonta suoritetaan eri joukoista.
            if (primaryLimit == secondaryLimit)
            {
                secondary = GenerateLine(primary, secondaryNumbers, secondaryLimit);
            }
            else
            {
                secondary = GenerateLine(null, secondaryNumbers, secondaryLimit);
            }

            string result = JoinNumbers(primary, secondary);
            return result;
        }

        private List<int> GenerateLine(List<int> checkList, int draws, int limit)
        {
            List<int> generated = new List<int>();

            // Jos tarkistuslista on null, tarkista itseä vasten.
            if (checkList == null)
            {
                checkList = generated;
            }

            for (int i = 0; i < draws; i++)
            {
                int next;
                do
                {
                    next = random.Next(1, limit + 1);
                } while (checkList.Contains(next));
                generated.Add(next);
            }
            generated.Sort();
            return generated;
        }

        private string JoinNumbers(List<int> primary, List<int> secondary)
        {
            StringBuilder result = new StringBuilder();

            // Lisää päänumerot lopulliseen tulokseen.
            foreach (int n in primary)
            {
                result.Append(n + ", ");
            }
            result.Remove(result.Length - 2, 1);

            // Lisää lisänumerot (jos on) lopulliseen tulokseen.
            if (secondary.Count > 0)
            {
                result.Append("+ ");
                foreach (int n in secondary)
                {
                    result.Append(n + ", ");
                }
                result.Remove(result.Length - 2, 1);
            }

            return result.ToString();
        }
        #endregion
    }
}
