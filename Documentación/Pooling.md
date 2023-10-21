# Documentación de scripts para el pooling

## PoolController.cs
Encargado de reciclar las balas usadas para mejorar el rendimiento
### Awake()
Es un método propio de Unity, en este se inicializa el script como una instancia para que otros scripts puedan utilizarlo
### NewBullet()
Instancia una nueva bala y la introduce en un arreglo de GameObjects para que pueda ser usada posteriormente
### ShootBullet()
Recorre el arreglo de balas y activa alguna de estas para dispararlo, en caso de que no haya balas disponibles llama al método "NewBullet()" para crear una nueva.

## LillithBullet.cs
Script básico para mover la bala
### Update()
En cada frame, hace que la bala se eleve.
### OnTriggerEnter2D()
Al detectar una colisión, ya sea con el object pooling o con un enemigo, hace que la bala se desactive.