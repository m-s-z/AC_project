using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AC_project
{
    public class Problem
    {
        private int _numberOfFeatures;
        private List<Project> _listProjects;
        private List<Expert> _listExperts;

        public List<Project> listProjects { get { return _listProjects; } }
        public List<Expert> listExperts { get { return _listExperts; } }
        public int numberOfFeatures { get { return _numberOfFeatures; } }
        public int numberOfExperts { get { return _listProjects.Count; } }
        public int numberOfProjects { get { return _listProjects.Count; } }

        public Problem(string filePath)
        {
            ReadFile(filePath);
            SetIndices();
        }

        public Problem(List<Project> projects, List<Expert> experts)
        {
            _listProjects = projects;
            _listExperts = experts;
            _numberOfFeatures = projects.First().Features.Count();
            
        }

        public void ReadFile(string pathToFile)
        {
            try
            {
                string line = File.ReadLines(pathToFile).First();
                string[] lines = line.Split(';');
                if (lines.Count() <= 1)
                {
                    lines = line.Split(',');
                } 

                int numberOfProjects = int.Parse(lines[0]);
                int numberOfExperts = int.Parse(lines[1]);
                _numberOfFeatures = int.Parse(lines[2]);

                _listProjects = File.ReadLines(pathToFile)
                                       .Skip(1)
                                       .Take(numberOfProjects)
                                       .Select(v => new Project(v))
                                       .ToList();

                _listExperts = File.ReadLines(pathToFile)
                                       .Skip(numberOfProjects + 1)
                                       .Take(numberOfExperts)
                                       .Select(v => new Expert(v))
                                       .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw e;
            }
        }

        public void SetIndices()
        {
            for (int i = 0; i < _listExperts.Count(); i++)
            {
                _listExperts[i].Index = i;
            }

            for (int i = 0; i < _listProjects.Count(); i++)
            {
                _listProjects[i].Index = i;
            }
        }

        private void SetFeatures()
        {

        }
    }
}
