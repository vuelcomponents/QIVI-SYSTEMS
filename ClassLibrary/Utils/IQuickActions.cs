namespace ClassLibrary.Utils;

public interface IQuickActions
{
    T QuickUpdate<DT, T>(DT updateDto, T updateModel) where DT : class where T : class;
    T QuickUpdate<DT, T>(DT updateDto, T updateModel, List<string> excluded) where DT : class where T : class;
}