# CRUD Blazor

Sistema básico Web Blazor con operaciones CRUD y base de datos SQL Server.

## ¿De qué trata esta aplicación?

Este proyecto consiste en un sistema de inventario donde se pueden registrar artículos agrupados por categoría, además permite la exportación de los datos
a formatos CSV, Excel, y PDF. Los componentes usados se listan a cotinuación:

* Entity Framework 6.0
* CsvHelper 33.0.1
* SweetAlert2 5.6.0
* EPPlus 4.5.3.3
* iText 8.0.1 (ex iTextSharp)

## ¿Cómo probar esta aplicación? (IIS)

Para poder desplegar esta aplicación en un servidor local con Internet 
Information Services (IIS) se necesita tener instalado los siguientes 
componentes:

* SQL Server Express
* Internet Informatión Services 10 (IIS)
	* URL Rewrite (componente para Blazor)
* .Net Core 3.1 runtime
* .Net Core Hosting Bundle 3.1

## Entorno de desarrollo 

- Visual Studio 2019 v16.11.49
- Windows 10 v22H2 (compilación 19045.6466)
- Navegadores: 
	- Chrome v142.0.7444.176 (Build oficial) (64 bits)
	- Brave v1.83.120 (Build oficial) (64 bits)
	- Firefox v125.0.3 (64-bit)

## Consideraciones IMPORTANTES de ejecución

Al ejecutar la aplicación me encontre con algunas situaciones que detallo a 
continuación.

1. La aplicación se detiene al ejecutarse en Brave.
	La aplicación se nicia correctamente pero cuando ejecuto la acción de exportar
	datos de alguna de las tablas a algún formato (CSV, Excel, PDF) se detiene. 
    
    **NO SE ENCONTRO ALGUNA RAZÓN APARENTE NI SOLUCIÓN PARA ESTO**

2. La aplicación no se inicia en Chrome o se detiene luego de un tiempo.
	La aplicación no inicia, es decir, se abre el navegador pero no se redirecciona
	la ruta de la aplicación, algo como `https://localhost:44372/`, además luego
	de un tiempo aparece el mensaje de error: 
	```
	Se han producido uno o varios errores

	No se pudo iniciar el adaptador de depuración. Puede haber más 
	información disponible en la ventana de salida.

	Messages not found
	```

	**SOLUCIÓN ENCONTRADA PARA ESTE CASO**

<details>
<summary>Ver</summary>
    A continuación la serie de pasos que seguí para que la aplicación funcionara
    y no se detuviera en Chrome.

1. Cerrar Visual Studio IDE 2019 y todos los procesos relacionados (PerfWatson, SvChost, etc.), borrar
las carpetas `/bin` y `/obj` si es necesario de todos los proyectos de la solución.

2. Cerrar el navegador Chrome. Adicionalmente también cerrar o matar los procesos
    de Edge desde el administrador de tareas.

3. Desde algún navegador que no sea Chrome intentar iniciar la aplicación,
    una vez en ejecución en la barra de tareas en a `IISExpress` darle a la
    opción "Deneter Sitio". 

4. Abrir una terminal con permisos de administrador y ejecutar el siguiente comando

	```
	net stop winnat

	net start winnat
	```

5. Adicionalmente muestro mi archivo `launchSettings.json` (proyecto `Server`) tiene esta apariencia:

    ```json
    {
      "iisSettings": {
        "windowsAuthentication": false,
        "anonymousAuthentication": true,
        "iisExpress": {
          "applicationUrl": "http://localhost:48142",
          "sslPort": 44372
        }
      },
      "profiles": {
        "IIS Express": {
          "commandName": "IISExpress",
          "launchBrowser": true,
          "environmentVariables": {
            "ASPNETCORE_ENVIRONMENT": "Development"
          },
          //"inspectUri": "{wsProtocol}://{url.hostname}:{url.port}/_framework/debug/ws-proxy?browser={browserInspectUri}"
        },
        "crud-blazor.Server": {
          "commandName": "Project",
          "launchBrowser": true,
          "environmentVariables": {
            "ASPNETCORE_ENVIRONMENT": "Development"
          },
          "applicationUrl": "https://localhost:5001;http://localhost:5000",
          //"inspectUri": "{wsProtocol}://{url.hostname}:{url.port}/_framework/debug/ws-proxy?browser={browserInspectUri}"
        }
      }
    }
    ```

    Esta configuración lo que hace es deshabilitar la depuracion de JavaScript/WebAssembly 
    en Blazor, es decir. Con esto se pierde la capacidad de poner breakpoints en archivos 
    .razor, clases en proyecto *.Client

**NOTA:** Este problema parece ser algo relacionado a Chrome en particular sobre todo
para las versiones modernas. Queda descartado (al menos según mis pruebas) que tenga que
ver con el uso de guíon medio "-" en el nombre de los proyectos ya que hice la prueba 
cambiando "crud-blazor..*" a "CruBlazor..*" y no hubo diferencias. Tal vez tenga que ver
con la versión de Net Core 3.1 usada en la solución. TENER EN CUENTA SI SE USA COMO TEMPLATE
PARA OTROS PROYECTOS

### Referencias
- [The Visual Studio 2022 Error "Failed to launch debug adapter. Additional information may be available in the output window."](https://stackoverflow.com/questions/70374907/the-visual-studio-2022-error-failed-to-launch-debug-adapter-additional-informa/78177525#78177525)

</details>

## Capturas

## Referencias

- [unable to bind object to blazer \<InputSelect\>](https://github.com/dotnet/aspnetcore/issues/26685)
- [Binding select element with database data in Blazor](https://www.pragimtech.com/blog/blazor/blazor-select-bind-database-data/)
- [Bootstrap Dropdowns](https://www.w3schools.com/bootstrap/bootstrap_dropdowns.asp)
- [csvhelper everything get written in one column in Excel](https://stackoverflow.com/questions/48647247/csvhelper-everything-get-written-in-one-column-in-excel)
- [CsvHelper changing how dates and times are output](https://stackoverflow.com/questions/39564585/csvhelper-changing-how-dates-and-times-are-output)
- [Error: The type or namespace name 'ExcelPackage' could not be found](https://stackoverflow.com/questions/13819962/error-the-type-or-namespace-name-excelpackage-could-not-be-found)
- [System.Text.Json version conflict](https://stackoverflow.com/questions/65794381/system-text-json-version-conflict)
- [How do I resolve Unknown PdfException when using itext7 in. net maui](https://stackoverflow.com/questions/76260218/how-do-i-resolve-unknown-pdfexception-when-using-itext7-in-net-maui)
- [Open Iconic List](https://fortawesome.com/sets/open-iconic)
- [InputSelect does not support the type System.Int32](https://www.pragimtech.com/blog/blazor/inputselect-does-not-support-system.int32/)