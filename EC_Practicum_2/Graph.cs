﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace EC_Practicum_2
{
    public class Graph : List<Graph.Vertex>, ICloneable
    {
        [Serializable]
        public class Vertex
        {
            public int Node { get; set; }
            public List<int> Edges { get; set; }
            public int Color { get; set; }

        };

        private int _colorsCount;
        private Random random = new Random();
        private List<Tuple<int, int>> _connections;

        public Graph(List<Tuple<int, int>> connections, int graphSize, int colorsCount)
        {
            _colorsCount = colorsCount;
            for (int i = 0; i < graphSize; i++)
            {

                var v = new Vertex
                {
                    Node = i,
                    Edges = new List<int>(),
                    Color = random.Next(1, colorsCount + 1)
                };

                Add(v);
            }

            for (int i = 0; i < connections.Count; i++)
                ConnectNodes(connections[i].Item1, connections[i].Item2);

            _connections = connections;
        }


        public List<int> GetEdges(int node)
        {
            return this[node].Edges;
        }

        public void ConnectNodes(int a, int b)
        {
            if (!this[a].Edges.Contains(b))
                this[a].Edges.Add(b);

            if (!this[b].Edges.Contains(a))
                this[b].Edges.Add(a);
        }

        public void DisconnectNodes(int a, int b)
        {
            if (this[a].Edges.Contains(b))
                this[a].Edges.Remove(b);

            if (this[b].Edges.Contains(a))
                this[b].Edges.Remove(a);
        }

        public List<Vertex> GetGreatestColorCluster()
        {
            var colorCnt = new int[_colorsCount + 1];

            for (int i = 0; i < Count; i++)
            {
                colorCnt[this[i].Color]++;
            }

            int gcnt = colorCnt.Max();
            int gc = colorCnt.ToList().IndexOf(gcnt);

            var biggestCluster = new List<Vertex>();

            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Color == gc)
                {
                    biggestCluster.Add(this[i]);
                }
            }

            return biggestCluster;
        }

        public int GetConflicts()
        {
            var size = Count;
            var ba = new BitArray(size);
            var conflicts = 0;

            for (var i = 0; i < size; i++)
            {
                var currentColor = this[i].Color;
                var currentEdges = GetEdges(i);

                foreach (int neighbor in currentEdges)
                {
                    if (ba[neighbor]) continue;

                    if (this[neighbor].Color == currentColor)
                        conflicts++;
                }

                ba[i] = true;
            }
            return conflicts;
        }

        public void Color(Vertex node, int i)
        {
            node.Color = i;
        }

        public List<int> GetConfiguration()
        {
            return this.Select(v => v.Color).ToList();
        }

        public object Clone()
        {
            var g = new Graph(_connections, Count, _colorsCount);

            for (var i = 0; i < g.Count; i++)
                g[i].Color = this[i].Color;

            return g;
        }
    }
}