using Telerik.Windows.Controls;

namespace RepairCardsUI
{
    public class CustomLocalizationManager : LocalizationManager
    {
        public override string GetStringOverride(string key)
        {
            switch (key)
            {
                case "GridViewSearchPanelTopText":
                    return "Поиск";
                case "GridViewGroupPanelText":
                    return "Переместите сюда заголовок столбца, чтобы сгруппировать по нему.";
                case "GridViewClearFilter":
                    return "Очистить фильтр";
                case "GridViewFilterShowRowsWithValueThat":
                    return "Показать строки со значением, которое";
                case "GridViewFilterSelectAll":
                    return "Выбрать все";
                case "GridViewFilterContains":
                    return "Содержит";
                case "GridViewFilterDoesNotContain":
                    return "Не содержит";
                case "GridViewFilterEndsWith":
                    return "Заканчивается на";
                case "GridViewFilterIsContainedIn":
                    return "Содержится в";
                case "GridViewFilterIsNotContainedIn":
                    return "Не содержится в";
                case "GridViewFilterIsEqualTo":
                    return "Равно";
                case "GridViewFilterIsGreaterThan":
                    return "Больше";
                case "GridViewFilterIsGreaterThanOrEqualTo":
                    return "Больше или равно";
                case "GridViewFilterIsLessThan":
                    return "Меньше";
                case "GridViewFilterIsLessThanOrEqualTo":
                    return "Меньше или равно";
                case "GridViewFilterIsNotEqualTo":
                    return "Не равно";
                case "GridViewFilterStartsWith":
                    return "Начинается с";
                case "GridViewFilterAnd":
                    return "И";
                case "GridViewFilterOr":
                    return "Или";
                case "GridViewFilterIsNull":
                    return "Есть null";
                case "GridViewFilterIsNotNull":
                    return "Не есть null";
                case "GridViewFilterIsEmpty":
                    return "Пустое";
                case "GridViewFilterIsNotEmpty":
                    return "Не пустое";
                case "GridViewFilterDistinctValueNull":
                    return "(Нет)";
                case "GridViewFilter":
                    return "Фильтр";
                case "RadDataPagerPage":
                    return "Страница";
                case "RadDataPagerOf":
                    return "из";
            }

            return base.GetStringOverride(key);
        }
    }
}
