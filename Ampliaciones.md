# Hebra de persistencia

En la clase FilePersistence, se ha añadido una hebra de ejecución paralela, la cual está encargada de serializar y persistir los eventos almacenados en una cola. 

La serialización de los eventos no se encuentra dentro de la clase FilePersistence, se hace según la estrategia de serialización seleccionada en el Tracker, no obstante, las llamadas a esta estrategia las realiza la hebra de persistencia.

Aunque esta hebra nos permite persistir los elementos cada cierto tiempo sin tener que estar, desde la instrumentalización del juego, llamando constantemente al tracker, hemos dejado la opción de poder forzar un volcado de la cola de eventos desde el juego. Esto se hace enviando una señal a la hebra, un booleano que indica que tiene que volcar el contenido de la cola de eventos, aunque no haya transcurrido el tiempo predefinido de volcado.

La frecuencia de volcado de los datos se puede modificar cambiando la constante updateFrequenceInMiliseconds situado en la clase FilePersistence.

# Serialización en formato CSV
Nuestro tracker da soporte a la serialización en formato CSV.
