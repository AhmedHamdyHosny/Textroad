using Classes.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes.Common
{
    
    public class PartialParamters
    {
        protected interface BasePartialParametersInterface
        {
            string AngularModel { get; set; }
        }
        public class BasePartialParameters
        {
            protected bool _Show = false;
            protected bool _UseSelect = false;
            protected string _SelectedItem = null;
            protected string _LabelText = null;
            protected bool _Required = false;
            
            public bool Show
            {
                get
                {
                    return _Show;
                }

                set
                {
                    _Show = value;
                }
            }
            public bool UseSelect
            {
                get
                {
                    return _UseSelect;
                }

                set
                {
                    _UseSelect = value;
                }
            }
            public string SelectedItem
            {
                get
                {
                    return _SelectedItem;
                }

                set
                {
                    _SelectedItem = value;
                }
            }
            public string LabelText
            {
                get
                {
                    return _LabelText;
                }

                set
                {
                    _LabelText = value;
                }
            }
            public bool Required
            {
                get
                {
                    return _Required;
                }
                set
                {
                    _Required = value;
                }
            }
            public IEnumerable<CustomSelectListItem> Items { get; set; }
        }
        public class Subject : BasePartialParameters, BasePartialParametersInterface
        {
            public Subject()
            {
                LabelText = Resources.Administration.Subject;
            }
            private string _ControlName = "SubjectID";
            private string _AngularModel = "model.SubjectID";
            public string ControlName
            {
                get
                {
                    return _ControlName;
                }

                set
                {
                    _ControlName = value;
                }
            }
            public string AngularModel
            {
                get
                {
                    return _AngularModel;
                }

                set
                {
                    _AngularModel = value;
                }
            }
        }
        public class Journal : BasePartialParameters, BasePartialParametersInterface
        {
            public Journal()
            {
                LabelText = Resources.Administration.Journal;
            }
            private string _ControlName = "JournalID";
            private string _AngularModel = "model.JournalID";
            public string ControlName
            {
                get
                {
                    return _ControlName;
                }

                set
                {
                    _ControlName = value;
                }
            }
            public string AngularModel
            {
                get
                {
                    return _AngularModel;
                }

                set
                {
                    _AngularModel = value;
                }
            }
        }
        public class JournalVersion : BasePartialParameters, BasePartialParametersInterface
        {
            public JournalVersion()
            {
                LabelText = Resources.Administration.JournalVersion;
            }
            private string _ControlName = "JournalVersionID";
            private string _AngularModel = "model.JournalVersionID";
            public string ControlName
            {
                get
                {
                    return _ControlName;
                }

                set
                {
                    _ControlName = value;
                }
            }
            public string AngularModel
            {
                get
                {
                    return _AngularModel;
                }

                set
                {
                    _AngularModel = value;
                }
            }
        }
    }
}