﻿using Assignment.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment.Renderer
{
    class Rendering
    {
        private Graphics graphicsPanel;
        private Panel panel;

        private Bitmap screen;
        private Graphics graphics;

        public Rendering(Graphics g, Panel p)
        {
            graphicsPanel = g;
            panel = p;
        }

        public void Render()
        {
            screen = new Bitmap((int)GameWorld.Instance.Width, (int)GameWorld.Instance.Height);

            using (graphics = Graphics.FromImage(screen))
            {
                RenderBackground();
                RenderObstacles();
                //RenderGrid();
                //RenderEntities();
                RenderNavGraph();
                RenderShortestPath();
            }

            graphicsPanel.DrawImage(screen, 0, 0, panel.Width, panel.Height);
        }

        private void RenderBackground()
        {
            graphics.FillRectangle(Brushes.White, 0, 0, (int)GameWorld.Instance.Width, (int)GameWorld.Instance.Height);
        }

        private void RenderObstacles()
        {
            const bool SHOW_OBSTACLE_LOCATION = false;

            foreach (var obstacle in GameWorld.Instance.Obstacles)
            {
                obstacle.Render(graphics);
                if (SHOW_OBSTACLE_LOCATION)
                {
                    Brush b = new SolidBrush(Color.DarkGoldenrod);
                    Font f = new Font(SystemFonts.DefaultFont.Name, 6);
                    graphics.DrawString(obstacle.Location.X + ", " + obstacle.Location.Y, f, b, (float)obstacle.Location.X, (float)obstacle.Location.Y);
                }
            }
        }

        private void RenderGrid()
        {
            var pen = new Pen(Color.Red);
            var font = new Font(FontFamily.GenericSansSerif, 7);
            var grid = GameWorld.Instance.Grid;

            for (int x = 0; x < grid.GridWidth; x++)
            {
                for (int y = 0; y < grid.GridHeight; y++)
                {
                    graphics.DrawRectangle(pen, x * Grid.CellWidth, y * Grid.CellHeight, Grid.CellWidth, Grid.CellHeight);

                    graphics.DrawString(grid.GridCells[x, y].Entities.Count.ToString(), font, Brushes.Blue, x * Grid.CellWidth, y * Grid.CellHeight);
                }
            }
        }

        private void RenderEntities()
        {
            foreach (var entity in GameWorld.Instance.Entities)
            {
                var oldLocation = new Location(entity.Location.X, entity.Location.Y);
                entity.Render(graphics);
                GameWorld.Instance.Grid.UpdateEntity(entity, oldLocation);
            }
        }

        private void RenderNavGraph()
        {
            const bool SHOW_VERTEX_LABEL = false;
            const bool SHOW_EDGES = true;
            const bool SHOW_VERTICES = true;

            var vs = GameWorld.Instance.NavGraph.vertices;
            for (int x = 0; x < vs.GetLength(0); x++)
            {
                for (int y = 0; y < vs.GetLength(1); y++)
                {
                    Graph.Vertex v = vs[x, y];
                    if (v != null)
                    {
                        if (SHOW_EDGES)
                        {
                            foreach (Graph.Edge e in v.Adj)
                            {
                                Graph.Vertex w = e.Dest;
                                graphics.DrawLine(new Pen(Color.Black, 1), (float)v.Loc.X, (float)v.Loc.Y, (float)w.Loc.X, (float)w.Loc.Y);
                            }
                        }
                        if (SHOW_VERTICES)
                        {
                            float renderedVertexRadius = 1;
                            graphics.DrawEllipse(new Pen(Color.LawnGreen), (float)v.Loc.X - renderedVertexRadius / 2,
                                (float)v.Loc.Y - renderedVertexRadius / 2, renderedVertexRadius, renderedVertexRadius);
                        }
                        if (SHOW_VERTEX_LABEL)
                        {
                            Brush b = new SolidBrush(Color.DarkBlue);
                            Font f = new Font(SystemFonts.DefaultFont.Name, 6);
                            graphics.DrawString(v.Label, f, b, (float)v.Loc.X, (float)v.Loc.Y);
                        }
                    }
                }
            }
        }

        private bool RenderShortestPath()
        {
            Graph.Vertex[,] vertices = GameWorld.Instance.NavGraph.vertices;
            try
            {
                if (GameWorld.Instance.PathAlreadyCalculated == null) { 
                    Random rand = new Random();
                    Graph.Vertex start = null, goal = null;
                    while (goal == null || start == null)
                    {
                        // Generate random start and goal for pathfinder example
                        start = vertices[rand.Next(0, vertices.GetLength(0)), rand.Next(0, vertices.GetLength(1))];
                        goal = vertices[rand.Next(0, vertices.GetLength(0)), rand.Next(0, vertices.GetLength(1))];
                    }

                    GameWorld.Instance.PathAlreadyCalculated = Movement.Pathfinding.AStar(start, goal);
                }

                List<Graph.Vertex> path = GameWorld.Instance.PathAlreadyCalculated;
                Pen p = new Pen(Color.DeepPink, Math.Max(1, (float) GameWorld.Instance.Width / 250));
                for (int i = 1; i < path.Count; i++)
                {
                    Graph.Vertex v = path[i - 1];
                    Graph.Vertex w = path[i];
                    graphics.DrawLine(p, (float)v.Loc.X, (float)v.Loc.Y, (float)w.Loc.X, (float)w.Loc.Y);
                }
                return true;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Could not render Shortest Path");
                return false;
            }
        }
    }
}
