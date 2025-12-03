# Maui Resource Creation

Quick guide on making components

## Style

Project wide styles are defined in `App.xaml` 
```
<Application ...
 ...
<Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Resources/Styles/Colors.xaml" />
 ...
```

In `Colors.xaml` the color is defined in the project namespace `xmlns:local="clr-namespace:<APPNAME>"`
```
<ResourceDictionary>
    <Color x:Key="Basic-Blue">#0000FF</Color>
```

Which are then accessable in the entire project

``` XML
< ...   Color="{StaticResource Basic-Blue}"
        BackgroundColor="{StaticResource Basic-Blue}"
        FontColor="{StaticResource Basic-Blue}"
        Stroke="{StaticResource Basic-Blue}"
...

```

## Making a component

### Link 

Add your component to the Application .csproj

If you have a .xaml view component you dont need to link the .xaml.cs attached




#### XAML

For a XAML view located in `<PROJECT-DIR>/Components/NewComponents/NewComponent.xaml`

``` XML
<Project ...
 ...
    <ItemGroup>
        <MauiXaml Update="Resources\Components\NewComponents\NewComponent.xaml">
            <SubType>Designer</SubType>
        </MauiXaml>
 ...
```

#### C#

For a C# view located in `<PROJECT-DIR>/Components/NewComponents/NewComponent.cs`

``` XML
<Project ...
 ...
    <ItemGroup>
        <MauiXaml Update="Resources\Components\NewComponents\NewComponent.cs">
            <SubType>Designer</SubType>
        </MauiXaml>
 ...
```

## Component Code

### For a new component based off of a ContentView

#### C#
``` C#
namespace <APPNAME>.Resources.Components.<NewComponents>;
public class <NewComponent> : ContentView <-- This is the type to derive from
{
    public static readonly BindableProperty NewPropertyName =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(<NewComponent>));
    
    ...

    public <NewComponent>()
    {
        Need to fix nullability here

        Button button = new Button
        {
            Text = "A button",
            BackgroundColor = (Style)Application.Current.Resources[key]
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
