using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes.Common
{
    public class Enums
    {

        public enum Languages
        {
            en=1,
            ar=2
        }

        public enum AlertMessageType
        {
            Success=1,
            Error=2,
            Warning=3,
            info=4
        }

        public enum Transactions
        {
            Create=1,
            Edit=2,
            Delete=3,
            Import=4,
            Export=5,
            Deactive = 6,
            Active = 7
        }

        public class PopupWindowClass
        {
            public const string Meduim_Model = "meduim-Modal";
            public const string Large_Model = "large-Modal";
        }

        public enum Layout
        {
            _Default = 1,
            _Popup = 2
        }
    }

    public class DBEnums
    {
        public class UserType
        {
            public static Guid Admin = new Guid("4D17C7EA-A647-4433-A763-21D0D154BAB8");
        }
        public class AccessType
        {
            public static Guid View = new Guid("4D17C7EA-A647-4433-A763-21D0D154BAB8");
            public static Guid Add = new Guid("4D17C7EA-A647-4433-A763-21D0D154BAB8");
            public static Guid Delete = new Guid("4D17C7EA-A647-4433-A763-21D0D154BAB8");
            public static Guid Edit = new Guid("4D17C7EA-A647-4433-A763-21D0D154BAB8");
            public static Guid Details = new Guid("4D17C7EA-A647-4433-A763-21D0D154BAB8");
            public static Guid Export = new Guid("4D17C7EA-A647-4433-A763-21D0D154BAB8");
            public static Guid Import = new Guid("4D17C7EA-A647-4433-A763-21D0D154BAB8");
            public static Guid Reports = new Guid("4D17C7EA-A647-4433-A763-21D0D154BAB8");
        }
    }
}