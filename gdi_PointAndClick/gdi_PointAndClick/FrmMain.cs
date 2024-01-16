using System.Collections.Generic; // benötigt für Listen

namespace gdi_PointAndClick
{
    public partial class FrmMain : Form
    {
        List<Rectangle> rectangles = new List<Rectangle>();

        public FrmMain()
        {
            InitializeComponent();
            ResizeRedraw = true;
        }

        private void FrmMain_Paint(object sender, PaintEventArgs e)
        {
            // Hilfsvarablen
            Graphics g = e.Graphics;
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            // Zeichenmittel

            Color newRandomColor = GetRandomColor();
            Brush b = new SolidBrush(newRandomColor);

            static Color GetRandomColor()
            {
                Random random = new Random();
                return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            }


            for (int i = 0; i < rectangles.Count; i++)
            {
                g.FillRectangle(b, rectangles[i]);
            }

        }

        private void FrmMain_MouseClick(object sender, MouseEventArgs e)
        {
            Point mausposition = e.Location;

            bool viereckTreffer = rectangles.Any(rect => rect.Contains(mausposition));

            if (!viereckTreffer)
            {
                Rectangle r = new Rectangle(mausposition.X, mausposition.Y, 40, 40);

                rectangles.Add(r);  // Kurze Variante: rectangles.Add( new Rectangle(...)  );

                Refresh();
            }

          
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                rectangles.Clear();
                Refresh();
            }
        }
    }
}