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

-	Una vez ejecutada la sentencia, entityframework genera las clases para la conexión dentro de la biblioteca de clases y trabajar sobre la base de datos, esto quiere decir que dentro de Data se crea un DbContex (que representa la conexión) y una clase por cada tabla que se tenga en la base de datos.
-	Es recomendable otro directorio en la biblioteca de clases llamado “Models” y pasar las clases que se crearon en Data, esto con el fin de tener una mejor organización, puesto que Data tendrá el elemento de conexión y Models las clases de cada tabla.
-	Se debe completar la conexión con la base de datos, para ellos se deben hacer unos cambios en el program.cs y en el appsettings.jason.
-	El encargado de la configuración de la aplicación web es appsettings.jason, por lo que es necesario agregar la cadena de conexión, como se presenta a continuación:

{
    "ConnectionStrings": {
        "DefaultConnection": "Server= **Nombre del servidor SQL server**; Database= **Nombre base de datos con la que se debe realizar la conexión**;Trusted_Connection=True;TrustServerCertificate=True;"
    },
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    }
}

-	El elemento program.cs es el archivo que se ejecuta primero en la aplicación para su funcionamiento. Por lo tanto, es necesario agregar la cadena de conexión, puesto que si no es así entityframework no se podrá conectar a la base de datos, para ello se deben agregar las siguientes librerías:

using negocio.Data; (relación con la biblioteca de clases y DbContext)
using Microsoft.EntityFrameworkCore;

-	Junto con la siguiente sentencia luego de crear la variable builder.
  
// Configurar Entity Framework Core con la conexión de appsettings.json
builder.Services.AddDbContext< nombre del DbContext en Data>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

**Arquitectura MVC (model – view – controller)**
 
Para este caso se implementa el patrón de arquitectura MVC para tener un código mas organizado separado en 3 componentes para su funcionalidad. Donde el modelo contendrá las clases de la base de datos y tendrá interacción con el negocio de la biblioteca de clases, la vista se encarga de albergar todos los archivos html para la interfaz gráfica y finalmente el controlador atiende las solicitudes del cliente pidiendo y enviando datos al modelo teniendo relación la vista. Para este caso se implementó lo siguiente:

-	Dado que el proyecto cuenta con dos tablas, se crearon dos modelos, uno para autores y otro para libros.
-	EL proyecto solicito tres representaciones graficas diferentes. Por lo tanto, la vista cuenta con un archivo html por cada una, junto con el layout que es el archivo html base. Teniendo en cuenta que el que para el diseño de la interfaz es necesario el uso del css y el js estos se encuentran en la carpeta wwwroot para los archivos estáticos.
-	Con respecto a los controladores se implementaros 3, uno por cada interfaz grafica con el fin de tener una mejor organización con respecto a las peticiones del cliente en cada una de las vistas. En este caso puntual, la aplicación funciona con peticiones http que envia y recibe información en formato JSON, dado que solo se solicitó mostrar y agregar datos se usan las peticiones GET y POST.

**Interfaces graficas del gestor de biblioteca**

Pagina principal

![image](https://github.com/user-attachments/assets/8e6ec581-3bb2-4859-a698-3972b86d3e05)

Pagina de registro de autor

![image](https://github.com/user-attachments/assets/75955b6a-48b3-4c76-a42c-07c5a21f515d)

Pagina de registro de libro

![image](https://github.com/user-attachments/assets/7a058be0-c99b-4229-9454-efd62b98a508)



