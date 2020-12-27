using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Additional
{
    public class ConnectionExcetionD : Exception
    {
        public ConnectionExcetionD() : base("Ошибка соединения с сервером") { }

        public ConnectionExcetionD(Exception InnerException) : base("Ошибка соединения с сервером", InnerException)
        {
        }
    }

    public static class ExceptionSystemD
    {
        public static event EventHandler<ConnectionExcetionD> ConnectLost;
        internal static void ConnectLostInv(ConnectionExcetionD except)
        {
            ConnectLost?.Invoke(null,except);
        }
    }
}