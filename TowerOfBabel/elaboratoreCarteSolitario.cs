/*
 *  This code is distribuited under GPL 3.0 or, at your opinion, any later version
 *  CBriscola 0.1
 *
 *  Created by numerunix on 22/05/22.
 *  Copyright 2022 Some rights reserved.
 *
 */

using System;
namespace org.altervista.numerone.framework
{
    public class ElaboratoreCarteSolitario : ElaboratoreCarte
    {
        private const ushort numeroCarte = 10;
        private bool[] doppione;
        public static Random r = new Random();
        public ElaboratoreCarteSolitario()
        {
            doppione = new bool[10];
            for (int i = 1; i < 9; i++)
                doppione[i] = false;
            doppione[0] = true;
        }
        public ushort GetCarta()
        {
            ushort fine = (ushort)(r.Next(1, 9) % numeroCarte),
            carta = (ushort)((fine + 1) % numeroCarte);
            while (doppione[carta] && carta != fine)
                carta = (ushort)((carta + 1) % numeroCarte);
            if (doppione[carta])
                throw new ArgumentException("Chiamato elaboratoreCarteItaliane::getCarta() quando non ci sono più carte da elaborare");
            else
            {
                doppione[carta] = true;
                return carta;
            }
        }

    }
}