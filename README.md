# 🌟 Formularios de solicitudes para recursos humanos 🌟

Este es un proyecto de Windows Forms en C# publicado como ejecutable.

## 🚀 Cómo Ejecutar

1. 📥 **Descarga los archivos del repositorio**.
2. 🗂️ **Navega a la carpeta donde descargaste los archivos**.
3. 🎯 **Ejecuta `Formulario_MinisterioAgri.exe` o `setup.exe`**.

## 🛠️ Configuración de la Base de Datos

### 🗄️ Usar SQL Server

1. 🛠️ **Instalar SQL Server** (si no está instalado).
2. 💻 **Abrir SQL Server Management Studio**.
3. 📂 **Crear una nueva base de datos**.
4. 📜 **Ejecutar el script `Base de Datos para Recursos Humanos`** para crear las tablas y datos iniciales.
   - 📋 **Nota**: Si deseas, puedes agregar un usuario nuevo ejecutando el script correspondiente dentro de `Base de Datos para Recursos Humanos`.
5. ✏️ **Modificar el archivo `App.config` o `Web.config`** de la aplicación para apuntar a la nueva base de datos en caso de que le cambies el nombre.

## 📋 Requisitos

- ⚙️ .NET Framework 6 o superior.
- 🗄️ SQL Server.

## 📝 Descripción

Este proyecto permite registrar la informacion de la persona que esta solicitando un nombramiento, cambio de designacion o reajuste en el departamento de recursos humanos y exportar la data de la base de datos en un archivo de excel con sus respectivos archivos adjuntos, solo permite al usuario editar un solo campo que es la de "Estado" en la solicitud de cambio de designacion, tiene manejo de excepcion, no puedes poner letra donde van los numeros y no te permitira guardar si no completas los campos que son obligatorios(*), si pones una cedula que ya esta registrada no te permitira guardar en las solicitudes de nombramiento y reajuste, en cambio de designacion como puede ser varias solicitudes solo te permitira registrar si ya una solicitud tiene en el campo "Estado" aprobrada o rechazada, este mismo campo lo puede editar si le da a buscar y busca por cedula o por nombre el solicitante ingresado, posteriormente le habilitara la edicion del campo respectivamente, a excepcion de los demas como ya se menciono antes.

## 📧 Contacto

Para cualquier pregunta, puedes contactarme a mi correo julianpaniaguamelo@gmail.com.
