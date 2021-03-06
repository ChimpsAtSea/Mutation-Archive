using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using Microsoft.DirectX;

namespace Xapien
{
    public class LargeButton : Control
    {
        public string Text { get; set; }

        Sprite Background;
        Texture Normal, Hover, Clicked;
        Microsoft.DirectX.Direct3D.Font font;

        public LargeButton() 
        {
            Text = "";
        }
        public LargeButton(string Text)
        {
            this.Text = Text;
        }

        public override void Initialize(Device device)
        {
            // Initialize the Sprite
            Background = new Sprite(device);
            Normal = Texture.FromBitmap(device, Xapien.Properties.Resources.LargeButton, Usage.None, Pool.Managed);
            Hover = Texture.FromBitmap(device, Xapien.Properties.Resources.LargeButtonOver, Usage.None, Pool.Managed);
            Clicked = Texture.FromBitmap(device, Xapien.Properties.Resources.LargeButtonClick, Usage.None, Pool.Managed);
            font = new Microsoft.DirectX.Direct3D.Font(device, new System.Drawing.Font(FontFamily.GenericSerif, 12.0f, FontStyle.Regular));

            // Initialize Size
            Size = new Size(200, 24);

            // Initialize Base
            base.Initialize(device);
        }

        public override void Render(Device device)
        {
            if (Initialized)
            {
                // Draw the Panel
                Background.Begin(SpriteFlags.AlphaBlend);
                if (IsClicked)
                    Background.Draw(Clicked, new Rectangle(0, 0, Size.Width, Size.Height), Vector3.Empty, new Vector3((int)(Position.X + (Parent != null ? Parent.Position.X : 0)), (int)(Position.Y + (Parent != null ? Parent.Position.Y : 0)), 0), BitConverter.ToInt32(new byte[] { 0xFF, 0xFF, 0xFF, (byte)Opacity }, 0));
                else if (IsMouseOver)
                    Background.Draw(Hover, new Rectangle(0, 0, Size.Width, Size.Height), Vector3.Empty, new Vector3((int)(Position.X + (Parent != null ? Parent.Position.X : 0)), (int)(Position.Y + (Parent != null ? Parent.Position.Y : 0)), 0), BitConverter.ToInt32(new byte[] { 0xFF, 0xFF, 0xFF, (byte)Opacity }, 0));
                else
                    Background.Draw(Normal, new Rectangle(0, 0, Size.Width, Size.Height), Vector3.Empty, new Vector3((int)(Position.X + (Parent != null ? Parent.Position.X : 0)), (int)(Position.Y + (Parent != null ? Parent.Position.Y : 0)), 0), BitConverter.ToInt32(new byte[] { 0xFF, 0xFF, 0xFF, (byte)Opacity }, 0));

                // Center and Draw Text
                int y = ((150 - (Text.Length * 2)) / 2) - 19;
                font.DrawText(Background, Text, new Point((int)(Position.X + (Parent != null ? Parent.Position.X : 0)) + y, (int)(Position.Y + (Parent != null ? Parent.Position.Y : 0)) + 3), Color.Black);
                Background.End();
            }
        }
    }
}
