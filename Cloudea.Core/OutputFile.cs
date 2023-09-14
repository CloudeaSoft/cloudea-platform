using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Core
{
    public class OutputFile
    {
        public static void outputTxt(IConfiguration configuration)
        {
            using (StreamReader sr = new StreamReader(configuration["Cloudea:Icon:TextIcon"], Encoding.UTF8)) {
                string line;
                while ((line = sr.ReadLine()) != null) {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
