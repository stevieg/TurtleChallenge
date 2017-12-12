using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace TurtleChallenge
{

    class Utils
    {
        private static string xSizePattern = @"x value = (\d+)";
        private static string ySizePattern = @"y value = (\d+)";
        private static string mineValuePattern = @"mine value = (\d+),(\d+)";
        private static string goalValuePattern = @"goal value = (\d+),(\d+)";
        private static string movePattern = "(move)|(rotate)";
        
        public static ArrayList getFileAsList(string fileNameAndPath) {
            ArrayList results = new ArrayList();
            string line;
            StreamReader file = new StreamReader(fileNameAndPath);
            while ((line = file.ReadLine()) != null)
            {
                results.Add(line.ToLower());   
            }
            file.Close();
            return results;
        }

        public static Nullable<bool>[,] getMap(ArrayList fileLines) {
            Nullable<bool>[,] grid = null;
            int xValue = 0;
            int yValue = 0;
            int[] goalValues = null;

            ArrayList mineLocations = new ArrayList();
            foreach (string value in fileLines) {
                if (xValue == 0) {
                    Match xMatch = Regex.Match(value, xSizePattern);
                    if (xMatch.Success)
                    {
                        xValue = Int32.Parse(xMatch.Groups[1].Value);
                        if (xValue == 0)
                        { return null; }
                        continue;
                    }
                }
                if (yValue == 0)
                {
                    Match yMatch = Regex.Match(value, ySizePattern);
                    if (yMatch.Success)
                    {
                        yValue = Int32.Parse(yMatch.Groups[1].Value);
                        if (yValue == 0)
                        { return null; }
                        continue;
                    }
                }

                Match mineMatch = Regex.Match(value, mineValuePattern);
                if (mineMatch.Success) {
                    int[] mineCoordinates = new int[2];
                    mineCoordinates[0] = Int32.Parse(mineMatch.Groups[1].Value);
                    mineCoordinates[1] = Int32.Parse(mineMatch.Groups[2].Value);
                    mineLocations.Add(mineCoordinates);
                    continue;
                }

                if (null == goalValues) {
                    Match goalMatch = Regex.Match(value, goalValuePattern);
                    if (goalMatch.Success) { 
                        goalValues = new int[2];
                        goalValues[0] = Int32.Parse(goalMatch.Groups[1].Value);
                        goalValues[1] = Int32.Parse(goalMatch.Groups[2].Value);
                        }
                    }
             }
                
            grid = new Nullable<bool>[xValue, yValue];

            /* Set Grid Values to False for Mine Locations */
            foreach (int[] mineCoordinates in mineLocations)
            {
                if (mineCoordinates[0] < xValue && mineCoordinates[1] < yValue)
                {
                    grid[mineCoordinates[0] - 1, mineCoordinates[1] - 1] = false;
                }
            }
            /*Set Grid value to true for end goal*/
            grid[goalValues[0] - 1, goalValues[1] - 1] = true;

            return grid;
        }


        public static ArrayList getTurtleInstructions(ArrayList fileLines) {
            ArrayList turtleInstructions = new ArrayList();
            foreach (string line in fileLines) {
                Match moveMatch = Regex.Match(line, movePattern);
                if (moveMatch.Success)
                {
                    if (moveMatch.Groups[1].Value != string.Empty)
                    {
                        turtleInstructions.Add(Action.MoveForward);
                    }
                    else
                    {
                        turtleInstructions.Add(Action.Rotate);
                    }
                }
            }
            return turtleInstructions;
        }
        
        
    }
    


}