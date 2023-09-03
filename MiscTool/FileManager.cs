using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloudea.Entity.MiscTool;
using Cloudea.Infrastructure.Models;

namespace MiscTool {
    public class FileManager {

        private readonly IFreeSql Database;

        public FileManager(IFreeSql database) {
            Database = database;
        }

        public async Task<List<FileManager_Files>> GetFileList(
            string userName = "Test"
            ) {
            var list = await Database.Select<FileManager_Files>().ToListAsync();
            return list;
        }

        public async Task<Result> Upload(string fileName) {
            try {
                FileManager_Files tempFile = new() {
                    FileName = fileName
                };
                var a = await Database.Insert<FileManager_Files>(tempFile).ExecuteAffrowsAsync();
                Console.WriteLine(a + "在这里");
            }
            catch (Exception ex){ 
                return Result.Fail(ex.Message);
            }
            return Result.Success("成功");
        }

        public void Download() { 
        
        }


    }
}
