using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Categorizer
{
    interface IDataSource
    {
        void MovetoNextElement();

        object getCurrentElement();
       
        void setCategoryOfCurrentElement(string newcategory);
    }
}
