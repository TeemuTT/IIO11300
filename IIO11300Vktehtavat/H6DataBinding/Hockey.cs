/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course.
* Created: 25.2.2016
* Authors: Teemu Tuomela
*/

using System.Collections.Generic;

namespace H6DataBinding
{
    public class HockeyPlayer
    {

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
