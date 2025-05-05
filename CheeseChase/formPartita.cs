using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheeseChase
{
    //enum che contiene tutti i possibili stati di un cliente
    public enum statoCliente
    {
        INVISIBIE, //quando non è visibile
        ATTIVO, //quando sta aspettando di ricevere il suo ordine
        ARRABBIATO, //quando sono passati 15 secondi dal suo arrivo
        FATTO //quando l'ordine è stato completato
    }


    public partial class formPartita : Form
    {
        
        //g sta per gioco per indicare che sono diverse da quelle passate dal menu start
        int viteG; 
        string nomeG;
        int secondi = 0;
        int contaCarne = 1;
        int secondiCliente1;

        statoCliente statoCliente1 = statoCliente.INVISIBIE;

        //variabile che contiene lo stato della serranda: true significa che è aperta 
        bool serrandaAperta = true;

        public formPartita(string nome, int vite)
        {
           

            InitializeComponent();
            //"importazione" variabili dalla form del menu di start
            viteG = vite;
            nomeG = nome;
            lblVite.Text = viteG.ToString();
            lblNome.Text = nomeG;


            pbCarneCotta1.Hide();
            pbCarneCotta2.Hide();
            pbCarneCotta3.Hide();

            pbCarneCrudaGriglia1.Hide();
            pbCarneCrudaGriglia2.Hide();
            pbCarneCrudaGriglia3.Hide();

            //nascondo il cliente1
            pbCliente1.Hide();
            statoCliente1 = statoCliente.INVISIBIE;
            secondiCliente1 = 0; //contiene i secondi che aspetta il cliente 1, sopra a 15 si arrabbia e a 30 va via e perde una vita il giocatore

            //avvia il timer che ticka ogni 1000ms
            scandisciSecondi.Enabled = true;
        }

        //procedura per diminuire le vite CONTIENE IL CONTROLLO PER VEDERE SE LE VITE SONO FINITE
        private void diminuisciVite()
        {
            viteG--;
            if (viteG < 1)
                terminaPartita();
        }

        private void terminaPartita()
        {
            //terminare la partita
            //da fare
        }

        //bottone per gestire la serranda
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //se è aperta la chiudo e viceversa
            if (serrandaAperta)
                this.BackgroundImage = Properties.Resources.gioco_serrandaChiusa;
            else
                this.BackgroundImage = Properties.Resources.gioco_serrandaAperta;

            //cambio lo stato della serranda
            serrandaAperta = !serrandaAperta;
        }

        //timer che ticka ogni 1000ms
        private void scandisciSecondi_Tick(object sender, EventArgs e)
        {
            if (serrandaAperta)
            {
                secondi++; //incrementa il contatore dei secondi
                           //eventualmente fa arrivare un cliente
                if (secondi % 5 == 0)
                {
                    if (statoCliente1 == statoCliente.INVISIBIE)
                    {
                        statoCliente1 = statoCliente.ATTIVO;
                        pbCliente1.Show();
                        //qui deve generare l'ordine e iniziare a contare secondi nel contatore cliente1
                    }
                }

                if (statoCliente1 == statoCliente.ATTIVO)
                {
                    //aumenta i secondi e controlla se è arrabbiato o se ne va
                    secondiCliente1++;
                    if (secondiCliente1 == 15)
                        statoCliente1 = statoCliente.ARRABBIATO;

                    if (secondiCliente1 == 30) { }
                        //si incazza troppo e quindi se ne va (da fare)

                }
            }
        }

        private async void pbPilaCarneCruda_Click(object sender, EventArgs e)
        {
            if (contaCarne == 1)
            {
                pbCarneCrudaGriglia1.Show();            //mostro la carne cruda sulla griglia
                contaCarne++;                           //contatore di quanti pezzzi di carne sono sulla griglia, massimo 3 pezzi alla volta
                await Task.Delay(5000);                 //delay di 5 secondi prima di passare dalla carne cruda a cotta
                pbCarneCrudaGriglia1.Hide();            //l'immagine della carne cruda viene tolta e si ha quella cotta
                pbCarneCotta1.Show();                   

            }
            else if (contaCarne == 2)
            {
                pbCarneCrudaGriglia2.Show();
                contaCarne++;
                await Task.Delay(5000);
                pbCarneCrudaGriglia2.Hide();
                pbCarneCotta2.Show();
            }
            else if (contaCarne == 3)
            {
                pbCarneCrudaGriglia3.Show();
                contaCarne++;
                await Task.Delay(5000);
                pbCarneCrudaGriglia3.Hide();
                pbCarneCotta3.Show();
            }
        }

        
    }
}




            