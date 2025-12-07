namespace APPNAME.Resources.Components.Animations;
using static APPNAME.Resources.Components.ResourceWrapper.ResourceExtensions;

public class VFlattenDrawable : IDrawable
{
    public float Progress { get; set; } = 0f; 
    public float StrokeWidth { get; set; } = 2f;
    
    public void Draw(ICanvas canvas, RectF bounds)
    {
        canvas.StrokeColor = GetResource<Color>("BasicBlue");
        canvas.StrokeSize = StrokeWidth;
        canvas.StrokeLineCap = LineCap.Round;
        
        float width = bounds.Width > 0 ? bounds.Width : 20f;
        float height = bounds.Height > 0 ? bounds.Height : 20f;
        
        float startX = 0;
        float endX = width;
        float midX = width / 2;
        
        float topY = height / 2 * Progress;
        float bottomY = height;


        float midY = bottomY - ((bottomY - topY) * Progress);

        
        PathF path = new PathF();
        path.MoveTo(startX, topY);
        path.LineTo(midX, midY);
        path.LineTo(endX, topY);
        
        canvas.DrawPath(path);
    }
}
