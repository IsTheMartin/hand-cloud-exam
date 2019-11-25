Dentro del repositorio se encuentra la solución para abrirse con VisualStudio
Al cargar la solución notaremos que hay tres proyectos: WebAPI, la interfa gráfica UI y el test unitario.

Para ejecutar la WebAPI y el UI a la vez, entramos a las propiedades de la solución y en la pestaña proyecto de inicio, seleccionar el radio botón Proyectos de inicio múltiple, después indicaremos los proyectos a ejecutar cambiando su acción de ninguna a iniciar, aplicamos cambios y aceptamos.

Al ejecutar, notaremos que ambos proyectos inician en ventanas diferentes y podremos usarlas :D

En caso de que el proyecto UI falle o no se comunique con la WebAPI, solo basta con modificar la url del HttpClient, que se encuentra en la clase Helper, este valor debe coincidir con la url de la WebAPI. También modifica la url en el javascript site.js con la url de la WebAPI.
