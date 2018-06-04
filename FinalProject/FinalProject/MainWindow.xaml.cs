using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public World world;
        public Player player;
        public Enemy enemy;
        DispatcherTimer mainTimer = new DispatcherTimer();
        MediaPlayer mediaPlayer = new MediaPlayer();
        public Random rand = new Random();
        List<Control> numlist = new List<Control>();
        
        public MainWindow()
        {
            InitializeComponent();
            world = new World(ref numlist, ref battleGrid, ref overworldImg);
            player = new Player("Light", ref playerWorldImg, ref mainMenuArrow, ref apbar, ref world);
            mainTimer.Tick += mainTimer_Tick;
            mainTimer.Interval = new TimeSpan(10000);
            mainTimer.Start();
            mediaPlayer.Open(world.AudtitleUri);
            mediaPlayer.Play();
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            if (player.WalkingUp)
            {
                player.Move(Direction.Up);
            }
            if (player.WalkingDown)
            {
                player.Move(Direction.Down);
            }
            if (player.WalkingLeft)
            {
                player.Move(Direction.Left);
            }
            if (player.WalkingRight)
            {
                player.Move(Direction.Right);
            }
            if (player.InWorld)
            {
                if (player.X >= 739)
                {
                    player.ChangeLocation(1, 0);
                }
                if (player.X <= -2)
                {
                    player.ChangeLocation(-1, 0);
                }
                if (player.Y >= 360)
                {
                    player.ChangeLocation(0, 1);
                }
                if (player.Y <= -2)
                {
                    player.ChangeLocation(0, -1);
                }
            }
            if (player.InBattle)
            {
                badhp.Content = "HP: " + enemy.Hp;
                hplbl.Content = "HP: " + player.Hp;
                if (apbar.Width < 400)
                {
                    apbar.Width += player.Speed;
                }
                MoveNum();
            }
            if(player.InBattle && enemy.Hp <= 0)
            {
                world.RemoveNum(null, true);
                player.InBattle = false; player.InWorld = true;
                battleGrid.Visibility = Visibility.Hidden;
                overworldGrid.Visibility = Visibility.Visible;
                mediaPlayer.Stop();
                mediaPlayer.Open(world.AudWorldUri);
                mediaPlayer.Play();
            }
        }

        public void MoveNum()
        {
            Thickness z;
            for(int i = 0; i < numlist.Count; i++)
            {
                z = numlist[i].Margin;
                numlist[i].Margin = new Thickness(z.Left, z.Top - 1.5, z.Right, z.Bottom);
                if (numlist[i].Margin.Top <= -30)
                {
                    world.RemoveNum(numlist[i], false);
                }
            }
        }

        public void BattleStart()
        {
            enemy = new Enemy(ref enemyImg, world.ImgEnemies);
            overworldGrid.Visibility = Visibility.Hidden;
            battleGrid.Visibility = Visibility.Visible;
            player.InBattle = true;
            mediaPlayer.Stop();
            mediaPlayer.Open(world.AudBattleUri);
            mediaPlayer.Play();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            player.HandleKeyDown(e);
            if(player.InMenu == true && mainMenuArrow.Margin.Top == 275 && e.Key == Key.Z)
            {
                player.InMenu = false; player.InWorld = true;
                startMenuGrid.Visibility = Visibility.Hidden;
                overworldGrid.Visibility = Visibility.Visible;
                mediaPlayer.Stop();
                mediaPlayer.Open(world.AudWorldUri);
                mediaPlayer.Play();
            }
            if(player.InMenu == true && mainMenuArrow.Margin.Top == 345 && e.Key == Key.Z)
            {
                groupBox.Visibility = Visibility.Visible;
            }
            if (e.Key == Key.X)
            {
                if (player.InWorld == true && player.InBattle == false)
                {
                    BattleStart();
                }
            }
            if(e.Key == Key.Z)
            {
                if(player.InBattle == true)
                {
                    player.Attack(ref enemy, ref world);
                }
            }
            if(e.Key == Key.C)
            {

            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            player.HandleKeyUp(e);
        }
    }
}
