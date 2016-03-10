/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course.
* Created: 25.2.2016
* Authors: Teemu Tuomela
*/

using System.Collections.Generic;
using System.ComponentModel;

namespace H6DataBinding
{
    public static class TestHockeyBench
    {
        public static List<HockeyPlayer> GetThreePlayers()
        {
            List<HockeyPlayer> players = new List<HockeyPlayer>()
            {
                new HockeyPlayer("Jarkko Immonen", "26"),
                new HockeyPlayer("Teemu Selänne", "8")
            };
            return players;
        }
    }

    public class HockeyPlayer : INotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                Notify("Name");
                Notify("NameAndNumber");
            }
        }

        private string number;

        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                Notify("Number");
                Notify("NameAndNumber");
            }
        }

        public string NameAndNumber
        {
            get
            {
                return Name + ", " + Number;
            }
        }
        public HockeyPlayer()
        {

        }

        public HockeyPlayer(string name, string number)
        {
            this.name = name;
            this.number = number;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }

    public class HockeyTeam
    {
        public string Name { get; set; } = "";
        public string City { get; set; } = "None";

        public HockeyTeam()
        {
        }

        public HockeyTeam(string name, string city)
        {
            Name = name;
            City = city;
        }

        public override string ToString()
        {
            return Name + " @ " + City;
        }
    }

    public class HockeyLeague
    {
        List<HockeyTeam> teams;
        public HockeyLeague()
        {
            teams = new List<HockeyTeam>()
            {
                new HockeyTeam("Ilves", "Tampere"),
                new HockeyTeam("JYP", "Jyväskylä"),
                new HockeyTeam("HIFK", "Helsinki"),
                new HockeyTeam("Kärpät", "Oulu"),
                new HockeyTeam("KalPa", "Kuopio")
            };
        }

        public List<HockeyTeam> GetTeams()
        {
            return teams;
        }
    }
}
