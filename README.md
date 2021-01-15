# Progetto-POIS

Titolo progetto:	WordsGsme<br>

### Specifica Progetto
Il progetto consiste nella realizzazione di un gioco di parole composto da tre livelli con difficoltà crescente.

Ogni livello deve contenere:
- un titolo con l'indicazione del livello;
- una descrizione che mostri la modalità di gioco;
- un punteggio aggiornato in tempo reale in base alla logica di gioco, che consiste nella scelta di lettere vicine tra loro per la composizione di parole di senso compiuto, avvalendosi di un dizionario per la verifica;
- una griglia di lettere estratte casualmente dall'alfabeto italiano, ad ogni lettera è assegnato un valore che si basa sulla difficoltà di composizione nella lingua italiana;
- un bottone per fare il reload del gioco.

Requisiti di gioco dei singoli livelli:
- il 1° livello è composto da una griglia di lettere 5x5 (25 lettere), sarà considerato superato se sono stati totalizzati almeno 30 punti;
- il 2° livello è composto da una griglia di lettere 6x6 (36 lettere), sarà considerato superato se sono stati totalizzati 60 punti;
- il 3° livello è composto da una griglia di lettere 7x7 (49 lettere), sarà considerato superato se sono stati totalizzati 120 punti:
  essendo l'ultimo livello, se superato il gioco termina;
- Sono ammesse concorrenti al punteggio solo parole di minimo 4 lettere;
- la composizione delle parole, sarà effettuata con le seguenti indicazioni: una volta selezionata la prima lettera sarà possibile selezionare solo lettere adiacenti, anche in selezione diagonale. Ad eccezion fatta se la lettera rimane nei bordi della griglia allora sarà possibile selezionare altre lettere anch'esse appartenenti al bordo esterno.
