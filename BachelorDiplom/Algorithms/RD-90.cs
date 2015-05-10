using System.Collections.Generic;

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
        /// Если нет показателя, то обезвреживающее в-во не нужно
        /// </summary>
        public static readonly Dictionary<string, KeyValuePair<string, double>> AntiSubstance = 
            new Dictionary<string,KeyValuePair<string,double>>
        {
            {"Аммиак",                      new KeyValuePair<string, double>("36% р-р соляной кислоты", 5.6)},
            {"Водород фтористый",           new KeyValuePair<string, double>("вода", 38)},
            {"Окись этилена",               new KeyValuePair<string, double>("25 % р-р аммиака", 2)},
            {"Сероуглерод",                 new KeyValuePair<string, double>("гипохлорид кальция", 4)},
            {"Соляная",                     new KeyValuePair<string, double>("кислота каустическая сода", 3.7)},
            {"Фосген",                      new KeyValuePair<string, double>("каустическая сода", 2)},
            {"Хлор",                        new KeyValuePair<string, double>("каустическая сода", 1.3)},
            {"Нитрил акриловой кислоты ",   new KeyValuePair<string, double>("каустическая сода", 0.8)},
            {"Азотная кислота",             new KeyValuePair<string, double>("каустическая сода", 1.9)}
        };
    }

    public interface IChemicalEnvironmentCalculation
    {
        double InfectionArea();
        KeyValuePair<string, double> AntiSubstanceCount();
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

        Rd90(string substance, double substanceCount)
        {
            _substance = substance;
            _substanceCount = substanceCount;

            QEq = _substanceCount / Ahov.Coefficient[this._substance];
        }

        public double InfectionArea()
        {
            return 0;
        }

        public KeyValuePair<string, double> AntiSubstanceCount()
        {
            return Ahov.AntiSubstance.ContainsKey(_substance) 
                ? new KeyValuePair<string, double>(Ahov.AntiSubstance[_substance].Key, 
                    Ahov.AntiSubstance[_substance].Value*_substanceCount)
                : new KeyValuePair<string, double>("", 0);
        }
    }
}
