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

## Capturas

## Referencias
* https://github.com/dotnet/aspnetcore/issues/26685
* https://www.pragimtech.com/blog/blazor/blazor-select-bind-database-data/
* https://www.w3schools.com/bootstrap/bootstrap_dropdowns.asp
* https://stackoverflow.com/questions/48647247/csvhelper-everything-get-written-in-one-column-in-excel
* https://stackoverflow.com/questions/39564585/csvhelper-changing-how-dates-and-times-are-output
* https://stackoverflow.com/questions/13819962/error-the-type-or-namespace-name-excelpackage-could-not-be-found
* https://stackoverflow.com/questions/65794381/system-text-json-version-conflict
* https://stackoverflow.com/questions/76260218/how-do-i-resolve-unknown-pdfexception-when-using-itext7-in-net-maui

