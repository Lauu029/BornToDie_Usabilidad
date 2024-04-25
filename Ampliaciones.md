# Hebra de persistencia

En la clase _FilePersistence_, se ha añadido una hebra de ejecución paralela, la cual está encargada de serializar y persistir los eventos almacenados en una cola. 

La serialización de los eventos no se encuentra dentro de la clase _FilePersistence_, se hace según la estrategia de serialización seleccionada en el _Tracker_, no obstante, las llamadas a esta estrategia las realiza la hebra de persistencia.

Aunque esta hebra nos permite persistir los elementos cada cierto tiempo sin tener que estar, desde la instrumentalización del juego, llamando constantemente al tracker, hemos dejado la opción de poder forzar un volcado de la cola de eventos desde el juego. Esto se hace enviando una señal a la hebra, un booleano que indica que tiene que volcar el contenido de la cola de eventos, aunque no haya transcurrido el tiempo predefinido de volcado.

# Serialización en formato CSV
Nuestro tracker da soporte a la serialización en formato CSV.

# Tracker configurable por datos
El tracker se puede configurar por datos a través de Unity. Como el Tracker no es Monobehaviour, usamos un objeto intermedio para aportar los datos al tracker. En nuestro caso utilizamos la clase GameManager, donde se puede configurar el tipo de persistencia, el tipo de serialización, así como la frecuencia de volcado de los datos en milisegundos.

# Anotaciones
Aunque el tipo de persistencia es ampliable y configurable con más estrategias, la persistencia a servidor en nuestro Tracker está declarada, pero no está implementada.
