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
        //array per gli ingredienti del panino
        //4 sono gli ingredienti
        bool[] paninoPrep = new bool[4];
        //array che contiene la richiesta del cliente
        bool[] paninoReq = new bool[4];

        //didascalia:
        //pos 0: carne cotta
        //pos 1: formaggio
        //pos 2: pomodori
        //pos 3: insalata

        //g sta per gioco per indicare che sono diverse da quelle passate dal menu start
        int viteG; 
        string nomeG;
        int secondi = 0;
        int contaCarne = 1;
        int secondiCliente1;
        int contaPane = 1;
        int contaInsalata = 0;
        int secondiCliente2;
        bool paninoAperto = false;

        int clienteAttivo = 0;  //indica il cliente attivo, 0 = nessun cliente attivo

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

            //nascondi vignette clienti
            pbReq1.Hide();
            pbReq2.Hide();

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

        private void generaOrdine()
        {
            //genera un random per i casi del panino
            Random R = new Random();
            int random = R.Next(1, 4);

            //in base al numero generato droppamo una combinazione di ingredienti
            //formaggio e carne cotta stanno su tutti

            switch (random)
            {
                case 1:
                    //ritorna panino formaggio e carne
                    paninoReq[0] = true;
                    paninoReq[1] = true;
                    paninoReq[2] = false;
                    paninoReq[3] = false;
                    //In base al cliente, mostro l'ordine generato
                    if (clienteAttivo == 1)
                    {
                        pbReq1.Image = global::CheeseChase.Properties.Resources.paninoReqFC;
                        pbReq1.Show();
                    }
                    else
                    {
                        pbReq2.Image = global::CheeseChase.Properties.Resources.paninoReqFC;
                        pbReq2.Show();
                    }
                    break;

                case 2:
                    //ritorna panino formaggio e carne e pomodori
                    paninoReq[0] = true;
                    paninoReq[1] = true;
                    paninoReq[2] = true;
                    paninoReq[3] = false;
                    //In base al cliente, mostro l'ordine generato
                    if (clienteAttivo == 1)
                    {
                        pbReq1.Image = global::CheeseChase.Properties.Resources.paninoReqFCP;
                        pbReq1.Show();
                    }
                    else
                    {
                        pbReq2.Image = global::CheeseChase.Properties.Resources.paninoReqFCP;
                        pbReq2.Show();
                    }
                    break;

                case 3:
                    //ritorna panino formaggio e carne e pomodori e insalata
                    paninoReq[0] = true;
                    paninoReq[1] = true;
                    paninoReq[2] = true;
                    paninoReq[3] = true;
                    //In base al cliente, mostro l'ordine generato
                    if (clienteAttivo == 1)
                    {
                        pbReq1.Image = global::CheeseChase.Properties.Resources.paninoReqIFCP;
                        pbReq1.Show();
                    }
                    else
                    {
                        pbReq2.Image = global::CheeseChase.Properties.Resources.paninoReqIFCP;
                        pbReq2.Show();
                    }
                    break;

                case 4:
                    //ritorna panino formaggio e carne e insalata
                    paninoReq[0] = true;
                    paninoReq[1] = true;
                    paninoReq[2] = false;
                    paninoReq[3] = true;
                    //In base al cliente, mostro l'ordine generato
                    if (clienteAttivo == 1)
                    {
                        pbReq1.Image = global::CheeseChase.Properties.Resources.paninoReqIFC;
                        pbReq1.Show();
                    }
                    else
                    {
                        pbReq2.Image = global::CheeseChase.Properties.Resources.paninoReqIFC;
                        pbReq2.Show();
                    }
                    break;

                default:
                    break;
            }
            
        }

        //didascalia:
        //pos 0: carne cotta
        //pos 1: formaggio
        //pos 2: pomodori
        //pos 3: insalata

        private void terminaPartita()
        {
            //terminare la partita
            //da fare
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

                    clienteAttivo = 0;

                    //se è un numero pari e non c'è nessun cliente mostro il 1, altrimenti il 2
                    if (statoCliente1 == statoCliente.INVISIBIE && statoCliente2 == statoCliente.INVISIBIE)
                    {
                        if (numeroCasualePerGenerareCliente % 2 == 0)
                        {
                            statoCliente1 = statoCliente.ATTIVO;
                            clienteAttivo = 1;
                            pbCliente1.Show();

                            //qui deve generare l'ordine e iniziare a contare secondi nel contatore cliente1
                            generaOrdine();

                        }
                        else
                        {
                            statoCliente2 = statoCliente.ATTIVO;
                            clienteAttivo = 2;
                            pbCliente2.Show();
                            //qui deve generare l'ordine e iniziare a contare secondi nel contatore cliente2
                            generaOrdine();
                        }
                    }
                }

                if (statoCliente1 == statoCliente.ATTIVO || statoCliente1 == statoCliente.ARRABBIATO)
                {
                    //aumenta i secondi e controlla se è arrabbiato o se ne va
                    secondiCliente1++;
                    if (secondiCliente1 >= 10 && statoCliente1 == statoCliente.ATTIVO)
                    {
                        //braca si arrabbia
                        statoCliente1 = statoCliente.ARRABBIATO;
                        pbCliente1.Image = global::CheeseChase.Properties.Resources.ClienteBracaArrabbiato;
                    }
                    if (secondiCliente1 >= 15 && statoCliente1 == statoCliente.ARRABBIATO)
                    {
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
                        pbCliente2.Image = global::CheeseChase.Properties.Resources.Cliente2Arrabbiato;
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

        //didascalia:
        //pos 0: carne cotta
        //pos 1: formaggio
        //pos 2: pomodori
        //pos 3: insalata

        private void pbPilaPane_Click(object sender, EventArgs e)
        {
            //mostra il pane
            pbPaninoCorrente.Show();
            pbPaninoCorrenteSopra.Show();
            paninoAperto = true;
        }

        private void pbCestino_Click(object sender, EventArgs e)
        {
            paninoAperto = false;
            //resetta l'array panino preparazione
            for (int i = 0; i < 3; i++)
            {
                paninoPrep[i] = false;
            }
            //nasconde il pane aperto
            pbPaninoCorrenteSopra.Hide();
            pbPaninoCorrente.Hide();
            //rimette l'immagine iniziale del pane
            pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.SottoPane;
        }

        private void pbPaninoCorrente_Click(object sender, EventArgs e)
        {

        }

        //Bottone che gestisce la serranda
        private void pbBottoneSerranda_Click(object sender, EventArgs e)
        {
            //se è aperta la chiudo e viceversa
            if (serrandaAperta)
            {
                //mette la pic della serranda chiusa
                this.BackgroundImage = Properties.Resources.gioco_serrandaChiusa;
               
                //se ci sono clienti li nasconde 
                if (statoCliente1 == statoCliente.ATTIVO || statoCliente1 == statoCliente.ARRABBIATO)
                {
                    //nascondo cliente ed ordine
                    pbCliente1.Hide();
                    pbReq1.Hide();
                }

                if (statoCliente2 == statoCliente.ATTIVO || statoCliente2 == statoCliente.ARRABBIATO)
                {
                    //nascondo cliente ed ordine
                    pbCliente2.Hide();
                    pbReq2.Hide();
                }

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
        //dentro tutte ste procedure dei click si aggiorna solo l'array e si chiama la funzione visualizza panino grafica

        //didascalia:
        //pos 0: carne cotta
        //pos 1: formaggio
        //pos 2: pomodori
        //pos 3: insalata

        private void pbPilaPomodoro_Click(object sender, EventArgs e)
        {
            if (paninoAperto)
            {
                paninoPrep[2] = true;
                visualizzaPaninoGrafica();
            }

        }
        private void pbPilaInsalata_Click(object sender, EventArgs e)
        {
            if (paninoAperto)
            {
                paninoPrep[3] = true;
                visualizzaPaninoGrafica();
            }
        }
        private void pbPilaFormaggio_Click(object sender, EventArgs e)
        {
            if (paninoAperto)
            {
                paninoPrep[1] = true;
                visualizzaPaninoGrafica();
            }
        }
        private void pbCarneCotta1_Click(object sender, EventArgs e)
        {
            if (paninoAperto)
            {
                if (contaCarne == 2 && paninoAperto)
                {
                    paninoPrep[0] = true;
                    visualizzaPaninoGrafica();
                    pbCarneCotta1.Hide();
                    contaCarne--;
                }
                else
                {
                    MessageBox.Show("Prendi l'ultima carne");
                }
            }
            else
            {
                MessageBox.Show("Apri prima il pane");
            }
        }

        private void pbCarneCotta2_Click(object sender, EventArgs e)
        {
            if (paninoAperto)
            {
                if (contaCarne == 3 && paninoAperto)
                {
                    paninoPrep[0] = true;
                    visualizzaPaninoGrafica();
                    pbCarneCotta2.Hide();
                    contaCarne--;
                }
                else
                {
                    MessageBox.Show("Prendi l'ultima carne");
                }
            }
            else
            {
                MessageBox.Show("Apri prima il pane");
            }

        }
        private void pbCarneCotta3_Click(object sender, EventArgs e)
        {
            if (paninoAperto)
            {
                if (contaCarne == 4)
                {
                    paninoPrep[0] = true;
                    visualizzaPaninoGrafica();
                    pbCarneCotta3.Hide();
                    contaCarne--;
                }
                else
                {
                    MessageBox.Show("Prendi l'ultima carne");
                }
            }
            else
            {
                MessageBox.Show("Apri prima il pane");
            }
        }

        private void visualizzaPaninoGrafica()
        {
            bool carne = paninoPrep[0];
            bool formaggio = paninoPrep[1];
            bool pomodoro = paninoPrep[2];
            bool insalata = paninoPrep[3];

            if (carne && !formaggio && !pomodoro && !insalata)
            {
                // Solo carne
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoC;
            }
            else if (!carne && formaggio && !pomodoro && !insalata)
            {
                // Solo formaggio
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoF;

            }
            else if (!carne && !formaggio && pomodoro && !insalata)
            {
                // Solo pomodoro
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoP;

            }
            else if (!carne && !formaggio && !pomodoro && insalata)
            {
                // Solo insalata
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoI;

            }
            else if (carne && formaggio && !pomodoro && !insalata)
            {
                // Carne + formaggio
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoFC;

            }
            else if (carne && !formaggio && pomodoro && !insalata)
            {
                // Carne + pomodoro
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoCP;

            }
            else if (carne && !formaggio && !pomodoro && insalata)
            {
                // Carne + insalata
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoIC;

            }
            else if (!carne && formaggio && pomodoro && !insalata)
            {
                // Formaggio + pomodoro
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoFP;

            }
            else if (!carne && formaggio && !pomodoro && insalata)
            {
                // Formaggio + insalata
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoIF;

            }
            else if (!carne && !formaggio && pomodoro && insalata)
            {
                // Pomodoro + insalata
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoIP;

            }
            else if (carne && formaggio && pomodoro && !insalata)
            {
                // Carne + formaggio + pomodoro
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoFCP;

            }
            else if (carne && formaggio && !pomodoro && insalata)
            {
                // Carne + formaggio + insalata
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoIFC;

            }
            else if (carne && !formaggio && pomodoro && insalata)
            {
                // Carne + pomodoro + insalata
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoICP;

            }
            else if (!carne && formaggio && pomodoro && insalata)
            {
                // Formaggio + pomodoro + insalata
               pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoIFP;


            }
            else if (carne && formaggio && pomodoro && insalata)
            {
                // Carne + formaggio + pomodoro + insalata
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoIFCP;

            }
        }

        private void pbPaninoCorrenteSopra_Click(object sender, EventArgs e)
        {
            bool carne = paninoPrep[0];
            bool formaggio = paninoPrep[1];
            bool pomodoro = paninoPrep[2];
            bool insalata = paninoPrep[3];

            if (carne && pomodoro && insalata && formaggio)
            {
                pbPaninoCorrenteSopra.Hide();
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoCompletoIFCP;
            } else if (carne && formaggio && !insalata && !pomodoro)
            {
                pbPaninoCorrenteSopra.Hide();
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoCompletoFC;
            }
            else if (carne && formaggio && !insalata && pomodoro)
            {
                pbPaninoCorrenteSopra.Hide();
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoCompletoFCP;
            }
            else if (carne && formaggio && insalata && !pomodoro)
            {
                pbPaninoCorrenteSopra.Hide();
                pbPaninoCorrente.Image = global::CheeseChase.Properties.Resources.paninoCompletoIFC;
            }
            else
            {
                MessageBox.Show("Inserire minimo carne e formaggio");
            }
        }
    }
}           