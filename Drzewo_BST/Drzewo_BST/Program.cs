using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            while (_korzen != null)
            {
                if (_korzen.liczba_L == _liczba_L && _korzen.liczba_P == _liczba_P)
                {
                    break;
                }

                else if (_korzen.liczba_L == _liczba_L && _korzen.liczba_P > _liczba_P)
                {
                    if (_korzen.L == null)
                    {
                        _korzen.L = nowy;
                    }
                    else
                    {
                        nowy.R = _korzen;
                        _korzen = _korzen.L;
                    }
                }

                else if (_korzen.liczba_L == _liczba_L && _korzen.liczba_P < _liczba_P)
                {
                    if (_korzen.P == null)
                    {
                        _korzen.P = nowy;
                    }
                    else
                    {
                        nowy.R = _korzen;
                        _korzen = _korzen.P;
                    }
                }

                else if (_korzen.liczba_L > _liczba_L)
                {
                    if (_korzen.L == null)
                    {
                        _korzen.L = nowy;
                    }
                    else
                    {
                        nowy.R = _korzen;
                        _korzen = _korzen.L;
                    }
                }

                else if (_korzen.liczba_L < _liczba_L)
                {
                    if (_korzen.P == null)
                    {
                        _korzen.P = nowy;
                    }
                    else
                    {
                        nowy.R = _korzen;
                        _korzen = _korzen.P;
                    }
                }
            }
            /*
            if (_korzen == null)
            {
                _korzen = new W();
                _korzen.liczba_L = _liczba_L;
                _korzen.liczba_P = _liczba_P;
            }
            */
        }

        static public bool szukaj(int _liczba_L, int _liczba_P, W _korzen)
        {
            while (_korzen != null)
            {
                if (_korzen.liczba_L == _liczba_L && _korzen.liczba_P == _liczba_P)
                {
                    return true;
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
            return false;
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
                    W rodzic = _korzen.R;                           // POBRANIE RODZICA

                    if (_korzen.L == null && _korzen.P == null)     // OBAJ SYNOWIE SĄ NULLAMI
                    {

                        if (rodzic.L == _korzen)
                        {
                            rodzic.L = null;
                        }
                        else
                        {
                            rodzic.P = null;
                        }
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
                    }

                    else                                           // OBAJ SYNOWIE NIE SĄ NULLAMI
                    {

                        //---- WYCIĄGANIE NASTĘPNIKA ----//

                        W nastepnik = _korzen.P;

                        while (nastepnik.L != null)                // SZUKANIE NASTĘPNIKA
                        {
                            nastepnik = nastepnik.L;
                        }

                        usun(nastepnik.liczba_L, nastepnik.liczba_P, _korzen);      // USUWANIE NASTEPNIKA

                        //---- WSTAWIANIE NASTĘPNIKA ZAMIAST USUWANEGO ELEMENTU ----//

                        nastepnik.L = _korzen.L;                   // PRZYPISANIE NOWYCH SYNÓW DO NASTĘPNIKA
                        nastepnik.P = _korzen.P;

                        if (rodzic.L == _korzen)                   // JEŚLI USUWANY ELEMENT JEST LEWYM SYNEM
                        {
                            rodzic.L = nastepnik;                  // ZASTAPIENIE USUNIETEGO ELEMENTU (JEŚLI BYŁ PO LEWEJ STRONIE)
                        }

                        else                                       // JEŚLI USUWANY ELEMENT JEST PRAWYM SYNEM
                        {
                            rodzic.P = nastepnik;                  // ZASTAPIENIE USUNIETEGO ELEMENTU (JEŚLI BYŁ PO PRAWEJ STRONIE)
                        }

                    }
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
            W korzen = new W();
            korzen.liczba_L = 10;
            korzen.liczba_P = 50;

            BST.dodaj(9, 50, korzen);
            BST.dodaj(11, 50, korzen);
            BST.dodaj(10, 0, korzen);
            BST.dodaj(11, 0, korzen);
            BST.dodaj(9, 0, korzen);
            BST.dodaj(14, 0, korzen);
            BST.dodaj(15, 0, korzen);
            BST.dodaj(13, 0, korzen);
            BST.usun(14, 0, korzen);
            // BST.usun(9, 50, korzen);
            BST.wypisz(korzen, 0);
            System.Console.Write(BST.ile_liczb(13, korzen));
            System.Console.ReadLine();
        }
    }
}
