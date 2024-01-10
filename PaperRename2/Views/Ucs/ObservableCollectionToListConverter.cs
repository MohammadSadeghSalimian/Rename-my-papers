using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;

namespace PaperRename2.Views.Ucs;

public class ObservableCollectionToListConverter : IBindingTypeConverter
{
    public int GetAffinityForObjects(Type fromType, Type toType)
    {
        if (fromType == typeof(ObservableCollection<string>) && toType == typeof(IList))
            return 100;

        if (fromType == typeof(IList) && toType == typeof(ObservableCollection<string>))
            return 100;

        return 0;
    }

    public bool TryConvert(object from, Type toType, object conversionHint, out object result)
    {
        switch (from)
        {
            case ObservableCollection<string> observableCollection when toType == typeof(IList):
                result = observableCollection;
                return true;
            case IList list when toType == typeof(ObservableCollection<string>):
                result = new ObservableCollection<string>(list.Cast<string>());
                return true;
            default:
                result = null;
                return false;
        }
    }
}