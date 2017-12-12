using System.Collections;

namespace TurtleChallenge
{
    class Turtle
    {
        public Turtle(ArrayList actions) {
            this.x = 0;
            this.y = 0;
            this.actions = actions;
            this.direction = Direction.North;
        }

        private int x;
        private int y;
        private Direction direction;
        private ArrayList actions;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public ArrayList Actions { get => actions; set => actions = value; }
        public Direction Direction { get => direction; set => direction = value; }
        
        public void rotate() {
            switch (this.Direction) {
                case Direction.North:
                    this.direction = Direction.East;
                    break;
                case Direction.East:
                    this.direction = Direction.South;
                    break;
                case Direction.South:
                    this.direction = Direction.West;
                    break;
                default:
                    this.direction = Direction.North;
                    break;
            }
            
        }

        public void move(int xAxisLength,int yAxisLength) {
            switch (direction)
                {
                    case Direction.North:
                        if (y < yAxisLength) { y++; }
                        break;
                    case Direction.East:
                        if (x < xAxisLength) { x++; }
                        break;
                    case Direction.South:
                        if (y > 0) { y--; }
                        break;
                    default:
                        if (x > 0) { x--; }
                        break;
                }
            }

        }

    }


