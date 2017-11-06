using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_project
{
    public class Edge
    {
        private int _project;
        private int _expert;
        private int _weight;

        public Edge(int project, int expert, int weight)
        {
            _project = project;
            _expert = expert;
            _weight = weight;
        }

        public string EdgeDescription()
        {
            return string.Format("Edge: project index: {0}, expert index {1}, feature {2}", _project, _expert, _weight);
        }
    }
}
