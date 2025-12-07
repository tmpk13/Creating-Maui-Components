using Microsoft.Maui.Controls.Shapes;
using static APPNAME.Resources.Components.ResourceWrapper.ResourceExtensions;

using APPNAME.Resources.Components.Animations;

namespace APPNAME.Resources.Components.Buttons;

[ContentProperty(nameof(DropdownContent))]
public class Dropdown : VerticalStackLayout
{
    private View _content_view;
    private VerticalStackLayout _stack;
    private Border _border;
    private Label _title;
    private GraphicsView _graphicsView;
    private VFlattenDrawable _drawable;

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text), 
            typeof(string), 
            typeof(Dropdown), 
            string.Empty);
    
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public static readonly BindableProperty DropdownContentProperty =
        BindableProperty.Create(
            nameof(DropdownContent),
            typeof(View),
            typeof(Dropdown),
            propertyChanged: OnContentChanged);
    
    public View DropdownContent
    {
        get => (View)GetValue(DropdownContentProperty);
        set => SetValue(DropdownContentProperty, value);
    }

    private static void OnContentChanged(BindableObject bindable, object old_val, object new_val)
    {
        var dropdown = (Dropdown)bindable;
        if (new_val is View view)
        {
            view.IsVisible = false;
            view.ScaleY = 0.5;
            dropdown._content_view = view;
            dropdown._stack.Add(view);
        }
    }
    
    public Dropdown()
    {
        Spacing = 10;
        Padding = 20;

        _title = new Label
        {
            Style = GetResource<Style>("DropdownButtonText")
        };
        _title.SetBinding(Label.TextProperty, new Binding(nameof(Text), source: this));

        _graphicsView = new GraphicsView 
        { 
            HeightRequest = 20,
            WidthRequest = 20,
            BackgroundColor = Colors.Transparent,
            Style = GetResource<Style>("DropdownButtonCanvas")
        };
        _drawable = new VFlattenDrawable();
        _graphicsView.Drawable = _drawable;

        var dropdownButton = new AbsoluteLayout
        {};
        
        dropdownButton.Children.Add(_graphicsView);
        dropdownButton.Children.Add(_title);

        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += OnToggleClicked;
        
        dropdownButton.GestureRecognizers.Add(tapGestureRecognizer);
        
        var button = new Button
        {
            BackgroundColor = Color.FromArgb("#FFFF"),
            TextColor = GetResource<Color>("BasicBlue"),
            Padding = 0
        };

        _stack = new VerticalStackLayout
        {
            Margin = 0,
            Padding = 0,
        };

        _stack.Add(dropdownButton);

        var StrokeShape = new RoundRectangle
        {
            CornerRadius = new CornerRadius(10, 10, 10, 10)
        };

        _border = new Border
        {
            BackgroundColor = Color.FromArgb("#FFFF"),
            Stroke = GetResource<Color>("Light-Border-Text-Input-Box"),
            StrokeThickness = 1,
            Content = _stack,
            StrokeShape = StrokeShape, 
            Padding = 10,
            ScaleY = 1.0
        };
        
        button.SetBinding(Button.TextProperty, new Binding(nameof(Text), source: this));
        button.Clicked += OnToggleClicked;
        
        Children.Add(_border);
    }

    void AnimateIndicator(float from, float to)
    {
        var animation = new Animation(v => 
        {
            _drawable.Progress = (float)v;
            _graphicsView.Invalidate();
        }, from, to);
        animation.Commit(this, "VFlatten", 16, 400, easing: Easing.CubicInOut);
    }
    
    private void OnToggleClicked(object? sender, EventArgs e)
    {
        if ( _content_view != null )
        {
            _content_view.IsVisible = !_content_view.IsVisible;
            if ( _content_view.IsVisible ) 
            {
                AnimateIndicator(0, 1);

                _title.FadeTo(0.5, 400, Easing.SpringOut);
                _content_view.ScaleYTo(1.0, 400, Easing.SpringOut);
                _content_view.FadeTo(1.0, 400, Easing.CubicOut);
            } else
            {
                AnimateIndicator(1, 0);

                _title.FadeTo(1.0, 400, Easing.SpringOut);
                _content_view.FadeTo(0.0, 400, Easing.CubicOut);
                _content_view.ScaleYTo(0.5, 400, Easing.SpringOut);
            }
        }
    }
}
