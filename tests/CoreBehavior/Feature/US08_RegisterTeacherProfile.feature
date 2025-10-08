Feature: US08 - Registro de información personal del docente
  Como administrador
  Quiero registrar la información personal del docente
  Para tener una base de datos actualizada

  Scenario: Registro de información exitoso
    Given que el administrador ha ingresado datos completos del docente
      | Username   | Password | Nombre | Apellido | Email               | Telefono    | AreaAcademica |
      | juan.p     | 123456   | Juan   | Perez    | juan.perez@upc.edu.pe | 987654321 | Matemáticas    |
    When el administrador envía el registro
    Then el sistema almacena los datos ingresados en la base de datos

  Scenario: Información incompleta
    Given que el administrador ha ingresado información incompleta del docente
      | Username   | Password | Nombre | Apellido | Email | Telefono | AreaAcademica |
      | juan.p     |          | Juan   |         |       |          |                |
    When el administrador envía el registro de información
    Then el sistema muestra un mensaje de error
