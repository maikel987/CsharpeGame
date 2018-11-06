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
    class Player : Avatar

    {

        public Player(int x=673, int y=374, double size=25) : base(x, y, size)
        {

        }

        public override Ellipse CreateEllipseFromAvatar()
        {
            Ellipse ell = new Ellipse();
            ell.Height = ell.Width = size;

            Canvas.SetLeft(ell, x);
            Canvas.SetTop(ell, y);

            ImageBrush playerImg = new ImageBrush();
            playerImg.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Player.png"));

            ell.Fill = playerImg;

            return ell;
        }


        

    }
}
