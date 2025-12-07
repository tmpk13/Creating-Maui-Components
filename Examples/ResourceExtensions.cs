namespace APPNAME.Resources.Components.ResourceWrapper; 
public static class ResourceExtensions
{
    public static T GetResource<T>(this ResourceDictionary resources, string key)
    {
        if (resources.TryGetValue(key, out object resource) && resource is T typed)
            return typed;
        
        if (Application.Current.Resources.TryGetValue(key, out resource) && resource is T app_typed)
            return app_typed;
        
        return default;
    }

    public static T GetResource<T>(string key)
    {
        if (Application.Current.Resources.TryGetValue(key, out object resource) && resource is T typed)
            return typed;
        
        return default;
    }
}
