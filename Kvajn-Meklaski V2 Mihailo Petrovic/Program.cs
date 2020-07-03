using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Kvajn_Meklaski_V2
{
    class Program
    {
        static string[] OdredjivanjeNE_ES_U_MDNF(string[] tabelaIzbacenihMinterma, int merac, bool[] MinTabelaPokrivenosti)
        {
            
            int brojactacnihuJednomMintermu = 0;
            for (int i = 0; i <tabelaIzbacenihMinterma.Length; i++)
            {
                int poredjenje = 0;
                for (int ki = i*merac; ki < (i+1)*merac; ki++)
                {
                    if (MinTabelaPokrivenosti[ki] == true)
                    {
                        poredjenje++;
                    }
                }
                if (poredjenje>brojactacnihuJednomMintermu)
                {
                    brojactacnihuJednomMintermu = poredjenje;
                }
                
            }

      
            int ukpozicija = 0;
            if (merac % brojactacnihuJednomMintermu == 1)
            {
                ukpozicija = (merac + 1) / brojactacnihuJednomMintermu;
            }
            else
            {
                ukpozicija = merac / brojactacnihuJednomMintermu;
            }
            int[] mesta = new int[ukpozicija];


            int maxPozicija = tabelaIzbacenihMinterma.Length - 1;

            for (int i = 0; i < mesta.Length; i++)
            {
                mesta[i] = 0;
            }
            
           
            
            bool[] kontrolnatabela = new bool[merac];
            for (int i = 0; i < kontrolnatabela.Length; i++)
            {
                kontrolnatabela[i] = false;
            }
            int brojackontrolnetabele = 0;
            

        rapa:


            long maxKombinacija = (long)Math.Pow(tabelaIzbacenihMinterma.Length, mesta.Length); //unapredi ovo tj umanji ga

            

            for (int m = 0; m < maxKombinacija ; m++)
            {
                for (int brojactabele = 0; brojactabele < mesta.Length; brojactabele++)
                {
                    for (int i = mesta[brojactabele] * merac; i < (mesta[brojactabele] + 1) * merac; i++)
                    {
                        
                        
                        if (MinTabelaPokrivenosti[i] == true)
                        {
                            kontrolnatabela[brojackontrolnetabele] = MinTabelaPokrivenosti[i];
                        }

                        brojackontrolnetabele++;
                    }

                    brojackontrolnetabele = 0;
                }

                int uktrue = 0;
                for (int i = 0; i < kontrolnatabela.Length; i++)
                {
                    if (kontrolnatabela[i] == true)
                    {
                        uktrue++;
                    }
                }
              

                if (uktrue == kontrolnatabela.Length)
                {

                    goto Kraj;
                }
                for (int i = 0; i < kontrolnatabela.Length; i++)
                {
                    kontrolnatabela[i] = false;
                }
                
                //uvecavanje za 1 i provera jel max poz
                for (int i = 0; i < mesta.Length; i++)
                {

                    if (mesta[i] == maxPozicija)
                    {
                        if (i == 0 || mesta[i - 1] == maxPozicija)
                        {
                           
                        }

                        else
                        {

                            mesta[i - 1]++;
                            mesta[i] = 0;
                        }

                    }
                    

                    if (i == mesta.Length - 1 )
                    {
                        mesta[i]++;

                    }

                }
                for (int i = 0; i < mesta.Length; i++)
                {
                    if (mesta[i]>maxPozicija)
                    {
                        mesta[i] = maxPozicija;
                    }
                }
               
                if (m == maxKombinacija - 1)
                {
                    if (mesta.Length>=merac)
                    {
                        Console.WriteLine("greska");
                        goto Kraj;
                    }
                    mesta = new int[mesta.Length + 1];
                    
                    for (int i = 0; i < mesta.Length; i++)
                    {
                        mesta[i] = 0;
                    }
                    goto rapa;
                }
            }
        Kraj:
            string[] NeEsencijalneUminFJI = new string[mesta.Length];
            for (int i = 0; i < NeEsencijalneUminFJI.Length; i++)
            {
                NeEsencijalneUminFJI[i] = tabelaIzbacenihMinterma[mesta[i]];
            }
            




            return NeEsencijalneUminFJI;
        
        }

        static bool[]PozicijeZaIzbacivanje(int[]pozicijeEsencijalnih,bool [] TabelaPokrivenosti,string [] UnetiMintermi)
        {
           

            bool[] staStvarnoPokrivajuEs = new bool[UnetiMintermi.Length];
            for (int i = 0; i < staStvarnoPokrivajuEs.Length; i++)
            {
                staStvarnoPokrivajuEs[i] = false;
            }
            
            for (int i = 0; i < pozicijeEsencijalnih.Length; i++)
            {
                int brojacBula = 0;
                for (int k = pozicijeEsencijalnih[i]*UnetiMintermi.Length; k < (pozicijeEsencijalnih[i]+1) * UnetiMintermi.Length; k++)
                {

                  
                    
                    if (TabelaPokrivenosti[k] == true)
                    {
                       
                        staStvarnoPokrivajuEs[brojacBula] = true;
                    }
                    brojacBula++;
                }
            }
           
            return staStvarnoPokrivajuEs;
        }
        
        
        static int MeracDuzine(bool[] TabelaStaStvarnoPokrivajuEsencijalne)
        {
            int UkupnoNeg = TabelaStaStvarnoPokrivajuEsencijalne.Length;
            for (int i = 0; i < TabelaStaStvarnoPokrivajuEsencijalne.Length; i++)
            {
                if (TabelaStaStvarnoPokrivajuEsencijalne[i] == true)
                {
                    UkupnoNeg--;
                }
            }
            return UkupnoNeg;
        }
       
        static bool[] TabelaBezNepotrebnihMintermaZaPerika(bool[] TabelaStaStvarnoPokrivajuEsencijalne,string[] RedukovaniMintermi,string [] UnetiMintermi)
        {
            int UkupnoNeg = TabelaStaStvarnoPokrivajuEsencijalne.Length;
            for (int i = 0; i < TabelaStaStvarnoPokrivajuEsencijalne.Length; i++)
            {
                if (TabelaStaStvarnoPokrivajuEsencijalne[i] == true)
                {
                    UkupnoNeg--;
                }
            }
            
            bool[] TabelaBezNepMinterma = new bool[UkupnoNeg * RedukovaniMintermi.Length];
            
            int brojactabele = 0;
            string[] unetimintermiPrepravljeni = new string[UkupnoNeg];
            int brojacovetabele = 0;

            for (int i = 0; i < TabelaStaStvarnoPokrivajuEsencijalne.Length; i++)
            {
                if (TabelaStaStvarnoPokrivajuEsencijalne[i] == true)
                {
                    
                }
                else
                {
                    unetimintermiPrepravljeni[brojacovetabele] = UnetiMintermi[i];
                    brojacovetabele++;
                }
            }
           
            for (int i = 0; i < RedukovaniMintermi.Length; i++)
            {
                for (int k = 0; k < unetimintermiPrepravljeni.Length; k++)
                {
            
                    if (RedukovaniMintermi[i]==StavljanjeRecki(unetimintermiPrepravljeni[k],RedukovaniMintermi[i]))
                    {
                        
                        TabelaBezNepMinterma[brojactabele] = true;
                        brojactabele++;
                    }
                    else
                    {
                        TabelaBezNepMinterma[brojactabele] = false;
                        brojactabele++;
                    }
                }
              
            }









           
            return TabelaBezNepMinterma;

           
        }
        

        static string[] TabelaIzbacenihMinterma(string [] TabelaUproscenihMinterma,int [] PozicijeEsencijalnih)
        {
           
                
            for (int i = 0; i < PozicijeEsencijalnih.Length; i++)
            {
                Array.Clear(TabelaUproscenihMinterma, PozicijeEsencijalnih[i], 1);
            }
            petlja:
            for (int i = 0; i < TabelaUproscenihMinterma.Length; i++)
            {
                if (TabelaUproscenihMinterma[i] == null)
                {
                    TabelaUproscenihMinterma = TabelaUproscenihMinterma.Where((source, index) => index != i).ToArray();
                    goto petlja;
                }
            }
            
            return TabelaUproscenihMinterma;
        }

        static void IspisivanjeMDNF(string [] TabelaUproscenihMinterma, int [] pozicijeES)
        {
            for (int i = 0; i < pozicijeES.Length; i++)
            {
                if (i == pozicijeES.Length - 1)
                {
                    Console.Write(TabelaUproscenihMinterma[pozicijeES[i]]);
                    Console.WriteLine("");
                    Console.WriteLine("------------------------------------------------------------");
                }
                else
                {
                    Console.Write(TabelaUproscenihMinterma[pozicijeES[i]] + " + ");

                }

            }

        }
        static void IspisivanjeMDNF(string[] TabelaUproscenihMinterma,int [] pozicijeES,string [] NE_ES_MDNF)
        {
           
            for (int i = 0; i < pozicijeES.Length; i++)
            {
                Console.Write(TabelaUproscenihMinterma[pozicijeES[i]] + " + ");
            }
            for (int i = 0; i < NE_ES_MDNF.Length; i++)
            {
                if (i == NE_ES_MDNF.Length - 1)
                {
                    Console.Write(NE_ES_MDNF[i]);
                    Console.WriteLine("");
                    Console.WriteLine("------------------------------------------------------------");
                }
                else
                {
                    Console.Write(NE_ES_MDNF[i] + " + ");

                }
            }

        }
        static int[] PozicijeEsencijalnih (string[] UnetiMintermi, bool [] Pokrivenost)
        {
            //Ova tabela nam pokazuje koje minterme pokriva samo 1 implikanta
            bool[] tabela = new bool[UnetiMintermi.Length];
            int brojactebele = 0;
            int ukupnotrue = 0;

            for (int i = 0; i < tabela.Length; i++)
            {
                tabela[i] = false;
            }

            for (int i = 0; i < UnetiMintermi.Length; i++)
            {
                
                int ukupnopozitivnih = 0;
                
                for (int k = i; k < Pokrivenost.Length; k= k +UnetiMintermi.Length)
                {
                    if (Pokrivenost[k] == true)
                    {
                        
                        ukupnopozitivnih++;
                    }
                }
                
                if (ukupnopozitivnih == 1)
                {
                    tabela[brojactebele] = true;
                    brojactebele++;
                    ukupnotrue++;
                }
                else
                {
                    brojactebele++;
                }
            }

            // izvlacimo pozicije esencijalnih implikanti
           

            int[] pozicijeEsencijalnih = new int[ukupnotrue];
            int brojacpozicija = 0;

            ;
            for (int i = 0; i < tabela.Length; i++)
            {
                if (tabela[i] == true)
                {
                    
                    int brojskokova = 0;
                    for (int k = i; k <Pokrivenost.Length; k= k +UnetiMintermi.Length)
                    {
                       
                        if (Pokrivenost[k] == true)
                        {
                            
                            pozicijeEsencijalnih[brojacpozicija] = brojskokova;
                            brojacpozicija++;
                            break;
                        }
                        brojskokova++;
                        
                    }
                }
            }
            pozicijeEsencijalnih = pozicijeEsencijalnih.Distinct().ToArray();
            Array.Sort(pozicijeEsencijalnih);
            return pozicijeEsencijalnih;
        }

        static bool [] TabelaPokrivenostiMinterma (string [] TabelaUproscenihMinterma, string [] UnetiMintermi)
        {
            bool[] tabelapokrivenosti = new bool[TabelaUproscenihMinterma.Length * UnetiMintermi.Length];
            int brojactabele = 0;
            // Punjenje tabele i uporedjivanje da li se sadrze ili ne
            for (int i = 0; i < TabelaUproscenihMinterma.Length; i++)
            {
                for (int k = 0; k < UnetiMintermi.Length; k++)
                {
                    char[] Niz1 = TabelaUproscenihMinterma[i].ToCharArray();
                    char[] Niz2 = UnetiMintermi[k].ToCharArray();
                    bool sadrziSe = true;
                    for (int z = 0; z < Niz1.Length; z++)
                    {
                        if (Niz1[z] == '-')
                        {
                            Niz2[z] = '-';
                        }
                        if (Niz1[z]!=Niz2[z])
                        {
                            sadrziSe = false;
                            break;
                        }
                    }
                    if (sadrziSe == true)
                    {
                        tabelapokrivenosti[brojactabele] = true;
                        brojactabele++;
                    }
                    else
                    {
                        tabelapokrivenosti[brojactabele] = false;
                        brojactabele++;
                    }
                }
            }
            return tabelapokrivenosti;

        }
        static string [] UproscavanjeUnetihMinterma(string [] tabelaUnetihMinterma)
        {
            int ukupanbrojMestaZaSazeteMinterme = 0;

            
            
            // Odredjuje koliko minterma mogu da udju u sazimanje 
            for (int i = 0; i < tabelaUnetihMinterma.Length ; i++)
            {
                for (int k = i+1; k < tabelaUnetihMinterma.Length; k++)
                {

   
                    if (BrojanjeRazlika(tabelaUnetihMinterma[i],tabelaUnetihMinterma[k]) ==1 && IstibrojElemenata(tabelaUnetihMinterma[i], tabelaUnetihMinterma[k]) == true)
                    {
                      
                        ukupanbrojMestaZaSazeteMinterme++;
                        
                    }
                }
            }
  
          

            // Ako nema minterma za sazimanje zavrsavamo operaciju
            if (ukupanbrojMestaZaSazeteMinterme == 0)
            {
                tabelaUnetihMinterma = tabelaUnetihMinterma.Distinct().ToArray();
                return tabelaUnetihMinterma;
            }
            // OdredjivanjeNesazetihMinterma
            int ukupanBrojZaNesazeteMinerme = 0;

            for (int i = 0; i < tabelaUnetihMinterma.Length; i++)
            {
                int mozedasesazme = 0;
                for (int k = 0; k < tabelaUnetihMinterma.Length; k++)
                {

                    if (BrojanjeRazlika(tabelaUnetihMinterma[i], tabelaUnetihMinterma[k]) == 1 
                        && IstibrojElemenata(tabelaUnetihMinterma[i], tabelaUnetihMinterma[k]) == true)
                    {

                        mozedasesazme++;
                        break;

                    }
                }
                if (mozedasesazme == 0)
                {
                    ukupanBrojZaNesazeteMinerme++;
                }
            }
            // Prazna tabela sa vrednostima sazetih minterma
            string[] privremeni = new string[ukupanbrojMestaZaSazeteMinterme + ukupanBrojZaNesazeteMinerme];
            int brojacprivremenog = 0;

            // sazimanje i unos u tabelu

            for (int i = 0; i < tabelaUnetihMinterma.Length; i++)
            {
                for (int k = i+1; k < tabelaUnetihMinterma.Length; k++)
                {
                    if (BrojanjeRazlika(tabelaUnetihMinterma[i], tabelaUnetihMinterma[k]) == 1
                        && IstibrojElemenata(tabelaUnetihMinterma[i], tabelaUnetihMinterma[k]))
                    {
                        privremeni[brojacprivremenog] = StavljanjeRecki(tabelaUnetihMinterma[i], tabelaUnetihMinterma[k]);
                        brojacprivremenog++;
                    }
                }
            }

            // unos onih koje ne mogu da se sazmu

            if (ukupanBrojZaNesazeteMinerme!= 0)
            {
                for (int i = 0; i < tabelaUnetihMinterma.Length; i++)
                {
                    int mozedasesazme = 0;
                    for (int k = 0; k < tabelaUnetihMinterma.Length; k++)
                    {

                        if (BrojanjeRazlika(tabelaUnetihMinterma[i], tabelaUnetihMinterma[k]) == 1
                            && IstibrojElemenata(tabelaUnetihMinterma[i], tabelaUnetihMinterma[k]) == true)
                        {

                            mozedasesazme++;
                            break;

                        }
                    }
                    if (mozedasesazme == 0)
                    {
                        privremeni[brojacprivremenog] = tabelaUnetihMinterma[i];
                        brojacprivremenog++;
                    }
                }
            }

            tabelaUnetihMinterma = privremeni;
            return UproscavanjeUnetihMinterma(tabelaUnetihMinterma);

        }
        static bool IstibrojElemenata (string prvi, string drugi)
        {
            int brojPrvi = 0;
            int brojDrugi = 0;
            for (int i = 0; i < prvi.Length; i++)
            {
                if (prvi[i] == '-')
                {
                    brojPrvi++;
                }
            }

            for (int i = 0; i < drugi.Length; i++)
            {
                if (drugi[i] == '-')
                {
                    brojDrugi++;
                }
            }

            bool kontrola = true;
            if (brojPrvi!= brojDrugi)
            {
                kontrola = false;
            }

            return kontrola;
        }
        static string StavljanjeRecki (string prvi,string drugi)
        {
            char[] Izmenjen = new char[prvi.Length];
            for (int i = 0; i < prvi.Length; i++)
            {
                Izmenjen[i] = prvi[i];
                if (prvi[i] != drugi[i])
                {
                    Izmenjen[i] = '-'; // Ovo je minus na num delu DA NE POMESAS!
                }
            }
            string vrednost = new string(Izmenjen);
            return vrednost;
        }
        static int BrojanjeRazlika(string Minterm,string Minterm2)
        {
            int ukupanBrojRazlika = 0;
            for (int i = 0; i < Minterm.Length; i++)
            {
                if (Minterm[i] != Minterm2[i])
                {
                    ukupanBrojRazlika++;
                }
            }
            
            return ukupanBrojRazlika;
        }
        static string KonvertovanjeUbinarno(int brojVarijabli,int unetbroj)
        {
            string binarnibroj = null;
            for (int i = 0; i < brojVarijabli; i++)
            {
                if (unetbroj%2 == 0)
                {
                    binarnibroj = binarnibroj + 0;
                    unetbroj = unetbroj / 2;
                }
                else
                {
                    binarnibroj = binarnibroj + 1;
                    unetbroj = (unetbroj - 1) / 2;
                }
            }
            binarnibroj = new string(binarnibroj.ToCharArray().Reverse().ToArray());
            return binarnibroj;
        }



        static void Main(string[] args)
        {
            Pocetak:
          
            Console.WriteLine("Unesite ukupan broj varijabli (0 za izlaz iz programa) : ");
            int brojvarijabli = int.Parse(Console.ReadLine());
            if (brojvarijabli ==0)
            {
                Environment.Exit(0);
            }

            Console.WriteLine("Unesite ukupan broj minterma za obradu (Ne moze biti veci ili jednak broju: {0} !)", Math.Pow(2, brojvarijabli));    
            int ukupanbrojMinterma = int.Parse(Console.ReadLine());
            while (ukupanbrojMinterma >= Math.Pow(2, brojvarijabli) )
            {
                Console.WriteLine("Nevalidan unos! Unesite ispravan broj");
                ukupanbrojMinterma = int.Parse(Console.ReadLine());
            }

            string[] TabelaUnetihMinterma = new string[ukupanbrojMinterma];
            

            for (int i = 0; i < ukupanbrojMinterma; i++)
            {
                Console.WriteLine("Unesite {0} minterm za obradu", i);
                int unetminterm = int.Parse(Console.ReadLine());
                while (unetminterm >= Math.Pow(2, brojvarijabli) 
                    || TabelaUnetihMinterma.Contains(KonvertovanjeUbinarno(brojvarijabli,unetminterm)) == true)
                {
                    Console.WriteLine("Nevalidan unos! ponovite unos za {0} minterm", i);
                    unetminterm = int.Parse(Console.ReadLine());
                }
                TabelaUnetihMinterma[i] = (KonvertovanjeUbinarno(brojvarijabli, unetminterm));


            }
            string [] TabelaUproscenihMinterma = UproscavanjeUnetihMinterma(TabelaUnetihMinterma);
            string[] kopija = new string [TabelaUproscenihMinterma.Length];
            Array.Copy(TabelaUproscenihMinterma, kopija, TabelaUproscenihMinterma.Length);
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("UDNF za unete vrednosti je:");
            Console.WriteLine("------------------------------------------------------------");
            Array.ForEach(TabelaUproscenihMinterma, Console.WriteLine);
            Console.WriteLine("------------------------------------------------------------");

            bool[] TabelaPokrivenosti = TabelaPokrivenostiMinterma(TabelaUproscenihMinterma, TabelaUnetihMinterma);


            int[] pozicijeMinterma = PozicijeEsencijalnih(TabelaUnetihMinterma, TabelaPokrivenosti);
            // drugi deo
            string[] tabelaIzbacenihMinterma = TabelaIzbacenihMinterma(TabelaUproscenihMinterma, pozicijeMinterma);

            
            bool[] TabelaStaStvarnoPokrivajuEsencijalne = PozicijeZaIzbacivanje(pozicijeMinterma, TabelaPokrivenosti, TabelaUnetihMinterma);

            bool[] TabelaBezNepMinterma = TabelaBezNepotrebnihMintermaZaPerika(TabelaStaStvarnoPokrivajuEsencijalne, tabelaIzbacenihMinterma, TabelaUnetihMinterma);

            int merac = MeracDuzine(TabelaStaStvarnoPokrivajuEsencijalne);
            

            // odredjivanje
            bool odredjivanje = true;
            for (int i = 0; i < TabelaStaStvarnoPokrivajuEsencijalne.Length; i++)
            {
                if (TabelaStaStvarnoPokrivajuEsencijalne[i] == false)
                {
                    odredjivanje = false;
                }
            }
            
            if (odredjivanje == true) 
            {
                Console.WriteLine("MDNF za unete vrednosti je:");
                Console.WriteLine("------------------------------------------------------------");
                IspisivanjeMDNF(kopija,pozicijeMinterma);
            }
            else
            {
                Console.WriteLine("MDNF za unete vrednosti je:");
                Console.WriteLine("------------------------------------------------------------");

                string[] NeEsencijalneU_MDNF = OdredjivanjeNE_ES_U_MDNF(tabelaIzbacenihMinterma, merac, TabelaBezNepMinterma);
                IspisivanjeMDNF(kopija, pozicijeMinterma, NeEsencijalneU_MDNF);


                


            }
           

            goto Pocetak;
           


         



        }
    }
}
