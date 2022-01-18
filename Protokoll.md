Linus Dehner

## Protokoll

#### Design

Der generelle Aufbau, ist wie auch in den Vorlesungen gestaltet. Die Battle logic habe ich so gestaltet, dass es Regeln gibt, die man auf Karten anwenden kann (eine Karte hat eine Liste von Regeln). Die Regeln werden automatisch bestimt und auf die Karte hinzugefügt. Eine Karte kann entweder eine Spell Karte sein, die von Card erbt, oder eine Monster Karte. Im Battle wird dann pro Runde eine zufällige Karte aus beiden Decks gezogen und die Regeln werden angewandt. Danach wird der Gewinner und Verlierer zurück gegeben, sowie der Verlauf der Runde.

UML:

https://s3.us-west-2.amazonaws.com/secure.notion-static.com/2d6b72b7-6f9d-44f4-9bb5-18f2e087c096/include.svg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Content-Sha256=UNSIGNED-PAYLOAD&X-Amz-Credential=AKIAT73L2G45EIPT3X45%2F20220116%2Fus-west-2%2Fs3%2Faws4_request&X-Amz-Date=20220116T215114Z&X-Amz-Expires=86400&X-Amz-Signature=fe9cb0c88a0d9553b48fc1408132655118a80729945e0fbac3d210841ae69a10&X-Amz-SignedHeaders=host&response-content-disposition=filename%20%3D%22include.svg%22&x-id=GetObject



#### Lessons learned

- Umfang des Projekts nicht unterschätzen
- Nicht erst im Nachhinein dokumentieren
- using verwenden, da sonst Npgsql Errors auftreten



#### Testing

- Card zu UniversalCard
- UniversalCard zu Card
- UserRequestCard zu Universal Card
- Regeln testen
- Battle testen



#### Unique Feature

- List Battles
  - Zeigt alle battles des users an

- Get specific Battle
  - Zeitgt ein bestimmtes battle eines users an




#### Time

Zirka 50 Stunden



#### Github

https://github.com/ldehner/SWEN1-MonsterTradingCardsGame

