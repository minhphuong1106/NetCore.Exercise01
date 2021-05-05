using System;
using System.Configuration;

namespace VMP.NetCore.Exercise01.Common
{
    public static class ConfigValues
    {
        
        public static string GetConnectionString(string connectionStringName)
        {
            string result = string.Empty;
            try
            {
                result = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static string DefaultConnectionString
        {
            get { return GetConnectionString(ProjectConstants.DBConstants.DefaultConnectionString); }
        }


        /*
        public static int ServerTimeZoneNo
        {
            get
            {
                try
                {
                    int result = GlobalConstant.TimeZoneNoDefault;
                    string timeZoneNo = ConfigurationManager.AppSettings["ServerTimeZoneNo"];
                    Int32.TryParse(timeZoneNo, out result);
                    return result;
                }
                catch
                {
                    return GlobalConstant.TimeZoneNoDefault;
                }
            }
        }
        */
    }
}
