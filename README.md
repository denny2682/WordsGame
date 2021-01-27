# Progetto-POIS

Titolo progetto:	WordsGame<br>
Autore progetto:    Denny Bini

### Specifica Progetto

Il progetto consiste nella realizzazione di un gioco di parole composto da tre livelli con difficoltà crescente, 
in linguaggio c# windows-based in considerazione anche di una portabilità futura verso il sistema operativo Android.

Ogni livello deve contenere:
- Un titolo con l'indicazione del livello; 
- Una descrizione che mostri la modalità di gioco; 
- Un punteggio aggiornato in tempo reale in base alla logica di gioco; 
- Una griglia di lettere estratte casualmente dall'alfabeto italiano. Ad ogni lettera è assegnato un valore che si basa sulla difficoltà di composizione nella lingua italiana; 
- Un bottone per richiedere il ricaricamento del livello; 
- Un bottone per richiedere il ricaricamento del gioco. 

Requisiti di gioco dei singoli livelli:
- Il 1° livello è composto da una griglia di lettere 5x5 (25 lettere), sarà considerato superato se sono stati totalizzati almeno 30 punti; 
- Il 2° livello è composto da una griglia di lettere 6x6 (36 lettere), sarà considerato superato se sono stati totalizzati almeno 60 punti; 
- Il 3° livello è composto da una griglia di lettere 7x7 (49 lettere), sarà considerato superato se sono stati totalizzati almeno 120 punti: essendo l'ultimo livello, se superato il gioco termina; 
- Sono ammesse concorrenti al punteggio solo parole di minimo 4 lettere; 
- Non possono essere selezionate nello stesso livello parole precedentemente composte; 
- La composizione delle parole, sarà effettuata con le seguenti indicazioni: una volta selezionata la prima lettera sarà possibile selezionare solo lettere adiacenti all'intera selezione, anche con scelta diagonale. Eccezion fatta se la lettera rimane nei bordi della griglia, in tal caso sarà possibile selezionare altre lettere anch'esse appartenenti al bordo esterno. 


