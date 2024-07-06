using System.Reflection;

namespace ClassLibrary.Utils;

public sealed class QuickActions : IQuickActions
{
    private static readonly Lazy<IQuickActions> QuickActionResolver = new(() => new QuickActions());
    public static IQuickActions Instance => QuickActionResolver.Value;

    private QuickActions()
    {

    }
    public T QuickUpdate<TDt, T>(TDt updateDto, T updateModel) where TDt : class where T : class
    {
        PropertyInfo[] propertiesDto = typeof(TDt).GetProperties();
        PropertyInfo[] propertiesModel = typeof(T).GetProperties();
        foreach (PropertyInfo propertyDto in propertiesDto)
        {
            foreach (PropertyInfo propertyModel in propertiesModel)
            {
                if (propertyDto.Name == propertyModel.Name)
                {
                    var value = propertyDto.GetValue(updateDto);
                    if (value != null)
                    {
                        propertyModel.SetValue(updateModel, value);
                    }
                }
            }
        }
        return updateModel;
    }
    public T QuickUpdate<TDt, T>(TDt updateDto, T updateModel, List<string> excluded) where TDt : class where T : class
    {
        PropertyInfo[] propertiesDto = typeof(TDt).GetProperties();
        PropertyInfo[] propertiesModel = typeof(T).GetProperties();
        foreach (PropertyInfo propertyDto in propertiesDto)
        {
            foreach (PropertyInfo propertyModel in propertiesModel)
            {
                if (propertyDto.Name == propertyModel.Name && !excluded.Any(e => e.Equals(propertyModel.Name)))
                {
                    var value = propertyDto.GetValue(updateDto);
                    if (value != null)
                    {
                        propertyModel.SetValue(updateModel, value);
                    }
                }
            }
        }
        return updateModel;
    }
}