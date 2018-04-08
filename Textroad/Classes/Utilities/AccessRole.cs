using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes.Utilities
{
    public class GenericAccessPermission
    {
        private bool _ViewPermission = false;
        private bool _AddPermission = false;
        private bool _EditPermission = false;
        private bool _DetailsPermission = false;
        private bool _DeletePermission = false;
        private bool _ExportPermission = false;
        private bool _ImportPermission = false;

        public bool ViewPermission
        {
            get
            {
                return _ViewPermission;
            }

            set
            {
                _ViewPermission = value;
            }
        }
        public bool AddPermission
        {
            get
            {
                return _AddPermission;
            }

            set
            {
                _AddPermission = value;
            }
        }
        public bool EditPermission
        {
            get
            {
                return _EditPermission;
            }

            set
            {
                _EditPermission = value;
            }
        }
        public bool DetailsPermission
        {
            get
            {
                return _DetailsPermission;
            }

            set
            {
                _DetailsPermission = value;
            }
        }
        public bool DeletePermission
        {
            get
            {
                return _DeletePermission;
            }

            set
            {
                _DeletePermission = value;
            }
        }
        public bool ExportPermission
        {
            get
            {
                return _ExportPermission;
            }

            set
            {
                _ExportPermission = value;
            }
        }
        public bool ImportPermission
        {
            get
            {
                return _ImportPermission;
            }

            set
            {
                _ImportPermission = value;
            }
        }


    }
}