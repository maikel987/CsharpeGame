using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace App8
{
    class Pills : AvatarEnemies
    {
        
        public Pills(int x, int y, double size, int moveX, int moveY) : base(x, y, size, moveX, moveY)
        {
            x = Math.Min(Math.Max(x, 10), XMax - 10);
            y = Math.Min(Math.Max(y, 10), YMax - 10);
        }

        public override void MovePill(Avatar play)
        {
            x = Math.Max(Math.Min(x + FollowX(play), XMax - 5 - (int)size), 5);
            y = Math.Max(Math.Min(y + FollowY(play), YMax - 5 - (int)size), 5);
        }

        public int FollowX(Avatar play)
        {
            if (play.x+play.size/2 > x+size/2)
            {
                moveX = Math.Abs(moveX);
            }else
            {
                moveX = -Math.Abs(moveX);
            }
            int fact = 1;
            if (size <= play.size) fact = -1;
            return moveX*fact;
        }

        public int FollowY(Avatar play)
        {
            if (play.y+play.size/2 > y+size/2)
            {
                moveY = Math.Abs(moveY);
            }
            else
            {
                moveY = -Math.Abs(moveY);
            }
            int fact = 1;
            if (size <= play.size) fact = -1;
            return moveY*fact;
        }

        public override Ellipse CreateEllipseFromEnemies(Player play)
        {
            Ellipse ell = new Ellipse();
            ell.Height = ell.Width = size;

            Canvas.SetLeft(ell, x);
            Canvas.SetTop(ell, y);
            
            ell.Fill = AffectImage(play);

            return ell;
        }

        public override ImageBrush AffectImage(Avatar player)
        {
            ImageBrush playerImg = new ImageBrush();

            if (size < player.size)
            {
                playerImg.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/OrangePills.jpg"));
            }
            else if (size > player.size)
            {
                playerImg.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/PillsRed.jpg"));
            }
            else
            {
                playerImg.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/OrangePills.jpg"));
            }
            return playerImg;
        }





    }
}
