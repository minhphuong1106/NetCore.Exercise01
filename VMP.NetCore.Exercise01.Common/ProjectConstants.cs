using System;
using System.Collections.Generic;
using System.Text;

namespace VMP.NetCore.Exercise01.Common
{
    public static class ProjectConstants
    {
        public static class DBConstants
        {
            public const string DefaultConnectionString = "ProjectConnectionString";
            public const string DefaultUser = "admin01";
            public const string DefaultPassword = "password@1";
        }

        public static class PagingConstants
        {
            public const int PageSize = 20;
        }
    }
}
