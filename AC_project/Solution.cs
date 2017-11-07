using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_project
{
    public class ProjectSolution
    {
        Project _project;
        public Project Project { get { return _project; } }
        List<Expert> _experts;

        public bool IsCompleted { get { return _project.IsCompleted; } }

        public ProjectSolution(Project project)
        {
            _project = project;
            _experts = new List<Expert>();
        }

        public void Add(Expert expert)
        {
            _experts.Add(expert);
        }

        public String Print()
        {
            String output = "";
            int i = 0;
            foreach (Expert expert in _experts)
            {
                output += String.Format("{0}", expert.Index);
                if (i < _experts.Count - 1)
                {
                    output += ", ";
                }
                i++;
            }
            return output;
        }
    }

    public class Solution
    {
        public int Sum = 0;
        List<ProjectSolution> _projectSolutions = new List<ProjectSolution>();

        public void Add(Project project, Expert expert = null)
        {
            ProjectSolution projSol = _projectSolutions.Find(s => s.Project.Index == project.Index);
            if(projSol == null)
            {
                projSol = new ProjectSolution(project);
                _projectSolutions.Add(projSol);
            }
            if (expert != null)
            {
                projSol.Add(expert);
            }
        }

        public void Print()
        {
            List<ProjectSolution> projSols = _projectSolutions
                .OrderBy(s => s.Project.Index)
                .ToList();
            int numberOfCompleted = projSols.Where(s => s.IsCompleted == true).Count();
            String output = String.Format("S = {0}\nProjects Completed: {1}\n\n", Sum, numberOfCompleted);
            foreach (ProjectSolution projSol in projSols)
            {
                output += String.Format("Project[{0}]:\n  Completed: {1}. ", projSol.Project.Index, projSol.IsCompleted);
                output += projSol.IsCompleted ? " " : "";
                output += String.Format("Experts: [{0}].\n", projSol.Print());
            }
            Console.WriteLine(output);
        }
    }
}
