using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Additional
{
    public class AddEditExeption : Exception
    {
        public AddEditExeption(string text, Exception InnerExeption) : base(text, InnerExeption) { }
    }

}
