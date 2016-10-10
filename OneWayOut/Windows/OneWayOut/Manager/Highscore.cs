using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace OneWayOut.Manager
{
    class Highscore
    {
        
        public static string readScore()
        {
        int score=0;
        List<string> entireFile = new List<string>();
        string recordScore= null;
            try
            {
               StreamReader input = new StreamReader("Content/highScore");
                string line = null;

                while((line = input.ReadLine()) !=null)
                {
                   entireFile.Add(line);
                }
                input.Close();
            }
            catch(Exception e)
            {
                Environment.Exit(0);
              
            }
            return entireFile[0];
        }
    }
}
