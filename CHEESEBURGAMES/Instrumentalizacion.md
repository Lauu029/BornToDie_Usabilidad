Las clases que se han modificado para hacer la instrumentalización del juego son las siguientes:
- GameManager. Es la encargada de iniciar y cerrar el singleton del tracker. También manda los eventos de cambio de nivel.
    - En el método NextLevel manda un evento de fin de nivel 
    - En el método ChangeLevel manda el evento de inicio de nivel
    - El método OnApplicationQuit cierra el tracker y en el método Awake se inicializa

- Se ha añadido a los conejos de la UI un componente "button", que manda al tracker un evento de click en los conejos de la UI

- A los conejos que se spawnean en la pantalla (Explosion_minion, FlyMinion, SmallMinion, Trampoline_minion) les hemos añadido un script llamado "TrackerCallerSpawned" que se encarga de mandar un evento de click en los conejos spawneados con un método OnClick, que detecta si se ha pulsado el ratón en la posición del conejo.

- En el prefab SigMinion hemos añadido dos scripts: TrackerCallerBornClick y TrackerCallerGoClick, que tienen un método send event que se llama desde el OnClick del botón "born" y "Go" para mandar en cada uno su evento correspondiente.