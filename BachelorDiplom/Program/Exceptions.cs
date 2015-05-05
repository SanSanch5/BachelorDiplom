using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BachelorLibAPI.Exceptions
{
    class PlacemarkGettingException : Exception
    {
        public override string Message
        {
            get
            {
                return "Ошибка при получении местоположения";
            }
        }
    }
}
