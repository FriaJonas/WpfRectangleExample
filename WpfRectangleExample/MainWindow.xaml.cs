using System;
using System.Collections.Generic;
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

namespace WpfRectangleExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    
    public partial class MainWindow : Window
    {
        int BurgerTop = 0;
        DispatcherTimer Timer = new DispatcherTimer();
        Random random = new Random();
        int Points = 0;
        public MainWindow()
        {   
            InitializeComponent();

            //Vi talar om för Timern vilken funktion som ska anropas
            Timer.Tick += new EventHandler(Timer_Tick);
           
            //Vi bestämmer hur ofta Timer_Tick ska anropas
            Timer.Interval = TimeSpan.FromMilliseconds(50);
            Timer.Start();
            ResetBurger();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            //Vi ökar avståndet på burgaren från toppen med 5 pixlar
            BurgerTop+=1;

            //Vi sätter burgarens nya position på vår Canvas som är spelplanen
            Canvas.SetTop(Burger, BurgerTop);

            //Kollisionskontroll - här måste vi skapa 2st Rect som vi kan köra en koll på "IntersectsWith"
            //För att skapa en Rect så behöver vi positionerna X o Y samt bredd och höjd;
            Rect BurgerRect = new Rect(Canvas.GetLeft(Burger),Canvas.GetTop(Burger),Burger.Width,Burger.Height);
            Rect UfoRect = new Rect(Canvas.GetLeft(Ufo), Canvas.GetTop(Ufo), Ufo.Width, Ufo.Height);

            //Koll om UfoRect krockar med BurgerRect
            if (UfoRect.IntersectsWith(BurgerRect))
            {
                //Vi ökar poängen med två vid träff
                Points+=2;

                //Skriver ut poängen
                TxtPoints.Text = "Poäng: " + Points;

                //Ny position för burgaren
                ResetBurger();
            }


            if (BurgerTop > 450)
            {
                //Vi minskar poängen med 1 vid miss
                Points--;

                //Skriver ut poängen
                TxtPoints.Text = "Poäng: " + Points;

                //Ny position för burgaren
                ResetBurger();
            }
            
            
            
        }

        //Funktion för att återställa och slumpa Burgaren
        private void ResetBurger()
        {
            //Vi startar ovanför skärmen
            BurgerTop = -50;

            //Slumpar ny X position
            Canvas.SetLeft(Burger, random.Next(0, 750));
        }
        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            //ta reda på nuvarande avstånd får MyBox från vänster
            double left = Canvas.GetLeft(Ufo);
            
            //Vi vill till vänster så vi minskar left med 5 pixlar
            left = left - 5;

            //Vi sätter vår MyBox med nya left
            Canvas.SetLeft(Ufo, left);
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            double left = Canvas.GetLeft(Ufo);
            left = left + 5;
            Canvas.SetLeft(Ufo, left);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                double left = Canvas.GetLeft(Ufo);
                left = left - 15;
                Canvas.SetLeft(Ufo, left);
            }
            else if(e.Key == Key.Right)
            {
                double left = Canvas.GetLeft(Ufo);
                left = left + 15;
                Canvas.SetLeft(Ufo, left);
            }
        }

        private void Burger_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Points++;
            TxtPoints.Text = "Poäng: " + Points;
            ResetBurger();
        }
    }
}
