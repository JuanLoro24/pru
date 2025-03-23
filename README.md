**Creación del proyecto**

Inicialmente se crea una “Aplicación web de ASP.NET core MVC”, una vez que se ha creado el proyecto web, en el explorador de soluciones se da click derecho y se crea una “biblioteca de clases”
- Click derecho sobre la solución del proyecto creado
-	Seleccionar crear nuevo proyecto
-	Buscar y seleccionar biblioteca de clases y crear.
-	Eliminar la clase creada por defecto puesto que no se va usar.
  
La creación de la biblioteca de clases tiene como finalidad la separación de responsabilidades en la aplicación web, esto quiere decir que actuara como el negocio (capa de servicios) que se encarga de interactuar y procesar los datos que son enviados a la interfaz gráfica. En otras palabras, es la encarga de la conexión entre la base de datos SQL con el proyecto web.

**Relacionamiento entre el proyecto web y la biblioteca de clases**

Dado que la biblioteca de clases se encarga de los datos es necesaria la comunicación entre el proyecto web y la misma, para ello se debe agregar como referencia en proyecto web, por medio de los siguientes pasos:
-	En el explorador de soluciones dar click derecho sobre el proyecto web y seleccionar agregar.
-	Buscar y seleccionar “referencia del proyecto”.
-	Ubicar la biblioteca de clase seleccionar y aceptar.

**Entityframework**

Una vez que se relacionaron la biblioteca de clases y el proyecto web, es necesario crear la conexión con la base de datos en SQL Server (ya debe estar creada la base de datos con sus respectivas tablas). El entityframework es el encargado de realizar la conexión implementado la arquitectura de un ORM, esto permite mapear la base de datos en objetos C# para trabajar dentro del proyecto web con clases y no consultas de SQL. De acuerdo a lo anterior, se deben instalar los diferentes paquetes de entityframework tanto en el proyecto web como en la biblioteca de clases.

-	Para instalar los paquetes de entityframework se ubica la parte superior de visual estudio se selecciona “herramientas” y “administrador de paquetes NuGet” para que se habrá la consola de paquetes NuGet.
-	Una vez que se tiene la consola de administrador de paquetes, se ubica en la parte superior de la consola “proyecto predeterminado” se selecciona el proyecto web y se ejecutan las siguientes sentencias una por una.

Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package Microsoft.EntityFrameworkCore.Tools

-	Se realiza lo mismo para el otro proyecto, esto quiere decir que en “proyecto predeterminado” se selecciona la biblioteca de clases y se instalan los paquetes.
-	Para la conexión con la base de datos, en la consola de administrador de paquetes se debe usar la siguiente sentencia para la conexión con la base de datos (nota: en la consola se debe verificar que en “proyecto predeterminado” se tenga la biblioteca de clases, puesto que la conexión se debe hacer en ese proyecto).

Scaffold-DbContext "Server= **Nombre del servidor SQL server**; Database= **Nombre base de datos con la que se debe realizar la conexión**; Trusted_Connection=True; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir **Data (directorio donde se alojaran las clases de la conexión)**
