using DAL.Additional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Additional
{
    public class ConnectionExeption : Exception
    {

        public ConnectionExeption() : base("Ошибка соединения с сервером!")
        {
        }

        public ConnectionExeption(Exception InnerExeption) : base("Ошибка соединения с сервером!", InnerExeption)
        {
        }
    }


    public static  class ExceptionSystem
    {
        public static event EventHandler<Exception> ConnectLost;

        internal static void ConnectLostInv(Exception ex)
        {
            if(ex is ConnectionExcetionD)
                
            ConnectLost?.Invoke(null,new ConnectionExeption(ex.InnerException));
        }


    }



}
