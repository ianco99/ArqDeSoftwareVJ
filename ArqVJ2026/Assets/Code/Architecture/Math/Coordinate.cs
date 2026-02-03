using System;
using System.Collections.Generic;

namespace ZooArchitect.Architecture.Math
{
    public struct Coordinate
    {
        private Point[] points;

        public bool IsSingleCoordinate => points.Length == 1;
        public Point Origin => points[0];
        public Point SecondPoint => points[1];
        public IEnumerable<Point> Points => points;

        public Coordinate(params Point[] points)
        {
            if (points == null || points.Length == 0)
            {
                throw new Exception();
            }
            this.points = points;
        }

        public void Move(Point offset)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] += offset;
            }
        }
    }
}
