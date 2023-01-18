using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace LanciaDado
{
    public partial class MainPage : ContentPage
    {
        //In posizione 0 numero totale di lanci, nelle successive i numeri
        //delle rispettive uscite
        static int[] lanciNumeri = new int[7];

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {

            try
            {
                Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
                Accelerometer.Start(SensorSpeed.UI);
            }
            catch (FeatureNotSupportedException)
            {
                System.Diagnostics.Debug.WriteLine("Accelerometer Unavailable");
            }

        }

        protected override void OnDisappearing()
        {
            Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
            Accelerometer.Stop();
        }

        private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Lancia_Clicked(null, null);
        }

        void Lancia_Clicked(System.Object sender, System.EventArgs e)
        {
            if (DueDadi.IsChecked)
            {
                DadoUno.Scale = 3;
                DadoUno.Margin = 40;
                DadoDue.Scale = 3;
                DadoDue.Margin = 40;
            }
            else
            {
                DadoUno.Scale = 3;
                DadoUno.Margin = 40;
                DadoDue.Margin = 0;
                DadoDue.Source = null;
            }

            int numeroUscito = LanciaIlDado();
            DadoUno.Source = ImageSource.FromResource("LanciaDado.Immagini." + numeroUscito.ToString() + ".png");
            Percentuali.Text = CalcolaPercentuali(lanciNumeri);

            if (DueDadi.IsChecked)
            {
                numeroUscito = LanciaIlDado();
                DadoDue.Source = ImageSource.FromResource("LanciaDado.Immagini." + numeroUscito.ToString() + ".png");
                Percentuali.Text = CalcolaPercentuali(lanciNumeri);
            }
        }

        int LanciaIlDado()
        {
            Random rnd = new Random();
            int valore = rnd.Next(1, 7);
            lanciNumeri[0]++;
            lanciNumeri[valore]++;

            return valore;
        }

        string CalcolaPercentuali(int[] valori)
        {
            string perc = "";

            int[] percentuali = new int[6];
            for (int i = 0; i < 6; i++)
            {
                percentuali[i] = valori[i + 1] * 100 / valori[0];
            }
            perc = $"Lanci: {valori[0].ToString()} " +
                $"\n 1: {percentuali[0].ToString(),2}% " +
                $"| 2: {percentuali[1].ToString(),2}% " +
                $"| 3: {percentuali[2].ToString(),2}% " +
                $"\n 4: {percentuali[3].ToString(),2}% " +
                $"| 5: {percentuali[4].ToString(),2}% " +
                $"| 6: {percentuali[5].ToString(),2}%";
            return perc;
        }



    }
}
