using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure
{
    public class OutputFile
    {
        public static void outputTxt(IConfiguration configuration)
        {
            try {
                using StreamReader sr = new(configuration["Cloudea:Icon:TextIcon"], Encoding.UTF8);
                string line;
                while ((line = sr.ReadLine()) != null) Console.WriteLine(line);
            }
            catch (Exception ex) { 
                
            }
        }
    }
}
