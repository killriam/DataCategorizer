using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Categorizer
{
    interface IDataSourceConnector
    {
        
        void MovetoNextElement();

        object getCurrentElement();

        int getCurrentElementIndex();
        void setCategoryOfCurrentElement(string newcategory);
    }
}
