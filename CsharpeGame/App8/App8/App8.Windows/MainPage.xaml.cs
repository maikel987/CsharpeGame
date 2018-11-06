using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MainPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer(); 
        public MainPage()
        {
            this.InitializeComponent();
            
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            
            timer.Interval = new TimeSpan(0, 0, 0, 0,50);
            timer.Tick += timer_Tick;

        }


        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.Left:
                    Canvas.SetLeft(placement.ellList[0], Math.Max(0, placement.GetX(0) - 10));
                    placement.SetX(Math.Max(0, placement.GetX(0) - 10), 0);
                    placement.SetY(placement.GetY(0),0);
                    break;
                case Windows.System.VirtualKey.Up:
                    Canvas.SetTop(placement.ellList[0], Math.Max(0, placement.GetY(0) - 10));
                    placement.SetY(Math.Max(0, placement.GetY(0) - 10), 0);
                    placement.SetX(placement.GetX(0), 0);
                    break;
                case Windows.System.VirtualKey.Right:
                    Canvas.SetLeft(placement.ellList[0], Math.Min(cnv.Width - placement.GetSize(0), placement.GetX(0) + 10));
                    placement.SetX((int)Math.Min(cnv.Width - placement.GetSize(0), placement.GetX(0) + 10), 0);
                    placement.SetY(placement.GetY(0), 0);
                    break;
                case Windows.System.VirtualKey.Down:
                    Canvas.SetTop(placement.ellList[0], Math.Min(cnv.Height - placement.GetSize(0), placement.GetY(0) + 10));
                    placement.SetY((int)Math.Min(cnv.Height - placement.GetSize(0), placement.GetY(0) + 10), 0);
                    placement.SetX(placement.GetX(0), 0);
                    break;
                default:
                    break;
            }
        }

        private void timer_Tick(object sender, object e)
        {
            Run();
        }

        Random ran = new Random();
        Item placement = new Item();

        public void Initialisation(int celerity)
        {
            Player player = new Player();

            placement.AddAvatar(player);

            for (int i = 0; i < 80; i++) //Enemies Placement
            {
                AvatarEnemies ava = new AvatarEnemies (ran.Next(425)+800*ran.Next(0,2), ran.Next(668),Math.Max(7,ran.Next(-10,46)),((ran.Next(0,2)*2)-1)* celerity, ((ran.Next(0, 2) * 2) - 1)* celerity);
                placement.AddAvatar(ava);
            }

            for (int i = 0; i < 2*(level-1); i++) //Pills Placement
            {
                Pills ava = new Pills(ran.Next(420, 780), ran.Next(200) + 468 * ran.Next(0, 2), ran.Next(15, 51), ((ran.Next(0, 2) * 2) - 1) * (celerity+level/3), ((ran.Next(0, 2) * 2) - 1) * (celerity+level/3));
                placement.AddAvatar(ava);
            }
            
            placement.AffectEllipse(player); //building the Ellipse
            for (int i = 0; i < placement.ellList.Count; i++)
            {
                cnv.Children.Add(placement.ellList[i]); // show the ellipse on the Canvas
            }
        }

       public void Run()
        {
            for (int i = 0; i < placement.count; i++) //moving the different Avatars on the Canvas
            {
                    Canvas.SetTop(placement.ellList[i], placement.GetY(i));
                    Canvas.SetLeft(placement.ellList[i], placement.GetX(i));              
            }
            placement.Move(); //moving the Avatars
            placement.SizeChangetAfterTouch(); //change the size if avatar touch between them
            EndGame(); 
        }

        int level = 1;
        private void button_Click(object sender, RoutedEventArgs e)
        {

            if (placement.count > 0)
            {
                placement.CleanBeforeStart(); //erase the data of last Game
                List<Ellipse> Elist = cnv.Children.OfType<Ellipse>().ToList();
                for (int i = 0; i < Elist.Count; i++)
                {
                    cnv.Children.Remove(Elist[i]); // clean the Canvas
                }
                if (level==2)
                {   //Clean Canvas of button text and image
                    rect.Visibility = Visibility.Collapsed;
                    image5.Opacity = 0;
                    image4.Opacity = 0;
                    textBlock3.Opacity = 0;
                    textBlock5.Opacity = 0;
                }
            }
            else
            {   //Clean Canvas of button text and image
                textBlock.Opacity = 0;
                textBlock1.Opacity = 0;
                textBlock2.Opacity = 0;
                textBlock4.Opacity = 0;

                image.Opacity = 0;
                image1.Opacity = 0;
                image2.Opacity = 0;
                image3.Opacity = 0;
            }
            Initialisation(level); //initialise the new game

            timer.Start();
            button.Opacity = 0; // hide the button
            button.ClickMode = ClickMode.Hover;
        }

        int life = 3;
        private void EndGame()
        {   // as the player have been initialised first in the list, if 1st in list isn't a player that mean the player lost.
            if (placement.IsPlayer(0)) 
            {
                if (placement.ellList.Count == 1) //if player is the last -> he wins
                {
                    level++;
                    timer.Stop();
                    YouWin();
                    NewGame();
                }
                else if (placement.count <= 10)
                {
                    bool areSmallerAvatar = false;
                    double sizeSumAvatar = 0;
                    bool areSmaller = false;
                    double sizeSum = 0;
                    for (int i = 1; i < placement.count; i++)
                    {
                        sizeSum+= placement.GetSize(i); // calculate the total Size of the enemies                        
                        areSmaller = areSmaller || (placement.GetSize(i) <= placement.GetSize(0)); //verify if there are ennemies smaller than the player

                        if (!placement.IsPills(i))
                        {                            
                            sizeSumAvatar += placement.GetSize(i); // calculate the total Size of the enemies                        
                            areSmallerAvatar = areSmallerAvatar || (placement.GetSize(i) <= placement.GetSize(0)); //verify if there are ennemies smaller than the player
                        }
                    }
                    if (placement.GetSize(0) > sizeSum * 2)// if you are bigger than 2 times the size of all ennemies player win
                    {
                        level++;
                        timer.Stop();
                        YouWin();
                        NewGame();                        
                    }
                    else if(!areSmallerAvatar) //if you are the smaller, you can't win 
                    {
                        timer.Stop();
                        TooSmall();
                        NewGame();
                    }
                }
            }
            else
            {
                timer.Stop();
                if (life > 0) //as you have been eaten you lost a life
                {
                    life--;
                    YouLoose();
                }
                else // no more life, you loose a level if you are at a level > 1
                {
                    if (level > 1)level--;
                    life = 3;
                    GameOver();
                }               
                NewGame();
            }
        }


        private async void YouWin()
        {
            var messageDialog = new MessageDialog("You Won :) Next Level. ");
            messageDialog.Content += ("you have " + life.ToString() + " life");
            if(life>1)messageDialog.Content += ("s");
            await messageDialog.ShowAsync();
        }

        private async void YouLoose()
        {
            var messageDialog = new MessageDialog("Sorry, You lost!  ");
            messageDialog.Content += ("you have "+ life.ToString()+" more life");
            if (life > 1) messageDialog.Content += ("s");
            await messageDialog.ShowAsync();
        }

        private async void GameOver()
        {
            var messageDialog = new MessageDialog("GAME OVER ");
            if (level > 1) messageDialog.Content += ("Try the level bellow..");
            await messageDialog.ShowAsync();
        }

        private async void TooSmall()
        {
            var messageDialog = new MessageDialog("You are the smaller, you can't win.. Try again this level! ");
            messageDialog.Content += ("you have " + life.ToString() + " life");
            if (life > 1) messageDialog.Content += ("s");
            await messageDialog.ShowAsync();
        }

        private void NewGame()    //As a new game begin, we make appear needed information and button to the player
        {
            button.Opacity = 100;
            button.ClickMode = ClickMode.Release;
            button.Content = "START - Level " + level.ToString();
         

            if (level == 2)
            {
                rect.Visibility = Visibility.Visible;
                image5.Opacity = 1;
                image4.Opacity = 1;
                textBlock3.Opacity = 1;
                textBlock5.Opacity = 1;
            }
        }



    }
}
