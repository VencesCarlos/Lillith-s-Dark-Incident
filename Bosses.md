# Documentación de scripts para los jefes

El controlador para el jefe sigue en proceso. De momento, se usa un script obsoleto temporal que cuenta con poca organización y mala legibilidad. A continuación, se documenta el controlador que se encuentra en desarrollo.

## BossController.cs
Clase base que se encarga de ciertas acciones que todos los jefes usarán-
### ChangeState()
Es una corrutina que, cada cierto tiempo, llama al método "Choose()" para seleccionar un valor aleatorio que se regresará para que otros jefes puedan usarlo.
### Choose()
Método utilizado desde la documentación de Unity. Recibe un arreglo de probabilidades y regresa el índice del arreglo en función de estas.
### MoveToDestination()
Se encarga de transportar al jefe hacia una dirección determinada.

## FloeraController()
Controlador para el primer jefe: Floera Sakr'. Hereda de "BossController.cs"
### Enum de estados
Una varible de tipo enum que contiene a los estados del jefe: MoveAttack, RushAttack y HellAttack.
### MoveAttack()
Desplaza de un lado a otro al jefe, activando una de las lluvias de balas definidas.
### RushAttack()
Se desplaza rápidamente a la posición del jugador, activando una de las lluvias de balas definidas.
### HellAttack()
El jefe se va al centro de la pantalla y comienza la lluvia de balas mayor con un patrón predefinido.
