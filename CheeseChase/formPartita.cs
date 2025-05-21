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
        int contaPane = 1;
        int contaInsalata = 0;
        int secondiCliente2;

        statoCliente statoCliente1 = statoCliente.INVISIBIE;
        statoCliente statoCliente2 = statoCliente.INVISIBIE;

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
            
            //nascondo le label che non servono all'inizio
            pbCarneCotta1.Hide();
            pbCarneCotta2.Hide();
            pbCarneCotta3.Hide();

            pbCarneCrudaGriglia1.Hide();
            pbCarneCrudaGriglia2.Hide();
            pbCarneCrudaGriglia3.Hide();

            pbPaninoCorrente.Hide();
            pbPaninoCorrenteSopra.Hide();

            //nascondo i clienti 
            pbCliente1.Hide();
            secondiCliente1 = 0; //contiene i secondi che aspetta il cliente 1, sopra a 15 si arrabbia e a 30 va via e perde una vita il giocatore

            pbCliente2.Hide();
            secondiCliente2 = 0;



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

                    Random R = new Random();
                    int numeroCasualePerGenerareCliente = R.Next(1, 99999);

                    //se è un numero pari e non c'è nessun cliente mostro il 1, altrimenti il 2
                    if (statoCliente1 == statoCliente.INVISIBIE && statoCliente2 == statoCliente.INVISIBIE)
                    {
                        if (numeroCasualePerGenerareCliente % 2 == 0)
                        {
                            statoCliente1 = statoCliente.ATTIVO;
                            pbCliente1.Show();
                            //qui deve generare l'ordine e iniziare a contare secondi nel contatore cliente1
                        }
                        else
                        {
                            statoCliente2 = statoCliente.ATTIVO;
                            pbCliente2.Show();
                            //qui deve generare l'ordine e iniziare a contare secondi nel contatore cliente2
                        }
                    }
                }

                if (statoCliente1 == statoCliente.ATTIVO || statoCliente1 == statoCliente.ARRABBIATO)
                {
                    //aumenta i secondi e controlla se è arrabbiato o se ne va
                    secondiCliente1++;
                    MessageBox.Show(secondiCliente1.ToString()); //test
                    if (secondiCliente1 >= 10 && statoCliente1 == statoCliente.ATTIVO)
                    {
                        //braca si arrabbia
                        statoCliente1 = statoCliente.ARRABBIATO;
                        pbCliente1.Image = global::CheeseChase.Properties.Resources.ClienteBracaArrabbiato;
                    }
                    if (secondiCliente1 >= 15 && statoCliente1 == statoCliente.ARRABBIATO)
                    {
                        MessageBox.Show("incazzatissimo"); //test
                        //braca si incazza tantissimo
                        pbCliente1.Image = global::CheeseChase.Properties.Resources.ClienteBracaFurioso;
                    }
                    //da finire


                    if (secondiCliente1 == 30) { }
                        //si incazza troppo e quindi se ne va (da fare)

                }

                if (statoCliente2 == statoCliente.ATTIVO)
                {
                    //aumenta i secondi e controlla se è arrabbiato o se ne va
                    secondiCliente2++;
                    if (secondiCliente2 == 15)
                    {
                        statoCliente2 = statoCliente.ARRABBIATO;
                        pbCliente1.Image = global::CheeseChase.Properties.Resources.Cliente2Arrabbiato;
                    }

                    if (secondiCliente2 == 30) { }
                    //si incazza troppo e quindi se ne va (da fare)

                }
            }
        }



        // Metodo asincrono che gestisce l'aggiunta di carne cruda sulla griglia al clic del giocatore.
        // In base al numero di pezzi già presenti, mostra la carne cruda in una delle tre posizioni disponibili.
        // Dopo aver posizionato la carne, attende 5 secondi per simulare il tempo di cottura.
        // La parola chiave "async" consente di eseguire operazioni asincrone, come "await Task.Delay",
        // senza bloccare il thread principale (UI thread). In questo modo l'interfaccia utente rimane fluida e reattiva
        // durante l'attesa, permettendo ad esempio ai clienti di continuare ad arrivare o ad altri eventi di verificarsi.
        private async void pbPilaCarneCruda_Click(object sender, EventArgs e)       
        {
            switch (contaCarne)
            {
                case 1:
                    pbCarneCrudaGriglia1.Show();            //mostro la carne cruda sulla griglia
                    contaCarne++;                           //contatore di quanti pezzzi di carne sono sulla griglia, massimo 3 pezzi alla volta
                    await Task.Delay(5000);                 //delay di 5 secondi prima di passare dalla carne cruda a cotta
                    pbCarneCrudaGriglia1.Hide();            //l'immagine della carne cruda viene tolta e si ha quella cotta
                    pbCarneCotta1.Show();
                    break;

                case 2:
                    pbCarneCrudaGriglia2.Show();
                    contaCarne++;
                    await Task.Delay(5000);
                    pbCarneCrudaGriglia2.Hide();
                    pbCarneCotta2.Show();
                    break;

                case 3:
                    pbCarneCrudaGriglia3.Show();
                    contaCarne++;
                    await Task.Delay(5000);
                    pbCarneCrudaGriglia3.Hide();
                    pbCarneCotta3.Show();
                    break;

                default:
                    //si possono aggiungere massimo 3 pezzi di carne alla griglia, lo stesso numero di clienti che possono essere presenti contemporaneamente al bancone
                    break;
            }
            
        }

        private void pbPilaPane_Click(object sender, EventArgs e)
        {
            //mostra il pane
            pbPaninoCorrente.Show();
            pbPaninoCorrenteSopra.Show();
        }

        private void pbCestino_Click(object sender, EventArgs e)
        {
            //cancella il panino corrente 
        }

        private void pbPaninoCorrente_Click(object sender, EventArgs e)
        {

        }

        private void pbPaninoCorrenteSopra_Click(object sender, EventArgs e)
        {

        }

        private void pbBottoneSerranda_Click(object sender, EventArgs e)
        {
            //se è aperta la chiudo e viceversa
            if (serrandaAperta)
            {
                //mette la pic della serranda chiusa
                this.BackgroundImage = Properties.Resources.gioco_serrandaChiusa;
               
                //se ci sono clienti li nasconde 
                if (statoCliente1 == statoCliente.ATTIVO || statoCliente1 == statoCliente.ARRABBIATO)
                    pbCliente1.Hide();

                if (statoCliente2 == statoCliente.ATTIVO || statoCliente1 == statoCliente.ARRABBIATO)
                    pbCliente2.Hide();

                //resetta la dinamica dei clienti 
                statoCliente1 = statoCliente.INVISIBIE;
                statoCliente2 = statoCliente.INVISIBIE;
                secondiCliente1 = 0;
                secondiCliente2 = 0;

            }
            else
            {
                //apre la serranda 
                this.BackgroundImage = Properties.Resources.gioco_serrandaAperta;
            }

            //cambio lo stato della serranda
            serrandaAperta = !serrandaAperta;
        }
    }
}           