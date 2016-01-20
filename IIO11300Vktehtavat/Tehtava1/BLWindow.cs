/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course project.
* Created: 14.1.2016
* Modified: 14.1.2016
* Authors: Teemu Tuomela
*/

namespace Tehtava1
{
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
