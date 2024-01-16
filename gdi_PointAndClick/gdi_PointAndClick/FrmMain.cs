using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy; // benötigt für Listen

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
                Random randomColor = new Random();
                return Color.FromArgb(randomColor.Next(256), randomColor.Next(256), randomColor.Next(256));

            }

            for (int i = 0; i < rectangles.Count; i++)
            {
                g.FillRectangle(b, rectangles[i]);
            }

        }

        private void FrmMain_MouseClick(object sender, MouseEventArgs e)
        {
            Random rZ = new Random();
            int rZahl = rZ.Next(1, 60);

            Point mausposition = e.Location;

            bool viereckTreffer = rectangles.Any(rect => rect.Contains(mausposition));
            bool viereckExestiert = rectangles.Any(rect => rect.Contains(mausposition)); // verwirrt mich weniger zwei zu machen 

            if (!viereckTreffer)
            {
                Rectangle r = new Rectangle(mausposition.X, mausposition.Y, rZahl, rZahl);

                rectangles.Add(r);  // Kurze Variante: rectangles.Add( new Rectangle(...)  );

                Refresh();
            }
            else if(viereckExestiert) //wenn an der stelle ein viereck exestiert soll es gelöscht werden (nicht sicher obs auch as der liste gelöscht wird) 
            {
                RemoveRectangleAtPosition(mausposition);
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

        //Viereckt aus FOrm Main.cs Löschen sowie aus der Rectangle Liste
        private void RemoveRectangleAtPosition(Point mausposition)
        {
            Rectangle rectangleToRemove = rectangles.FirstOrDefault(rect => rect.Contains(mausposition));

            if (rectangleToRemove != Rectangle.Empty)
            {
                rectangles.Remove(rectangleToRemove);
            }
        }


    }
}