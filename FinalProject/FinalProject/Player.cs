using System;
using System.Collections.Generic;
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

        Image Img { get; set; }
        Label MenuLbl { get; set; }
        Rectangle Apbar { get; set; }

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
        public bool defending { get; set; }
        public string actionsStr { get; set; }

        public void Attack(ref Enemy enemy, ref World world)
        {
            Random rand = new Random();
            DispatcherTimer attackSince = new DispatcherTimer();
            attackSince.Interval = new TimeSpan(10000);
            if (attackSince.IsEnabled)
            {
                attackSince.Stop();
                attackSince.Interval = new TimeSpan(10000);
                attackSince.Start();
            }
            else { attackSince.Start(); }
            int chance = rand.Next(0, 100);
            if (chance > 15 && Apbar.Width >= 50)
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
        public void defend()
        {
            defending = true;
            actionsStr = Name + " takes a defensive stance.";
        }

        public void useItem()
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
            else if (choice == i && InBattle == true) { defend(); }
            Console.Clear();
        }

        public void heal(int amnt)
        {
            Hp += amnt; if (Hp > MaxHp) { Hp = MaxHp; }
        }
        public void takeDamage(Enemy enemy)
        {
            int atk = enemy.Atk;
            if (atk <= Def || (defending == true && atk <= Def * 2))
            {
                actionsStr = Name + " defelected the attack";
            }
            else
            {
                if (defending == true)
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
                actionsStr = "You tried to feel but your escape failed.";
            }
        }

        public Player(string name, ref Image playerImg, ref Label menulbl, ref Rectangle apbar)
        {
            Name = name;
            MaxHp = Hp = 20;
            Level = 1; Exp = 0; NextLvlExp = 10;
            Atk = 50;
            Def = 3;
            Speed = 0.4;
            defending = false;
            actionsStr = "";
            Items = new List<Item>();
            InBattle = false;
            WalkingDown = WalkingLeft = WalkingRight = WalkingUp = false;
            InWorld = false;
            InMenu = true;
            Img = playerImg;
            MenuLbl = menulbl;
            Apbar = apbar;
        }
    }
}
