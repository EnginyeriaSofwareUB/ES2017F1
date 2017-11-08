Permanencia de los scripts:
  Tal y como hemos hablado en las reuniones, la creación de sloths se hace en la escena de selección de equipos, y más concretamente en el script teamSelection.cs . Por ello, 
	para garantizar la permanencia de los sloths en la escena siguiente, se ha de llamar a la función dontDestroyOnLoad(). En un principio se llamaba directamente a esta función
	en el propio script, no obstante, si en un futuro queremos añadir más variables que no se destruyan entre escenas, tendríamos que llamar a esta función cada vez que quisiéramos
	garantizar su permanencia. 
	Por ello, he pensado que la mejor opción sería crear una clase Singleton (llamada Singleton.cs) que se encargara directamente del control de dontDestroyOnLoad().(Está el código comentado sobre lo que hace el singleton con un poco más de detalle). Así, es la clase StorePersistentVariables la que hereda del Singleton para guardar las variables que queremos
	que mantengan permanencia entre escenas. Aquí ya están puestas las variables slothTeam1 y slothTeam2.
	Por último, en TeamSelection he accedido a las variables de StorePersistentVariables para tener las listas de sloths que llenar.

Clase Sloth:
  He pensado que es mejor idea que, en lugar de crear por separado la lista de imágenes de sloths y el equipo de sloths, sea desde la clase sloth desde donde poder acceder al path de
	la imagen del susodicho. Por tanto a la hora de crear un sloth, tendras que asignarle el path de la imagen correspondiente.

GameController:
  Por útlimo, he decidido que sería mejor tener un controlador (GameController) que se encargue del flujo de ejecución del juego, y que derive en el resto de controladores como TurnController o UIController (que son los encargados de controlar el flujo de turnos y de la interacción con el menú de pausa respectivamente) dejando al script setUp con la única funcionalidad de mostrar por pantalla los sloths.