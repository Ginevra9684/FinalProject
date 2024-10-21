## MasterMind: progettazione

-  [x] Generare una lista di colori tra cui il Codificatore potrà scegliere
-  [ ] Chiedere all'utente se ha intenzione di essere Codificatore o Decodificatore

## Wishlist
-  [x] Mermaid
-  [ ] Rendere l'interfaccia appetibile con un panel
-  [ ] Possibilità di salvare il nome dell'utente e il suo punteggio (inventare un sistema per punteggio (basato sul numero di turni?))
-  [ ] Implementare funzioni
-  [ ] Livelli di difficoltà

### Versione 1.0: capire la logica inserendo i confronti manualmente
-  [ ] Chiedere all'utente il suo nome: rendere persistente questa info con anche il suo punteggio alla fine del gioco
-  [x] Generare una lista di colori tra cui il Codificatore potrà scegliere
-  [x] Generare singolarmente i colori estratti dalla CPU
-  [x] Creare singolarmente menu multiselezione per scelta colori
-  [x] Ciclo while [break: numero tentativi: 10; codiceCpu = codiceSegreto]: comparazione tra codice manuale (16 if)

### Versione 2.0: una volta capire la logica dietro ai confronti manuali, sintetizzarli tramite cicli for
-  [ ] Chiedere all'utente il suo nome: rendere persistente questa info con anche il suo punteggio alla fine del gioco
-  [x] Generare una lista di colori tra cui il Codificatore potrà scegliere
-  [x] Generare  i colori estratti dalla CPU e inserirli in un array
-  [x] Creare  menu multiselezione per scelta colori e inserimento in un array
-  [x] Ciclo while [break: numero tentativi: 10; codiceCpu = codiceSegreto]: comparazione con for
-  [ ] Possibilità di tenere i risultati tracciati tramite tabella



```mermaid
flowchart TD
    A(Welcome! \n Let's play MasterMind) -->|print: rules| B{{code generated: \n CPU picks 4 random colours from a 6 colours palette}}
    B --loop--> C([new attemp: \n input guess code])
    C --> D(if 1° guess colour == 1° CPU's colour)
    D --true \n black dot--> E(if 2° guess colour == 2° CPU's colour)    
        D --false--> J(if 1° guess colour == 2° CPU's colour \n OR \n if 1° guess colour == 3° CPU's colour \n OR \n if 1° guess colour == 4° CPU's colour)--true \n white dot--> E
        J --false \n no dot--> E
    
    E --true \n black dot--> F(if 3° guess colour == 3° CPU's colour)
        E --false--> L(if 2° guess colour == 1° CPU's colour \n OR \n if 2° guess colour == 3° CPU's colour \n OR \n if 2° guess colour == 4° CPU's colour)--true \n white dot -->F
        L--false \n no dot-->F
        F --true \n black dot--> G(if 4° guess colour == 4° CPU's colour)
    
    F --false-->N(if 3° guess colour == 1° CPU's colour \n OR \n if 3° guess colour == 2° CPU's colour \n OR \n if 3° guess colour == 4° CPU's colour)--true \n white dot-->G
    N--false \n no dot-->G
    G --true--> H(WE WON)
    G--false-->O(if 4° guess colour == 1° CPU's colour \n OR \n if 4° guess colour == 2° CPU's colour \n OR \n if 4° guess colour == 3° CPU's colour)--true \n white dot -->C
    O--false-->C

B-->P(if attemps = 10 \n OR \n 1° guess = 1° CPU's colour AND 2°guess =2°CPU's colour AND 3°guess =3°CPU's colour AND 4°guess =4°CPU's colour)-->Q(WE LOST)
```