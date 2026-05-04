using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class ModernSlider : Control
{
    // Proprietăți pentru personalizare
    public int Minimum { get; set; } = 0;
    public int Maximum { get; set; } = 100;
    private int _value = 50;
    public int Value
    {
        get => _value;
        set { _value = Math.Max(Minimum, Math.Min(Maximum, value)); Invalidate(); }
    }

    public Color ProgressColor { get; set; } = Color.FromArgb(30, 215, 96); // Verde tip Spotify
    public Color TrackColor { get; set; } = Color.FromArgb(80, 80, 80);    // Gri închis
    
    public event EventHandler ValueChanged;

    public ModernSlider()
    {
        this.DoubleBuffered = true; // Previne pâlpâirea (flicker)
        this.Height = 20;
        this.Cursor = Cursors.Hand;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        // Calculăm poziția în funcție de valoare
        float percent = (float)(Value - Minimum) / (Maximum - Minimum);
        int thumbPosition = (int)(percent * (Width - 10));

        // 1. Desenăm fundalul barei (linia gri)
        using (GraphicsPath path = RoundedRect(new Rectangle(5, Height / 2 - 2, Width - 10, 4), 2))
        {
            g.FillPath(new SolidBrush(TrackColor), path);
        }

        // 2. Desenăm progresul (linia colorată)
        if (thumbPosition > 0)
        {
            using (GraphicsPath path = RoundedRect(new Rectangle(5, Height / 2 - 2, thumbPosition, 4), 2))
            {
                g.FillPath(new SolidBrush(ProgressColor), path);
            }
        }

        // 3. Desenăm butonul (Thumb) - un cerc alb curat
        g.FillEllipse(Brushes.White, thumbPosition, Height / 2 - 6, 12, 12);
    }

    // Funcție helper pentru colțuri rotunjite
    private GraphicsPath RoundedRect(Rectangle bounds, int radius)
    {
        int diameter = radius * 2;
        Size size = new Size(diameter, diameter);
        Rectangle arc = new Rectangle(bounds.Location, size);
        GraphicsPath path = new GraphicsPath();

        path.AddArc(arc, 180, 90);
        arc.X = bounds.Right - diameter;
        path.AddArc(arc, 270, 90);
        arc.Y = bounds.Bottom - diameter;
        path.AddArc(arc, 0, 90);
        arc.X = bounds.Left;
        path.AddArc(arc, 90, 90);
        path.CloseFigure();
        return path;
    }

    // Gestionarea mouse-ului pentru a schimba valoarea
    protected override void OnMouseDown(MouseEventArgs e) { UpdateValue(e.X); base.OnMouseDown(e); }
    protected override void OnMouseMove(MouseEventArgs e) { if (e.Button == MouseButtons.Left) UpdateValue(e.X); base.OnMouseMove(e); }

    private void UpdateValue(int x)
    {
        float percent = (float)(x - 5) / (Width - 10);
        Value = Minimum + (int)(percent * (Maximum - Minimum));
        ValueChanged?.Invoke(this, EventArgs.Empty);
    }
}