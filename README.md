# CheeseBurgames_Repository
GameJam Jamon 2021
# Documento de Diseño de la Evaluación

## 1. Objetivo y preguntas de investigación de la evaluación

**Objetivo:**
Evaluar la claridad de la mecánica de spawnear los conejos.

**Preguntas de investigación:**
- -¿El jugador entiende cómo se deben mover los conejos?
- -¿El jugador entiende el orden de creación en el juego del nacimiento de los conejos?
- -¿Entiende que una vez spawneado el siguiente conejo, no puede mover el anterior?

## 2. Métricas que se van a utilizar para validar el objetivo

A continuación consideramos “hacer click” como la acción correspondiente al evento popularmente conocido como “mousedown”.

**Métricas:**
- -Distribución de las veces que hace click un jugador sobre un conejo no nacido en cada nivel.
  - -Esta métrica nos ayuda a ver si el jugador ha entendido que no se puede alterar el orden de creación de conejos en el juego y que no se pueden spawnear los conejos de alguna forma diferente a pulsar el botón “Born”.
- -Distribución de las veces que hacen clicks los jugadores sobre un conejo ya spawneado en cada nivel.
  - -Esta métrica nos ayuda a ver si el jugador ha entendido que tras spawnear un conejo, no se pueden mover los anteriores.

## 3. Eventos que van a ser recogidos por el sistema de telemetría

**Inicio y fin de una sesión de juego:**
- **Evento inicio de sesión:** Este evento indica el comienzo de una partida y se origina cuando se muestra en pantalla el menú principal tras la carga de recursos. Cuenta con los siguientes parámetros:
  - -event_id
  - -event_timestamp (Tiempo Unix)
- **Evento fin de sesión:** Este evento indica el final de una partida y se origina cuando se cierra el juego pulsando sobre el botón “Quit” o por una causa externa. Cuenta con los siguientes parámetros:
  - -event_id
  - -event_timestamp (Tiempo Unix)

**Inicio y fin de un nivel con información adicional de su finalización:**
- **Evento inicio de nivel:** Este evento indica el comienzo de un nivel y se origina cuando el jugador pulsa sobre el botón de un determinado nivel o reinicia la partida utilizando el botón de “Reset”. Cuenta con los siguientes parámetros:
  - -event_id
  - -event_timestamp (Tiempo Unix)
  - -level_id (número nivel que se ha iniciado)
- **Evento fin de nivel:** Este evento indica el final de un nivel y se origina cuando el jugador supera el nivel llegando hasta la zanahoria o sale del nivel hacia otro menú a través del botón de pausa. Cuenta con los siguientes parámetros:
  - -event_id
  - -event_timestamp (Tiempo Unix)
  - -level_id (número nivel que ha finalizado)

**Click al botón “Born”:**
- **Evento de pulsar el botón “Born”:** Este evento indica que se ha pulsado sobre el botón “Born” que se encuentra dentro del juego. Cuenta con los siguientes parámetros:
  - -event_id
  - -event_timestamp (Tiempo Unix)
  - -current_level

**Click al botón “Go”:**
- **Evento de pulsar el botón “Go”:** Este evento indica que se ha pulsado sobre el botón “Go” que se encuentra dentro del juego. Cuenta con los siguientes parámetros:
  - -event_id
  - -event_timestamp (Tiempo Unix)
  - -current_level

**Click a cada conejo:**
- **Evento de pulsar a un conejo de la UI:** Este evento indica que se ha pulsado sobre un conejo que se encuentra en el panel izquierdo (UI) de la pantalla de juego; es decir, que aún no ha sido utilizado para resolver el nivel. Cuenta con los siguientes parámetros:
  - -event_id
  - -event_timestamp (Tiempo Unix)
  - -current_level
- **Evento de pulsar a un conejo spawneado:** Este evento indica que se ha pulsado sobre un conejo que ya ha sido utilizado en el nivel. Cuenta con los siguientes parámetros:
  - -event_id
  - -event_timestamp (Tiempo Unix)
  - -current_level
