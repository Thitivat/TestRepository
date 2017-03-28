using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BND.Services.IbanStore.Service.Bll
{
    internal class DbExceptionHelper
    {
        /// <summary>
        /// This method handle the DbUpdateException to split by case to return the proper message to outside.
        /// </summary>
        /// <param name="dbException">The database exception.</param>
        /// <param name="exceptionName">The exception key.</param>
        /// <param name="exceptionProcess">The exception process.</param>
        /// <returns>Exception.</returns>
        public static Exception ThrowException(DbUpdateException dbException,string exceptionName, string exceptionProcess)
        {
            SqlException sqlException;
            if (dbException.InnerException != null &&
                (sqlException = dbException.InnerException.InnerException as SqlException) != null)
            {
                switch(sqlException.Number)
                {
                    case 2627: // Unique constraint error
                        return new IbanOperationException(MessageLibs.MSG_ALREADY_EXIST_DATABASE.Code,
                                                          String.Format(MessageLibs.MSG_ALREADY_EXIST_DATABASE.Message, exceptionName),
                                                          dbException);
                    default: // TODO:: in case that we don't know yet.
                        return new IbanOperationException(MessageLibs.MSG_CANNOT_PROCESS_DATABASE.Code,
                                                          String.Concat("Unknown:", sqlException.Number),
                                                          dbException);
                }
            }
            else 
            {
                return new IbanOperationException(
                    MessageLibs.MSG_CANNOT_PROCESS_DATABASE.Code,
                    string.Format(MessageLibs.MSG_CANNOT_PROCESS_DATABASE.Message, exceptionName, exceptionProcess),
                    dbException
                );
            }
        }
    }
}
