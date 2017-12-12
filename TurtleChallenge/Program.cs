using System;
using System.Collections;
using System.IO;

namespace TurtleChallenge
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string setUpFilepath = @".\instructions\setup.txt";
            string directionsFolder = @".\instructions";
            ArrayList lines = Utils.getFileAsList(setUpFilepath);
            Nullable<bool>[,] grid = Utils.getMap(lines);
            if (null == grid) {
                Console.WriteLine("Error Loading Grid");
                Console.Read();
                Environment.Exit(1);
            }


            DirectoryInfo d = new DirectoryInfo(@directionsFolder);
            FileInfo[] files = d.GetFiles("*.txt");
            int i = 0;
            foreach (FileInfo file in files) {
                if (file.Name.StartsWith("directions")) {
                    i++;
                    lines = Utils.getFileAsList(directionsFolder + @"\" + file.Name);
                    ArrayList instructions = Utils.getTurtleInstructions(lines);
                    if (instructions.Count == 0)
                    {
                        Console.WriteLine("Error Loading Turtle instructions");
                        Console.Read();
                        Environment.Exit(1);
                    }
                    
                    Turtle turtle = new Turtle(instructions);
                    bool isActionsBreak = false;
                    foreach (Action action in turtle.Actions)
                    {
                        if (action.Equals(Action.Rotate))
                        {
                            turtle.rotate();
                        }
                        else
                        {
                            turtle.move(grid.GetLength(0), grid.GetLength(1));
                        }
                        if (grid[turtle.X, turtle.Y] == true)
                        {
                            isActionsBreak = true;
                            Console.WriteLine("Sequence " + i + " We have Reached Our Goal !");
                            break;
                        }
                        if (grid[turtle.X, turtle.Y] == false)
                        {
                            isActionsBreak = true;
                            Console.WriteLine("Sequence " + i + " We have Struck a Mine!!");
                            break;
                        }
                    }

                    if (!isActionsBreak)
                    {
                        Console.WriteLine("Sequence " + i + " Still in Danger!!");
                    }


                }
            }

            
            Console.Read();
        }

    }
}
