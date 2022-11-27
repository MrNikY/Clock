using System.Drawing.Drawing2D;

namespace Clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int SLength = 120;
        int MLength = 100;
        int HLength = 80;
        float MagFactor = 1;
        double size = 420;

        protected override void OnPaint(PaintEventArgs e)
        {
            MagFactor = (float)(Math.Sqrt(Math.Pow(Math.Min(this.Width, this.Height), 2) * 2) /
                        Math.Sqrt(Math.Pow(size, 2) * 2));

            Graphics g = this.CreateGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TranslateTransform(200 * MagFactor, 200 * MagFactor);
            g.Clear(BackColor);

            Pen p = new Pen(Color.Black, 7 * MagFactor);

            for (int i = 0; i < 12; i++)
            {
                g.RotateTransform(30);
                g.DrawLine(p, 0, 0, 0, -150 * MagFactor);
            }

            p.Width = 5;
            for (int i = 0; i < 60; i++)
            {
                g.RotateTransform(6);
                g.DrawLine(p, 0, 0, 0, -135 * MagFactor);
            }

            p.Color = BackColor;
            g.FillEllipse(p.Brush, -130 * MagFactor, -130 * MagFactor, 260 * MagFactor, 260 * MagFactor);
        }
        private void GDI_Resize(object sender, EventArgs e)
        {
            OnPaint(null);
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            Graphics g = this.CreateGraphics();
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TranslateTransform(200 * MagFactor, 200 * MagFactor);

            Pen p = new Pen(BackColor, 3 * MagFactor);
            g.FillEllipse(p.Brush, -130 * MagFactor, -130 * MagFactor, 260 * MagFactor, 260 * MagFactor);
            p.Color = Color.Black;

            g.RotateTransform(-90);
            p.Width = 3;
            g.DrawLine(p, 0, 0,
                (float)(SLength * MagFactor * Math.Cos(Math.PI / 30 * (dateTime.Second + 1))),
                (float)(SLength * MagFactor * Math.Sin(Math.PI / 30 * (dateTime.Second + 1))));

            p.StartCap = LineCap.Round;
            p.EndCap = LineCap.ArrowAnchor;

            p.Width = 5;
            g.DrawLine(p, 0, 0,
                (float)(MLength * MagFactor * Math.Cos(Math.PI / 30 * (dateTime.Minute))),
                (float)(MLength * MagFactor * Math.Sin(Math.PI / 30 * (dateTime.Minute))));

            p.Width = 7;
            g.DrawLine(p, 0, 0,
                (float)(HLength * MagFactor * Math.Cos(Math.PI / 6 * (dateTime.Hour))),
                (float)(HLength * MagFactor * Math.Sin(Math.PI / 6 * (dateTime.Hour))));

        }
    }
}
