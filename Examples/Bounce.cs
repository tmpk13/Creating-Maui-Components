namespace APPNAME.Resources.Components.Animations;
public class Bouncer
{
    public static async Task BounceElement(VisualElement element, float start=1.0f, float end=1.1f, uint length=150)
    {
        await element.ScaleTo(end, length, Easing.SinInOut);
        await element.ScaleTo(start, length, Easing.SinInOut);
    }
}
