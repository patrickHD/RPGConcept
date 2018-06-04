using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using static FinalProject.MainWindow;

namespace FinalProject
{
    public class Player
    {

        public bool WalkingUp { get; set; }
        public bool WalkingDown { get; set; }
        public bool WalkingLeft { get; set; }
        public bool WalkingRight { get; set; }
        public bool InWorld { get; set; }
        public bool InMenu { get; set; }
        public int MenuPos { get; set; }
        public World World { get; set; }
        public Image Img { get; set; }
        Label MenuLbl { get; set; }
        Rectangle Apbar { get; set; }
        public double X
        {
            get => Img.Margin.Left;
            set => Img.Margin = new System.Windows.Thickness(value, Img.Margin.Top, 0, 0);
        }
        public double Y
        {
            get => Img.Margin.Top;
            set => Img.Margin = new System.Windows.Thickness(Img.Margin.Left, value, 0, 0);
        }

        /// <summary>
        /// ^ new V old
        /// </summary>
        //main variables
        public string Name { get; set; }
        public int MaxHp { get; set; }
        public int Level { get; set; }
        public int NextLvlExp { get; set; }
        public int Exp { get; set; }
        public string MainStr { get; set; }
        public string MainStr2 { get; set; }
        public bool InBattle { get; set; }
        public double Speed { get; set; }

        //gear variables
        public Gear Armor { get; set; }
        public Gear Weapon { get; set; }
        public List<Item> Items { get; set; }

        internal void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    Img.Margin = new System.Windows.Thickness(Img.Margin.Left, Img.Margin.Top - 3, Img.Margin.Right, Img.Margin.Bottom);
                    Img.Source = new BitmapImage(new Uri(@"\res\img\player\walking\up.png", UriKind.Relative));
                    break;
                case Direction.Down:
                    Img.Margin = new System.Windows.Thickness(Img.Margin.Left, Img.Margin.Top + 3, Img.Margin.Right, Img.Margin.Bottom);
                    Img.Source = new BitmapImage(new Uri(@"\res\img\player\walking\down.png", UriKind.Relative));
                    break;
                case Direction.Left:
                    Img.Margin = new System.Windows.Thickness(Img.Margin.Left - 3, Img.Margin.Top, Img.Margin.Right, Img.Margin.Bottom);
                    Img.Source = new BitmapImage(new Uri(@"\res\img\player\walking\left.png", UriKind.Relative));
                    break;
                case Direction.Right:
                    Img.Margin = new System.Windows.Thickness(Img.Margin.Left + 3, Img.Margin.Top, Img.Margin.Right, Img.Margin.Bottom);
                    Img.Source = new BitmapImage(new Uri(@"\res\img\player\walking\right.png", UriKind.Relative));
                    break;
            }
        }

        internal void ChangeLocation(int x, int y)
        {
            Debug.WriteLine("MapX:" + MapX + ", MapY:" + MapY);
            Debug.WriteLine("WorldCX:" + World.CurrentX + ", WorldCY:" + World.CurrentY);
            if(MapX >= 2 && x > 0 || MapX <= -2 && x < 0 || MapY >= 2 && x > 0 || MapY <= -2 && x < 0)
            {
                if (x > 0) { X = 700; } if(x < 0) { X = 10; }
                if (y > 0) { Y = 330; } if(y < 0) { Y = 10; }
            }
            else
            {
                World.ChangeMap(x, y, true);
                if (x > 0)
                {
                    X = 10;
                } else if(x < 0)
                {
                    X = 700;
                }
                if(y > 0)
                {
                    Y = 10;
                } else if(y < 0)
                {
                    Y = 340;
                }
            }
        }

        internal void MenuMove(Direction dir)
        {
            if (InMenu == true)
            {
                if (dir == Direction.Up)
                {
                    if (MenuLbl.Margin.Top == 275)
                    {
                        MenuLbl.Margin = new System.Windows.Thickness(0, 380, 134, 0);
                    }                                               
                    if (MenuLbl.Margin.Top == 310)                  
                    {                                               
                        MenuLbl.Margin = new System.Windows.Thickness(0, 275, 134, 0);
                    }                                               
                    if (MenuLbl.Margin.Top == 345)                  
                    {                                               
                        MenuLbl.Margin = new System.Windows.Thickness(0, 310, 134, 0);
                    }                                               
                    if (MenuLbl.Margin.Top == 380)                  
                    {                                               
                        MenuLbl.Margin = new System.Windows.Thickness(0, 345, 134, 0);
                    }
                }
                if (dir == Direction.Down)
                {
                    if (MenuLbl.Margin.Top == 275)
                    {
                        MenuLbl.Margin = new System.Windows.Thickness(0, 310, 134, 0);
                    }
                    if (MenuLbl.Margin.Top == 310)
                    {
                        MenuLbl.Margin = new System.Windows.Thickness(0, 345, 134, 0);
                    }
                    if (MenuLbl.Margin.Top == 345)
                    {
                        MenuLbl.Margin = new System.Windows.Thickness(0, 380, 134, 0);
                    }
                    if (MenuLbl.Margin.Top == 380)
                    {
                        MenuLbl.Margin = new System.Windows.Thickness(0, 275, 134, 0);
                    }
                }
            }
        }
        internal void HandleKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                if (InWorld == true)
                {
                    WalkingUp = true;
                }
                if(InMenu == true)
                {
                    MenuMove(Direction.Up);
                }
            }
            else if (e.Key == Key.Down)
            {
                if (InWorld == true)
                {
                    WalkingDown = true;
                }
                if (InMenu == true)
                {
                    MenuMove(Direction.Down);
                }
            }
            else if (e.Key == Key.Left)
            {
                if (InWorld == true)
                {
                    WalkingLeft = true;
                }
            }
            else if (e.Key == Key.Right)
            {
                if (InWorld == true)
                {
                    WalkingRight = true;
                }
            }
            else if (e.Key == Key.Z)
            {

            }
            else if (e.Key == Key.X)
            {

            }
            else if (e.Key == Key.C)
            {

            }
        }

        internal void HandleKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                if (InWorld == true)
                {
                    WalkingUp = false;
                }
            }
            else if (e.Key == Key.Down)
            {
                if (InWorld == true)
                {
                    WalkingDown = false;
                }
            }
            else if (e.Key == Key.Left)
            {
                if (InWorld == true)
                {
                    WalkingLeft = false;
                }
            }
            else if (e.Key == Key.Right)
            {
                if (InWorld == true)
                {
                    WalkingRight = false;
                }
            }
            else if (e.Key == Key.Z)
            {

            }
            else if (e.Key == Key.X)
            {

            }
            else if (e.Key == Key.C)
            {

            }
        }

        //battle variables
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public bool Defending { get; set; }
        public string ActionsStr { get; set; }
        public DispatcherTimer AtkTimer { get; set; }
        public int AtkSince { get; set; }
        public int MapX
        {
            get => World.CurrentX;
            set => World.CurrentX = value;
        }
        public int MapY
        {
            get => World.CurrentY;
            set => World.CurrentY = value;
        }

        public void Attack(ref Enemy enemy, ref World world)
        {
            Random rand = new Random();
            if (AtkSince > 30)
            {
                int chance = rand.Next(0, 100);
                if (chance > 12 && Apbar.Width >= 50)
                {
                    if (chance > 87)
                    {
                        enemy.Hp -= Atk * 2;
                        world.SpawnNum(World.Numoptions.crit, Atk * 2);
                    }
                    else
                    {
                        world.SpawnNum(World.Numoptions.normal, Atk);
                        enemy.Hp -= Atk;
                    }
                    Apbar.Width -= 50;
                }
                else
                {
                    world.SpawnNum(World.Numoptions.miss, 0);
                }
            }
            if (AtkTimer.IsEnabled && AtkSince > 30)
            {
                AtkSince = 0;
                AtkTimer.Stop();
                AtkTimer.Interval = new TimeSpan(10000);
                AtkTimer.Start();
            }
            else { AtkTimer.Start(); }

        }
        public void Defend()
        {
            Defending = true;
            ActionsStr = Name + " takes a defensive stance.";
        }

        public void UseItem()
        {
            Console.Clear();
            ConsoleKey keychoice = ConsoleKey.NoName;
            Console.WriteLine("Select an item to use"); Console.WriteLine("");
            int i = 0;
            for (i = 0; i < Items.Count; i++)
            {
                Console.WriteLine(i + ". " + Items[i].Name);
            }
            Console.WriteLine(i + ". Cancel/Defend");
            keychoice = Console.ReadKey().Key;
            int choice = Int16.Parse(keychoice.ToString().Last().ToString());
            if (Items != null && Items.Count > 0 && Items.ElementAtOrDefault(choice) != null)
            {
                Items.ElementAtOrDefault(choice).Use(this);
                Items.RemoveAt(choice);
            }
            else if (choice == i && InBattle == true) { Defend(); }
            Console.Clear();
        }

        public void Heal(int amnt)
        {
            Hp += amnt; if (Hp > MaxHp) { Hp = MaxHp; }
        }
        public void TakeDamage(Enemy enemy)
        {
            int atk = enemy.Atk;
            if (atk <= Def || (Defending == true && atk <= Def * 2))
            {
                ActionsStr = Name + " defelected the attack";
            }
            else
            {
                if (Defending == true)
                {
                    Hp -= (atk - Def * 2);
                }
                else
                {
                    Hp -= atk - Def;
                }
            }
        }

        public void flee(ref bool end)
        {
            Random rnd = new Random();
            if (rnd.Next(0, 10) >= 3)
            {
                end = true;
                MainStr = "You fled but took damage in your escape.";
            }
            else
            {
                ActionsStr = "You tried to feel but your escape failed.";
            }
        }

        public Player(string name, ref Image playerImg, ref Label menulbl, ref Rectangle apbar, ref World world)
        {
            World = world;
            Name = name;
            MaxHp = Hp = 20;
            Level = 1; Exp = 0; NextLvlExp = 10;
            Atk = 50;
            Def = 3;
            Speed = 0.6;
            Defending = false;
            ActionsStr = "";
            Items = new List<Item>();
            InBattle = false;
            WalkingDown = WalkingLeft = WalkingRight = WalkingUp = false;
            InWorld = false;
            InMenu = true;
            Img = playerImg;
            MenuLbl = menulbl;
            Apbar = apbar;
            AtkSince = 1000;
            AtkTimer = new DispatcherTimer();
            AtkTimer.Tick += (s, e) => { AtkSince++; };
            AtkTimer.Interval = new TimeSpan(10000);
        }
    }
}
