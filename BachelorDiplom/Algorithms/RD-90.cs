using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using MathNet.Numerics;

namespace BachelorLibAPI.Algorithms
{
    public static class Ahov
    {
        /// <summary>
        /// Если здесь нет показателя, то проводится расчёт с помощью интерполяции
        /// </summary>
        public static readonly Dictionary<string, double> Coefficient = new Dictionary<string, double>
        {
            {"Хлор",                            1.0},
            {"Азотная кислота",                 21.0},
            {"Аммиак (хранение под давлением)",	25.0},
            {"Водород хлористый",               3.7},
            {"Водород фтористый",               7.8},
            {"Нитрил акриловой кислоты",        3.7},
            {"Окись этилена",                   70.0},
            {"Сероуглерод",                     350.0},
            {"Соляная кислота",                 7.0},
            {"Фосген",                          1.0},
            {"Ацетонитрил",                     150},
            {"Ацетонциангидрин",                250},
            {"Диметиламин",                     7.1},
            {"Метиламин",                       7.8},
            {"Метил бромистый",                 165},
            {"Метил хлористый",                 165},
            {"Сероводород",                     28},
            {"Формальдегид",                    1.0},
            {"Хлорпикрин",                      0.52},
            {"Сернистый ангидрид",              30.0}
        };

        /// <summary>
        /// Таблица для свободного разлива
        /// ключ - количество вещества
        /// значение: ключ - глубина, значение - площадь распространения
        /// </summary>
        public static readonly Dictionary<double, KeyValuePair<double, double>> DepthAndAreaFree =
            new Dictionary<double, KeyValuePair<double, double>>
            {
                {0.1, new KeyValuePair<double, double>(0.3653,0.0135)},
                {0.3, new KeyValuePair<double, double>(0.6327,0.0406)},
                {0.5, new KeyValuePair<double, double>(0.8167,0.0676)},
                {1,   new KeyValuePair<double, double>(1.1551,0.1352)},
                {5,   new KeyValuePair<double, double>(2.5929,0.6760)},
                {10,  new KeyValuePair<double, double>(3.7434,1.4198)},
                {30,  new KeyValuePair<double, double>(6.8954,4.8175)},
                {50,  new KeyValuePair<double, double>(9.2427,8.6557)}, 
                {100, new KeyValuePair<double, double>(13.8232,19.3607)},
                {500, new KeyValuePair<double, double>(35.9962,95.5892)}
            };

        /// <summary>
        /// Если нет показателя, то обезвреживающее в-во не нужно
        /// </summary>
        public static readonly Dictionary<string, KeyValuePair<string, double>> AntiSubstance = 
            new Dictionary<string,KeyValuePair<string,double>>
        {
            {"Аммиак",                      new KeyValuePair<string, double>("36% р-р соляной кислоты", 5.6)},
            {"Водород фтористый",           new KeyValuePair<string, double>("вода", 38)},
            {"Окись этилена",               new KeyValuePair<string, double>("25 % р-р аммиака", 2)},
            {"Сероуглерод",                 new KeyValuePair<string, double>("гипохлорид кальция", 4)},
            {"Соляная кислота",             new KeyValuePair<string, double>("кислота каустическая сода", 3.7)},
            {"Фосген",                      new KeyValuePair<string, double>("каустическая сода", 2)},
            {"Хлор",                        new KeyValuePair<string, double>("каустическая сода", 1.3)},
            {"Нитрил акриловой кислоты ",   new KeyValuePair<string, double>("каустическая сода", 0.8)},
            {"Азотная кислота",             new KeyValuePair<string, double>("каустическая сода", 1.9)}
        };
    }

    public interface IChemicalEnvironmentCalculation
    {
        double InfectionArea { get; }
        KeyValuePair<string, double> AntiSubstanceCount { get; }
    }

    /// <summary>
    /// метеорологические условия: изотермия; 
    /// скорость приземного ветра на высоте 1 м - 3 м/с 
    /// (на высоте флюгера - 5-7 м/с); 
    /// температура воздуха - +20 С; 
    /// </summary>
    class Rd90 : IChemicalEnvironmentCalculation
    {
        private readonly string _substance;
        private readonly double _substanceCount;
        private double QEq { get; set; }

        public Rd90(string substance, double substanceCount)
        {
            _substance = substance;
            _substanceCount = substanceCount;

            QEq = _substanceCount / Ahov.Coefficient[_substance];
        }

        public double InfectionArea
        {
            get
            {
                var keys = Ahov.DepthAndAreaFree.Select(x => x.Key).ToList();
                var values = Ahov.DepthAndAreaFree.Select(x => x.Value.Value).ToList();
                var interp = Interpolate.CubicSpline(keys, values);
                return interp.Interpolate(QEq); 
            }
        }

        public KeyValuePair<string, double> AntiSubstanceCount
        {
            get
            {
                return Ahov.AntiSubstance.ContainsKey(_substance) 
                    ? new KeyValuePair<string, double>(Ahov.AntiSubstance[_substance].Key, 
                        Ahov.AntiSubstance[_substance].Value*_substanceCount)
                    : new KeyValuePair<string, double>("", 0);
            }
        }
    }
}
