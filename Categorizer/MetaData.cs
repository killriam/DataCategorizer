using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Categorizer
{
    public class MetaData<T>
    {
        public T Data; // can also be an identifier
        public String Catagory;

        public MetaData(T data)
        {
            Data = data;
        }

        public MetaData(T data, string catagory)
        {
            Data = data;
            Catagory = catagory;
        }
    }
}
