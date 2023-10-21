# Documentación de scripts para el jugador

## PlayerController.cs
El script principal "PlayerController.cs" emplea distintos métodos para controlar al jugador:
### ReadInput() y Move()
El método "ReadInput()" se encarga de obtener la entrada del control del jugador, ya sea mediante un teclado o controlador, y guarda esta entrada en una variable "Vector2" normalizada. Más adelante, esta entrada se usa para cambiar la velocidad del objeto jugador en el método "Move()", empleando "Vector2.SmoothDamp()" para obtener un movimiento más suavizado.
### Shoot()
Este método se encarga generar las balas que el jugador disparará, controlado mediante un contador para evitar una generación infinita. Las balas que se generan se obtienen a través de otro script, "PoolController()", encargado también de la optimización.
### Knockback()
"Knockback()" se encarga de mover al jugador hacia cierta dirección dependiendo del lugar donde fue impactado. Este método se llama desde otro script.

## PlayerHealth.cs
Este script se encarga de controlar la vida del jugador.
### Update()
Este es un método propio de Unity, dentro del mismo, se encarga controlar los sprites de la vida, cambiando entre la estrella rota o la estrella completa; además de evitar que exista vida negativa.
### TakeDamage()
Este método se llama al detectar una colisión con las balas enemigas, reduce la vida en uno y llama a otros dos métodos "ScreenShake()" y "Konckback()", además de iniciar dos corrutinas "NoCollission()" y "NoControl()"
### ScreenShake()
Este es un script externo que hace vibrar la pantalla
### NoCollission() y NoControl()
"NoCollission()" se encarga de desactivar las colisiones del jugador contra el enemigo, cambiando al primero de capa y evitando que se detecten, además de vovler transparente al jugador; "NoControl() impide que se pueda controlar al jugador. Ambas corrutinas vuelven a activar sus características luego de una fracción de segundo.