using System.Collections.Generic;
using UnityEngine;

namespace Matrix
{
    public static class VoodooCharacterSheet
    {
        public static List<string> CharacterSheet;

        static VoodooCharacterSheet()
        {
            CharacterSheet = new List<string>();

            // kanji
            CharacterSheet.Add('\u65e5'.ToString());

            // katakana
            CharacterSheet.Add('\uff8a'.ToString());
            CharacterSheet.Add('\uff90'.ToString());
            CharacterSheet.Add('\uff8b'.ToString());
            CharacterSheet.Add('\uff70'.ToString());
            CharacterSheet.Add('\uff73'.ToString());
            CharacterSheet.Add('\uff7c'.ToString());
            CharacterSheet.Add('\uff85'.ToString());
            CharacterSheet.Add('\uff93'.ToString());
            CharacterSheet.Add('\uff86'.ToString());
            CharacterSheet.Add('\uff7b'.ToString());
            CharacterSheet.Add('\uff9c'.ToString());
            CharacterSheet.Add('\uff82'.ToString());
            CharacterSheet.Add('\uff75'.ToString());
            CharacterSheet.Add('\uff98'.ToString());
            CharacterSheet.Add('\uff71'.ToString());
            CharacterSheet.Add('\uff8e'.ToString());
            CharacterSheet.Add('\uff83'.ToString());
            CharacterSheet.Add('\uff8f'.ToString());
            CharacterSheet.Add('\uff79'.ToString());
            CharacterSheet.Add('\uff92'.ToString());
            CharacterSheet.Add('\uff73'.ToString());
            CharacterSheet.Add('\uff76'.ToString());
            CharacterSheet.Add('\uff77'.ToString());
            CharacterSheet.Add('\uff91'.ToString());
            CharacterSheet.Add('\uff95'.ToString());
            CharacterSheet.Add('\uff97'.ToString());
            CharacterSheet.Add('\uff7e'.ToString());
            CharacterSheet.Add('\uff88'.ToString());
            CharacterSheet.Add('\uff7d'.ToString());
            CharacterSheet.Add('\uff80'.ToString());
            CharacterSheet.Add('\uff87'.ToString());
            CharacterSheet.Add('\uff8d'.ToString());
            CharacterSheet.Add('\uff66'.ToString());
            CharacterSheet.Add('\uff72'.ToString());
            CharacterSheet.Add('\uff78'.ToString());
            CharacterSheet.Add('\uff7a'.ToString());
            CharacterSheet.Add('\uff7f'.ToString());
            CharacterSheet.Add('\uff81'.ToString());
            CharacterSheet.Add('\uff84'.ToString());
            CharacterSheet.Add('\uff89'.ToString());
            CharacterSheet.Add('\uff8c'.ToString());
            CharacterSheet.Add('\uff94'.ToString());
            CharacterSheet.Add('\uff96'.ToString());
            CharacterSheet.Add('\uff99'.ToString());
            CharacterSheet.Add('\uff9a'.ToString());

            // numbers 
            CharacterSheet.Add("0");
            CharacterSheet.Add("6");
            CharacterSheet.Add("8");
            CharacterSheet.Add("9");
            CharacterSheet.Add('\u0196'.ToString());
            CharacterSheet.Add('\u1105'.ToString());
            CharacterSheet.Add('\u0190'.ToString());
            CharacterSheet.Add('\u3123'.ToString());
            CharacterSheet.Add('\u03db'.ToString());
            CharacterSheet.Add('\u3125'.ToString());

            // special characters
            CharacterSheet.Add('\u003a'.ToString());
            CharacterSheet.Add('\u30fb'.ToString());
            CharacterSheet.Add('\u002e'.ToString());
            CharacterSheet.Add('\u0022'.ToString());
            CharacterSheet.Add('\u003d'.ToString());
            CharacterSheet.Add('\u002a'.ToString());
            CharacterSheet.Add('\u002b'.ToString());
            CharacterSheet.Add('\u002d'.ToString());
            CharacterSheet.Add('\u003c'.ToString());
            CharacterSheet.Add('\u003e'.ToString());

            // other
            CharacterSheet.Add('\u00a6'.ToString());
            CharacterSheet.Add('\uff5c'.ToString());
            CharacterSheet.Add('\u00e7'.ToString());

            // roman
            CharacterSheet.Add("Z");
        }

        public static string GetRandomCharacter()
        {
            return CharacterSheet[Random.Range(0, CharacterSheet.Count)];
        }
    }
}
