using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Drzewo_BST
{
    public class W
    {
        public int liczba_L;
        public int liczba_P;
        public W L;
        public W P;
        public W R;
    }

    static public class BST
    {
        static public void wypisz(W _korzen, int poziom)
        {
            string tabulacja = "";
            if (_korzen != null)
            {
                for (int i = 0; i < poziom; i++)
                {
                    tabulacja += "      ";
                }
                System.Console.Write(tabulacja);

                poziom++;
                System.Console.Write(_korzen.liczba_L);
                System.Console.Write(',');
                System.Console.Write(_korzen.liczba_P);
                System.Console.Write('\n');
                wypisz(_korzen.P, poziom);
                wypisz(_korzen.L, poziom);

            }
            else
            {
                for (int i = 0; i < poziom; i++)
                {
                    tabulacja += "      ";
                }
                System.Console.Write(tabulacja);
                System.Console.Write("brak");
                System.Console.Write('\n');
            }
        }

        static public void dodaj(int _liczba_L, int _liczba_P, W _korzen)
        {
            W nowy = new W();
            nowy.liczba_L = _liczba_L;
            nowy.liczba_P = _liczba_P;
            W rodzic = new W();

            while (_korzen != null)
            {
                nowy.R = _korzen;
                if (_korzen.liczba_L == _liczba_L && _korzen.liczba_P == _liczba_P)
                {
                    break;
                }

                else if (_korzen.liczba_L == _liczba_L && _korzen.liczba_P > _liczba_P)
                {
                    if (_korzen.L == null)
                    {
                        _korzen.L = nowy;
                        break;
                    }
                    else
                    {
                        _korzen = _korzen.L;
                    }
                }

                else if (_korzen.liczba_L == _liczba_L && _korzen.liczba_P < _liczba_P)
                {
                    if (_korzen.P == null)
                    {
                        _korzen.P = nowy;
                        break;
                    }
                    else
                    {
                        _korzen = _korzen.P;
                    }
                }

                else if (_korzen.liczba_L > _liczba_L)
                {
                    if (_korzen.L == null)
                    {
                        _korzen.L = nowy;
                        break;
                    }
                    else
                    {
                        _korzen = _korzen.L;
                    }
                }

                else if (_korzen.liczba_L < _liczba_L)
                {
                    if (_korzen.P == null)
                    {
                        _korzen.P = nowy;
                        break;
                    }
                    else
                    {
                        _korzen = _korzen.P;
                    }
                }
            }
        }

        static public string szukaj(int _liczba_L, int _liczba_P, W _korzen)
        {
            while (_korzen != null)
            {
                if (_korzen.liczba_L == _liczba_L && _korzen.liczba_P == _liczba_P)
                {
                    return "TAK";
                }

                else if (_korzen.liczba_L == _liczba_L && _korzen.liczba_P > _liczba_P)
                {
                    _korzen = _korzen.L;
                }

                else if (_korzen.liczba_L == _liczba_L && _korzen.liczba_P < _liczba_P)
                {
                    _korzen = _korzen.P;
                }

                else if (_korzen.liczba_L > _liczba_L)
                {
                    _korzen = _korzen.L;
                }

                else if (_korzen.liczba_L < _liczba_L)
                {
                    _korzen = _korzen.P;
                }
            }
            return "NIE";
        }

        static public int ile_liczb(int _liczba_L, W _korzen)
        {
            int ilosc = 0;
            while (_korzen != null)
            {
                if (_korzen.liczba_L == _liczba_L)
                {
                    ilosc++;
                    ilosc += ile_liczb(_liczba_L, _korzen.L);
                    ilosc += ile_liczb(_liczba_L, _korzen.P);

                    break;
                }

                else if (_korzen.liczba_L > _liczba_L)
                {
                    _korzen = _korzen.L;
                }

                else if (_korzen.liczba_L < _liczba_L)
                {
                    _korzen = _korzen.P;
                }
            }
            return ilosc;
        }

        static public void usun(int _liczba_L, int _liczba_P, W _korzen)
        {
            while (_korzen != null)
            {
                if (_korzen.liczba_L == _liczba_L && _korzen.liczba_P == _liczba_P)     // PODANA LICZBA JEST RÓWNA OBECNEJ PRZEGLĄDANEJ
                {
                    W rodzic = _korzen.R;                                   // POBRANIE RODZICA
                    //if (rodzic != null)
                    //{
                        if (_korzen.L == null && _korzen.P == null)         // OBAJ SYNOWIE SĄ NULLAMI
                        {
                            if (rodzic.L == _korzen)
                            {
                                rodzic.L = null;
                            }
                            else
                            {
                                rodzic.P = null;
                            }
                            break;
                        }

                        else if (_korzen.L != null && _korzen.P == null)    // TYLKO PRAWY SYN NIE JEST NULLEM
                        {

                            if (rodzic.L == _korzen)
                            {
                                rodzic.L = _korzen.L;
                            }
                            else
                            {
                                rodzic.P = _korzen.L;
                            }
                            _korzen = _korzen.L;
                            _korzen.R = rodzic;
                            break;
                        }

                        else if (_korzen.L == null && _korzen.P != null)    // TYLKO LEWY SYN NIE JEST NULLEM
                        {
                            if (rodzic.L == _korzen)
                            {
                                rodzic.L = _korzen.P;
                            }
                            else
                            {
                                rodzic.P = _korzen.P;
                            }
                            _korzen = _korzen.P;
                            _korzen.R = rodzic;
                            break;
                        }

                        else                                           // OBAJ SYNOWIE NIE SĄ NULLAMI
                        {
                            //---- WYCIĄGANIE NASTĘPNIKA ----//

                            W nastepnik = _korzen.P;
                            W tmp = _korzen.L;
                            W tmp2 = _korzen.P;
                            
                            while (nastepnik.L != null)                // SZUKANIE NASTĘPNIKA
                            {
                                nastepnik = nastepnik.L;
                            }
                            
                            if(nastepnik != _korzen.P)              // PRZYPADEK GDY NASTEPNIK TO PRAWY SYN USUWANEGO ELEMENTU
                            {
                                usun(nastepnik.liczba_L, nastepnik.liczba_P, _korzen);      // USUWANIE NASTEPNIKA                                                                                            // PRZYPISANIE NOWYCH SYNÓW DO NASTĘPNIKA
                                nastepnik.P = _korzen.P;
                                tmp2.R = nastepnik;
                            }

                                nastepnik.L = _korzen.L;                          
                           
                            //---- WSTAWIANIE NASTĘPNIKA ZAMIAST USUWANEGO ELEMENTU ----//

                            if (rodzic.L == _korzen)                   // JEŚLI USUWANY ELEMENT JEST LEWYM SYNEM
                            {
                                rodzic.L = nastepnik;                  // ZASTAPIENIE USUNIETEGO ELEMENTU (JEŚLI BYŁ PO LEWEJ STRONIE)                                
                            }

                            else                                       // JEŚLI USUWANY ELEMENT JEST PRAWYM SYNEM
                            {
                                rodzic.P = nastepnik;                  // ZASTAPIENIE USUNIETEGO ELEMENTU (JEŚLI BYŁ PO PRAWEJ STRONIE)
                            }

                            nastepnik.R = rodzic;                      // PRZYPISANIE RODZICA NASTEPNIKOWI
                            tmp.R = nastepnik;                         // USTAWIENIE RODZICA DLA LEWEGO SYNA NASTEPNIKA
                        }
                    //}

                  /* else
                   {           
                        W tmp = _korzen;
                       
                        if(tmp.P != null && tmp.L == null)
                        {
                            _korzen = _korzen.P;
                            tmp.liczba_L = _korzen.liczba_L;
                            tmp.liczba_P = _korzen.liczba_P;
                            tmp.P = _korzen.P;
                            tmp.L = _korzen.L;
                        }
                        else if(tmp.L != null && tmp.P == null)
                        {
                            _korzen = _korzen.L;
                            tmp.liczba_L = _korzen.liczba_L;
                            tmp.liczba_P = _korzen.liczba_P;
                            tmp.P = _korzen.P;
                            tmp.L = _korzen.L;
                        }
                        else
                        {
                            tmp.liczba_L = 0;
                            tmp.liczba_P = 0;
                        }    
                        
                    }*/

                    break;
                }

                else if (_korzen.liczba_L == _liczba_L && _korzen.liczba_P > _liczba_P)
                {
                    _korzen = _korzen.L;
                }

                else if (_korzen.liczba_L == _liczba_L && _korzen.liczba_P < _liczba_P)
                {
                    _korzen = _korzen.P;
                }

                else if (_korzen.liczba_L > _liczba_L)
                {
                    _korzen = _korzen.L;
                }

                else if (_korzen.liczba_L < _liczba_L)
                {
                    _korzen = _korzen.P;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (StreamWriter streamW = new StreamWriter("out.txt"))        // CZYSZCZENIE PLIKU OUT.TXT
            {
                streamW.Write(string.Empty);
            }

            W korzen = new W();
            string[] file = System.IO.File.ReadAllLines("duzy1.txt");      // POBRANIE LINIJEK Z PLIKU DO TABLICY STRING
            string[] tab_pom;
            int n;
            int x, y;

            tab_pom = file[0].Split(' ');
            n = int.Parse(tab_pom[0]);      // WYODRĘBNIENIE LICZBY POLECEŃ
            string c;

            for (int i = 1; i <= n; i++)
            {
                tab_pom = file[i].Split(' ');
                c = tab_pom[0];

                switch (c)
                {

                    case "W":

                        tab_pom = tab_pom[1].Split(',');
                        x = int.Parse(tab_pom[0]);
                        y = int.Parse(tab_pom[1]);

                        if (korzen.liczba_L == 0 && korzen.liczba_P == 0)
                        {
                            korzen.liczba_L = x;
                            korzen.liczba_P = y;
                        }
                        else
                        {
                            BST.dodaj(x, y, korzen);
                        }

                        break;

                    case "U":

                        tab_pom = tab_pom[1].Split(',');
                        x = int.Parse(tab_pom[0]);
                        y = int.Parse(tab_pom[1]);
                        BST.usun(x, y, korzen);
                        
                        break;

                    case "S":
                        
                        tab_pom = tab_pom[1].Split(',');
                        x = int.Parse(tab_pom[0]);
                        y = int.Parse(tab_pom[1]);
                        BST.szukaj(x, y, korzen);
                        using (StreamWriter streamW = new StreamWriter("out.txt", true))        
                        {
                            streamW.WriteLine(BST.szukaj(x, y, korzen));
                        }
                        break;

                    case "L":
                        
                        x = int.Parse(tab_pom[1]);
                        using (StreamWriter streamW = new StreamWriter("out.txt", true))
                        {
                            streamW.WriteLine(BST.ile_liczb(x, korzen));
                        }
                        
                        break;
                }
            }
            
            // BST.usun(9, 50, korzen);
            BST.wypisz(korzen, 0);
            //System.Console.Write(BST.ile_liczb(13, korzen));
            
            System.Console.ReadLine();
         
        }
    }
}
