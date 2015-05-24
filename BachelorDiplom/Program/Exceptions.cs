using System;

namespace BachelorLibAPI.Program
{
    [Serializable]
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

    [Serializable]
    class UnknownPlacemark : Exception
    {
        public override string Message
        {
            get
            {
                return "Ошибка получения адреса. Возможны проблемы с подключением. Попробуйте позже.";
            }
        }
    }

    [Serializable]
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
