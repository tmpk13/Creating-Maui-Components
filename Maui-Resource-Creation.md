# Maui Resource Creation

Quick guide on making components

Replace the names `<THING-NAME>` with your specifics

This assumes you have a Directory structure simular to `<APPNAME>/Resources/Components`
```
<APPNAME>/
├── Resources/
│   ├── Components/
│   │   └── <NewComponents>/
│   │       ├── <NewComponent>.xaml
│   │       └── <NewComponent>.xaml.cs
│   └── Styles/
│       ├── Colors.xaml
│       └── Styles.xaml
└── App.xaml
```

## Style

Project wide styles are defined in `App.xaml` 
``` XML
<Application ...
 ...
<Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Resources/Styles/Colors.xaml" />
                <ResourceDictionary Source="Resources/Styles/Styles.xaml" />
 ...
```

### Colors

In `Colors.xaml` the color is defined in the project namespace `xmlns:local="clr-namespace:<APPNAME>"`
``` XML
<ResourceDictionary>
    <Color x:Key="Basic-Blue">#0000FF</Color>
```

### Styles

In `Styles.xaml` the style is defined in the project namespace `xmlns:local="clr-namespace:<APPNAME>"`
``` XML
<ResourceDictionary>
    <Style x:Key="newLabelStyle" TargetType="Label">
        <Setter Property="HorizontalOptions" Value="Center" />
        <Setter Property="VerticalOptions" Value="Center" />
        <Setter Property="FontSize" Value="18" />
    </Style>

```

### Usage

#### Which are then accessable in the entire project

``` XML
<TAGNAME ...
    Color="{StaticResource Basic-Blue}"
    BackgroundColor="{StaticResource Basic-Blue}"
    FontColor="{StaticResource Basic-Blue}"
    Stroke="{StaticResource Basic-Blue}"
    ... >
```

``` XML
<Label ...
    Style="{StaticResource newLabelStyle}"
    ... >
```

## Making a component

### Link 

Add your component to the Application .csproj

#### XAML

For a XAML view located at `<PROJECT-DIR>/Resources/Components/<NewComponents>/<NewComponent>.xaml`

``` XML
<Project ...
 ...
    <ItemGroup>
        <MauiXaml Update="Resources\Components\<NewComponents>\<NewComponent>.xaml">
            <SubType>Designer</SubType>
        </MauiXaml>
 ...
```

#### C#

C# will automatically be included no need to link

## Component Code

### For a new component based off of a ContentView

#### C#
``` C#
namespace <APPNAME>.Resources.Components.<NewComponents>;
public class <NewComponent> : ContentView <-- This is the type to derive from
{
    public static readonly BindableProperty <NewPropertyName> =
        BindableProperty.Create(nameof(<NewProperty>), typeof(<NewPropertyType>), typeof(<NewComponent>), defaultValue: <DefaultValue>);

   public <NewPropertyType> <NewProperty>
   {
       get => (<NewPropertyType>)GetValue(<NewPropertyName>);
       set => SetValue(<NewPropertyName>, value);
   }
    ...

    public <NewComponent>()
    {
        Button button = new Button
        {
            Text = "A button",
            BackgroundColor = Application.Current.Resources["Basic-Blue"] as Color ?? Colors.Transparent
        };
    }

    ...

}
```

#### XAML
``` XML
<?xml version="1.0" encoding="utf-8" ?>
<ContentView xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="<APPNAME>.Resources.Components.<NewComponents>.<NewComponent>">
    
    ...

    <Button Text = "A button" BackgroundColor="{StaticResource <COLOR>}" />

    ...
    
</ContentView>
```

## Usage

#### C#


#### XAML
``` XML
<ContentPage ...
    xmlns:newcomponents="clr-namespace:<APPNAME>.Resources.Components.<NewComponents>">

    <!--
    Usage examples:
         Color property: <NewProperty>="#FF0000"
         String property: <NewProperty>="Hello World"
         Number property: <NewProperty>="42"
    -->

    <newcomponents:<NewComponent> <NewProperty>="<value>" />

</ContentPage>
```

The XML namespace `xmlns` for the components ( example: "newcomponents" ) is arbitrary you can name that basically whatever name you want.
newcomponents, components, buttons ...

The XML namespace is then set to: 
`xmlns:<componentnamespace>="clr-namespace:<APPNAME>.Resources.Components.<NewComponents>">`.

If your component is at: `<APPNAME>/Resources/Components/<NewComponents>/<NewComponent>.cs`
Then use namespace: `<APPNAME>.Resources.Components.<NewComponents>`

# Examples

### Navigation service in `<APPNAME>/Resources/Components/Navigation/RouteNavigation.cs`

``` C#
namespace <APPNAME>.Resources.Components.Navigation;
public static class OnTapGestureGoToRoute
{
    // Route that will be navigated to
    private static readonly BindableProperty RouteProperty =
        BindableProperty.CreateAttached(
            "Route", 
            typeof(string), 
            typeof(OnTapGestureGoToRoute),
            null,
            propertyChanged: OnRouteChanged);
    
    // Public facing function for navigating to the element passed in
    // Example usage: border.TapToRoute(this, nameof(Route));
    // Route is the string of the view "<View>" difined in AppShell.xaml.cs
    // Routing.RegisterRoute(nameof(<View>), typeof(<View>));
    public static void TapToRoute(this View element, BindableObject source, string propertyName)
    {
        element.SetBinding(RouteProperty, new Binding(propertyName, source: source));
    }
    
    private static void OnRouteChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not View view) return;
        
        view.GestureRecognizers.Clear();
        
        if (newValue is string route && !string.IsNullOrEmpty(route))
        {
            // Setup and add the Tap Gesture to the passed in element
            var tapGesture = new TapGestureRecognizer();
            // When gesture is triggered navigate to page
            tapGesture.Tapped += async (s, e) => await Shell.Current.GoToAsync(route);
            view.GestureRecognizers.Add(tapGesture);
        }
    }
}
```


``` C#

```
