﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AC_project
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public abstract class Graph<TVertex>
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public abstract IEnumerable<TVertex> Vertices { get; }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public abstract IEnumerable<TVertex> GetOutgoingNeighbours([NotNull] TVertex from);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public abstract IEnumerable<TVertex> GetIncomingNeighbours([NotNull] TVertex to);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual ISet<TVertex> GetUnreachableVertices([NotNull] IReadOnlyList<TVertex> roots)
        {
            var unreachableVertices = new HashSet<TVertex>(Vertices);
            unreachableVertices.ExceptWith(roots);
            var visitingQueue = new List<TVertex>(roots);

            var currentVertexIndex = 0;
            while (currentVertexIndex < visitingQueue.Count)
            {
                var currentVertex = visitingQueue[currentVertexIndex];
                currentVertexIndex++;
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var neighbour in GetOutgoingNeighbours(currentVertex))
                {
                    if (unreachableVertices.Remove(neighbour))
                    {
                        visitingQueue.Add(neighbour);
                    }
                }
            }

            return unreachableVertices;
        }
    }
}