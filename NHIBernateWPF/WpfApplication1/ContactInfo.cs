using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace WpfApplication1
{
    public class ContactInfo:IDataErrorInfo
    {
        public virtual int ContactId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual int Age { get; set; }
        public virtual string Mobile { get; set; }

        public virtual string Error
        {
            get
            {
                return this[string.Empty];
            }
        }

        public virtual string this[string columnName]
        {
            get
            {
                if (columnName == "FirstName")
                {
                    if (string.IsNullOrEmpty(columnName))

                    {

                        return "Name Cannot be Empty.";

                    }
                }
                return null;
            }
        }
    }
}
