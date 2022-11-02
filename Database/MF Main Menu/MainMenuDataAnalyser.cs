using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    class MainMenuDataAnalyser
    {
        MainMenuManager mainMenuManager;
        public MainMenuDataAnalyser(MainMenuManager parentMainMenu)
        {
            mainMenuManager = parentMainMenu;
        }
        public string[] ConvertPathsToNames(string[] paths)
        {
            string[] names = new string[paths.Length];
            int i = 0;
            foreach(string path in paths)
            {
                names[i] = path.Split(@"\")[^1];
                i++;
            }
            return names;
        }
        public bool CheckIfNameExists(string[] paths, string name)
        {
            return ConvertPathsToNames(paths).Contains(name);
        }
    }
}
