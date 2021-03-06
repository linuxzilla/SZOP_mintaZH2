﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        //Órai feladat: minta 2. ZH
        //Készíts egy kliens-szerver alkalmazást egy népességnyilvántartó rendszer("ügyfélkapu") megvalósítására!

        //A szervernek kezelnie kell felhasználókat.Minden felhasználóról tároljuk: usernév, jelszó, teljes név, lakhely (településnév), bűncselekmények listája("criminal record").

        //A felhasználók adatait egy XML fájl tartalmazza, melyet Neked kell összeállítanod pár user adatával.A szerver induláskor olvassa be az adatokat, majd leálláskor írja vissza az XML fájlba!

        //A szervernek támogatni kell az alábbi funkciókat:

        //LOGIN|<usernév>|<jelszó>: Bejelentkezteti az adott felhasználót.
        //LOGOUT: Kijelentkezteti az adott klienst a szerverről
        //NOCRIME: Listázza azokat a usereket, akiknek tiszta a "criminal record"-ja.
        //EXIT: A kliens kilép.

        //A következő funkciók csak bejelentkezés után érhetőek el:

        //NAME|<usernév>: Visszaadja a megadott usernévhez tartozó teljes nevet.
        //LOCALS|<településnév>: Visszaadja a megadott településen élő emberek userneveinek listáját.
        //ADDCRIME|<usernév>|<bűncselekmény>: Az adott bűncselekménnyel bővíti az adott user "criminal record"-ját.
        //PARDON|<usernév>: Az adott usert kegyelemben részesíti az összes bűne alól (törli a "criminal record"-ját), de csak akkor, ha a bejelentkezett felhasználó neve "donald_trump".

        //Szorgalmi: Vezess be a userekhez szerepköröket("role") és bizonyos funkciókat csak az adott role-ba tartozó userek tudjanak elérni! Például az ADDCRIME funkciót csak "judge" role-lal lehessen elérni, a PARDON-t csak "president"-tel, stb

        static IPAddress ServerIp = IPAddress.Parse(ConfigurationManager.AppSettings.Get("ipaddress"));
        static int ServerPort = int.Parse(ConfigurationManager.AppSettings.Get("port"));

        static void ServerListening(TcpListener listener)
        {
            do
            {
                // wait van, amíg egy kliens nem csatlakozik
                ClientThread client = new ClientThread(listener.AcceptTcpClient());

                Thread thread = new Thread(client.StartThread);
                thread.Start();
            } while (true);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Server satarting");
            try
            {
                DataManager.ReadUsersFromXml(@"Database/UsersData.xml");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Database not found!");
            }
            TcpListener listener = new TcpListener(ServerIp, ServerPort);
            listener.Start();

            Thread th = new Thread(() => ServerListening(listener));
            th.Start();

            Console.WriteLine("Ha meguntad a szerver futását, nyomj le egy billentyűt!");
            Console.ReadKey();

            Console.WriteLine("A szerver leállítását elkezdem...");

            th.Abort();

            Console.WriteLine("A szerver leállítottam...");

            listener.Stop();

            //User.WriteToXmlFile("users.xml");

            Console.ReadKey();

            Console.ReadKey();
        }
    }
}
