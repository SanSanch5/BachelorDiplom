using System;

namespace BachelorLibAPI.Program
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
    class UnknownPlacemark : Exception
    {
        public override string Message
        {
            get
            {
                return "На карте нет точки с таким названием";
            }
        }
    }
    class RouteBuilderLogicException : Exception
    {
        public override string Message
        {
            get
            {
                return "Вознилка ошибка в ходе обработки данных";
            }
        }
    }
}
