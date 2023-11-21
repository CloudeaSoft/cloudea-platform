using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure.Database
{
    public class DatabaseSetupException : Exception
    {
        public DatabaseSetupException(string databaseName)
            : base(databaseName)
        {
            throw new DatabaseSetupException(databaseName);
        }

        private DatabaseSetupException()
        {
        }

        private DatabaseSetupException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}