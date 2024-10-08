using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace MES_Monitoring.Classes
{
    public static class CustomGraphics
    {
        /// <summary>
        /// </summary>
        public enum RectangleRoundPoint
        {
            TopStart = 1,
            TopEnd = 2,
            BottomStart = 3,
            BottomEnd = 4
        }

        /// <summary>
        ///     사각형 라운드 처리
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="radius"></param>
        /// <param name="roundPoints"></param>
        /// <returns></returns>
        public static GraphicsPath GetRoundedRectanglePath(RectangleF rectangle, int radius,
            RectangleRoundPoint[] roundPoints = null)
        {
            var graphicsPath = new GraphicsPath();
            var diameter = 2 * radius;
            var circleRectangle = new RectangleF(rectangle.Location, new Size(diameter, diameter));
            if (roundPoints == null)
                roundPoints = new[]
                {
                    RectangleRoundPoint.TopStart, RectangleRoundPoint.TopEnd, RectangleRoundPoint.BottomStart,
                    RectangleRoundPoint.BottomEnd
                };

            //왼쪽 위
            if (roundPoints.Contains(RectangleRoundPoint.TopStart))
                graphicsPath.AddArc(circleRectangle, 180, 90);
            else
                graphicsPath.AddLine(rectangle.Left, rectangle.Top, rectangle.Right - radius, rectangle.Top);

            //X 이동
            circleRectangle.X = rectangle.Right - diameter;

            //오른쪽 위
            if (roundPoints.Contains(RectangleRoundPoint.TopEnd))
                graphicsPath.AddArc(circleRectangle, 270, 90);
            else
                graphicsPath.AddLine(new PointF(rectangle.Right, rectangle.Top),
                    new PointF(rectangle.Right, rectangle.Bottom - radius));

            //Y 이동
            circleRectangle.Y = rectangle.Bottom - diameter;

            //오른쪽 아래
            if (roundPoints.Contains(RectangleRoundPoint.BottomEnd))
                graphicsPath.AddArc(circleRectangle, 0, 90);
            else
                graphicsPath.AddLine(new PointF(rectangle.Right, rectangle.Bottom),
                    new PointF(rectangle.Left - radius, rectangle.Bottom));

            //이동
            circleRectangle.X = rectangle.Left;
            //왼쪽 아래
            if (roundPoints.Contains(RectangleRoundPoint.BottomStart))
                graphicsPath.AddArc(circleRectangle, 90, 90);
            else
                graphicsPath.AddLine(new PointF(rectangle.Left, rectangle.Bottom),
                    new PointF(rectangle.Left, rectangle.Top - radius));

            return graphicsPath;
        }
    }
}