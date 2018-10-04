using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MorphAnalysis.HelperClasses
{
    class FinalSolutionEstimate : IDisposable
    {

        //Создать словарь для хранения id решения и его итоговой оценки

        private Dictionary<int, decimal> _solByParamEstimateDict;

        private Dictionary<int, decimal> _modByParamEstimateDict;

        private Dictionary<int, decimal> _solByFuncEstimateDict;

        private Dictionary<int, decimal> _solFinalEstimateDict;

        private Dictionary<int, decimal> _solWithModByParamEsitmateDict;

        private static int counterForGetSolutionByParam = 0;

        //for dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        //TEST
        // private Dictionary<string, Dictionary<int, decimal>> _dict;

        private static FinalSolutionEstimate instance;

        private FinalSolutionEstimate()
        {
            _solByParamEstimateDict = new Dictionary<int, decimal>();

            _modByParamEstimateDict = new Dictionary<int, decimal>();

            _solByFuncEstimateDict = new Dictionary<int, decimal>();

            _solFinalEstimateDict = new Dictionary<int, decimal>();

            _solWithModByParamEsitmateDict = new Dictionary<int, decimal>();

            //TEST
            //_dict = new Dictionary<string, Dictionary<int, decimal>>();
        }

        public static FinalSolutionEstimate GetInstance()
        {
            if (instance == null)
                instance = new FinalSolutionEstimate();
            return instance;
        }

        // Методы для додавання оцінок в словник
        public void SetEstimates<T>(List<T> list)
        {
            var dict = GetDictByType<T>();
            Type type = typeof(T);
            if (type.Name == nameof(ParametersGoalsForSolution))
            {
                SetEstimatesByParametersGoalsForSolutionToDictionary(list.Cast<ParametersGoalsForSolution>().ToList(), dict);
            }
            else if(type.Name == nameof(ParametersGoalsForModification))
            {
                SetEstimatesByParametersGoalsForModificationToDictionary(list.Cast<ParametersGoalsForModification>().ToList(), dict);
            }
            else if(type.Name == nameof(SolutionsOfFunction))
            {
                SetEstimatesBySolutionsOfFunctionToDictionary(list.Cast<SolutionsOfFunction>().ToList(), dict);
            }
        }

        private Dictionary<int, decimal> GetDictByType<T>()
        {
            string nameClass = typeof(T).Name;
            switch (nameClass)
            {
                case nameof(ParametersGoalsForSolution):
                    return _solByParamEstimateDict;
                    break;
                case nameof(ParametersGoalsForModification):
                    return _modByParamEstimateDict;
                    break;
                case nameof(SolutionsOfFunction):
                    return _solByFuncEstimateDict;
                    break;

                default:
                    break;
            }
            return null;
        }

        #region Методи для зберігання оцінок ParametersGoalsForSolution, ParametersGoalsForModification, SolutionsOfFunction у словники

        //Додати до словника id рішення і його оцінку отриману з классу ParametersGoalsForSolution
        private void SetEstimatesByParametersGoalsForSolutionToDictionary(List<ParametersGoalsForSolution> list, Dictionary<int, decimal> dict)
        {
            foreach(ParametersGoalsForSolution item in list)
            {
                decimal rating = item.rating ?? 0;
                int id = item.Solution.id_solution;
                AddElementToDictionary(dict, id, rating);
            }
        }

        //Додати до словника id модифікації і її оцінку отриману з классу ParametersGoalsForModification
        private void SetEstimatesByParametersGoalsForModificationToDictionary(List<ParametersGoalsForModification> list, Dictionary<int, decimal> dict)
        {
            foreach (ParametersGoalsForModification item in list)
            {
                decimal rating = item.rating ?? 0;
                int id = item.Modification.id_modification;
                AddElementToDictionary(dict, id, rating);
            }
        }

        //Додати до словника id рішення функції і її оцінку отриману з классу SolutionsOfFunction
        private void SetEstimatesBySolutionsOfFunctionToDictionary(List<SolutionsOfFunction> list, Dictionary<int, decimal> dict)
        {
            foreach (SolutionsOfFunction item in list)
            {
                decimal rating = item.rating ?? 0;
                int id = item.Solution.id_solution;
                AddElementToDictionary(dict, id, rating);
            }
        }

        //Для группування оцінок по id
        private void AddElementToDictionary(Dictionary<int, decimal> dict, int id, decimal rating)
        {
            if (dict.ContainsKey(id))
                dict[id] += rating;
            else
                dict.Add(id, rating);
        }

        #endregion

        #region Методи для розрахунків оцінок

        //Розрахунок кінцевої оцінки рішення
        private void CalcFinalSolutionEstimate()
        {
            CalcSolutionWithModificationByGoal();

            //У випадку, якщо id рішень по параматрем цілей та рішень по функціям співпадають 
            foreach (KeyValuePair<int, decimal> solutionByParam in _solWithModByParamEsitmateDict)
            {
                int idKey = solutionByParam.Key;
                decimal rating = 0.0m;

                if (_solByFuncEstimateDict.ContainsKey(idKey))
                {
                    rating = solutionByParam.Value + _solByFuncEstimateDict[idKey];
                }
                AddElementToDictionary(_solFinalEstimateDict, idKey, rating);
            }

            AddUniqueElementToFinalEstimateDict(_solByParamEstimateDict);
            AddUniqueElementToFinalEstimateDict(_solByFuncEstimateDict);
        }

        //У випадку, якщо id рішень по параметрам не співпали з рішеннями по функціям
        private void AddUniqueElementToFinalEstimateDict(Dictionary<int, decimal> dict)
        {
            foreach (KeyValuePair<int, decimal> kvp in dict)
            {
                int id = kvp.Key;
                decimal rating = kvp.Value;
                if (_solFinalEstimateDict.ContainsKey(id))
                    continue;
                else
                    _solFinalEstimateDict.Add(id, rating);
            }

        }

        //Оцінка кожного рішення, яка складається з суми оцінки модифікацій та оцінки рішення згідно параметрам цілей
        private void CalcSolutionWithModificationByGoal()
        {
            decimal sumMod = CalcSumModificationEstimate();

            foreach (KeyValuePair<int,decimal> item in _solByParamEstimateDict)
            {
                //_solByParamEstimateDict[item.Key] = item.Value + sumMod;
                _solWithModByParamEsitmateDict.Add(item.Key, item.Value + sumMod);
            }
        }

        //Розрахунок суми всіх модифікацій
        private decimal CalcSumModificationEstimate()
        {
            //decimal sum = 0.0m;
            //foreach(KeyValuePair<int, decimal> kvp in _modByParamEstimateDict)
            //{
            //    sum += kvp.Value;
            //}
            //return sum;

            return _modByParamEstimateDict.Sum(e => e.Value);
        }

        #endregion

        #region Методи для отримання оцінок

        //Отримати кінцевий результат оцінок
        public Dictionary<int, decimal> GetFinalSolutionsEstimates
        {
            get
            {
                CalcFinalSolutionEstimate();
                return _solFinalEstimateDict;
            }
        }

        public Dictionary<int, decimal> GetEstimatesBy<T>()
        {
            //Повертаємо словник рішень по параметрам цілей до того, як будуть розраховані оцінки
            //рішень з параметрами цілей разом з модифікаціями по параметрам цілей
            if (counterForGetSolutionByParam == 0 && typeof(T).Name == nameof(ParametersGoalsForSolution))
            {
                counterForGetSolutionByParam += 1;
                return GetDictByType<ParametersGoalsForSolution>();
            }
            else if (counterForGetSolutionByParam > 0 && typeof(T).Name == nameof(ParametersGoalsForSolution))
            {
                //return GetDictByType<ParametersGoalsForSolution>();
                return _solWithModByParamEsitmateDict;
            }

            return GetDictByType<T>();

            //_solByParamEstimateDict;
            //
            //_modByParamEstimateDict;
            //
            //_solByFuncEstimateDict;
            //
            //_solFinalEstimateDict;
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();

                instance = null;
            }

            disposed = true;
        }

        #endregion
    }
}
