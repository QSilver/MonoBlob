using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MonoBlob
{
    class Button
    {
        float x;
        float y;
        float size = 30;

        public Button(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(new Vector2(this.x, this.y), new Vector2(size*2, size), Color.Red);
        }
    }
}
