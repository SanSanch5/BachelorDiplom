using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BachelorLibAPI.Forms
{
    public sealed partial class AngleSelector : UserControl
    {
        private int _angle;

        private Rectangle _drawRegion;
        private Point _origin;

        public AngleSelector()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void AngleSelector_Load(object sender, EventArgs e)
        {
            SetDrawRegion();
        }

        private void AngleSelector_SizeChanged(object sender, EventArgs e)
        {
            Height = Width; //Keep it a square
            SetDrawRegion();
        }

        private void SetDrawRegion()
        {
            _drawRegion = new Rectangle(0, 0, Width, Height);
            _drawRegion.X += 2;
            _drawRegion.Y += 2;
            _drawRegion.Width -= 4;
            _drawRegion.Height -= 4;

            const int offset = 2;
            _origin = new Point(_drawRegion.Width / 2 + offset, _drawRegion.Height / 2 + offset);

            Refresh();
        }

        public int Angle
        {
            get { return _angle; }
            private set
            {
                _angle = value;

                if (!DesignMode && AngleChanged != null)
                    AngleChanged(); //Raise event

                Refresh();
            }
        }

        public delegate void AngleChangedDelegate();
        public event AngleChangedDelegate AngleChanged;

        private static PointF DegreesToXy(float degrees, float radius, Point origin)
        {
            var xy = new PointF();
            var radians = degrees * Math.PI / 180.0;

            xy.X = (float)Math.Cos(radians) * radius + origin.X;
            xy.Y = (float)Math.Sin(-radians) * radius + origin.Y;

            return xy;
        }

        private static float XyToDegrees(Point xy, Point origin)
        {
            var angle = 0.0;

            if (xy.Y < origin.Y)
            {
                if (xy.X > origin.X)
                {
                    angle = (xy.X - origin.X) / (double)(origin.Y - xy.Y);
                    angle = Math.Atan(angle);
                    angle = 90.0 - angle * 180.0 / Math.PI;
                }
                else if (xy.X < origin.X)
                {
                    angle = (origin.X - xy.X) / (double)(origin.Y - xy.Y);
                    angle = Math.Atan(-angle);
                    angle = 90.0 - angle * 180.0 / Math.PI;
                }
            }
            else if (xy.Y > origin.Y)
            {
                if (xy.X > origin.X)
                {
                    angle = (xy.X - origin.X) / (double)(xy.Y - origin.Y);
                    angle = Math.Atan(-angle);
                    angle = 270.0 - angle * 180.0 / Math.PI;
                }
                else if (xy.X < origin.X)
                {
                    angle = (origin.X - xy.X) / (double)(xy.Y - origin.Y);
                    angle = Math.Atan(angle);
                    angle = 270.0 - angle * 180.0 / Math.PI;
                }
            }

            if (angle > 180) angle -= 360; //Optional. Keeps values between -180 and 180
            return (float)angle;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            var outline = new Pen(Color.FromArgb(86, 103, 141), 2.0f);
            var fill = new SolidBrush(Color.FromArgb(90, 255, 255, 255));

            PointF anglePoint = DegreesToXy(_angle, _origin.X - 2, _origin);
            Rectangle originSquare = new Rectangle(_origin.X - 1, _origin.Y - 1, 3, 3);

            //Draw
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawEllipse(outline, _drawRegion);
            g.FillEllipse(fill, _drawRegion);
            g.DrawLine(Pens.DarkRed, _origin, anglePoint);

            g.SmoothingMode = SmoothingMode.HighSpeed; //Make the square edges sharp
            g.FillRectangle(Brushes.Black, originSquare);

            fill.Dispose();
            outline.Dispose();

            base.OnPaint(e);
        }

        private void AngleSelector_MouseDown(object sender, MouseEventArgs e)
        {
            var thisAngle = FindNearestAngle(new Point(e.X, e.Y));

            if (thisAngle == -1) return;
            Angle = thisAngle;
            Refresh();
        }

        private void AngleSelector_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right) return;
            var thisAngle = FindNearestAngle(new Point(e.X, e.Y));

            if (thisAngle == -1) return;
            Angle = thisAngle;
            Refresh();
        }

        private int FindNearestAngle(Point mouseXy)
        {
            var thisAngle = (int)XyToDegrees(mouseXy, _origin);
            if (thisAngle != 0)
                return thisAngle;
            return -1;
        }
    }
}
